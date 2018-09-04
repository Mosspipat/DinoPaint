using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkViewPlayer : MonoBehaviour {

    

    NetworkView _NetworkView;


    void Start () {
        _NetworkView = GetComponent<NetworkView>();
        _NetworkView.RPC(("SendMessage"), RPCMode.All, null);

        DontDestroyOnLoad(this.gameObject);
    }

    [RPC]
    void SendMessage() {
        Debug.Log("Sending Message form" + _NetworkView.viewID);
    }
}
