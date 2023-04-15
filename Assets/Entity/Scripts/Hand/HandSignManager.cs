using System;
using UnityEngine;

namespace Entity.Scripts.Hand
{
    public class HandSignManager : MonoBehaviour
    {
        [SerializeField] private GameObject _CollectSign;
        [SerializeField] private GameObject _FeedTheRoomSign;
        [SerializeField] private GameObject _FeedSign;
        [SerializeField] private GameObject _PickUpSign;
        [SerializeField] private GameObject _ReplaceTheItemSign;
        [SerializeField] private GameObject _ReplaceSign;


        public void SetSign(Signs sign, bool isActive)
        {
            switch (sign)
            {
                case Signs.CollectSign:
                    _CollectSign.SetActive(isActive);
                    break;
                case Signs.FeedTheRoomSign:
                    _FeedTheRoomSign.SetActive(isActive);
                    break;
                case Signs.FeedSign:
                    _FeedSign.SetActive(isActive);
                    break;
                case Signs.PickUpSign:
                    _PickUpSign.SetActive(isActive);
                    break;
                case Signs.ReplaceTheItemSign:
                    _ReplaceTheItemSign.SetActive(isActive);
                    break;
                case Signs.ReplaceSign:
                    _ReplaceSign.SetActive(isActive);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sign), sign, null);
            }
        }
    }

    public enum Signs
    {
        CollectSign,
        FeedTheRoomSign,
        FeedSign,
        PickUpSign,
        ReplaceTheItemSign,
        ReplaceSign
    }
}