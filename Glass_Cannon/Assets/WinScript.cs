using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScript : MonoBehaviour
{
    public SaveManager SaveMan;
    public GameObject WinUI;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0;
            WinUI.SetActive(true);
        }
    }


    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SaveMan.ResetGameStatus();
        SceneManager.LoadScene(1);
    }
}
