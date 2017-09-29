using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Manager : MonoBehaviour {
    static int stageLevel = 0;      //0 : 튜토리얼
    static int maxLevel = 3;        //0 포함 개수
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
        if (stageLevel == maxLevel)
        {
            Time.timeScale = 0f;
        }
        else
        SceneManager.LoadScene(stageLevel, LoadSceneMode.Single);
    }
}
