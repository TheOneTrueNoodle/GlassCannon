using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

[Serializable]
public struct GameStatus
{
    public int Health;
    public int Damage;
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
            gameStatus.Health = 2;
            gameStatus.Damage = 5;
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
        gameStatus.Health = PlayerData.Health;
        gameStatus.Damage = PlayerData.Damage;
        gameStatus.Position = PlayerData.Position;
    }

    private void OnApplicationQuit()
    {
        SaveGameStatus();
    }

}
