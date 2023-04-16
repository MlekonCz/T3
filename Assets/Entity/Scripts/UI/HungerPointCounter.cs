using System;
using TMPro;
using UnityEngine;

namespace Entity.Scripts.UI
{
    public class HungerPointCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _Text;


        private void Start()
        {
            _Text.text = "Hunger points " + GetHungryPoints();
            Game.Instance.ScoreManager.OnCurrencyChanged.AddListener(OnCurrencyChanged);
        }

        private void OnDestroy()
        {
            Game.Instance.ScoreManager.OnCurrencyChanged.RemoveListener(OnCurrencyChanged);
        }

        private void OnCurrencyChanged()
        {
            _Text.text = "Hunger points " + GetHungryPoints();
        }

        private static string GetHungryPoints()
        {
            var value = Game.Instance.ScoreManager.GetPlayerCurrentScore();
            return $"{(value < 10 ? "0" + value : value)}";
        }
    }
}
