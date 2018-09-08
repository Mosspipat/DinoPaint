using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkController : MonoBehaviour {

    string SceneSelected;
    [SerializeField]
    List<GameObject> allTypeDinosaur;

    [SerializeField]
    List <Transform> allPosDisplay;
    [SerializeField]
    Vector2 positionDinoplace;
    Transform posDinoGetter;
    Vector3 newScale;

    [SerializeField]
    Dropdown dropdownSceneSelection;
    NetworkView _NetworkView;

    /*public string IPAddress = "127.0.0.1";
    public int port = 8632;*/
    public string newIPAddress;
    public int newport;

    bool isCreateHost;

    bool isConnect = false;

    public int TypeDinoSelected { get; set; }

    [SerializeField]
    GameObject dinoGetter;

    Vector3[] atest = new Vector3[20];

    public int[] UserHeadColorGet { get; set; }
    public int[] UserArmLColorGet { get; set; }
    public int[] UserArmRColorGet { get; set; }
    public int[] UserLegLColorGet { get; set; }
    public int[] UserLegRColorGet { get; set; }

    int[] allColorHeadSet;
    int[] allColorHandLSet;
    int[] allColorHandRSet;
    int[] allColorLegLSet;
    int[] allColorLegRSet;

    public InputField IpAddressInput;
    public InputField PortInput;


    void Start() {

        UserHeadColorGet = new int[20];
        UserArmLColorGet = new int[20];
        UserArmRColorGet = new int[20];
        UserLegLColorGet = new int[20];
        UserLegRColorGet = new int[20];

        _NetworkView = GetComponent<NetworkView>();
        dropdownSceneSelection.onValueChanged.AddListener(delegate {
            DropdownValueChanged(dropdownSceneSelection);
        });

        DontDestroyOnLoad(this.gameObject);
    }

    void Update() {

    }
    #region State Machine
    void OnServerInitialized() {
        Debug.Log("Server was Create");
        isConnect = true;
    }
    void OnConnectedToServer() {
        Debug.Log("Connect To Server");
        isConnect = true;
    }
    void OnPlayerDisconnected(NetworkPlayer player) {
        Debug.Log("Clean up after player " + player);

        Network.RemoveRPCs(player);
        Network.DestroyPlayerObjects(player);
    }
    #endregion
    void DropdownValueChanged(Dropdown changeValue) {
        int sceneCase = changeValue.value;
        switch (sceneCase) {
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
    void OnGUI() {
        if (!isConnect) {
            /* if (GUI.Button(new Rect(10, 100, 70, 30), "Connect ") && SceneSelected != null) {

                 Network.Connect(IPAddress, port);
                 Application.LoadLevel(SceneSelected);

                 isConnect = true;
             }
             if (GUI.Button(new Rect(10, 150, 70, 30), " Hosts") && SceneSelected != null) {
                 Network.InitializeServer(4, port);
                 Application.LoadLevel(SceneSelected);

                 isConnect = true;
             }*/

            if (GUI.Button(new Rect(10, 100, 70, 30), " Create Server") && SceneSelected != null) {
                newIPAddress = IpAddressInput.text;
                newport = int.Parse(PortInput.text);

                Debug.Log(newIPAddress);
                Debug.Log(newport);

                Network.InitializeServer(4, newport);
                Application.LoadLevel(SceneSelected);

                isConnect = true;
            }

            if (GUI.Button(new Rect(10, 150, 70, 30), " Join") && SceneSelected != null) {
                newIPAddress = IpAddressInput.text;
                newport = int.Parse (PortInput.text);

                Debug.Log(newIPAddress);
                Debug.Log(newport);

                Network.Connect(newIPAddress, newport);
                Application.LoadLevel(SceneSelected);

                isConnect = true;
            }

           /* if (GUI.Button(new Rect(10, 150, 70, 30), " Create Server") && SceneSelected != null) {
                newIPAddress = IpAddressInput.text;
                newport = int.Parse(PortInput.text);

                Network.InitializeServer(4, newport);
                Application.LoadLevel(SceneSelected);

                isConnect = true;
            }*/

        } else {
            GUILayout.Label("Connections " + Network.connections.Length.ToString());
            if (GUI.Button(new Rect(10, 50, 70, 30), " Disconnect")) {

                Network.Disconnect(0);
                isConnect = false;
            }
            //phototype GUI set
            /*if (GUI.Button(new Rect(10, 80, 70, 30), "sum"+ someInfos)) {
                 i++;
                 _NetworkView.RPC("ReceiveInfoFromClient", RPCMode.AllBuffered, i.ToString());
                 _NetworkView.RPC("CloneDinosaur", RPCMode.AllBuffered);
                 _NetworkView.RPC("ReceiveInfoFromClient", RPCMode.AllBuffered, TypeDinoSelected.ToString());
             }*/
            if (GUI.Button(new Rect(10, 80, 150, 30), "sendType" + allTypeDinoSelected)) {

                /*for (int i  = 0;i< 10; i++) {
                    atest[i] = new Vector3(1+i,1,1);
                }*/

                for (int i = 0; i < 20; i++) {
                    UserHeadColorGet[i] = GameObject.Find("Head").transform.GetChild(i).GetComponent<changeColour>().ColorThisBox;
                    UserArmLColorGet[i] = GameObject.Find("armL").transform.GetChild(i).GetComponent<changeColour>().ColorThisBox;
                    UserArmRColorGet[i] = GameObject.Find("armR").transform.GetChild(i).GetComponent<changeColour>().ColorThisBox;
                    UserLegLColorGet[i] = GameObject.Find("legL").transform.GetChild(i).GetComponent<changeColour>().ColorThisBox;
                    UserLegRColorGet[i] = GameObject.Find("legR").transform.GetChild(i).GetComponent<changeColour>().ColorThisBox;
                }

                /*_NetworkView.RPC("SendChageTypeOnServer", RPCMode.Server);*/
                _NetworkView.RPC("GetTypeOnServer", RPCMode.AllBuffered);
            }
        }
    }

    // typeDino
    /*[RPC]
    void ReceiveInfoFromClient(string someInfo) {
        someInfos += someInfo;
    }*/


    [RPC]
    void CloneDinosaur() {
        Vector3 posDinoZone = GameObject.Find("dinosaurZone").transform.position;

        GameObject dinoClone = Instantiate(allTypeDinosaur[0],
            new Vector3(Random.Range(-200, 200), 100, 0),
        allTypeDinosaur[0].transform.rotation);
        dinoClone.transform.SetParent(GameObject.Find("dinosaurZone").transform);
    }
    /*Vector3[] atest = new Vector3[20];
    public Vector3[] showcol = new Vector3[20];*/

    /*[RPC]
    void SendChageTypeOnServer(int typeWasSelected, Vector3[] atests) {
        showcol = atests;
        GameObject.FindObjectOfType<DisplayManager>().typeWasSelected = typeWasSelected;
        Debug.Log("sendToServer");
    }*/

    [RPC]
    void SendChageTypeOnServer(int typeWasSelected,int allPixel,
        int[] UserHeadColorSet,
        int[] UserArmLColorSet,
        int[] UserArmRColorSet,
        int[] UserLegLColorSet,
        int[] UserLegRColorSet) {

        Debug.Log("typeWasSelected"+ typeWasSelected);

        int randomPos = Random.Range(0,3);
        switch (randomPos) {
            case 0:
                posDinoGetter = GameObject.Find("posFar").transform;
                positionDinoplace = posDinoGetter.position + new Vector3(Random.Range(-400, 400),0,0);
                newScale = new Vector3(0.3f, 0.3f, 0.3f);
                break;
            case 1:
                posDinoGetter = GameObject.Find("posMiddle").transform;
                positionDinoplace = posDinoGetter.position + new Vector3(Random.Range(-500, 500), 0, 0);
                newScale = new Vector3(0.5f, 0.5f, 0.5f);
                break;
            case 2:
                posDinoGetter = GameObject.Find("posNear").transform;
                 positionDinoplace = posDinoGetter.position + new Vector3(Random.Range(-600, 600), 0, 0);
                newScale = new Vector3(1f, 1f, 1f);
                break;
        }

        dinoGetter = Instantiate(allTypeDinosaur[typeWasSelected], positionDinoplace, allTypeDinosaur[typeWasSelected].transform.rotation);
        dinoGetter.transform.SetParent(posDinoGetter.transform);
        Invoke("setSize", 0.001f);
        dinoGetter.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        Destroy(dinoGetter.gameObject,10f);

        allColorHeadSet = dinoGetter.transform.GetChild(0).GetComponent<PaintBox>().allValuePaint;
        allColorHandLSet = dinoGetter.transform.GetChild(1).GetComponent<PaintBox>().allValuePaint;
        allColorHandRSet = dinoGetter.transform.GetChild(2).GetComponent<PaintBox>().allValuePaint;
        allColorLegLSet = dinoGetter.transform.GetChild(3).GetComponent<PaintBox>().allValuePaint;
        allColorLegRSet = dinoGetter.transform.GetChild(4).GetComponent<PaintBox>().allValuePaint;

        allColorHeadSet = UserHeadColorSet;
        allColorHandLSet = UserArmLColorSet;
        allColorHandRSet = UserArmRColorSet;
        allColorLegLSet = UserLegLColorSet;
        allColorLegRSet = UserLegRColorSet;
        
        foreach(int a in allColorHeadSet) {
        Debug.Log("set" + a);
        }
        //wait for Map Color 
        Invoke("SetCol", 0.05f);

        /*GameObject.FindObjectOfType<DisplayManager>().typeWasSelected = typeWasSelected;*/
        Debug.Log("sendToServer");
    }
    int[] UserHeadColorSet;
        int[] UserArmLColorSet;
        int[] UserArmRColorSet;
        int[] UserLegLColorSet;
        int[] UserLegRColorSet;
    int _maxPixel;
    [RPC]
    void GetTypeOnServer() {
        Debug.Log("getInServer");

        /* for (int i = 0; i < allPixel; i++) {
             UserHeadColorGet[i] = UserHeadColorSet[i];
             UserArmLColorGet[i] = UserArmLColorSet[i];
             UserArmRColorGet[i] = UserArmRColorSet[i];
             UserLegLColorGet[i] = UserLegLColorSet[i];
             UserLegRColorGet[i] = UserLegRColorSet[i];
         }*/
        TypeDinoSelected = GameObject.FindObjectOfType<DisplayManager>().typeWasSelected;
        allTypeDinoSelected += TypeDinoSelected;
    }

    [RPC]
    void sendDinoTypeSelect(int typeWasSelected, int maxPixel,
        int[] _UserHeadColorSet,
        int[] _UserArmLColorSet,
        int[] _UserArmRColorSet,
        int[] _UserLegLColorSet,
        int[] _UserLegRColorSet) 
        {
        for (int i = 0; i < maxPixel; i++) 
            {

            }

       /* Instantiate(all[typeWasSelected], posSetDino.transform.position, allTypeDinoSelected[typeWasSelected]);

        for (int i = 0; i < maxPixel; i++) {
            GameObject.Find("head")FindObjectOfType<PaintBox> = UserHeadColorSet[i];
            GameObject.Find("head")FindObjectOfType < PaintBox > = UserArmLColorSet[i];
            GameObject.Find("head")FindObjectOfType < PaintBox > = UserArmRColorSet[i];
            GameObject.Find("head")FindObjectOfType < PaintBox > = UserLegLColorSet[i];
            GameObject.Find("head")FindObjectOfType < PaintBox > = UserLegRColorSet[i];
        }*/

    }/*
    public void setcol (){
            for (int i = 0; i<_maxPixel; i++) {

            print(headDino.transform.GetChild(i).gameObject);
            print(handLDino.transform.GetChild(i).gameObject);
            print(handRDino.transform.GetChild(i).gameObject);
            print(legLDino.transform.GetChild(i).gameObject);
            print(legRDino.transform.GetChild(i).gameObject);
            if (headDino.transform.GetChild(i).gameObject) {
                if (headDino.transform.GetChild(i).gameObject.GetComponent<changeColour>()) {
                    headDino.transform.GetChild(i).gameObject.GetComponent<changeColour>().ColorThisBox = UserHeadColorSet[i];
                    handLDino.transform.GetChild(i).gameObject.GetComponent<changeColour>().ColorThisBox = UserArmLColorSet[i];
                    handRDino.transform.GetChild(i).gameObject.GetComponent<changeColour>().ColorThisBox = UserArmRColorSet[i];
                    legLDino.transform.GetChild(i).gameObject.GetComponent<changeColour>().ColorThisBox = UserLegLColorSet[i];
                    legRDino.transform.GetChild(i).gameObject.GetComponent<changeColour>().ColorThisBox = UserLegRColorSet[i];
                }
             }

        }
}*/
    void SetCol() {
        for (int i = 0; i < 100; i++) {

        dinoGetter.transform.GetChild(0).GetChild(i).GetComponent<changeColour>().setColToNewDino(allColorHeadSet[i]);
        dinoGetter.transform.GetChild(1).GetChild(i).GetComponent<changeColour>().setColToNewDino(allColorHandLSet[i]);
        dinoGetter.transform.GetChild(2).GetChild(i).GetComponent<changeColour>().setColToNewDino(allColorHandRSet[i]);
        dinoGetter.transform.GetChild(3).GetChild(i).GetComponent<changeColour>().setColToNewDino(allColorLegLSet[i]);
        dinoGetter.transform.GetChild(4).GetChild(i).GetComponent<changeColour>().setColToNewDino(allColorLegRSet[i]);
        }
    }

    void setSize() {
        dinoGetter.GetComponent<RectTransform>().localScale = newScale;
    }

}
