using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    GameObject[] panels;
    GameObject[] mmButtons;
    int maxLives = 3;

    void Start() 
    {
        panels = GameObject.FindGameObjectsWithTag("subPanel");
        mmButtons = GameObject.FindGameObjectsWithTag("mmButton");

        foreach (GameObject p in panels)
            p.SetActive(false);
    }

    public void ClosePanel(Button button)
    {
       button.gameObject.transform.parent.gameObject.SetActive(false);
       foreach (GameObject b in mmButtons)
            b.SetActive(true);
    }

    public void OpenPanel(Button button)
    {
         button.gameObject.transform.GetChild(1).gameObject.SetActive(true);
         foreach (GameObject b in mmButtons)
            if(b != button.gameObject)
                b.SetActive(false);
    }

    public void LoadGameScene()
    {
        PlayerPrefs.SetInt("lives", maxLives);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void Update() 
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            QuitGame();
        }
    }
}
