using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    private static UIManager instance;

    public static UIManager GetInstance() { return instance; }


    private GameObject pauseObject = null;
    public void setPauseObject(GameObject po) { pauseObject = po; }

    private FirstPersonLook firstPersonLook = null;
    public void setFirstPersonLook(FirstPersonLook fpl) { firstPersonLook = fpl; }

    private GameObject optionsObject = null;
    public void setOptionsObject(GameObject o) { optionsObject = o; }
    private GameObject mainTitleObject = null;
    public void setMainTitleObject(GameObject o) { mainTitleObject = o;}

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (SceneManager.GetActiveScene().name == "MainScene")
            {
                pause();
            }
        }
    }

    public void startGame()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void openOptions()
    {
        SceneManager.LoadScene("OptionsScene");
    }
    public void quitGame()
    {
        Application.Quit();
    }

    public void pause()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0.0f;
        pauseObject.SetActive(true);
        firstPersonLook.enabled = false;
    }
    public void resume()
    {
        firstPersonLook.enabled = true;
        pauseObject.SetActive(false);
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void backToMainTitle()
    {
        SceneManager.LoadScene("MainTitle");
    }

    public void exitOptions()
    {
        optionsObject.SetActive(false);
        if (pauseObject != null)
        {
            pauseObject.SetActive(true);
        }
        else if (mainTitleObject != null)
        {
            mainTitleObject.SetActive(true);
        }
    }
    public void enterOptions()
    {
        if (pauseObject != null)
        {
            pauseObject.SetActive(false);
        }
        else if (mainTitleObject != null)
        {
            mainTitleObject.SetActive(false);
        }
        optionsObject.SetActive(true);
    }
}
