using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour {
    [SerializeField] int playerLives = 3;

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
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void ResetGameSession() {
        SceneManager.LoadScene(0);
        Destroy(this.gameObject);
    }
}
