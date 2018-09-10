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
        switch (playerScore) {
            case 0:
                scoreNumber0.sprite = zero;
                scoreNumber1.sprite = zero;
                break;
            case 1:
                scoreNumber0.sprite = one;
                scoreNumber1.sprite = zero;
                break;
        }
    }

    public void AddToScore(int pointsToAdd) {
        playerScore += pointsToAdd;
        SetScoreImageNumber();
    }

    public void ProcessPlayerDeath() {
        if (playerLives > 0) {
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
