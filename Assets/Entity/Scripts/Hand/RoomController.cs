using strange.extensions.signal.impl;
using UnityEngine;

namespace Entity.Scripts.Hand
{
    public class RoomController : MonoBehaviour
    {
        [SerializeField] private HandController _HandController;

        public Signal<float> OnPickableConsumed;
        private void Update()
        {
        
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            Debug.Log("Hand entered the room");
            if (col.gameObject != _HandController.gameObject || _HandController.Pickable == null) return;
            _HandController.Pickable.OnBeingReplaced();
            _HandController.Pickable = null;
        }
    }
}
