using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Rendering;
using UnityEngine;
using DG.Tweening;

public class LockController : MonoBehaviour
{
    private List<int> digits = new List<int>() { 0, 0, 0, 0 };
    [SerializeField] private List<int> correctCode = new List<int>() { 1, 0, 1, 2 };
    private List<GameObject> keyObjects = new List<GameObject>();
    private int currentSelected = 0;
    public bool solved = false;

    private void Start()
    {
        DOTween.Init();
        for(int i=0; i< transform.GetChild(0).childCount; i++)
        {
            keyObjects.Insert(keyObjects.Count, transform.GetChild(0).GetChild(i).gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(digits.SequenceEqual(correctCode))
        {
        }
        if (!solved)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                currentSelected -= 1;
                currentSelected %= 4;
            }

            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                currentSelected += 1;
                currentSelected %= 4;
            }

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                digits[currentSelected]++;
                digits[currentSelected] %= 10;

                keyObjects[currentSelected].transform.Rotate(36,0,0,Space.Self);
            }

            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                digits[currentSelected]--;
                digits[currentSelected] %= 10;

                keyObjects[currentSelected].transform.Rotate(-36, 0, 0, Space.Self);
            }
        }
        else
        {
        }
    }   
}
