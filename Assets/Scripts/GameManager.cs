using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameUI gameUI;
    public GameAudio gameAudio;
    public CameraShake screenshake;
    public BallScript ball;
    
    public int scorePlayer1, scorePlayer2;
    public System.Action onReset;
    public float maxScore;
    public PlayMode playMode;
    public int matchesWinnedAsPlayer1;
    public TextMeshProUGUI revealSecretCode;

    public enum PlayMode
    {
        PlayerVsPlayer,
        PlayerVsCPU,
        CpuVsCpu
    }

    private void Awake(){
        if(instance){
            Destroy(gameObject);
        }
        else {
            instance = this;
            gameUI.onStartGame += OnStartGame;
        }
    }

    public void Update(){
        if(matchesWinnedAsPlayer1 == 1){
            revealSecretCode.text = "First Secret Code: 111111";
        }
        if(matchesWinnedAsPlayer1 == 2){
            revealSecretCode.text = "Second Secret Code: 222222";
        }
        if(matchesWinnedAsPlayer1 == 3){
            revealSecretCode.text = "Third Secret Code: 333333";
        }
        if(matchesWinnedAsPlayer1 >= 4){
            revealSecretCode.text = "";
        }
    }

    private void OnDestroy(){
        gameUI.onStartGame -= OnStartGame;
    }

    public void OnScoreZoneReached(int id){
        if(id == 1)
          scorePlayer1++;
        if(id == 2)
          scorePlayer2++;

        gameUI.UpdateScores(scorePlayer1, scorePlayer2);
        gameUI.HighlightScore(id);
        CheckWin();
    }

    private void CheckWin(){
        int winnerId = scorePlayer1 == maxScore ? 1 : scorePlayer2 == maxScore ? 2 : 0;

        if(winnerId != 0){
            if(winnerId == scorePlayer1){
                matchesWinnedAsPlayer1++;
            }
            gameUI.OnGameEnds(winnerId);
            gameAudio.PlayWinSound();
        }
        else {
            onReset?.Invoke();
            gameAudio.PlayScoreSound();
        }
    }

    private void OnStartGame(){
        scorePlayer1 = 0;
        scorePlayer2 = 0;
        gameUI.UpdateScores(scorePlayer1, scorePlayer2);
    }

    public void SwitchPlayMode(){
        switch(playMode){
            case PlayMode.PlayerVsPlayer:
                playMode = PlayMode.PlayerVsCPU;
                break;
            case PlayMode.PlayerVsCPU:
                playMode = PlayMode.CpuVsCpu;
                break;
            case PlayMode.CpuVsCpu:
                playMode = PlayMode.PlayerVsPlayer;
                break;
        }
    }

    public bool IsPlayer2CPU(){
        return playMode == PlayMode.PlayerVsCPU || playMode == PlayMode.CpuVsCpu;
    }

    public bool IsPlayer1CPU(){
        return playMode == PlayMode.CpuVsCpu;
    }

    public void ExitGame(){
        if(Application.platform == RuntimePlatform.WebGLPlayer || Application.platform == RuntimePlatform.WindowsEditor){
            SceneManager.LoadScene("ThanksForPlaying");
        }
        else if(Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.Android){
            Application.Quit();
        }
    }
}
