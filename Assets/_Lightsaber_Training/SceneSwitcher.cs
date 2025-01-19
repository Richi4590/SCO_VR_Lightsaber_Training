using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        if (sceneName != "")
            SceneManager.LoadScene(sceneName);
        else
            Debug.LogWarning("No scene name present for the SceneSwitcher!");
    }
}
