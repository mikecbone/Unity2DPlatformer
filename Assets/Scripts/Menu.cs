using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	public void StartFirstLevel() {
        FindObjectOfType<GameSession>().PlayGameMusic();
        SceneManager.LoadScene(1);
    }
}
