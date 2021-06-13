using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Level Level;


    public event Action start;
    public event Action<bool> levelFinish;
    public event Action<float> boundary;
    public static int LevelNo=1;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        Level.CreateLevel(LevelNo);
        Debug.Log("Level No"+LevelNo);
    }


    public void GameStart()
    {
        start?.Invoke();
    }
    public void LevelFinish(bool isComplete)
    {
        levelFinish?.Invoke(isComplete);
        if (isComplete)
        {
            LevelNo++;
            if (LevelNo > Level.gameSetting.levelSettings.Count)
            {
                LevelNo = 1;
            }
        }

    }

    public void SetBoundary(float boundary)
    { 
        this.boundary?.Invoke(boundary);
    
    }

    public int TotalEnemy = 0;
    public void EnemyDead()
    {
        TotalEnemy--;
        if (TotalEnemy <= 0)
        {
            LevelFinish(true);
        }
    }

}
