using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            collision.gameObject.SetActive(false);
            ScoreData.instance.AddScore(1);
            GameUI.instance.DisplayScore();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Time.timeScale = 0f;
            GameUI.instance.Panel.SetActive(true);
            GameUI.instance.DisplayScore();
            foreach (var obj in GameUI.instance.ObjectsToDeactivate)
            {
                obj.gameObject.SetActive(false);
            }
        }
    }
}
