using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonRestartGame : MonoBehaviour
{
    public void OnClickRestartGame()
    {
        GameFieldObjectsController.instance.DestroyPlayerObject();
        GameFieldObjectsController.instance.DestroyCoinObjects();
        GameFieldObjectsController.instance.SpawnPlayer();
        GameFieldObjectsController.instance.PerformStartGameSpawn();
        Destroy(gameObject);
    }
}
