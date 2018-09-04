using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeColour : MonoBehaviour {
    public gameManager _gameManager;
	// Use this for initialization
	void Start () {
        _gameManager = FindObjectOfType<gameManager>();
	}
    public  void changecolours(){
        if (GetComponent<Image>().color != _gameManager.newColor)
        {
            GetComponent<Image>().color = _gameManager.newColor;
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}

