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
    public GameObject quitButton;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI playModeButtonText;
    public TextMeshProUGUI volumeValueText;
    public TextMeshProUGUI goalValueText;
    public System.Action onStartGame;
    public Slider goalSlider;
    public bool GamePaused;
    public GameObject tutorials;

    private void Start(){
        AdjustPlayModeButtonText();
        CheckDisableQuitButton();
        #if UNITY_ANDROID
            tutorials.gameObject.SetActive(false);
        #endif
    }

    private void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(!menuObject.activeInHierarchy){
                menuObject.SetActive(true);
                BallScript.instance.ResetBallPositionAfterPressEscape();
                Time.timeScale = 0f;
                GamePaused = false;
            }
        }
        if(!GamePaused){
            if(Input.GetKeyDown(KeyCode.P) && !menuObject.activeInHierarchy){
                Time.timeScale = 0f;
                GamePaused = true;
            }
        } 
        else 
        {
            if(Input.GetKeyDown(KeyCode.P)){
                Time.timeScale = 1f;
                GamePaused = false;
            }
        }
    }

    public void PauseButton(){
        if(!GamePaused){
            Time.timeScale = 0f;
            GamePaused = true;
        } 
        else 
        {
            Time.timeScale = 1f;
            GamePaused = false;
        }
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
        Time.timeScale = 1f;
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

    private void CheckDisableQuitButton(){
        #if UNITY_WEBGL
            quitButton.SetActive(false);
        #endif
    }

    public void OnQuitButtonClicked(){
        Application.Quit();
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
