using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public void StartFirstLevel() {
        GameSession.instance.PlayLevelMusic();
        SceneManager.LoadScene(1);
    }
}
