using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneUserID : MonoBehaviour {
    [SerializeField]
    GameObject userID;

    // Use this for initialization
    void Start() {
        GameObject UserNetID = Instantiate(userID);
        UserNetID.AddComponent<NetworkView>();
        UserNetID.AddComponent<NetworkViewPlayer>();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
