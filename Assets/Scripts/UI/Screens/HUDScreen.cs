using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDScreen : Screen
{
    [SerializeField] private Button _pauseBtn;

    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI PlayerHealthText;

    private void Start()
    {
        _pauseBtn.onClick.AddListener(StayPause);
    }

    public void StayPause()
    {
        GameController.Instance.ScreenController.PushScreen<PauseScreen>();
        Time.timeScale = 0.01f;
    }   


    public void UpdateScoreText(int currentScore)
    {
        ScoreText.text = " Score: " + currentScore.ToString();
    }

    public void UpdateHealthView(int playerHealth)
    {
        PlayerHealthText.text = " Health: " + playerHealth.ToString();
    }

}
