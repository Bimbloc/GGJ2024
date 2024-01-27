using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private static GameManager instance;

    public static GameManager GetInstance() { return instance; }

    private LightManager lightManager;
    private RadioManager radioManager;
    public void setLightManager(LightManager lm) { lightManager = lm; }
    public void setRadioManager(RadioManager rm) { radioManager = rm; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void CompletePuzzle(int puzzleNumber)
    {
        lightManager.CompletePuzzle();
        switch (puzzleNumber)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
        }
    }
}
