﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinosaurBehavior : MonoBehaviour {

    [SerializeField]
    GameObject Dinosaur;

    [SerializeField]
    float speed;
    [SerializeField]
    float forceJump;

    bool isflip;
    [SerializeField]
    float timeToflip;

	void Start () {
        InvokeRepeating("Flip", 3f,timeToflip);
        Jump();
    }
    
    void Update () {
        Walk();
	}

    void Walk()
    {
        transform.Translate(Vector2.right * Time.deltaTime * speed);
    }

    void Run()
    {
        transform.Translate(this.transform.forward * Time.deltaTime * speed *2f);
    }

    void Jump()
    {
        GetComponent<Rigidbody>().AddForce(Vector2.up * forceJump);
    }

    IEnumerator SpeedUp()
    {
        speed = 3;
        Debug.Log("speed up" + speed);
        yield return new WaitForSeconds(5);
        speed = 0;
        Debug.Log("speed down" + speed);
    }

    void Flip()
    {
        isflip = !isflip;
        if(isflip)
        {
            this.transform.rotation = Quaternion.Euler(0f, 180f, 0);
        }
        else
        {
            this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }

}
