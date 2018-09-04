using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeColour : MonoBehaviour {
    public PaintManager _paintManager;
	// Use this for initialization
	void Start () {
        _paintManager = FindObjectOfType<PaintManager>();
	}
    public  void changecolours(){
        if (GetComponent<Image>().color != _paintManager.newColor)
        {
            GetComponent<Image>().color = _paintManager.newColor;
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}

