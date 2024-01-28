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
    [SerializeField] InteractionController ic;

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
        if(solved || digits.SequenceEqual(correctCode))
        {


            GameObject tapa = gameObject.transform.GetChild(0).gameObject;
            Vector3 openedRotation = tapa.transform.rotation.eulerAngles + new Vector3(80, 0, 0);
            tapa.transform.DORotate(openedRotation, 1).OnComplete(() => {
                //GameManager.GetInstance().CompletePuzzle(3);
                ic.SpawnKey();
            });
            
                //GameManager.GetInstance().CompletePuzzle(3);

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

           // GameManager.GetInstance().CompletePuzzle(3);
            //enabled=false;

        }
    }   
}
