using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{

    [SerializeField] private List<GameObject> menuItems;

    void Update()
    {
        if (Input.GetKeyDown (KeyCode.Space))
        {
            foreach (GameObject g in menuItems)
            {
                g.SetActive (!g.activeSelf);
            }
                
        }
    }
}
