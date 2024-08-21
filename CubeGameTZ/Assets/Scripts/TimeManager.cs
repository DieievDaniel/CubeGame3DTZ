using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private bool isPaused = false; 

    public void TogglePause()
    {
        if (isPaused)
        {
            ReturnGame(); 
        }
        else
        {
            PauseGame(); 
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true; 
    }

    public void ReturnGame()
    {
        Time.timeScale = 1f;
        isPaused = false; 
    }
}
