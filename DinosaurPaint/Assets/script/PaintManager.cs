using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaintManager : MonoBehaviour {

    NetworkController _NetworkViewCommunicate;

    [SerializeField]
    Image backgroundImage;
    [SerializeField]
    List<Sprite> allBackgroundImage;

    [SerializeField]
    List<GameObject> AllTypeDino;

    [SerializeField]
    GameObject placePoint;

    [SerializeField]
    GameObject paintDino;

    [SerializeField]
    GameObject box;

    [SerializeField]
    LayerMask otherDish;

    [SerializeField]
    Dropdown typeDinosaur;
    public int typeDinosaurInt;

    /*public float width{ get; set;}
    public float height{ get; set;}*/
    public Image myColorDish { get; set; }
    public Color newColor;
    public int newColorInt { get; set; }

    [SerializeField]
    LayerMask layerBoxColor;

    public GameObject headDino;
    public GameObject handLDino;
    public GameObject handRDino;
    public GameObject legLDino;
    public GameObject legRDino;

    [SerializeField]
    int maxPixel;

    int[] allPaintPartDinohead;
    int[] allPaintPartDinohandL;
    int[] allPaintPartDinohandR;
    int[] allPaintPartDinolegL;
    int[] allPaintPartDinolegR;

    void Awake() {
        myColorDish = GameObject.Find("myColorDish").GetComponent<Image>();
    }
    void Start() {

        allPaintPartDinohead = new int[maxPixel];
        allPaintPartDinohandL = new int[maxPixel];
        allPaintPartDinohandR = new int[maxPixel];
        allPaintPartDinolegL = new int[maxPixel];
        allPaintPartDinolegR = new int[maxPixel];

        // Debug.Log("height and width :" + height + "and"+ width);
        ChangeTypePaint(typeDinosaurInt);

        _NetworkViewCommunicate = FindObjectOfType<NetworkController>();

        typeDinosaur.onValueChanged.AddListener(delegate {
            DropdownValueChanged(typeDinosaur);
        });
    }

    void Update() {

        box.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -100f);
        Debug.DrawRay(box.transform.position, box.transform.forward * 150f, Color.red);
        //  MouseClick();

        /* GetPixel();*/
    }
    void MouseClick() {
        if (Input.GetMouseButton(0)) {
            RaycastHit hit;
            if (Physics.Raycast(box.transform.position, box.transform.forward, out hit, Mathf.Infinity, otherDish)) {
                Debug.Log(hit.transform.name);
                newColor = hit.transform.GetComponent<Image>().color;
                myColorDish.GetComponent<Image>().color = newColor;
                /*GameObject colorPaint = Instantiate(spriteColor, new Vector2(Input.mousePosition.x,Input.mousePosition.y), spriteColor.transform.rotation);
                colorPaint.transform.SetParent(CharacterParent.transform);*/
            } else if (Physics.Raycast(box.transform.position, box.transform.forward, out hit, Mathf.Infinity, layerBoxColor)) {
                Debug.Log("paint to " + hit.transform.name);
                hit.transform.GetComponent<Image>().color = newColor;
            }
        }
    }


    private void OnGUI() {
        GUILayout.Label("Connections " + Network.connections.Length.ToString());
        if (GUI.Button(new Rect(10, 50, 70, 30), " Disconnect")) {
            Network.Disconnect(0);
            Application.LoadLevel("mainConnection");
        }
    }

    void DropdownValueChanged(Dropdown changeValue) {
        typeDinosaurInt = changeValue.value;
        ChangeTypePaint(typeDinosaurInt);
        Debug.Log("changeDino");
        switch (typeDinosaurInt) {
            case 0:
                _NetworkViewCommunicate.TypeDinoSelected = 0;
                backgroundImage.sprite = allBackgroundImage[0];
                break;
            case 1:
                _NetworkViewCommunicate.TypeDinoSelected = 1;
                backgroundImage.sprite = allBackgroundImage[1];
                break;
            case 2:
                _NetworkViewCommunicate.TypeDinoSelected = 2;
                backgroundImage.sprite = allBackgroundImage[2];
                break;
            case 3:
                _NetworkViewCommunicate.TypeDinoSelected = 3;
                backgroundImage.sprite = allBackgroundImage[3];
                break;
            default:
                _NetworkViewCommunicate.TypeDinoSelected = 0;
                backgroundImage.sprite = allBackgroundImage[0];
                break;
        }
    }

    void ChangeTypePaint(int typeDino) {
        Debug.Log(typeDino);
        Destroy(paintDino);

        paintDino = Instantiate(AllTypeDino[typeDino], placePoint.transform.position, AllTypeDino[typeDino].transform.rotation);


        Destroy(paintDino.GetComponent<Rigidbody2D>());
        Destroy(paintDino.GetComponent<DinosaurBehavior>());
        Destroy(paintDino.GetComponent<Animator>());

        headDino = paintDino.transform.GetChild(0).gameObject;
        handLDino = paintDino.transform.GetChild(1).gameObject;
        handRDino = paintDino.transform.GetChild(2).gameObject;
        legLDino = paintDino.transform.GetChild(3).gameObject;
        legRDino = paintDino.transform.GetChild(4).gameObject;

        paintDino.transform.SetParent(placePoint.transform);
        paintDino.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }

    public void SendPainting() {

        Debug.Log("send");

        for (int i = 0; i < maxPixel; i++) {
            allPaintPartDinohead[i] = headDino.transform.GetChild(i).GetComponent<changeColour>().ColorThisBox;
            allPaintPartDinohandL[i] = handLDino.transform.GetChild(i).GetComponent<changeColour>().ColorThisBox;
            allPaintPartDinohandR[i] = handRDino.transform.GetChild(i).GetComponent<changeColour>().ColorThisBox;
            allPaintPartDinolegL[i] = legLDino.transform.GetChild(i).GetComponent<changeColour>().ColorThisBox;
            allPaintPartDinolegR[i] = legRDino.transform.GetChild(i).GetComponent<changeColour>().ColorThisBox;
        }

        NetworkView netViewPlayer = FindObjectOfType<NetworkView>();

        //send these Color num int to Server;
        netViewPlayer.RPC("SendChageTypeOnServer", RPCMode.Server,
            typeDinosaurInt,
            maxPixel,
            allPaintPartDinohead,
            allPaintPartDinohandL,
            allPaintPartDinohandR,
            allPaintPartDinolegL,
            allPaintPartDinolegR);
    }

    public void AnimChooseCol(GameObject thisObj) {
        Vector3 newPos = thisObj.transform.position + Vector3.right * 100f;
        thisObj.transform.position = new Vector3(newPos.x, thisObj.transform.position.y, thisObj.transform.position.z);
    }

    public void NotAnimChooseCol(GameObject thisObj) {
        Vector3 newPos = thisObj.transform.position - Vector3.right * 100f;
        thisObj.transform.position = new Vector3(newPos.x, thisObj.transform.position.y, thisObj.transform.position.z);
    }

    public void ResetCol() {
        for (int i = 0; i < maxPixel; i++) {
            headDino.transform.GetChild(i).GetComponent<Image>().color = new Color(255, 255, 255, 255);
            handLDino.transform.GetChild(i).GetComponent<Image>().color = new Color(255, 255, 255, 255);
            handRDino.transform.GetChild(i).GetComponent<Image>().color = new Color(255, 255, 255, 255);
            legLDino.transform.GetChild(i).GetComponent<Image>().color = new Color(255, 255, 255, 255);
            legRDino.transform.GetChild(i).GetComponent<Image>().color = new Color(255, 255, 255, 255);
        }
    }
}
