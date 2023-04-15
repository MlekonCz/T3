using System;
using UnityEngine;

namespace Entity.Scripts.Core
{
    public class ScoreManager : MonoBehaviour
    {

[SerializeField]
        private int _PlayerScore;

        [NonSerialized]
        public int LifetimeScore;

        private void Awake()
        {
            LifetimeScore = _PlayerScore;
        }

        public int GetPlayerCurrentScore()
        {
            return _PlayerScore;
        }

        public bool HasEnoughScore(int value)
        {
            return _PlayerScore >= value;
        }


        public void AddScore(int value)
        {
            _PlayerScore += value;
            LifetimeScore += value;
            Debug.Log(_PlayerScore);
        }

        public void SubtractScore(int value)
        {
            _PlayerScore -= value;
            Debug.Log(_PlayerScore);
        }
    }
}