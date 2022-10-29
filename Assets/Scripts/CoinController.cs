using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    int id = -1;

    public void SetId(int val)
    {
        id = val;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameData.instance.DeleteCoinData(id);
        GameFieldObjectsController.instance.RefreshCoinsCounter();
        if (GameManager.instance.IsGameOver())
            GameFieldObjectsController.instance.GameOverProcedure();
        Destroy(gameObject);
    }
}
