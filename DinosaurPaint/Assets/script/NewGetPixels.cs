﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGetPixels : MonoBehaviour {

    public Texture2D sourceTex;
    public Rect sourceRect;

	// Use this for initialization
	void Start () {
        GetNewPixels();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void GetNewPixels()
    {
        int x = Mathf.FloorToInt(sourceRect.x);
        int y = Mathf.FloorToInt(sourceRect.y);
        int width = Mathf.FloorToInt(sourceRect.width);
        int height = Mathf.FloorToInt(sourceRect.height);

        Color[] pix = sourceTex.GetPixels(x, y, width, height);
        Texture2D destTex = new Texture2D(width, height);
        destTex.SetPixels(pix);
        destTex.Apply();

        // Set the current object's texture to show the
        // extracted rectangle.
        GetComponent<Renderer>().material.mainTexture = destTex;
    }
}
