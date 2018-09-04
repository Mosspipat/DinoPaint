using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class setchangecolour : MonoBehaviour {
    public gameManager _gameManager;
	// Use this for initialization
    GameObject myDishColor;

    Color newColor;

    void Awake()
    {
    }

    void Start () {
        _gameManager = GameObject.FindObjectOfType<gameManager>();
    }


    public void colortoget(int i){
        Debug.Log("getNewColor");
        switch (i)
        {
            case 1:
                newColor = ConflicNewColor(150, 150, 150,255);
                break;
            case 2:
                newColor = ConflicNewColor(255, 13, 13,255);
                break;
            case 3:
                newColor = ConflicNewColor(30, 255, 0,255);
                break;
            case 4:
                newColor = ConflicNewColor(0, 33, 255,255);
                break;
            case 5:
                newColor = ConflicNewColor(0, 0, 0,255);
                break;
            default:
                newColor = ConflicNewColor(255, 255, 255,255);
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
        _gameManager.newColor = newColor;
        _gameManager.myColorDish.color = newColor;

        return newColor;
    }
}
