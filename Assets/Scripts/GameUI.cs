using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{

    public ScoreText scoreTextPlayer1, scoreTextPlayer2;
    public GameObject menuObject;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI playModeButtonText;
    public TextMeshProUGUI volumeValueText;
    public TextMeshProUGUI goalValueText;
    public System.Action onStartGame;
    public Slider goalSlider;

    private void Start(){
        AdjustPlayModeButtonText();
    }

    public void UpdateScores(int scorePlayer1, int scorePlayer2){
        scoreTextPlayer1.SetScore(scorePlayer1);
        scoreTextPlayer2.SetScore(scorePlayer2);
    }

    public void HighlightScore(int id){
        if(id == 1)
            scoreTextPlayer1.Highlight();
        else
            scoreTextPlayer2.Highlight();
    }

    public void OnStartGameButtonClicked(){
        menuObject.SetActive(false);
        onStartGame?.Invoke();
    }

    public void OnGameEnds(int winnerId){
        menuObject.SetActive(true);
        winText.text = $"Player {winnerId} wins!";
    }

    public void OnVolumeChanged(float value){
        AudioListener.volume = value;
        volumeValueText.text = $"{Mathf.RoundToInt(value * 100)} %";
    }

    public void OnGoalChanged(float value){
        GameManager.instance.maxScore = value;
        goalValueText.text = goalSlider.value.ToString();
    }

    public void OnSwitchPlayModeButtonClicked(){
        GameManager.instance.SwitchPlayMode();
        AdjustPlayModeButtonText();
    }

    public void OnSecretAreaButtonClicked(){
        SceneManager.LoadScene("Secret");
    }

    private void AdjustPlayModeButtonText(){
        switch(GameManager.instance.playMode){
            case GameManager.PlayMode.PlayerVsPlayer:
                playModeButtonText.text = "2 Players";
                break;
            case GameManager.PlayMode.PlayerVsCPU:
                playModeButtonText.text = "Player vs CPU";
                break;
            case GameManager.PlayMode.CpuVsCpu:
                playModeButtonText.text = "CPU vs CPU";
                break;
        }
    }
}
