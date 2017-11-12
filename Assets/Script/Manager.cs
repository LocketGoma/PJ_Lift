#define Debug_set
#if Debug_set
using System;
#endif
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class Manager : MonoBehaviour {
    static int stageLevel = 0;      //0 : 튜토리얼
    static int maxLevel = 11;        //0 포함 개수  - 총 맵 수 : 튜토리얼 + 맵 9개 + 테스트 1 = 11개
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //디버깅용
#if Debug_set
        int input = 0;

        if (Input.GetKeyDown("1"))
            input = 1;
        if (Input.GetKeyDown("2"))
            input = 2;
        if (Input.GetKeyDown("3"))
            input = 4;
        if (Input.GetKeyDown("4"))
            input = 5;
        if (Input.GetKeyDown("5"))
            input = 6;
        if (Input.GetKeyDown("6"))
            input = 7;
        if (Input.GetKeyDown("7"))
            input = 8;
        if (Input.GetKeyDown("8"))
            input = 9;

        if (input!=0)
        SceneManager.LoadScene(input, LoadSceneMode.Single);
        //Debug.Log("index:" + e.keyCode);
#endif

    }
    public static void ResetGame()
    {
        SceneManager.LoadScene(stageLevel, LoadSceneMode.Single);
    }
    public static void Set_level(int level)
    {
        if(level!=-1)
        stageLevel = level;
    }
   public static void EndGame()
    {
        stageLevel++;
        if (stageLevel > maxLevel)
        {
            Time.timeScale = 0f;
        }
        else
        SceneManager.LoadScene(stageLevel, LoadSceneMode.Single);
    }
}
