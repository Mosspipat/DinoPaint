using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class setchangecolour : MonoBehaviour {
    public PaintManager _paintManager;
	// Use this for initialization
    GameObject myDishColor;

    Color newColor;
    public int newColorGet { get; set; }

    void Awake()
    {
    }

    void Start () {
        _paintManager = GameObject.FindObjectOfType<PaintManager>();
    }


    public void colortoget(int i){
        //set int[num] boxPaint's color from Trigger
        _paintManager.newColorInt = i;

        Debug.Log("getNewColor");
        switch (i)
        {
            case 1:
                newColor = ConflicNewColor(253, 211, 8,255);
                break;
            case 2:
                newColor = ConflicNewColor(247, 147, 29,255);
                break;
            case 3:
                newColor = ConflicNewColor(224, 77, 37,255);
                break;
            case 4:
                newColor = ConflicNewColor(175, 27, 110,255);
                break;
            case 5:
                newColor = ConflicNewColor(100, 38, 96,255);
                break;
            case 6:
                newColor = ConflicNewColor(2, 80, 147, 255);
                break;
            case 7:
                newColor = ConflicNewColor(95, 185, 202, 255);
                break;
            case 8:
                newColor = ConflicNewColor(85, 168, 68, 255);
                break;
            case 9:
                newColor = ConflicNewColor(173, 204, 54, 255);
                break;
            case 0:
                newColor = ConflicNewColor(255, 255, 255, 255);
                break;
            default:
                //newColor = ConflicNewColor(255, 255, 255,255);
                break;
        }
       // _gameManager.newColor = i;

    }
	// Update is called once per frame
	void Update () {
		
	}

    Color ConflicNewColor(int r, int g, int b, int alpha)
    {
        newColor = new Color(r/255f,g/255f,b/255f,alpha/255f);
        _paintManager.newColor = newColor;
        _paintManager.myColorDish.color = newColor;

        return newColor;
    }
}
