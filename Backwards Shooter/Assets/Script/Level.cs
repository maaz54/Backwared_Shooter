using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
public class Level : MonoBehaviour
{
    public GameSetting gameSetting;


    public GameObject[] hurdles;
    public GameObject finishLine;
    public GameObject track;
    public Transform startPoint;
    public Transform finsihPoint;


    public SplineComputer splineComputer;
    public EnemyHolder enemyHolder;
    public Enemy enemy;


    public void CreateLevel(int levelNo)
    {
        PlayerSetting(levelNo);
        CreateHurdles(levelNo);
        CreateEnemies(levelNo);
        TrackSize(gameSetting.levelSettings[levelNo - 1].trackLenght, gameSetting.levelSettings[levelNo - 1].trackWidth, levelNo);
    }


    void PlayerSetting(int levelNo)
    {
        Player.instance.speedMax = gameSetting.levelSettings[levelNo - 1].playerSpeedMax;
        Player.instance.speedMin = gameSetting.levelSettings[levelNo - 1].playerSpeedMin;
    }

    void CreateHurdles(int levelNo)
    {
        for (int i = 0; i < gameSetting.levelSettings[levelNo - 1].hurdleSettings.Count; i++)
        {
            GameObject hurdle = Instantiate(hurdles[gameSetting.levelSettings[levelNo - 1].hurdleSettings[i].hurdleIndex]);
            hurdle.transform.position = gameSetting.levelSettings[levelNo - 1].hurdleSettings[i].hurdlePos;
            hurdle.transform.parent = levelThings;
            hurdle.name = "Hurdle" + i;
        }

    }
    void TrackSize(float trackLenght, float trackwidth,int levelNo)
    {
        Vector3 trackPos = new Vector3(0, -1, 0);
        Vector3 trackScale = new Vector3(10, 1, 1);
        trackScale.z = trackLenght;
        trackScale.x = trackwidth;
        trackPos.z = (trackLenght / 2) - 1;
        track.transform.localScale = trackScale;
        track.transform.position = trackPos;
        GameObject finish = Instantiate(finishLine);
        finish.transform.position = new Vector3(finsihPoint.position.x, 0, finsihPoint.position.z - 1);
        finish.transform.parent = levelThings;


        SplinePoint splinePointstart = new SplinePoint();
        splinePointstart.position = startPoint.position;

        SplinePoint splinePointEndPoint = new SplinePoint();
        splinePointstart.position = finsihPoint.position;

        splineComputer.SetPoint(1, splinePointstart);
        splineComputer.SetPoint(2, splinePointEndPoint);

        StartCoroutine(InvokeSetBoundary(trackwidth));

    }

    IEnumerator InvokeSetBoundary(float trackWidth)
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        GameManager.instance.SetBoundary(trackWidth);
    }

    void DeleteLevel()
    {
        
            for (int i = 0; i < levelThings.childCount; i++)
            {
                Destroy(levelThings.GetChild(i).gameObject);
            }
            //for (int i = 0; i < enemyHolder.transform.childCount; i++)
            //{
            //    Destroy(enemyHolder.transform.GetChild(i).gameObject);
            //}
        
    }


    public Transform[] EnemiesPositions;
    void CreateEnemies(int levelNo)
    {

       
        int posZIndex = 0;// if pos enimespositon 1st slot is finish it will restart enemies position and with z -1
        int posIndex = 0;
        EnemyHolder eh = Instantiate(enemyHolder);

        EnemiesPositions = eh.EnemiesPositions;
        for (int i = 0; i < gameSetting.levelSettings[levelNo - 1].enemyCount; i++)
        {
            if (posIndex >= EnemiesPositions.Length)
            {
                posIndex = 0;
                posZIndex++;
            }
            Enemy en = Instantiate(enemy);
            en.Xspeed = gameSetting.levelSettings[levelNo - 1].enemySpeedX;
            en.Zspeed = gameSetting.levelSettings[levelNo - 1].enemySpeed;
            en.transform.parent = eh.transform;
            Vector3 pos = EnemiesPositions[posIndex].position;
            pos.z = pos.z - (posZIndex+.5f);

            en.transform.position = pos;
            posIndex++;
        }
        GameManager.instance.TotalEnemy = gameSetting.levelSettings[levelNo - 1].enemyCount;
        eh.transform.parent = levelThings;
    }





    public Transform levelThings;


#if UNITY_EDITOR
    public int levelNo;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            DeleteLevel();
            CreateLevel(levelNo);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            DeleteLevel();
        }
    }
#endif
}
