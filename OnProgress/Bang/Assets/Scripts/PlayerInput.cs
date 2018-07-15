using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    public string verticalAxisName = "Vertical";        //The name of the thruster axis
    public string horizontalAxisName = "Horizontal";    //The name of the rudder axis
    public string fireButtonName = "Fire1";                 //The name of the brake button

    public float move
    {
        get
        {
            return m_Move;
        }
    }

    private float m_Move;

    public float rotate
    {
        get
        {
            return m_Rotate;
        }
    }

    private float m_Rotate;

    public bool fire
    {
        get { return m_Fire; }
    }

    private bool m_Fire;


    void Update()
    {
        // 유니티 에디터가 아닌 실제 빌드에서, 종료 버튼을 누르면 게임 종료
        if (Input.GetButtonDown("Cancel") && !Application.isEditor)
        {
            Application.Quit();
        }

        /*
        //If a GameManager exists and the game is not active...
        if (GameManager.instance != null && !GameManager.instance.IsActiveGame())
        {
            //...set all inputs to neutral values and exit this method
            thruster = rudder = 0f;
            isBraking = false;
            return;
        }
        */

        //Get the values of the thruster, rudder, and brake from the input class
        m_Move = Input.GetAxis(verticalAxisName);
        m_Rotate = Input.GetAxis(horizontalAxisName);
        m_Fire = Input.GetButton(fireButtonName);
    }
}
