using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private float _scoreMultiplier;
    public const string HighScoreKey = "HighScore";

    private float _score;

    void Update()
    {
        _score += Time.deltaTime * _scoreMultiplier;

        _scoreText.text = Mathf.FloorToInt(_score).ToString();
    }

    private void OnDestroy(){
        int currentHighScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        if(_score > currentHighScore){
            PlayerPrefs.SetInt(HighScoreKey, Mathf.FloorToInt(_score));
        }
    } 

}
