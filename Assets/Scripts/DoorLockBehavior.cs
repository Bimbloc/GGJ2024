using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLockBehavior : MonoBehaviour
{
    private GameObject door ;

    private void Start()
    {
        door = transform.parent.gameObject;
        DOTween.Init();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Key")
        {
            GameManager.GetInstance().ending=true;
            // Vector3 openedRotation = door.transform.rotation.eulerAngles + new Vector3(0, 90, 0);
            //door.transform.DORotate(openedRotation, 1).OnComplete(()=> { enabled = false; });
            abrir();
        }
    }

    public void abrir()
    {
        Vector3 openedRotation = door.transform.rotation.eulerAngles + new Vector3(0, 90, 0);
        door.transform.DORotate(openedRotation, 1).OnComplete(() => { enabled = false; });
        GameManager.GetInstance().CompletePuzzle(3);
    }
}
