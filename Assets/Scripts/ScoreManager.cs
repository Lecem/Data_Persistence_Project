using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;


    public Text scoreText;
    public Text bestScoreText;

    public int score = 0;
    public string playerName;
    public int bestScore = 0;
    public string bestScorePlayerName = "Yok";

    private string savePath;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        savePath = Application.persistentDataPath + "/savefile.json";
        LoadBestScore();
    }

    private void Start()
    {
        Debug.Log(Application.persistentDataPath);
        score = 0;
        UpdateScoreUI();
        UpdateBestScoreUI();
    }


    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Skor: " + score;
    }
    private void UpdateBestScoreUI()
    {
        Debug.Log(bestScore);
        if (bestScoreText != null)
            bestScoreText.text = $" {bestScorePlayerName} : {bestScore}";
    }

    [System.Serializable]
    class BestScoreData
    {
        public int bestScore;
        public string bestScorePlayerName;
    }

    public void SaveBestScore()
    {
        BestScoreData data = new BestScoreData
        {
            bestScore = bestScore,
            bestScorePlayerName = bestScorePlayerName
        };

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/saveFile.json", json); //Son olarak, bir dizeyi bir dosyaya yazmak için File.WriteAllText özel yöntemini kullandýnýz 
    }

    public void LoadBestScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {

            string json = File.ReadAllText(path); //Dosya mevcutsa, yöntem içeriðini File.ReadAllText ile okur
            BestScoreData data = JsonUtility.FromJson<BestScoreData>(json);

            bestScore = data.bestScore;
            bestScorePlayerName= data.bestScorePlayerName;
        }
    }

    public void AddScore(int point)
    {
        score += point;
        UpdateScoreUI ();

        if (score > bestScore)
        {
            bestScore = score;
            bestScorePlayerName = playerName;

           SaveBestScore();
           FindObjectOfType<MainManager>().UpdateBestScoreText(bestScorePlayerName, bestScore);
        }
    }



}
