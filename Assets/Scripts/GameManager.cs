using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private static GameManager instance;

    public static GameManager GetInstance() { return instance; }

    private LightManager lightManager;
    public void setLightManager(LightManager lm) { lightManager = lm; }

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
            case 3:
                break;
        }
    }
}
