using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTicker : MonoBehaviour {
    public int nowlevel=-1;
	// Use this for initialization
	void Start () {
        Manager.Set_level(nowlevel);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
