using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public void StartFirstLevel() {
        GameSession.instance.SetLevelMusic();
        SceneManager.LoadScene(1);
    }
}
