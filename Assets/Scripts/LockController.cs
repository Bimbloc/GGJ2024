using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Rendering;
using UnityEngine;

public class LockController : MonoBehaviour
{
    private List<int> digits = new List<int>() { 0, 0, 0, 0 };
    private List<int> correctCode = new List<int>() { 1, 0, 1, 2 };
    private int currentSelected = 0;

    // Update is called once per frame
    void Update()
    {
            if(digits.SequenceEqual(correctCode))
            {
                GameManager.GetInstance().CompletePuzzle(3);
            }

            if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                currentSelected -= 1;
                currentSelected %= 4;
            }

            if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                currentSelected += 1;
                currentSelected %= 4;
            }

            if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                digits[currentSelected]++;
            }

            if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                digits[currentSelected]--;
            }
    }   
}
