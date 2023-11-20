using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMainManager : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private GameObject panelMainMenu;
    [SerializeField] private GameObject panelCredits;
    [SerializeField] private GameObject caixaDeTextoCreditos;
    private Transform transformCreditos;
    public float scrollSpeed = 15.0f;
    public float initialYPosition = -600.0f;
    private bool setado = false;

    private bool creditos=false;

    private void Start()
    {
        transformCreditos = caixaDeTextoCreditos.transform;
        transformCreditos.position = new Vector3(transformCreditos.position.x, initialYPosition, transformCreditos.position.z);
    }

    public void Play()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OpenCredits()
    {
        panelCredits.SetActive(true);
        creditos = true;
        panelMainMenu.SetActive(false);
    }

    public void CloseCredits()
    {
        panelCredits.SetActive(false);
        creditos = false;
        panelMainMenu.SetActive(true);
    }

    public void ExitGame()
    {
        Debug.Log("Saiu do Jogo");
        //Application.Quit();
    }

    private void Update()
    {

        if (creditos)
        {
            if (!setado)
            {
                transformCreditos.position = new Vector3(transformCreditos.position.x, transformCreditos.position.z);
                setado = true;
            }
            transformCreditos.position += Vector3.up * Time.deltaTime*15;
            Debug.Log(transformCreditos.position.y.ToString());
        }
        else
        {
            setado = false;
        }
    }

}
