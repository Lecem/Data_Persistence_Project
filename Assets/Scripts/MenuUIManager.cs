using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

[DefaultExecutionOrder(1000)]
public class MenuUIManager : MonoBehaviour
{
    public InputField playerNameInput; //kullanýcý ismi

    public Text bestScoreText;


    private void Start()
    {
        // Skor baþlangýç deðerlerini UI’ya yaz
        
        bestScoreText.text = $"{ScoreManager.Instance.bestScorePlayerName} - {ScoreManager.Instance.bestScore}";
    }
    public void StartButton()
    {
        string playerName = playerNameInput.text;

        if (!string.IsNullOrEmpty(playerName))
        {
            ScoreManager.Instance.playerName = playerName;
            SceneManager.LoadScene(1);
        }
    }

    public void ExitButton()
    {


#if UNITY_EDITOR //if else bloðu gibi 
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }


}
