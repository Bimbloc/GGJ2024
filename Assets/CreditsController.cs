using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour
{
    [SerializeField] GameObject positionToMove;
    private void Start()
    {
        DOTween.Init();
        gameObject.transform.DOMove(positionToMove.transform.position, 20).OnComplete(() => { SceneManager.LoadScene("MainTitle"); });
    }
}
