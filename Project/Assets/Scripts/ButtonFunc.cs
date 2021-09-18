using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunc : MonoBehaviour
{
    public void LoadLevel(int level)
    {
        SceneManager.LoadScene("level " + level);
    }

    public void LoadNextLevel()
    {
        string scene = "level " + (GlobalControl.Instance.prevLevelIndex + 1);
        if (GlobalControl.Instance.scenesInBuild.Contains(scene))
        {
            SceneManager.LoadScene(scene);
        }
        else
        {
            LoadMenu();
        }
    }

    public void LoadLastLevel()
    {
        SceneManager.LoadScene("level " + GlobalControl.Instance.prevLevelIndex);
    }

    public void LoadSceneFromString(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void LoadLevelSelect()
    {
        SceneManager.LoadScene("Level Select");
    }

    public void LoadVolume()
    {
        SceneManager.LoadScene("Volume");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
