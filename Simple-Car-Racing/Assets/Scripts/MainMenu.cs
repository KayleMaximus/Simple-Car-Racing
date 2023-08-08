using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text _highScoreText;
    [SerializeField] private TMP_Text _energyText;
    [SerializeField] private Button _playButton;
    [SerializeField] private AndroidNotificationHandler _androidNotificationHandler;
    [SerializeField] private int _maxEnergy;
    [SerializeField] private int _energyRechargeDuration;

    private int _energy;
    private const string EnergyKey = "Energy";
    private const string EnergyReadyKey = "EnergyReady";

    private void Start(){
        OnApplicationFocus(true);
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus) { return; }

        CancelInvoke();

        int highScore = PlayerPrefs.GetInt(ScoreSystem.HighScoreKey, 0);

        _highScoreText.text = $"High Score: {highScore}";

        _energy = PlayerPrefs.GetInt(EnergyKey, _maxEnergy);
        if (_energy == 0)
        {
            string energyReadyString = PlayerPrefs.GetString(EnergyReadyKey, string.Empty);

            if (energyReadyString == string.Empty) { return; }

            DateTime energyReady = DateTime.Parse(energyReadyString);

            if (DateTime.Now > energyReady)
            {
                _energy = _maxEnergy;
                PlayerPrefs.SetInt(EnergyKey, _energy);
            }
            else
            {
                _playButton.interactable = false;
                Invoke(nameof(EnergyRechared), (energyReady - DateTime.Now).Seconds);
            }
        }

        _energyText.text = $"Play ({_energy})";
    }

    private void EnergyRechared()
    {
        _playButton.interactable = true;
        _energy = _maxEnergy;
        PlayerPrefs.SetInt(EnergyReadyKey, _energy);
        _energyText.text = $"Play ({_energy})";
    }

    public void Play()
    {
        if (_energy < 1) { return; }
        _energy--;
        PlayerPrefs.SetInt(EnergyKey, _energy);

        if (_energy == 0)
        {
            DateTime energyReady = DateTime.Now.AddMinutes(_energyRechargeDuration);
            PlayerPrefs.SetString(EnergyReadyKey, energyReady.ToString());
#if UNITY_ANDROID
            _androidNotificationHandler.ScheduleNotification(energyReady);
#endif
        }

        SceneManager.LoadScene(1);
    }
}
