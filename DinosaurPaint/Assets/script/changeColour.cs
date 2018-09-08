using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeColour : MonoBehaviour {
    public PaintManager _paintManager;
    public int newColorGet { get; set; }

    //Int [Color number]from this Box
    public int ColorThisBox;

    void Start() {
        _paintManager = FindObjectOfType<PaintManager>();
    }


    public void changecolours() {

        if (GetComponent<Image>().color != _paintManager.newColor) {
            GetComponent<Image>().color = _paintManager.newColor;
            newColorGet = FindObjectOfType<PaintManager>().newColorInt;

            Debug.Log("colorChange" + newColorGet);
            ColorThisBox = newColorGet;

            //set int[num] boxPaint's color from Trigger
        }
    }

    public void setColToNewDino(int colo) {

        Image thisBoxCol = GetComponent<Image>();
        Debug.Log("setNewColor");

        switch (colo) {
            case 1:
                thisBoxCol.color = ConflicNewColor(253, 211, 8, 255);
                break;
            case 2:
                thisBoxCol.color = ConflicNewColor(247, 147, 29, 255);
                break;
            case 3:
                thisBoxCol.color = ConflicNewColor(224, 77, 37, 255);
                break;
            case 4:
                thisBoxCol.color = ConflicNewColor(175, 27, 110, 255);
                break;
            case 5:
                thisBoxCol.color = ConflicNewColor(100, 38, 96, 255);
                break;
            case 6:
                thisBoxCol.color = ConflicNewColor(2, 80, 147, 255);
                break;
            case 7:
                thisBoxCol.color = ConflicNewColor(95, 185, 202, 255);
                break;
            case 8:
                thisBoxCol.color = ConflicNewColor(85, 168, 68, 255);
                break;
            case 9:
                thisBoxCol.color = ConflicNewColor(173, 204, 54, 255);
                break;
            default:
                thisBoxCol.color = ConflicNewColor(255, 255, 255, 255);
                break;
        }
    }
        Color ConflicNewColor(int r, int g, int b, int alpha) 
            {
            Color newColor = new Color(r / 255f, g / 255f, b / 255f, alpha / 255f);
            return newColor;
            }

        /*void setColor() {
            for (int i = 0; i < transform.GetChildCount(); i++) {
                transform.GetChild(0).GetComponent<Image>().color = colorGet[i];
            }
        }*/
    }

