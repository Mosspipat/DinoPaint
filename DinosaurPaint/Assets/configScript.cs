using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class configScript : MonoBehaviour {

    [SerializeField]
    InputField slotXText, slotYText;

    [SerializeField]
    Text showConfigSet;

    public int slotXInt{get;set;}
    public int slotYInt{get;set;}

    public int maxPixel { get; set; }

    public InputField IpAddressInput;
    public InputField PortInput;

    public string newIPAddress;
    public int newport;

    bool isConnect;

    [SerializeField]
    Dropdown selectionScene;

    string SceneSelected;

    [SerializeField]
    Button buttonActiveCreateServer;

    [SerializeField]
    Button buttonActiveJoin;

    private void Start() {
        DontDestroyOnLoad(this.gameObject);

        //addEvent DropDown
        selectionScene.onValueChanged.AddListener(delegate {
            DropdownValueChanged(selectionScene);
        });
    }

    private void Update() {
        //canCreateServer();
    }

    public void SetMaxPixel() {
        //set Config to script
       slotXInt = int.Parse (slotXText.text);
       slotYInt = int.Parse(slotXText.text);

        maxPixel = slotXInt * slotYInt;

        showConfigSet.text = "slotX :" + slotXInt.ToString() + "\n slotY :" + slotYInt.ToString() + "\n maxPixel :" + slotXInt * slotYInt;
        
    }

    public void CreateServer() {

        newIPAddress = IpAddressInput.text;
        newport = int.Parse(PortInput.text);

        Debug.Log(newIPAddress);
        Debug.Log(newport);

        Network.InitializeServer(4, newport);
        Application.LoadLevel("Display");

        isConnect = true;
    }

    public void Join() {

        newIPAddress = IpAddressInput.text;
        newport = int.Parse(PortInput.text);

        Debug.Log(newIPAddress);
        Debug.Log(newport);

        Network.Connect(newIPAddress, newport);
        Application.LoadLevel("Paint");

        buttonActiveJoin.interactable = true;
        isConnect = true;
    }

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

    /*void canCreateServer() {
        if (IpAddressInput.text == null || PortInput.text == null) {
            buttonActiveCreateServer.interactable = false;
        }
        else { buttonActiveCreateServer.interactable = true; }
    }*/
}
