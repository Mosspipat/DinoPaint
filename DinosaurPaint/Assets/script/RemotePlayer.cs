using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemotePlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void ControllPlayer() 
    {
        if (Input.GetKey(KeyCode.W)) 
        {

        }
    }

    [RPC]
    public void ClonePlayer() 
        {
        Debug.Log("clone player");
        }
}
