using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {
    GameObject key;
	// Use this for initialization
	void Start () {
        key = GameObject.FindGameObjectWithTag("Key");
	}
	
	// Update is called once per frame
	void Update () {
        //StartCoroutine("Spin");
        key.transform.Rotate(Vector3.up*2);
    }

   
}
