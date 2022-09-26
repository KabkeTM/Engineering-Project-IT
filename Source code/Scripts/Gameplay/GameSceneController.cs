using Assets.Scripts.Cam.Effects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public static class GameSceneVariables
{
    public static bool gameIsPaused = false;
}

public class GameSceneController : MonoBehaviour
{
    private GameObject varGameObject;    
    public GameObject pauseMenuUI = null;

    public void DitherSwitch(GameObject varGameObject)
    {
        if (GraphicVariables.isDitherOn == true)
        {
            varGameObject.GetComponent<Dither>().enabled = true;
        }
        else
        {
            varGameObject.GetComponent<Dither>().enabled = false;
        }
    }

    public void PosterizeSwitch(GameObject varGameObject)
    {
        if (GraphicVariables.isPosterizeOn == true)
        {
            varGameObject.GetComponent<Posterize>().enabled = true;
        }
        else
        {
            varGameObject.GetComponent<Posterize>().enabled = false;
        }
    }

    public void RColorsSwitch(GameObject varGameObject)
    {
        if(GraphicVariables.isRColorsOn == true)
        {
            
           
            switch (GraphicVariables.presetIndex)
            {
                case 0:
                    varGameObject.GetComponent<RetroPalette_Green>().enabled = true;
                    break;
                case 1:
                    varGameObject.GetComponent<RetroPalette_Red>().enabled = true;
                    break;
                case 2:
                    varGameObject.GetComponent<RetroPalette_Blue>().enabled = true;
                    break;
                default:
                    break;
            }
        }
        else
        {
            varGameObject.GetComponent<RetroPalette_Green>().enabled = false;
            varGameObject.GetComponent<RetroPalette_Red>().enabled = false;
        }
    }

    public void RSizeSwitch(GameObject varGameObject)
    {
        if (GraphicVariables.isRSizeOn == true)
        {
            varGameObject.GetComponent<RetroSize>().enabled = true;
        }
        else
        {
            varGameObject.GetComponent<RetroSize>().enabled = false;
        }
    }

    private void Start()
    {
        Resume();

        varGameObject = GameObject.Find("FPS Player/PlayerCamera");      

        DitherSwitch(varGameObject);

        PosterizeSwitch(varGameObject);

        RColorsSwitch(varGameObject);

        RSizeSwitch(varGameObject);

    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {            
            if (GameSceneVariables.gameIsPaused)
            {
                Resume();
            }
            else
            {
                
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameSceneVariables.gameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameSceneVariables.gameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void SceneSelectDialogYes(string sceneSelectLevel)
    {
        SceneManager.LoadScene(sceneSelectLevel);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
