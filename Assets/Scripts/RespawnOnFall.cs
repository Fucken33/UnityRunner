using UnityEngine;
using System.Collections;

public class RespawnOnFall : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        if (transform.position.y < -20f)
        {
            Application.LoadLevel(Application.loadedLevel);
        }	
	}
}
