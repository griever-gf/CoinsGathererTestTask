using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData instance { get; private set; }
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

    Vector3[] coinData; //can be used more comlpex data type in the future, if needed

    [HideInInspector]
    public int initNumberOfCoins { get; set; }
    [HideInInspector]
    public int currentNumberOfCoins { get; private set; }
    
    void Initialization()
    {
    }

    public void SetCurrentNumberOfCoins()
    {
        currentNumberOfCoins = initNumberOfCoins;
    }

    public void FillCoinData(float spawnConstantY, float excludeCenterRadius, Vector3 fieldSize, Vector3 fieldCenter)
    {
        coinData = new Vector3[initNumberOfCoins];
        for (int i = 0; i < initNumberOfCoins; i++)
        {
            do
            {
                coinData[i] = new Vector3(fieldCenter.x + fieldSize.x * (Random.value - 0.5f), spawnConstantY, fieldCenter.z + fieldSize.z * (Random.value - 0.5f));
            }
            while ((Mathf.Abs(coinData[i].x) < excludeCenterRadius) || (Mathf.Abs(coinData[i].z) < excludeCenterRadius));
        }
    }

    public Vector3 GetCoinData(int idx)
    {
        return coinData[idx];
    }

    public void DeleteCoinData(int idx)
    {
        coinData[idx] = Vector3.zero;
        currentNumberOfCoins--;
    }

    public bool IsNoMoreCoins()
    {
        return (currentNumberOfCoins == 0);
    }
}
