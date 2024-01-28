using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private GameObject credits;
    private static GameManager instance;

    public static GameManager GetInstance() { return instance; }

    private LightManager lightManager;
    private RadioManager radioManager;
    public void setLightManager(LightManager lm) { lightManager = lm; }
    public void setRadioManager(RadioManager rm) { radioManager = rm; }

    public bool ending=false;

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

    private void Update()
    {
        if(ending&&radioManager.radioPlaying()&&radioManager.finalAudioPlaying())
        {
            Instantiate(credits);
        }
    }
    public void CompletePuzzle(int puzzleNumber)
    {
        lightManager.CompletePuzzle();
        switch (puzzleNumber)
        {
            case 1:
                radioManager.IntereferenceSkip();
                radioManager.SetPuzzle2();
                radioManager.BreakRadio();
                break;
            case 2:
                radioManager.IntereferenceSkip();
                radioManager.SetPuzzle3();
                break;
            case 3:
                radioManager.IntereferenceSkip();
                player.GetComponent<InteractionController>().SpawnKey();
                radioManager.EndGameConversation();
                player.GetComponent<FirstPersonMovement>().enabled=false;
                break;
        }
    }
}
