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

    public void LoadGameStatus ()
    {
        if (File.Exists (filePath + "/" + FILE_NAME))
        {
            string loadedJson = File.ReadAllText(filePath + "/" + FILE_NAME);
            gameStatus = JsonUtility.FromJson<GameStatus>(loadedJson);
            GameObject.FindGameObjectWithTag("Player").transform.position = gameStatus.Position;
        }
        else
        {
            gameStatus.MaxHealth = 20;
            gameStatus.MinDamage = 5;
            gameStatus.MinDamage = 7;
            gameStatus.Position = new Vector3(0, 0, 0);
        }
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
