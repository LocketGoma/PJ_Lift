using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    [Header("작동 여부")]
    public bool can_open = true;    //false:트리거 작동 안함.
    public float speed = 300f;        //문짝 열리는 속도
    public float length = 2f;       //문짝 열리는 크기
//    public int code = 1;            //엘리베이터 번호 <- 특정화 용

    public Transform door;

    Vector3 start;
	// Use this for initialization
	void Start () {
        door = GetComponent<Transform>();
        start = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
	}
    /*
    public int get_code()
    {
        return code;
    }
    */
    //    public void open_door(int vaild)
    public void open_door()
    {
        //        if (this.code == vaild)
        if (can_open)
            {
                StopCoroutine("move_close_door");
                StartCoroutine("move_open_door");
            }
    }
    public void close_door()
    {
        if (can_open)
        {
            StopCoroutine("move_open_door");
            StartCoroutine("move_close_door");
        }
    }


    IEnumerator move_open_door()
    {
 //       Debug.Log("start:" + (start.z + length));
 //       Debug.Log("now:" + transform.position.z);
        
        transform.position += new Vector3(0,0,Time.deltaTime);


        yield return new WaitForSeconds(1 / speed);
        StartCoroutine("move_open_door");
        if(start.z+length < transform.position.z)
        {
            StopCoroutine("move_open_door");
        }
    }
    IEnumerator move_close_door()
    {
        //       Debug.Log("start:" + (start.z));
        //       Debug.Log("now:" + transform.position.z);

        while (true)
        {
            transform.position += new Vector3(0, 0, -(speed*Time.deltaTime/100));
            //yield return new WaitForSeconds(1 / speed);
            yield return new WaitForFixedUpdate();

            if (start.z > transform.position.z)
            {
                //StopCoroutine("move_close_door");
                yield break;
            }
        }
    }



}
