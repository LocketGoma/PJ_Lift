using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Switch : MonoBehaviour {
    // 스위치를 터치하면 막힌 문이 열리게.

    [Header("초기 설정")]
    public float speed = 500f;
    public int length = 100;     //문 열리는 크기
    int init = 0;

    [Header("적용될 오브젝트")]
    public GameObject[] door;

    Transform lever;
    int lever_init = 0;
    int lever_switched = 45;

	// Use this for initialization
	void Start () {
        lever = GetComponent<Transform>();

        /*
        for (int i = 0; i < door.Length; i++)
        {
            StartCoroutine(open_door(door[i]));
        }
        */
    }
	
	// Update is called once per frame
	void Update () {

    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("scripted");
 //       if (other.gameObject.tag == "Player")
  //      {
            StartCoroutine("lever_up");
            for (int i = 0; i<door.Length; i++)
            {               
                StartCoroutine(open_door(door[i]));
            }
  //      }
    }
    IEnumerator open_door(GameObject other)             //신버전 스크립트
    {
        //       Debug.Log("start:" + (start.z));
        //       Debug.Log("now:" + transform.position.z);
        
        while (true)
        {
            other.transform.position += new Vector3(0, 0, speed * Time.deltaTime / 100);
            //yield return new WaitForSeconds(1 / speed);
            yield return new WaitForFixedUpdate();

            if (init++>length)
            {
                Debug.Log("init:" + init + "lenght:"+length);
                yield break;
            }
        }
    }
    IEnumerator lever_up()
    {
        while (true)
        {
            lever.transform.Rotate(-Vector3.up);
            if (lever_init++ > lever_switched)
                yield break;
        }
    }
}
