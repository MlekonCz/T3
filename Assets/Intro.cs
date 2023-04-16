using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Intro : MonoBehaviour
{
    [SerializeField] private Transform obj1;
    [SerializeField] private Transform obj2;
    [SerializeField] private Transform obj3;


    [SerializeField] private float targetScale;

    private float time = 3f;


    private void Start()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.AppendCallback((() =>
        {
            obj1.gameObject.SetActive(true);
            obj1.DOScale(new Vector3(targetScale, targetScale, targetScale), time)
                .OnComplete((() => { obj1.gameObject.SetActive(false); })).OnComplete(() =>
                {
                    obj1.gameObject.SetActive(false);
                    obj2.gameObject.SetActive(true);

                    obj2.DOScale(new Vector3(targetScale, targetScale, targetScale), time)
                        .OnComplete((() => { obj2.gameObject.SetActive(false); })).OnComplete((() =>
                        {
                            obj2.gameObject.SetActive(false);

                            obj3.gameObject.SetActive(true);

                            obj3.DOScale(new Vector3(targetScale, targetScale, targetScale), time)
                                .OnComplete((() => { obj3.gameObject.SetActive(false); }));
                        }));
                    ;
                    ;
                });
        }));
        //   .AppendCallback((() =>
        // {
        //   obj2.gameObject.SetActive(true);
        //
        //   obj2.DOScale(new Vector3(targetScale, targetScale, targetScale), time)
        //     .OnComplete((() =>
        //   {
        //     obj2.gameObject.SetActive(false);
        //   }));
        //
        // })).SetDelay(time)
        //   .AppendCallback((() =>
        // {
        //   obj3.gameObject.SetActive(true);
        //
        //   obj3.DOScale(new Vector3(targetScale, targetScale, targetScale), time)
        //     .OnComplete((() =>
        //     {
        //       obj3.gameObject.SetActive(false);
        //     }));
    }
}

