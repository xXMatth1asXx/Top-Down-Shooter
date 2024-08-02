using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagment : MonoBehaviour
{
    private InputManager inputManager;

    private void Start()
    {
        inputManager = GameObject.FindGameObjectWithTag("Player").GetComponent<InputManager>();
    }

    private void Update()
    {
        if (inputManager.Escape())
        {
            SceneManager.LoadScene(0);
        }
    }
}
