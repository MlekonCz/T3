using UnityEngine;

namespace Entity.Scripts.Core
{
    public class ScoreManager : MonoBehaviour
    {


        private int _playerScore;

        public int LifetimeScore;



        public int GetPlayerCurrentScore()
        {
            return _playerScore;
        }

        public bool HasEnoughScore(int value)
        {
            return _playerScore >= value;
        }


        public void AddScore(int value)
        {
            _playerScore += value;
            LifetimeScore += value;
            Debug.Log(_playerScore);
        }

        public void SubtractScore(int value)
        {
            _playerScore -= value;
            Debug.Log(_playerScore);
        }
    }
}