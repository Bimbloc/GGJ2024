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
    private List<int> correctCode = new List<int>() { 1, 0, 1, 2 };
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
                GameManager.GetInstance().CompletePuzzle(3);
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

                keyObjects[currentSelected].transform.DOLocalRotate(new Vector3(keyObjects[currentSelected].transform.rotation.x+36, keyObjects[currentSelected].transform.rotation.y, keyObjects[currentSelected].transform.rotation.z), 0.2f);
            }

            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                digits[currentSelected]--;
                digits[currentSelected] %= 10;

                keyObjects[currentSelected].transform.DOLocalRotate(new Vector3(keyObjects[currentSelected].transform.rotation.x - 36, keyObjects[currentSelected].transform.rotation.y, keyObjects[currentSelected].transform.rotation.z), 0.2f);
            }
        }
        else
        {
            GameManager.GetInstance().CompletePuzzle(3);
            enabled=false;
        }
    }   
}
