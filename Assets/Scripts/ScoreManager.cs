using System;
using System.IO;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    
    private int _score;
    private string _name;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        LoadBestScore();
    }
    
    [System.Serializable]
    class ScoreSaveData
    {
        public string name;
        public int score;
    }

    public string GetBestScore()
    {
        return $"Best Score: {Instance._name}:{Instance._score}";
    }

    public void SaveScore(int newScore, string playerName)
    {
        ScoreSaveData data = new ScoreSaveData();
        string path  = Application.persistentDataPath + "/score.json";

        if (File.Exists(path))
        {
            //File exists and eventually score
            string loadedJson =  File.ReadAllText(path);
            ScoreSaveData loadedData = JsonUtility.FromJson<ScoreSaveData>(loadedJson);
            
            //Save only if score is new high score

            if (newScore > loadedData.score)
            {
                data.score = newScore;
                data.name = playerName;
            }
            else
            {
                data.score = loadedData.score;
                data.name = loadedData.name;
            }
        }
        else
        {
            data.score = newScore;
            data.name = playerName;
        }
        
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(path, json);
        
        _score = data.score;
        _name = data.name;

    }

    public void LoadBestScore()
    {
        string path =  Application.persistentDataPath + "/score.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            ScoreSaveData data = JsonUtility.FromJson<ScoreSaveData>(json);
            
            _score = data.score;
            _name = data.name;
            
            Debug.Log($"Loaded: {_name} - {_score}");
        }
        else
        {
            _score = 0;
            _name = "Unknown";
        }
    }
}
