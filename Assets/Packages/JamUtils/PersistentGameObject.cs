using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JamUtilities {

public class PersistentGameObject : MonoBehaviour {
    void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }
}

}