using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PaintManager : MonoBehaviour {

    NetworkController _NetworkViewCommunicate;

    int maxPixel;

    configScript configSlot;

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

    int[] allPaintPartDinohead;
    int[] allPaintPartDinohandL;
    int[] allPaintPartDinohandR;
    int[] allPaintPartDinolegL;
    int[] allPaintPartDinolegR;

    [SerializeField]
    GameObject sendPanel;
    [SerializeField]
    GameObject fadeImage;

    void Awake() {
        myColorDish = GameObject.Find("myColorDish").GetComponent<Image>();
        typeDinosaurInt = GameObject.FindObjectOfType<configScript>().typeDino;
    }
    void Start() {

        configSlot = GameObject.FindObjectOfType<configScript>();
        maxPixel = configSlot.maxPixel;

        Debug.Log("maxPixel"+maxPixel);

        allPaintPartDinohead = new int[maxPixel];
        allPaintPartDinohandL = new int[maxPixel];
        allPaintPartDinohandR = new int[maxPixel];
        allPaintPartDinolegL = new int[maxPixel];
        allPaintPartDinolegR = new int[maxPixel];

        // Debug.Log("height and width :" + height + "and"+ width);
        ChangeTypePaint(typeDinosaurInt);

        _NetworkViewCommunicate = FindObjectOfType<NetworkController>();

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

        Invoke("MakeStroke", 0.1f);

        paintDino.transform.SetParent(placePoint.transform);
        paintDino.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }

    public void SendPainting() {

        Debug.Log("send");
        Debug.Log("Pixel" + maxPixel);
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

    public void PrepareSend() {
        sendPanel.transform.DOScale(1, 1f);
        fadeImage.GetComponent<Image>().DOColor(Color.black, 1f);
        fadeImage.GetComponent<Image>().DOFade(1, 1f);
    }

    public void CancelSend() {
        sendPanel.transform.DOScale(0, 1f);
        fadeImage.GetComponent<Image>().DOColor(Color.white, 1f);
        fadeImage.GetComponent<Image>().DOFade(0, 1f);
    }

    void MakeStroke() {

        //.......... make Stroke .........
        GameObject strokeheadDino = Instantiate(paintDino.transform.GetChild(0).gameObject, headDino.transform.position,
            paintDino.transform.GetChild(0).gameObject.transform.rotation);
        Destroy(strokeheadDino.GetComponent<PaintBox>());
        strokeheadDino.GetComponent<Image>().DOFade(0.2f, 1f);
        strokeheadDino.GetComponent<Image>().raycastTarget = false;
        strokeheadDino.transform.SetParent(headDino.transform);
        for (int i = 0; i < maxPixel; i++) {
            Destroy(strokeheadDino.transform.GetChild(i).gameObject);
        }

    GameObject strokehandLDino = Instantiate(paintDino.transform.GetChild(1).gameObject, handLDino.transform.position,
        paintDino.transform.GetChild(1).gameObject.transform.rotation);
    Destroy(strokehandLDino.GetComponent<PaintBox>());
        strokehandLDino.GetComponent<Image>().DOFade(0.2f, 1f);
        strokehandLDino.GetComponent<Image>().raycastTarget = false;
        strokehandLDino.transform.SetParent(handLDino.transform);
        for (int i = 0; i < maxPixel; i++) {
            Destroy(strokehandLDino.transform.GetChild(i).gameObject);
        }

        GameObject strokehandRDino = Instantiate(paintDino.transform.GetChild(2).gameObject, handRDino.transform.position,
            paintDino.transform.GetChild(2).gameObject.transform.rotation);
        Destroy(strokehandRDino.GetComponent<PaintBox>());
        strokehandRDino.GetComponent<Image>().DOFade(0.2f, 1f);
        strokehandRDino.GetComponent<Image>().raycastTarget = false;
        strokehandRDino.transform.SetParent(handRDino.transform);
        for (int i = 0; i < maxPixel; i++) {
            Destroy(strokehandRDino.transform.GetChild(i).gameObject);
        }

        GameObject strokelegLDino = Instantiate(paintDino.transform.GetChild(3).gameObject, legLDino.transform.position,
            paintDino.transform.GetChild(3).gameObject.transform.rotation);
        Destroy(strokelegLDino.GetComponent<PaintBox>());
        strokelegLDino.GetComponent<Image>().DOFade(0.2f, 1f);
        strokelegLDino.GetComponent<Image>().raycastTarget = false;
        strokelegLDino.transform.SetParent(legLDino.transform);
        for (int i = 0; i < maxPixel; i++) {
            Destroy(strokelegLDino.transform.GetChild(i).gameObject);
        }

        GameObject strokelegRDino = Instantiate(paintDino.transform.GetChild(4).gameObject, legRDino.transform.position,
            paintDino.transform.GetChild(4).gameObject.transform.rotation);
        Destroy(strokelegRDino.GetComponent<PaintBox>());
        strokelegRDino.GetComponent<Image>().DOFade(0.2f, 1f);
        strokelegRDino.GetComponent<Image>().raycastTarget = false;
        strokelegRDino.transform.SetParent(legRDino.transform);
        for (int i = 0; i < maxPixel; i++) {
            Destroy(strokelegRDino.transform.GetChild(i).gameObject);
        }
    }
}
