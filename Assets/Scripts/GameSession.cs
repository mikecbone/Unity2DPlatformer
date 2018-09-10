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

    [SerializeField] AudioClip finishMusic;

    public Sprite zero;
    public Sprite one;
    public Sprite two;
    public Sprite three;
    public Sprite four;
    public Sprite five;
    public Sprite six;
    public Sprite seven;
    public Sprite eight;
    public Sprite nine;

    AudioSource audioSource;

    private void Awake() {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1) {
            Destroy(this.gameObject);
        }
        else {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void Start() {
        SetLivesImageNumber();
        SetScoreImageNumber();
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayGameMusic() {
        audioSource.Play();
    }

    private void SetLivesImageNumber() {
        switch (playerLives) {
            case 3:
                livesNumber.sprite = three;
                break;
            case 2:
                livesNumber.sprite = two;
                break;
            case 1:
                livesNumber.sprite = one;
                break;
            default:
                livesNumber.sprite = zero;
                break;
        }
    }

    private void SetScoreImageNumber() {
        if (playerScore < 10) {
            scoreNumber0.sprite = ReturnImageForNumber(playerScore);
            scoreNumber1.sprite = zero;
        }
        else {
            string playerScoreString = playerScore.ToString();
            char[] playerScoreChars = playerScoreString.ToCharArray();
            int playerScore0 = int.Parse(playerScoreChars[0].ToString());
            int playerScore1 = int.Parse(playerScoreChars[1].ToString());

            scoreNumber0.sprite = ReturnImageForNumber(playerScore0);
            scoreNumber1.sprite = ReturnImageForNumber(playerScore1);
        }
    }

    private Sprite ReturnImageForNumber(int number) {
        switch (number) {
            case 0:
                return zero;
            case 1:
                return one;
            case 2:
                return two;
            case 3:
                return three;
            case 4:
                return four;
            case 5:
                return five;
            case 6:
                return six;
            case 7:
                return seven;
            case 8:
                return eight;
            case 9:
                return nine;
            default:
                return zero;
        }
    }

    public void AddToScore(int pointsToAdd) {
        playerScore += pointsToAdd;
        SetScoreImageNumber();
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
