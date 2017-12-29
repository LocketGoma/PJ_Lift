using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class virturalPad : MonoBehaviour {

    /*
    [Header("Renderer")]
    public UnityEngine.UI.Image Renderer2D_left;
    public UnityEngine.UI.Image Renderer2d_right;
    public Button btn_left;
    public Button btn_right;
    */
    [Header("Target")]
    public GameObject player;

    private player_unity playerscript;

    private bool pressed;
    private bool pressed_left;
    private bool pressed_right;



    // Use this for initialization
    void Start () {
        pressed = false;
        pressed_left = false;
        pressed_right = false;

        playerscript = player.GetComponent<player_unity>();

	}
	
	// Update is called once per frame
    void Update () {
        if (pressed)
        {
            if (pressed_left) move_left();
            if (pressed_right) move_right();
        }
        if (Input.GetMouseButtonUp(0)) release();
      
        
    }
    public void press_reset()
    {
        Manager.ResetGame();
    }
    public void press_left()
    {
        pressed_left = true;
        pressed = true;
    }   
    public void press_right()
    {
        pressed_right = true;
        pressed = true;
    }
    void release()
    {
        pressed = false;
        pressed_left = false;
        pressed_right = false;
        playerscript.drag(0);
    }



   private void move_left()
    {
        
        Debug.Log("left");

        playerscript.drag(-1);

    }
    private void move_right()
    {
        Debug.Log("right");
        playerscript.drag(1);
    }
}
