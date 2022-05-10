using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private Player_Controls pInput;
    public GameObject PauseMenuUI;

    private void Awake()
    {
        pInput = new Player_Controls();
    }

    private void OnEnable()
    {
        pInput.UI.Enable();
    }

    private void OnDisable()
    {
        pInput.UI.Disable();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
        PauseMenuUI.SetActive(false);
    }

}
