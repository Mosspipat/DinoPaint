using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class configScript : MonoBehaviour {

    [SerializeField]
    InputField slotXText, slotYText;

    [SerializeField]
    Text showConfigSet;

    public int slotXInt { get; set; }
    public int slotYInt { get; set; }

    public int maxPixel { get; set; }

    public InputField IpAddressInput;
    public InputField PortInput;

    public string newIPAddress;
    public int newport;

    bool isConnect;

    [SerializeField]
    Dropdown selectionDino;

    string SceneSelected;

    [SerializeField]
    Button buttonActiveCreateServer;

    [SerializeField]
    Button buttonActiveJoin;

    public int typeDino {get;set;}

    private void Start() {

        DontDestroyOnLoad(this.gameObject);

        //addEvent DropDown
        
        selectionDino.onValueChanged.AddListener(delegate {
            ChangeDino(selectionDino);
        });
    }

    void ChangeDino(Dropdown selectionDino) {
       switch (selectionDino.value) {
           case 1:typeDino = selectionDino.value;
                Debug.Log(selectionDino);
               break;
           case 2:typeDino = selectionDino.value;
                Debug.Log(selectionDino);
                break;
           case 3:typeDino = selectionDino.value;
                Debug.Log(selectionDino);
                break;
           case 4:typeDino = selectionDino.value;
                Debug.Log(selectionDino);
                break;
           default:typeDino = 0;
               break;
       }
   }

    public void SetMaxPixel() {
        //set Config to script
       slotXInt = int.Parse (slotXText.text);
       slotYInt = int.Parse(slotXText.text);

        maxPixel = slotXInt * slotYInt;

        showConfigSet.text = "slotX :" + slotXInt.ToString() + "\n slotY :" + slotYInt.ToString() + "\n maxPixel :" + slotXInt * slotYInt;

        buttonActiveJoin.interactable = true;
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

        Debug.Log("typeis " +typeDino);

        newIPAddress = IpAddressInput.text;
        newport = int.Parse(PortInput.text);

        Debug.Log(newIPAddress);
        Debug.Log(newport);

        Network.Connect(newIPAddress, newport);
        Application.LoadLevel("Paint");

        buttonActiveJoin.interactable = true;
        isConnect = true;
    }

    }

    /*void canCreateServer() {
        if (IpAddressInput.text == null || PortInput.text == null) {
            buttonActiveCreateServer.interactable = false;
        }
        else { buttonActiveCreateServer.interactable = true; }
    }*/
