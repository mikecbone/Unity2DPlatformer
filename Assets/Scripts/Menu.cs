using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    private void Start() {
        GameSession.instance.PlayMenuMusic();
    }

    public void StartFirstLevel() {
        GameSession.instance.PlayLevelMusic();
        SceneManager.LoadScene(1);
    }
}
