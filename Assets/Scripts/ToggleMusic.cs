using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMusic : MonoBehaviour {

    public void Toggle() {
        GameSession.instance.ToggleGameMusic();
    }
}
