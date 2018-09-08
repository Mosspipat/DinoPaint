using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetColorFromServer : MonoBehaviour {

    //int
    List<Color> colorGet = new List<Color>();

    Color colorPixel = new Color(0,0,0,0);

	void Start () {
        setColor();
	}

    void setColor() {
        for(int i = 0 ; i < transform.GetChildCount() ;i++) {
            transform.GetChild(0).GetComponent<Image>().color = colorGet[i];
        }
    }
}
