using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaintManager : MonoBehaviour {

    [SerializeField]
    GameObject box;

    [SerializeField]
    LayerMask otherDish;

    [SerializeField]
    GameObject prefabOrigin;
    [SerializeField]
    GameObject ParentInstancePosition;

    [SerializeField]
    Dropdown typeDinosaur;

    /*public float width{ get; set;}
    public float height{ get; set;}*/
    public Image myColorDish { get; set; }
    public Color newColor;

    [SerializeField]
    LayerMask layerBoxColor;

    void Awake() {
        myColorDish = GameObject.Find("myColorDish").GetComponent<Image>();
    }

    void Start() {
        // Debug.Log("height and width :" + height + "and"+ width);
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

    public void ResetPicture() {

    }

    public void SendPicture() {
        GameObject newInstaceUser = Instantiate(prefabOrigin, ParentInstancePosition.transform.position, prefabOrigin.transform.rotation);
        newInstaceUser.transform.SetParent(ParentInstancePosition.transform);
    }

    private void OnGUI() {
        GUILayout.Label("Connections " + Network.connections.Length.ToString());
        if (GUI.Button(new Rect(10, 50, 70, 30), " Disconnect")) {
            Network.Disconnect(0);
            Application.LoadLevel("mainConnection");
        }
    }

    void DropdownValueChanged(Dropdown changeValue) {
        NetworkController _NetworkViewCommunicate = FindObjectOfType<NetworkController>();
        int typeDinosaur = changeValue.value;
        switch (typeDinosaur) {
            case 1:
                _NetworkViewCommunicate.TypeDinoSelected = typeDinosaur;
                break;
            case 2:
                _NetworkViewCommunicate.TypeDinoSelected = typeDinosaur;
                break;
            case 3:
                _NetworkViewCommunicate.TypeDinoSelected = typeDinosaur;
                break;
            case 4:
                _NetworkViewCommunicate.TypeDinoSelected = typeDinosaur;
                break;
            default:
                _NetworkViewCommunicate.TypeDinoSelected = 0;
                break;
        }
    }
}

