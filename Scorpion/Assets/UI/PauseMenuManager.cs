using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{

    [SerializeField] private GameObject pauseMenu;

    void Update()
    {
        if (Input.GetKeyDown (KeyCode.Space))
        {
            if (pauseMenu.activeSelf)
            {
                pauseMenu.SetActive (false);
            }
            else
            {
                pauseMenu.SetActive (true);
            }
                
        }
    }
}
