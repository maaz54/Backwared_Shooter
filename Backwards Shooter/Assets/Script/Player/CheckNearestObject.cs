using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckNearestObject : MonoBehaviour
{
    public PlayerMovement PlayerMovement;
    bool canPlay=false;
    private void Start()
    {
        GameManager.instance.start += GameStart;
        GameManager.instance.levelFinish += LevelComplete;
    }
    void GameStart()
    {
        canPlay = true;
    }
    void LevelComplete(bool iscomplte)
    {
        canPlay = false;
    }


    private void OnTriggerStay(Collider col)
    {
        if (canPlay)
        {
            if (col.gameObject.CompareTag("enemy"))
            {
                PlayerMovement.Shoot(col.gameObject);
            }
        }
    }
}
