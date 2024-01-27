using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle2Controller : MonoBehaviour
{
    private int trashNum = 0;
    [SerializeField] private int trashTarget=5;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Basura")
        {
            trashNum++;
            other.tag = "Untagged";
            if(trashNum==trashTarget) {
                GameManager.GetInstance().CompletePuzzle(2);
            }
        }
    }
}
