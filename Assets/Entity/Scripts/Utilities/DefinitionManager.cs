using System.Collections.Generic;
using System.Linq;
using Entity.Scripts.Ai;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Entity.Scripts.Utilities
{
    public enum ManagerState
    {
        // 1. Add new field that will be used as a Tab
        Waypoints,
        Items
    }

    public class DefinitionManager : OdinMenuEditorWindow
    {
        [OnValueChanged("StateChange")]
        [LabelText("Manager View")]
        [LabelWidth(100f)]
        [EnumToggleButtons]
        [ShowInInspector]
        private ManagerState _managerState;

        private bool _treeRebuild = false;
        private int _enumIndex = 0;

        // 2. Declare definition that you want to display in Tab
        // If you will want more folders in one Tab like in Weapons have Melee and Ranged 
        // You have to declare it here for each new folder that will be in Tab to be able to select path for it
        private readonly DrawSelected<WaypointsDefinition> _drawWaypointsDefinitions = new DrawSelected<WaypointsDefinition>();
        private readonly DrawSelected<ItemTierDefinition> _drawItemTierDefinitions = new DrawSelected<ItemTierDefinition>();

        
        // 3. Declare path for each folder you will want to be displayed in which are SO 
        
        private const string WaypointDefinitionsPath = "Assets/Entity/Definitions/Waypoints";
        private const string ItemTierDefinitionsPath = "Assets/Entity/Definitions/ItemTiers";
        
        

        [MenuItem("Tools/Definition Manager &#D")]
        public static void OpenWindow()
        {
            GetWindow<DefinitionManager>().Show();
        }

        private void StateChange()
        {
            _treeRebuild = true;
        }

        // 4. Setting path to definition
        protected override void Initialize()
        {
            _drawWaypointsDefinitions.SetPath(WaypointDefinitionsPath);
            _drawItemTierDefinitions.SetPath(ItemTierDefinitionsPath);
        }

        protected override void OnGUI()
        {
            if (_treeRebuild && Event.current.type == EventType.Layout)
            {
                ForceMenuTreeRebuild();
                _treeRebuild = false;
            }

            SirenixEditorGUI.Title("Definition Manager", "Tool for managing scriptable objects", TextAlignment.Left,
                true);
            EditorGUILayout.Space();
            switch (_managerState)
            {
                // 5. Here you add enum so it will create new Tab in Definition Manager
                case ManagerState.Waypoints:
                case ManagerState.Items: 
                    DrawEditor(_enumIndex);
                    break;
                default:
                    break;
            }
            EditorGUILayout.Space();
            base.OnGUI();
        }

        protected override void DrawEditors()
        {
            switch (_managerState)
            {
                // 6. For each Tab you will have you need to add case so system will be able to update when selected new Tab
                case ManagerState.Waypoints:
                    _drawWaypointsDefinitions.SetSelected(MenuTree.Selection.SelectedValue);
                    break;
                case ManagerState.Items:
                    _drawItemTierDefinitions.SetSelected(MenuTree.Selection.SelectedValue);
                    break;
                default:
                    break;
            }

            DrawEditor((int) _managerState);
        }

        protected override IEnumerable<object> GetTargets()
        {
            // 7. Adding definitions to the enums in the list
            // THEY HAS TO BE IN SAME ORDERS AS ENUMS THAT ARE ON TOP!
            // in case you are not using some enum Then you just write: targets.Add(null);
            List<object> targets = new List<object>();
            targets.Add(_drawWaypointsDefinitions);
            targets.Add(_drawItemTierDefinitions);
            targets.Add(base.GetTarget());

            _enumIndex = targets.Count - 1;
            return targets;
        }

        protected override void DrawMenu()
        {
            switch (_managerState)
            {
                // 8. Add here State that you want to add
                case ManagerState.Waypoints:
                case ManagerState.Items:
                    base.DrawMenu();
                    break;
                default:
                    break;
            }
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            OdinMenuTree tree = new OdinMenuTree();
            switch (_managerState)
            {
                // 9. Here you add folders to the Tabs that you want to be displayed
                // If you want to add multiple folders to one Tab you just need to 
                // write tree.AddAllAssetsAtPath(...) 
                //      typeof(...)
                // for each folder under case which you want to add it to
                case ManagerState.Waypoints:
                    tree.AddAllAssetsAtPath("Waypoints", WaypointDefinitionsPath, 
                        typeof(WaypointsDefinition));
                    break;
                case ManagerState.Items:
                    tree.AddAllAssetsAtPath("Item Tiers", ItemTierDefinitionsPath, 
                        typeof(ItemTierDefinition));
                    break;
                default:
                    break;
            }
            return tree;
        }
    }

    public class DrawSelected<T> where T : ScriptableObject
    {
        [InlineEditor(InlineEditorObjectFieldModes.CompletelyHidden)]
        public T selected;

        [LabelWidth(100)] [PropertyOrder(-1)] [HorizontalGroup("Horizontal")]
        public string nameForNew;

        private string path;
        
        private bool deleteCheck = false;
        
        [HorizontalGroup("Horizontal")]
        [GUIColor(0.7f, 1f, 0.5f)]
        [Button(ButtonSizes.Medium)]
        public void CreateNew()
        {
            if (string.IsNullOrWhiteSpace(nameForNew))
            {
                return;
            }

            List<T> instancesInFolder = new List<T>();
            
            instancesInFolder = AssetDatabase.FindAssets("", new string[] {path})
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<T>).ToList();

            foreach (var scriptableObject in instancesInFolder)
            {
                if (scriptableObject.name == nameForNew)
                {
                    UnityEngine.Debug.Log("Name already used!");
                    return;
                }
            }
            
            T newItem = ScriptableObject.CreateInstance<T>();

            if (string.IsNullOrWhiteSpace(path))
            {
                path = "Assets/";
            }

            AssetDatabase.CreateAsset(newItem, path + "\\" + nameForNew + ".asset");
            AssetDatabase.SaveAssets();
            nameForNew = "";
        }
        [HorizontalGroup("Horizontal")]
        [GUIColor(0.4f, 0.8f, 1f)]
        [Button(ButtonSizes.Medium)]
        public void CopySelected()
        {
            if (string.IsNullOrWhiteSpace(nameForNew))
            {
                return;
            }
            
            List<T> instancesInFolder = new List<T>();
            
            instancesInFolder = AssetDatabase.FindAssets("", new string[] {path})
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<T>).ToList();

            foreach (var scriptableObject in instancesInFolder)
            {
                if (scriptableObject.name == nameForNew)
                {
                    UnityEngine.Debug.Log("Name already used!");
                    return;
                }
            }
            
            T clonedScriptableObject = ScriptableObject.Instantiate(original:selected) as T;
            
            if (string.IsNullOrWhiteSpace(path))
            {
                path = "Assets/";
            }

            AssetDatabase.CreateAsset(clonedScriptableObject, path + "\\" + nameForNew + ".asset");
            AssetDatabase.SaveAssets();
            nameForNew = "";
        }

        [HideIf("deleteCheck",true)]
        [HorizontalGroup("Horizontal")]
        [GUIColor(1f, 0f, 0f)]
        [Button]
        public void DeleteSelected()
        {
            deleteCheck = true;
        }
        [ShowIf("deleteCheck",true)]
        [HorizontalGroup("Horizontal")]
        [GUIColor(0.7f, 1f, 0.5f)]
        [Button(ButtonSizes.Small)]
        public void No()
        {
            deleteCheck = false;
        }
        
        [ShowIf("deleteCheck",true)]
        [HorizontalGroup("Horizontal")]
        [GUIColor(1f, 0f, 0f)]
        [Button(ButtonSizes.Small)]
        public void Yes()
        {
            deleteCheck = false;
            if (selected != null)
            {
                string _path = AssetDatabase.GetAssetPath(selected);
                AssetDatabase.DeleteAsset(_path);
                AssetDatabase.SaveAssets();
            }
        }
       

        public void SetSelected(object item)
        {
            if (selected != item)
            {
                deleteCheck = false;
            }
            var attempt = item as T;
            if (attempt != null)
            {
                this.selected = attempt;
            }
        }

        public void SetPath(string path)
        {
            this.path = path;
        }
    }
}