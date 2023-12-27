using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstScreenController : MonoBehaviour
{
    public GameObject nextScreen;
    public void PlayButton()
    {
        nextScreen.SetActive(true);
        gameObject.SetActive(false);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
