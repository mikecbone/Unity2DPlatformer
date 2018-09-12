using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour {
    [SerializeField] int playerLives = 3;
    [SerializeField] int playerScore = 0;

    [SerializeField] Image livesNumber;
    [SerializeField] Image scoreNumber0;
    [SerializeField] Image scoreNumber1;
    [SerializeField] Image musicToggle;

    [SerializeField] Sprite musicOnSprite;
    [SerializeField] Sprite musicOffSprite;

    [SerializeField] AudioClip menuMusic;
    [SerializeField] AudioClip levelMusic;
    [SerializeField] AudioClip finishMusic;

    [SerializeField] Sprite[] numbers;

    AudioSource audioSource;
    public static GameSession instance;
    private bool isMusicMuted = false;

    private void Awake() {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1) {
            Destroy(this.gameObject);
        }
        else {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void Start() {
        SetLivesImageNumber();
        SetScoreImageNumber();
        audioSource = GetComponent<AudioSource>();
    }

    public void SetMenuMusic() {
        audioSource.clip = menuMusic;
        PlayMusic();
    }

    public void SetLevelMusic() {
        audioSource.clip = levelMusic;
        PlayMusic();
    }

    public void SetFinishMusic() {
        audioSource.clip = finishMusic;
        PlayMusic();
    }

    private void PlayMusic() {
        if (!isMusicMuted) {
            audioSource.Play();
        }
    }

    public void ToggleGameMusic() {
        if (audioSource.isPlaying) {
            audioSource.Pause();
            musicToggle.sprite = musicOffSprite;
            isMusicMuted = true;
        }
        else {
            audioSource.Play();
            musicToggle.sprite = musicOnSprite;
            isMusicMuted = false;
        }
    }

    private void SetLivesImageNumber() {
        livesNumber.sprite = numbers[playerLives];
    }

    private void SetScoreImageNumber() {
        if (playerScore < 10) {
            scoreNumber0.sprite = numbers[playerScore];
            scoreNumber1.sprite = numbers[0];
        }
        else {
            string playerScoreString = playerScore.ToString();
            char[] playerScoreChars = playerScoreString.ToCharArray();
            int playerScore0 = int.Parse(playerScoreChars[1].ToString());
            int playerScore1 = int.Parse(playerScoreChars[0].ToString());

            scoreNumber0.sprite = numbers[playerScore0];
            scoreNumber1.sprite = numbers[playerScore1];
        }
    }

    public void AddToScore(int pointsToAdd) {
        playerScore += pointsToAdd;
        SetScoreImageNumber();
    }

    public bool CheckIfMaxLives() {
        return (playerLives == 3);
    }

    public void AddToLives(int livesToAdd) {
        if (playerLives == 3) {
            return;
        }
        playerLives += livesToAdd;
        SetLivesImageNumber();
    }

    public void ProcessPlayerDeath() {
        if (playerLives > 1) {
            RemoveLife();
        }
        else {
            ResetGameSession();
        }
    }

    private void RemoveLife() {
        playerLives--;
        SetLivesImageNumber();
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void ResetGameSession() {
        SceneManager.LoadScene(0);
        Destroy(this.gameObject);
    }
}
