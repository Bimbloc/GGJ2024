using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseComponent : MonoBehaviour
{
    UIManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = UIManager.GetInstance();
        manager.setPauseObject(gameObject);
        gameObject.SetActive(false);
    }

    public void resume()
    {
        manager.resume();
    }
    public void options()
    {
        manager.enterOptions();
    }
    public void backToMainTitle()
    {
        manager.backToMainTitle();
    }
}
