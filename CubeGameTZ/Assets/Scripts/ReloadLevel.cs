using UnityEngine.SceneManagement;
using UnityEngine;

public class ReloadLevel : MonoBehaviour
{
    public void ReloadScene()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1.0f;  
    }
 
}
