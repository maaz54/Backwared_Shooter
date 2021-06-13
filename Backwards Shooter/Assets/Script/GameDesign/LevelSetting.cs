using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelSetting
{

    public float trackLenght;
    public float trackWidth=10;
    public float enemySpeed;
    public float enemySpeedX;
    public int enemyCount;


    public float playerSpeedMin;
    public float playerSpeedMax;
    public float playerSpeedX;
    public List<hurdleSetting> hurdleSettings;

}



    [System.Serializable]
public class hurdleSetting
{
    /// <summary>
    /// if we use different types of hurdles we call it with there index
    /// </summary>
    public int hurdleIndex;
    public Vector3 hurdlePos;
}

