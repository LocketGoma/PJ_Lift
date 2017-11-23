#define Debug_set
#if Debug_set
using System;
#endif
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

public class Manager : MonoBehaviour {
    static int stageLevel = 0;      //0 : 튜토리얼
    static int maxLevel = 11;        //0 포함 개수  - 총 맵 수 : 튜토리얼 + 맵 9개 + 테스트 1 = 11개
    bool load = false;
    // Use this for initialization
    void Start () {
        if (load)
        {
            LoadGame();
        }
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
        if (Input.GetKeyDown("9"))
            input = 10;
        if (Input.GetKeyDown("c"))
        {
            LoadGame();
        }

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
    public static void SaveGame()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fs = File.Create(Application.persistentDataPath + "/DataFile.dat");
        //FileStream fs = File.Create("/DataFile.dat");
        Save sv = new Save();
        sv.level = stageLevel;
      //  Debug.Log("lv : " + sv.level);
        formatter.Serialize(fs, sv);
        fs.Close();
    }
    void LoadGame()
    {
        FileStream fs = new FileStream(Application.persistentDataPath + "/DataFile.dat", FileMode.Open);
        BinaryFormatter formatter = new BinaryFormatter();
        if (fs != null && fs.Length > 0)
        {
            Save sv = (Save)formatter.Deserialize(fs);
            Set_level(sv.level);
            ResetGame();
        }
        fs.Close();
    }

    /*Application.persistentDataPath는 Debug.Log 혹은 Print 해보면 그 경로를 알 수 있다.
    Windows의 경우 아래의 경로였다.
    %USERPROFILE%\AppData\LocalLow\게임회사이름\게임이름
    게임회사이름과 게임이름은 Build Setting에서 지정한 명칭이다.*/

    [Serializable]
    public class Save
    {
        public int level;

        public override string ToString()
        {
            return "level : " + level;
        }
        public int getLevel()
        {
            return level;
        }
    }

}
