using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkController : MonoBehaviour {

    string SceneSelected;

    [SerializeField]
    GameObject Dinosaur;
    [SerializeField]
    Dropdown dropdownSceneSelection;
    NetworkView _NetworkView;
    public string IPAddress = "127.0.0.1";
    public int port = 8632;

    bool isCreateHost;

    bool isConnect = false;

    public int TypeDinoSelected { get; set; }

    void Start() {
        _NetworkView = GetComponent<NetworkView>();
        dropdownSceneSelection.onValueChanged.AddListener(delegate {
            DropdownValueChanged(dropdownSceneSelection);
        });

        DontDestroyOnLoad(this.gameObject);
    }

    void Update() {

    }
    #region State Machine
    void OnServerInitialized() 
    {
        Debug.Log("Server was Create");
        isConnect = true;
    }
    void OnConnectedToServer() 
    {
        Debug.Log("Connect To Server");
        isConnect = true;
    }
    void OnPlayerDisconnected(NetworkPlayer player) {
        Debug.Log("Clean up after player " + player);

        Network.RemoveRPCs(player);
        Network.DestroyPlayerObjects(player);
    }
    #endregion
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
    //Phototype Value
    /*
    string someInfos;
    int i = 0;
    */
    string allTypeDinoSelected;
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

                Network.Disconnect(0);
                isConnect = false;
            }
            //phototype GUI set
           /* if (GUI.Button(new Rect(10, 80, 70, 30), "sum"+ someInfos)) {
                i++;
                _NetworkView.RPC("ReceiveInfoFromClient", RPCMode.AllBuffered, i.ToString());
                _NetworkView.RPC("CloneDinosaur", RPCMode.AllBuffered);
                _NetworkView.RPC("ReceiveInfoFromClient", RPCMode.AllBuffered, TypeDinoSelected.ToString());
            }*/

            if (GUI.Button(new Rect(10, 80, 150, 30), "sendType" + allTypeDinoSelected)) {
                _NetworkView.RPC("SendTypeSelected", RPCMode.AllBuffered, TypeDinoSelected.ToString());
            }
        }
    }

    // typeDino
    /*[RPC]
    void ReceiveInfoFromClient(string someInfo) {
        someInfos += someInfo;
    }*/

    [RPC]
    void CloneDinosaur()
    {
        Vector3 posDinoZone = GameObject.Find("dinosaurZone").transform.position;

        GameObject dinoClone = Instantiate(Dinosaur,
            new Vector3(Random.Range(-200, 200), 100, 0)
            , Dinosaur.transform.rotation);
        dinoClone.transform.SetParent(GameObject.Find("dinosaurZone").transform);
    }

    [RPC]
    void SendTypeSelected(string someInfo) {
        allTypeDinoSelected += TypeDinoSelected;
    }

}
