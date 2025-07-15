using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab; //tu�la prefab�
    public int LineCount = 6; //tu�la sat�r�
    public Rigidbody Ball; 

    public Text ScoreText;
    public Text BestScoreText;
    public GameObject GameOverText;
    
    private bool isGameStarted = false;
    private int totalScore;
    
    private bool isGameOver = false;

    
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
        BestScoreText.text = scoreManager.bestScorePlayerName + ": " + scoreManager.bestScore;
    }

    private void Update()
    {
        if (!isGameStarted)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isGameStarted = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        totalScore += point;
        ScoreText.text = $"Score : {totalScore}";

        FindObjectOfType<ScoreManager>().AddScore(point);
    }

    public void UpdateBestScoreText(string bestPlayerName, int bestScore)
    {
        BestScoreText.text = bestPlayerName + ": " + bestScore;
    }

    public void GameOver()
    {
        isGameOver = true;
        GameOverText.SetActive(true);
    }
}
