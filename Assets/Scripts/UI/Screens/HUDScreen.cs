using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDScreen : Screen
{
    [SerializeField] private Button _pauseBtn;
    [SerializeField] private Button _musicBtn;
    [SerializeField] private Button _soundBtn;

    [SerializeField] private Sprite _musicOnImg;
    [SerializeField] private Sprite _musicOffImg;
    [SerializeField] private Sprite _soundsOnImg;
    [SerializeField] private Sprite _soundsOffImg;

    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI PlayerHealthText;

    private void Start()
    {
        _pauseBtn.onClick.AddListener(ClickPause);
        _musicBtn.onClick.AddListener(OnMusicMute);
        _soundBtn.onClick.AddListener(OnSoundMute);
    }

    public void ClickPause()
    {
        GameController.Instance.SoundController.PlaySound(SFX.SFXTypeUI.ClickToggle);
        GameController.Instance.ScreenController.PushScreen<PauseScreen>();
        Time.timeScale = 0f;
    }  
    
    public void OnMusicMute()
    {
        GameController.Instance.SoundController.PlaySound(SFX.SFXTypeUI.ClickToggle);
        GameController.Instance.SoundController.MuteBGMusic();

        if (_musicBtn.image.sprite == _musicOnImg)
        {
            _musicBtn.image.sprite = _musicOffImg;
        }
        else
        {
            _musicBtn.image.sprite = _musicOnImg;
        }
    }

    public void OnSoundMute()
    {
        GameController.Instance.SoundController.PlaySound(SFX.SFXTypeUI.ClickToggle);
        GameController.Instance.SoundController.MuteSFX();

        if (_soundBtn.image.sprite == _soundsOnImg)
        {
            _soundBtn.image.sprite = _soundsOffImg;
        }
        else
        {
            _soundBtn.image.sprite = _soundsOnImg;
        }
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
