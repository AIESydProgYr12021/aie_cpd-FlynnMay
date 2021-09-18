using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalControl : MonoBehaviour
{
    public static GlobalControl Instance;

    public int prevLevelIndex = 0;
    public List<string> scenesInBuild = new List<string>();

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            OnInstanceStart();
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void OnInstanceStart()
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            scenesInBuild.Add(System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i)));
        }
    }
}
