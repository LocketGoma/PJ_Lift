using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_unity : MonoBehaviour {
    /*  1. 좌 우만 움직일것
        *  2. 액션 버튼을 넣어둘것 (좌 우 구별함)
        *  3. 부모자식관계 해제 -> 회전 -> 재설정
        *  4. 각도 조절.
        */
    [Header("Moving speed")]
    public float speed = 10f;

    [Header("열쇠 사용 여부")]
    public bool use_key = false;
    bool has_key = false;
    public Rigidbody rbody;
    private float inputH; // 수평 입력

    public enum ControlType { keyboard, mouse, mouseDrag, touch }
    [Header("움직임 타입")]
    public ControlType control;

    //애니메이션
    public Animator anim;
    private bool st = true;    // switch

    //기타 설정
    [Header("카메라")]
    public Camera cama;
    private Vector3 cama_axis;

    Vector3 movement = new Vector3(0, 0, 0);
    int count = 0;

    int touch_axis = 0;         //touch용.

    // Use this for initialization
    void Start()
    {
        rbody.freezeRotation = true;
       
        
    }

    // Update is called once per frame
    void Update()
    {
        cama_axis = new Vector3(rbody.transform.position.x, rbody.transform.position.y+1, rbody.transform.position.z-7);
        anim.SetFloat("inputH", inputH);
        if (inputH < 0&&st)
        {
            rbody.transform.rotation = Quaternion.Euler(rbody.rotation.x,270,rbody.rotation.z);
            cama.transform.position = cama_axis;
            cama.transform.rotation = Quaternion.Euler(rbody.rotation.x, 0, rbody.rotation.z);
            st = false;
        }
        if (inputH > 0 && st)
        {
            rbody.transform.rotation = Quaternion.Euler(rbody.rotation.x, 90, rbody.rotation.z);
            cama.transform.position = cama_axis;
            cama.transform.rotation = Quaternion.Euler(rbody.rotation.x, 0, rbody.rotation.z);
            st = false;
        }
        if (inputH == 0) st = true;

        if (control == ControlType.keyboard)
        {
            moveKeyboard();            
        }
        if (control == ControlType.mouse)
        {
            moveMouse();
        }
        if (control == ControlType.mouseDrag)
        {
            moveMouseDrag();
        }
        if (control == ControlType.touch)
        {
            moveTouch();
        }


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
                    Destroy(other.gameObject, 0f);
                }
            }
        }   //-------

        if (other.gameObject.tag == "DeadSpace")
        {
            Manager.ResetGame();
        }

    }

    void moveKeyboard()
    {
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
    void moveMouse()
    {
        if (Input.GetMouseButtonUp(0))
        {
            count = (int)speed * 2;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10f))
            {
                inputH = hit.point.x - transform.position.x;
                movement = new Vector3(inputH, 0, 0);
            }
        }
        //if (movement.x != 0)
        if (count != 0)
        {
            transform.position += movement * speed * 0.5f * Time.deltaTime;
            count--;

            if (count == 0) inputH = 0;
        }
    }
    void moveMouseDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10f))
        {
            transform.position = new Vector3(hit.point.x, transform.position.y, transform.position.z); //<- drag
        }
    }
    void moveTouch()
    {
        {
            if (Input.GetKeyDown("x"))
            {
                Manager.ResetGame();
            }

            inputH = (float)touch_axis;

            float moveX = inputH * speed * Time.deltaTime;
            //       if (moveX != 0.0) print(moveX);
            rbody.velocity = new Vector3(moveX, 0f, 0f);
            transform.position += transform.right * moveX;
        }
    }
    public void drag(int i)
    {
        touch_axis = i;
    }

}

