using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameFieldObjectsController : MonoBehaviour
{
    public GameObject gameField;
    public GameObject pCoin;
    public GameObject coinsParent;
    public float spawnHeight = 0.75f;
    GameObject[] coins;
    public Text labelCoinsLeft;
    public Text labelTimeLeft;

    public GameObject pPlayer;
    GameObject player;
    public Canvas mainCanvas;
    public GameObject pPanelGameOver;

    public Vector3 fieldSize { get; private set; }
    public Vector3 fieldCenter { get; private set; }

    public static GameFieldObjectsController instance { get; private set; }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        Initialization();
    }

    private void Initialization()
    {
        Vector3 bounds = gameField.GetComponent<MeshRenderer>().bounds.size;
        fieldSize = new Vector3(bounds.x, 0, bounds.z);
        fieldCenter = gameField.transform.position;
        
        SpawnPlayer();
        DetermineNumberOfCoinsOnField();
    }

    private void Start()
    {
        PerformStartGameSpawn();
    }

    public void SpawnPlayer()
    {
        player = Instantiate(pPlayer, coinsParent.transform.position + new Vector3(0, 1, 0), Quaternion.identity);
    }

    public void PerformStartGameSpawn()
    {
        GameManager.instance.SetTimer(() => GameOverProcedure());
        
        GameData.instance.FillCoinData(spawnHeight, player.GetComponent<MeshRenderer>().bounds.size.x/2, fieldSize, fieldCenter);
        coins = new GameObject[GameData.instance.initNumberOfCoins];
        for (int i = 0; i < GameData.instance.initNumberOfCoins; i++)
        {
            coins[i] = Instantiate(pCoin, GameData.instance.GetCoinData(i), Quaternion.identity, coinsParent.transform);
            coins[i].GetComponent<CoinController>().SetId(i);
        }
        GameData.instance.SetCurrentNumberOfCoins();
        RefreshCoinsCounter();
    }

    float GetNumberOfPlayerScreensInFieldSpace()
    {
        RaycastHit hit;
        Ray[] rays = new Ray[4];
        Vector3[] points = new Vector3[4];
        rays[0] = Camera.main.ScreenPointToRay(new Vector3(0, 0, 0));
        rays[1] = Camera.main.ScreenPointToRay(new Vector3(Screen.width, 0, 0));
        rays[2] = Camera.main.ScreenPointToRay(new Vector3(Screen.width, Screen.height, 0));
        rays[3] = Camera.main.ScreenPointToRay(new Vector3(0, Screen.height, 0));

        for (int i = 0; i < 4; i++)
            if (Physics.Raycast(rays[i], out hit))
            {
                points[i] = hit.point;
            }
        Vector3 diagonal_1 = points[0] - points[2];
        Vector3 diagonal_2 = points[3] - points[1];

        float angle = Vector3.Angle(diagonal_1, diagonal_2);
        float areaPlayeScreen = 0.5f * diagonal_1.magnitude * diagonal_2.magnitude * Mathf.Sin(angle * Mathf.Deg2Rad);

        Vector3 boundsSize = gameField.GetComponent<MeshRenderer>().bounds.size;
        float areaFull = boundsSize.x * boundsSize.z;

        return (areaFull / areaPlayeScreen);
    }

    void DetermineNumberOfCoinsOnField()
    {
        //В среднем по одной штуке на N - экранов игрока.
        float screensPerCoin = 1;
        GameData.instance.initNumberOfCoins = Mathf.RoundToInt(GetNumberOfPlayerScreensInFieldSpace() / screensPerCoin);
    }

    public void DestroyCoinObjects()
    {
        foreach (GameObject c in coins)
            Destroy(c);
    }

    public void DestroyPlayerObject()
    {
        Destroy(player);
    }

    public void GameOverProcedure()
    {
        Instantiate(pPanelGameOver, mainCanvas.transform);
    }

    public void RefreshCoinsCounter()
    {
        labelCoinsLeft.text = "Coins left: " + GameData.instance.currentNumberOfCoins.ToString("000");
    }

    public void RefreshTimer()
    {
        double t_val = GameManager.instance.timer.GetTime();
        TimeSpan ts = TimeSpan.FromSeconds(t_val);
        labelTimeLeft.text = "Time left: " +  ts.Minutes.ToString("00") + ":" + ts.Seconds.ToString("00") + ":" + ts.Milliseconds.ToString("000");
    }
}
