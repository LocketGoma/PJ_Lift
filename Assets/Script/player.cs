using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {
    /*  1. 좌 우만 움직일것
     *  2. 액션 버튼을 넣어둘것 (좌 우 구별함)
     *  
     */
    [Header("Moving speed")]
    public float speed = 10f;

    [Header("키 사용 여부")]
    public bool use_key = false;
    bool has_key = false;
    public Rigidbody rbody;
    private float inputH; // 수평 입력



    
    // Use this for initialization
    void Start () {
        rbody = GetComponent<Rigidbody>();
        GetComponent<Rigidbody>().freezeRotation = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("x"))
        {
            Manager.ResetGame();
        }

        inputH = Input.GetAxis("Horizontal");


        float moveX = inputH * speed * Time.deltaTime;
 //       if (moveX != 0.0) print(moveX);
        rbody.velocity = new Vector3(moveX, 0f, 0f);
        transform.position += transform.right * moveX;
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Next")          //게임 종료
        {
            if (use_key == false)
            {
                other.enabled = false;      //지금 화면 멈추게 하기.
                Manager.EndGame();
            }
            if (use_key == true)            //키 사용 설정을 켜면.
            {
                if (has_key == true)
                {
                    other.enabled = false;      
                    Manager.EndGame();
                }
            }
        }   //-----
        if (other.gameObject.tag == "Key")
        {
            if (use_key == true)
            {
                if (has_key == false)
                {
                    has_key = true;
                    Destroy(other.gameObject,0f);
                }
            }
        }   //-------

        if (other.gameObject.tag == "DeadSpace")
        {
            Manager.ResetGame();
        }

    }

}
