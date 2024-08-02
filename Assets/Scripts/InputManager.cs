using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public enum input
    {
        keyboard,
        mobile
    }
    public input InputController;

    [HideInInspector] public float horizontal;
    [HideInInspector] public float vertical;
    [HideInInspector] public Vector3 mousePosition;

    private MousePointer mousePointer;

    private void Start()
    {
        mousePointer = GameObject.Find("GameManager").GetComponent<MousePointer>();
    }

    private void Update()
    {
        switch (InputController)
        {
            case input.keyboard:
                Keyboard();
                break;
            case input.mobile:
                Mobile();
                break;
        }
    }



    private void Keyboard()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        mousePosition = mousePointer.pointer.position;
    }

    public bool LeftClick()
    {
        if (Input.GetButton("Fire1"))
            return true;
        else
            return false;
    }

    public bool R()
    {
        if (Input.GetKeyDown(KeyCode.R))
            return true;
        else
            return false;
    }

    public bool Escape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            return true;
        else
            return false;
    }

    private void Mobile()
    {
    }
}
