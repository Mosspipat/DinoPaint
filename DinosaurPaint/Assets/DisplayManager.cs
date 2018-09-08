using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayManager : MonoBehaviour {

    public int typeWasSelected { get; set; }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnGUI() {
        GUILayout.Label("Connections " + Network.connections.Length.ToString());
        if (GUI.Button(new Rect(10, 50, 70, 30), " Disconnect")) {
            Network.Disconnect(0);
            Application.LoadLevel("mainConnection");
        }
    }
}
