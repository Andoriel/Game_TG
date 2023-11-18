using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMainManager : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private GameObject panelMainMenu;
    [SerializeField] private GameObject panelCredits;
    
    public void Play()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OpenCredits()
    {
        panelCredits.SetActive(true);
        panelMainMenu.SetActive(false);
    }

    public void CloseCredits()
    {
        panelCredits.SetActive(false);
        panelMainMenu.SetActive(true);
    }

    public void ExitGame()
    {
        Debug.Log("Saiu do Jogo");
        //Application.Quit();
    }
}
