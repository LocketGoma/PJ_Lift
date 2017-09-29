using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Presser : MonoBehaviour {

    
//    public int code=1;
    public GameObject trigger;
    public GameObject target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
 //       Debug.Log("Triggered");
        if (other.gameObject.tag == "Player")
        {
 //           Debug.Log("ok");
            target.GetComponent<Door>().open_door();        //타 오브젝트 스크립트 갖고오기
        }
    }
    
}
