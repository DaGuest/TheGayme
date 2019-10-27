using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterController : MonoBehaviour
{
    private static bool created = false;
    
    void Awake() {
        if (!created) {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
        else {
            GameObject.Destroy(this.gameObject);
        }
    }
}
