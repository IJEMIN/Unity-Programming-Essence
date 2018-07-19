using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    public string verticalAxisName = "Vertical"; //The name of the thruster axis
    public string horizontalAxisName = "Horizontal"; //The name of the rudder axis
    public string fireButtonName = "Fire1"; //The name of the brake button

    public float move { get; private set; }

    public float rotate { get; private set; }

    public bool fire { get; private set; }

    void Update()
    {
        if (GameManager.instance.isGameover)
        {
            return;
        }


        move = Input.GetAxis(verticalAxisName);
        rotate = Input.GetAxis(horizontalAxisName);
        fire = Input.GetButton(fireButtonName);
    }
}