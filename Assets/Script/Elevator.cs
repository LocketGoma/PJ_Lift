﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    /* 1. 엘리베이터는 고정된 층만 움직임
          예를들어, 1,3층으로 할당된 경우 = 1층에서 누를시 3층으로만 감.
       2. 이 스크립트는 엘리베이터 오브젝트 전체에 해당되는 것이며, 문을 여는 스크립트는 따로 존재.  
       3. 층 구별은 어케하냐고? 올라간or내려간 좌표 / 1개 층, 즉 15칸 움직임, 1층당 3칸 = 15/3 => 5층.  
    */
    // Use this for initialization
    [Header("엘리베이터")]
    public GameObject target_elevator;
    public float speed = 10;
//    public int code = 1;

    [Header("멈출 층 적기")]
    public int Up_floor = 2;
    public int Down_floor = 1;
    public bool toroof = true;  //false : 아래가 시작

    [Header("발판 정보")]
    public GameObject floor;

    [Header("문 정보")]
    public GameObject door_left;
    public GameObject door_right;

    [Header("Cross Open")]
    public bool isCross = false;
    public enum Cross_door { Left, Right }
    public Cross_door crossd_up;

    [Header("ETC")]
    public bool traced = false;     //엘리베이터가 유저 추적하는지 여부.

    int isup = 1;
    bool ismoving = false;
    bool use = false;
    int movefloor;
    Vector3 start;

    void Start()
    {
        start = transform.position;
        movefloor = Up_floor - Down_floor;
        speed = speed / 100;                    //0.02 = 1프레임당 1유닛 (1단위 거리)
        if (!toroof)
        {
            isup = 0;
            start.y -= (movefloor * 5);
        }
    }

    // Update is called once per frame
    void Update()   //수정 필요
    {
        
        if (traced == true && use == false)     //traced는 못써먹겠음;
        {            
            GameObject player = GameObject.FindGameObjectWithTag("Player");
   //         Debug.Log("user : " + player.transform.position.y + " obj : "+ transform.position.y);
            if (player.transform.position.y-1 <= transform.position.y && start.y < transform.position.y)
            {
                StartCoroutine("move_Down");
                StopCoroutine("move_Up");
            }
            else if (player.transform.position.y+1 >= transform.position.y && start.y + movefloor * 5 > transform.position.y)
            {
                StartCoroutine("move_Up");
                StopCoroutine("move_Down");
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            use = true;
            door_left.GetComponent<Door>().close_door();
            door_right.GetComponent<Door>().close_door();
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.parent = floor.transform;        //움직이는 플랫폼에서
            Debug.Log(other.transform.position.z);
            other.transform.position = new Vector3(other.transform.position.x, other.transform.position.y, floor.transform.position.z-(float)0.7);  //앞으로 튀어나오는 버그 설정.
        }                                                   //플레이어 오브젝트를
        if (use)                                            //아예 플랫폼 자식으로 때려박기.
        {
            Debug.Log(isup);
            if (isup == 1)
            {
                ismoving = true;
                StartCoroutine("move_Up");
            }
            else if (isup == 0)
            {
                ismoving = true;
                StartCoroutine("move_Down");
            }
        }
    }
    IEnumerator move_Up()
    {
        //       Debug.Log("start:" + (start.z + length));
        //       Debug.Log("now:" + transform.position.z);

        target_elevator.transform.position += new Vector3(0, speed, 0);
        

        yield return new WaitForFixedUpdate();
        StartCoroutine("move_Up");
        if (start.y + movefloor * 5 < transform.position.y)
        {
            StopCoroutine("move_Up");
            isup ^= 1;
            ismoving = false;
            use = false;
            this.Open_door();
        }
    }
    IEnumerator move_Down()
    {
        //       Debug.Log("start:" + (start.z + length));
        //       Debug.Log("now:" + transform.position.z);

        target_elevator.transform.position += new Vector3(0, -speed, 0);

        yield return new WaitForFixedUpdate();
        StartCoroutine("move_Down");
        if (start.y > transform.position.y)
        {
            StopCoroutine("move_Down");
            isup ^= 1;
            ismoving = false;
            use = false;
            this.Open_door();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (ismoving == true)               //움직이는 중에 탈주시
        {
            ismoving = false;
            StopCoroutine("move_Up");
            StopCoroutine("move_Down");
            if (isup == 1)
            {
                StartCoroutine("move_Down");
            }
            if (isup == 0)
            {
                StartCoroutine("move_Up");
            }

        }
        

        if (other.gameObject.tag == "Player")
        {
            other.transform.parent = null;                  //null = 최상단
        }

    }
    void Open_door()
    {
        if (!ismoving)
        {
            if (isCross)
            {
                Open_cross(isup);
            }
            else
            {
                door_left.GetComponent<Door>().open_door();
                door_right.GetComponent<Door>().open_door();
            }
            use = false;
        }
    }
    void Open_cross(int isUp)
    {
 //       Debug.Log("Crossed");
        if (crossd_up == Cross_door.Left)
        {
//            Debug.Log("Left");
            if (isUp == 1)
            {
                door_right.GetComponent<Door>().open_door();
            }
            else
            {
                door_left.GetComponent<Door>().open_door();
            }

        }
       else if (crossd_up == Cross_door.Right)
        {
 //           Debug.Log("Right");
            if (isUp == 1)
            {
                door_left.GetComponent<Door>().open_door();
            }
            else
            {
                door_right.GetComponent<Door>().open_door();
            }
        }
    }
}
