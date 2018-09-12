using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour {

	void Start () {
        GameSession.instance.SetFinishMusic();
    }
}
