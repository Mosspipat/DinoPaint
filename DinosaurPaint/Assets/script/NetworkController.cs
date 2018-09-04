using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkController : MonoBehaviour {

    string SceneSelected;

    [SerializeField]
    Dropdown dropdownSceneSelection;
    NetworkView _NetworkView;
    public string IPAddress = "127.0.0.1";
    public int port = 8632;

    bool isCreateHost;

    bool isConnect = false;

    void Start() {

        dropdownSceneSelection.onValueChanged.AddListener(delegate {
            DropdownValueChanged(dropdownSceneSelection);
        });
        

    }

    void Update() {

    }

    void OnConnectedToServer() 
    {
        Debug.Log("Connect To Server");
        isConnect = true;
    }

    void OnServerInitialized() 
    {
        Debug.Log("Server was Create");
        isConnect = true;
    }

    void DropdownValueChanged(Dropdown changeValue) 
        {
        int sceneCase = changeValue.value;
        switch (sceneCase) 
        {
            case 1:
                SceneSelected = "Display";
                //Application.LoadLevel(SceneSelected);
                break;
            case 2:
                SceneSelected = "Paint";
                //Application.LoadLevel(SceneSelected);
                break;
            default:
                SceneSelected = null;
                //Debug.Log("please selet User Scene");
                break;
        }
        }

    void OnGUI() 
    {
        if (!isConnect) 
        {
            if (GUI.Button(new Rect(10, 10, 70, 30), "Connect ") && SceneSelected != null) 
            {
                Network.Connect(IPAddress, port);
                Application.LoadLevel(SceneSelected);

                isConnect = true;
            }
            if (GUI.Button(new Rect(10, 50, 70, 30), " Hosts") && SceneSelected != null) 
            {
                Network.InitializeServer(4,port);
                Application.LoadLevel(SceneSelected);

                isConnect = true;
            }
        }
        else 
        {
            GUILayout.Label("Connections " + Network.connections.Length.ToString());
            if (GUI.Button(new Rect(10, 50, 70, 30), " Disconnect")) {
                Network.Disconnect(2);
                isConnect = false;
            }
        }
    }
}
