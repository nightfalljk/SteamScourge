using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuFunctionality : MonoBehaviour
{
    public void StartGame(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void ToggleGameObject(GameObject gameObj)
    {
        gameObj.SetActive(!gameObj.activeSelf);
    }
    
    public void Quit()
    {
        Application.Quit();
    }
}
