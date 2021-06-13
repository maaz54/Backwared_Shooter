using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
public class Player : MonoBehaviour
{
    public static Player instance;
    public SplineFollower splineFollower;

    public float speed = 5;
    public float speedMin = 5;
    public float speedMax = 5;
     
    public bool canPlay = false;
    private void Awake()
    {
        instance = this;
        GameManager.instance.start += GameStart;
        GameManager.instance.levelFinish += LevelComplete;
    }

    public void SetFollowSpeed(float s)
    {
        speed = s;
        speed = Mathf.Clamp(speed, speedMin, speedMax);

        splineFollower.followSpeed = speed;

    }
   
    public void LevelComplete(bool isComplete)
    {
        canPlay = false;
        splineFollower.follow = false;
    }
    public void GameStart()
    {
        canPlay = true;
        splineFollower.follow = true;

    }
}
