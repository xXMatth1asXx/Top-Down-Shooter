using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    [Range(1, 30)]
    private float moveSpeed, rotationSpeed;

    private InputManager inputManager;  

    private Rigidbody rb;
    private Vector3 movement = Vector3.zero;

    [HideInInspector]
    public TextMeshPro ammunationText;

    [HideInInspector]
    public bool isMoving = false;


    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        inputManager = gameObject.GetComponent<InputManager>();
        ammunationText = GameObject.Find("AmmunationText").GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        movement.x = inputManager.horizontal;
        movement.z = inputManager.vertical;

        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
        //transform.position = (rb.position + movement * moveSpeed * Time.deltaTime);

        Vector3 direction = inputManager.mousePosition - rb.position;
        Debug.DrawRay(transform.position, direction, Color.red);
        direction.y = 0;


        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);


        ammunationText.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 3f);

        if (inputManager.horizontal != 0 || inputManager.vertical != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        
    }
}
