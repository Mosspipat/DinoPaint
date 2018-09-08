using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoDisplay : MonoBehaviour {

    private void Awake() {
        GetComponent<RectTransform>().localScale = new Vector3(0.2f, 0.2f, 0.2f);
    }

    void Start () {
		
	}
	

	void Update () {
		
	}
}
