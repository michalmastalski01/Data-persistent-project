using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;
using System.IO;

[Serializable]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int highScore;
    public string bestPlayerName;
    public string playerName;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            highScore = 0;
            playerName = string.Empty;
        }
    }

    [Serializable]
    class SaveData
    {
        public string playerName;
        public int score;
    }

    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.playerName = bestPlayerName;
        data.score = highScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savedata.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savedata.json";

        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestPlayerName = data.playerName;
            highScore = data.score;

        }
    }

}
