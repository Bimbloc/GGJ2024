using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsController : MonoBehaviour
{
    [SerializeField] GameObject positionToMove;
    private void Start()
    {
        DOTween.Init();
        gameObject.transform.DOMove(positionToMove.transform.position, 20);
    }
}
