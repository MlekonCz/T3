using System;
using System.Collections;
using System.Collections.Generic;
using Entity.Scripts;
using TMPro;
using UnityEngine;

public class Goal : MonoBehaviour
{
   [SerializeField] private TMP_Text test;


   [SerializeField] private GameObject victory;

   private void Start()
   {
      test.text = "Victory in " + $"{Game.Instance.ScoreManager.LifetimeScore + "/" + "80"}";
      Game.Instance.ScoreManager.OnCurrencyChanged.AddListener(OnCurrency);
   }

   private void OnCurrency()
   {
      test.text = "Victory in " + $"{Game.Instance.ScoreManager.LifetimeScore + "/" + "80"}";

      if (Game.Instance.ScoreManager.LifetimeScore > 80)
      {
         victory.SetActive(true);
      }
   }
}
