using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public GameObject panel_Menu;
    public GameObject panel_GamePlay;
    public GameObject panel_LevelComplete;
    public GameObject panel_LevelFail;


    public Text counterText;

    public Button button_NextLevel;
    public Button button_StartGame;
    public Button button_RestartLevel;

    void Start()
    {

        button_NextLevel.onClick.AddListener(() => { NextLevel(); });
        button_StartGame.onClick.AddListener(() => {/* StartGame(); */ StartCoroutine(StartCounter()); });
        button_RestartLevel.onClick.AddListener(() => { RestartLevelLevel(); });
        GameManager.instance.levelFinish += LevelComplete;

    }


    void LevelComplete(bool isComplete)
    {
        if (isComplete)
        {
            panel_LevelComplete.SetActive(true);
            panel_LevelFail.SetActive(false);
        }
        else
        {
            panel_LevelComplete.SetActive(false);
            panel_LevelFail.SetActive(true);
        }
        panel_Menu.SetActive(false);
        panel_GamePlay.SetActive(false);
    }

    void NextLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }


    IEnumerator StartCounter()
    {
        panel_Menu.SetActive(false);
        panel_GamePlay.SetActive(true);
        panel_LevelComplete.SetActive(false);
        panel_LevelFail.SetActive(false);
        counterText.gameObject.SetActive(true);
        for (int i = 3; i > 0; --i)
        {
            counterText.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        counterText.gameObject.SetActive(false);
        StartGame();
    }

    void StartGame()
    {
        //panel_Menu.SetActive(false);
        //panel_GamePlay.SetActive(true);
        //panel_LevelComplete.SetActive(false);
        //panel_LevelFail.SetActive(false);
        GameManager.instance.GameStart();
    }
    void RestartLevelLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }


}
