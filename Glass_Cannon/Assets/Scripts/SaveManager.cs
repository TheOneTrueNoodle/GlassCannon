using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

[Serializable]
public struct GameStatus
{
    public int MaxHealth;
    public int CurrentHP;
    public int MinDamage;
    public int MaxDamage;
    public Vector3 Position; 
}

public class SaveManager : MonoBehaviour
{
    GameStatus gameStatus;
    string filePath;
    const string FILE_NAME = "SaveStatus.json";
    private Player PlayerData;

    public GameObject DeathUI;
    public GameObject PauseUI;

    public void LoadGameStatus ()
    {
        if (File.Exists (filePath + "/" + FILE_NAME))
        {
            string loadedJson = File.ReadAllText(filePath + "/" + FILE_NAME);
            gameStatus = JsonUtility.FromJson<GameStatus>(loadedJson);
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = gameStatus.Position;
            player.GetComponent<Player>().MaxHealth = gameStatus.MaxHealth;
            player.GetComponent<Player>().CurrentHP = gameStatus.CurrentHP;
            player.GetComponent<Player>().MinDamage = gameStatus.MinDamage;
            player.GetComponent<Player>().MaxDamage = gameStatus.MaxDamage;

            if (DeathUI.activeInHierarchy)
            {
                player.GetComponent<Player>().Died = false;
                Time.timeScale = 1;
                DeathUI.SetActive(false);
            }
            if (PauseUI.activeInHierarchy)
            {
                Time.timeScale = 1;
                PauseUI.SetActive(false);
            }
        }
        else
        {
            gameStatus.MaxHealth = 20;
            gameStatus.CurrentHP = 20;
            gameStatus.MinDamage = 5;
            gameStatus.MaxDamage = 7;
            gameStatus.Position = new Vector3(4.75f, -2, 8.75f);

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<Player>().MaxHealth = gameStatus.MaxHealth;
            player.GetComponent<Player>().CurrentHP = gameStatus.MaxHealth;
            player.GetComponent<Player>().MinDamage = gameStatus.MinDamage;
            player.GetComponent<Player>().MaxDamage = gameStatus.MaxDamage;
        }
    }

    public void ResetGameStatus ()
    {
        Debug.Log("Reset");

        gameStatus.MaxHealth = 20;
        gameStatus.CurrentHP = 20;
        gameStatus.MinDamage = 5;
        gameStatus.MaxDamage = 7;
        gameStatus.Position = new Vector3(4.75f, -2, 8.75f);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Player>().MaxHealth = gameStatus.MaxHealth;
        player.GetComponent<Player>().CurrentHP = gameStatus.MaxHealth;
        player.GetComponent<Player>().MinDamage = gameStatus.MinDamage;
        player.GetComponent<Player>().MaxDamage = gameStatus.MaxDamage;

        LoadGameStatus();
        Time.timeScale = 1;
        SaveGameStatus();
    }

    public void SaveGameStatus ()
    {
        string gameStatusJson = JsonUtility.ToJson(gameStatus);

        File.WriteAllText(filePath + "/" + FILE_NAME, gameStatusJson);
    }

    private void Start()
    {
        filePath = Application.persistentDataPath;
        PlayerData = FindObjectOfType<Player>();
        gameStatus = new GameStatus();
        LoadGameStatus();
    }

    private void Update()
    {
        gameStatus.MaxHealth = PlayerData.MaxHealth;
        gameStatus.CurrentHP = PlayerData.CurrentHP;
        gameStatus.MinDamage = PlayerData.MinDamage;
        gameStatus.MaxDamage = PlayerData.MaxDamage;
        gameStatus.Position = PlayerData.Position;

        Debug.Log(filePath);
    }

    private void OnApplicationQuit()
    {
        SaveGameStatus();
    }

}
