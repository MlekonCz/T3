using System;
using TMPro;
using UnityEngine;

namespace Entity.Scripts.UI
{
    public class UpgradeCostUpdater : MonoBehaviour
    {
        [SerializeField] private TMP_Text _Text;


        private void OnEnable()
        {
            Game.Instance.ScoreManager.OnCurrencyChanged.AddListener(OnCurrencyChanged);
            _Text.text = $"{Game.Instance.ScoreManager.GetPlayerCurrentScore()}" + "/" + $"{Game.Instance.PlayerManager.CurrentUpgrades()[0].Cost}";
        }

        private void OnDisable()
        {
            Game.Instance.ScoreManager.OnCurrencyChanged.RemoveListener(OnCurrencyChanged);
        }

        private void OnCurrencyChanged()
        {
            _Text.text = $"{Game.Instance.ScoreManager.GetPlayerCurrentScore()}" + "/" + $"{Game.Instance.PlayerManager.CurrentUpgrades()[0].Cost}";
        }
    }
}