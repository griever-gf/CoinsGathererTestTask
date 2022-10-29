using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float PlayerSpeed = 10f;
    float max_x, max_z, min_x, min_z;

    void Awake()
    {
        Camera.main.GetComponent<CameraFollow>().SetFollowObject(transform);
    }

    void Start()
    {
        Vector3 playersBounds = gameObject.GetComponent<MeshRenderer>().bounds.size;
        max_x = GameFieldObjectsController.instance.fieldCenter.x + GameFieldObjectsController.instance.fieldSize.x / 2 - playersBounds.x/2;
        min_x = GameFieldObjectsController.instance.fieldCenter.x - GameFieldObjectsController.instance.fieldSize.x / 2 + playersBounds.x / 2;
        max_z = GameFieldObjectsController.instance.fieldCenter.z + GameFieldObjectsController.instance.fieldSize.z / 2 - playersBounds.z / 2;
        min_z = GameFieldObjectsController.instance.fieldCenter.z - GameFieldObjectsController.instance.fieldSize.z / 2 + playersBounds.z / 2;
    }

    void Update()
    {
        if (!GameManager.instance.IsGameOver())
        {
            Vector3 Movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            //limit movement by field size
            gameObject.transform.position = new Vector3(Mathf.Clamp(gameObject.transform.position.x + Movement.x * PlayerSpeed * Time.deltaTime, min_x, max_x),
                                                        gameObject.transform.position.y,
                                                        Mathf.Clamp(gameObject.transform.position.z + Movement.z * PlayerSpeed * Time.deltaTime, min_z, max_z));
        }
    }
}
