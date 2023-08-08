using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text _highScoreText;

    private void Start(){
        int highScore = PlayerPrefs.GetInt(ScoreSystem.HighScoreKey,0);

        _highScoreText.text = $"High Score: {highScore}";
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }
}
