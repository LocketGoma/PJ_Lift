using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorStayed : MonoBehaviour {
    //무조건 양 쪽 문 다 열리는 엘리베이터에서만 적용되는 클래스.
    //양 문이 열리고 닫길때까지 시간이 주어짐.
    //버그 있음. 같은 설정인데 Elevator 클래스와 비교시 약 5배 느림
    //조치 : spped * 5로 해버림. 임시 조치이므로 나중에 다시 볼것.
    [Header("엘리베이터")]
    public GameObject target_elevator;
    public float speed = 10;
//    public int code = 1;

    [Header("멈출 층 적기")]
    public int start_floor = 1;
    public int roof_floor = 2;
    public bool toroof = true;  //false : 아래가 시작

    [Header("발판 정보")]
    public GameObject roof;

    [Header("문 정보")]
    public GameObject door_left;
    public GameObject door_right;

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
        movefloor = roof_floor - start_floor;
        speed = speed / 100;                    //0.02 = 1프레임당 1유닛 (1단위 거리)
        if (!toroof)
        {
            isup = 0;
            start.y -= (movefloor * 5);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (traced == true && use == false)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            //         Debug.Log("user : " + player.transform.position.y + " obj : "+ transform.position.y);
            if (player.transform.position.y - 2 <= transform.position.y && start.y < transform.position.y)
            {
                StartCoroutine("move_Down");
                StopCoroutine("move_Up");
            }
            else if (player.transform.position.y + 2 >= transform.position.y && start.y + movefloor * 5 > transform.position.y)
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
            Open_door();
            Debug.Log("use?! : " + use);
            if (use)                                            
            {
                Debug.Log("oepn");
                if (isup == 1)
                {
                    StartCoroutine("move_Up");
                }
                else if (isup == 0)
                {
                    StartCoroutine("move_Down");
                }
            }

        }
    }
    void OnTriggerStay(Collider other)
    {
        Debug.Log("use? : " + use);
        if (other.gameObject.tag == "Player")
        {
            other.transform.parent = roof.transform;        //움직이는 플랫폼에서
        }                                                   //플레이어 오브젝트를
                                                            //아예 플랫폼 자식으로 때려박기.
    }
    IEnumerator move_Up()               //이거 기반으로 코드 수정할것
    {
        //       Debug.Log("start:" + (start.z + length));
        //       Debug.Log("now:" + transform.position.z);
        
            ismoving = true;
            yield return new WaitForSeconds(3);

            use = false;
            Close_door();

        while (true)
        {
            target_elevator.transform.position += new Vector3(0, speed, 0);
            if (start.y + movefloor * 5 < transform.position.y)
            {
                StopCoroutine("move_Up");
                isup ^= 1;
                ismoving = false;
                use = false;
 
                break;
            }
            yield return new WaitForFixedUpdate();
        }
        this.Open_door();
    }
    IEnumerator move_Down()
    {
        //       Debug.Log("start:" + (start.z + length));
        //       Debug.Log("now:" + transform.position.z);
        ismoving = true;
        yield return new WaitForSeconds(3);

        use = false;
        Close_door();

        while (true)
        {
            target_elevator.transform.position += new Vector3(0, -speed, 0);
            if (start.y > transform.position.y)
            {
                StopCoroutine("move_Down");
                isup ^= 1;
                ismoving = false;
                use = false;

                break;
            }
            yield return new WaitForFixedUpdate();
        }
        this.Open_door();
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StopAllCoroutines();
            ismoving = false;
            use = false;

            other.transform.parent = null;                  //null = 최상단
        }

    }
    void Open_door()
    {
        if (!ismoving)
        {
           
           door_left.GetComponent<Door>().open_door();
           door_right.GetComponent<Door>().open_door();
  
        }
    }   
    void Close_door()
    {
//        Debug.Log("close!");
        door_left.GetComponent<Door>().close_door();
        door_right.GetComponent<Door>().close_door();
    }
}
