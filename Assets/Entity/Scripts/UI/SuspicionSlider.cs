using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Entity.Scripts.UI
{
    public class SuspicionSlider : MonoBehaviour
    {


        [SerializeField] private Image _SliderImage;

        private float _maxSus;
        private void Start()
        {
            var GameManager = Game.Instance.GameManager;
            GameManager.SignalOnSuspicionChanged.AddListener(OnSusChanged);
            _maxSus = GameManager.MaxSuspicion;
            _SliderImage.transform.localScale = new Vector3(1, GameManager.Suspicion / _maxSus, 1);

        }

        private void OnDestroy()
        {
            Game.Instance.GameManager.SignalOnSuspicionChanged.RemoveListener(OnSusChanged);
        }

        private void OnSusChanged(float obj)
        {
            _SliderImage.transform.localScale = new Vector3(1, obj / _maxSus, 1);

            if (obj >= _maxSus)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
