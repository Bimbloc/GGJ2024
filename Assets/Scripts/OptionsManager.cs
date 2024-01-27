using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    UIManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = UIManager.GetInstance();
        manager.setOptionsObject(gameObject);
        gameObject.SetActive(false);

    }
    public void fullScreen()
    {
        if (Screen.fullScreenMode == FullScreenMode.Windowed)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }
    public void exit()
    {
        manager.exitOptions();
    }
}
