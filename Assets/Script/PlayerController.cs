using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Header("移動速度")]
    float playerMoveSpeed;

    // Update is called once per frame
    void Update()
    {
        //マウスの入力で移動
        InputKey();
    }

    /// <summary>
    /// キーボードの入力で移動
    /// </summary>
    void InputKey()
    {
        //移動量に使用
        var move = new Vector3();

        if (Input.GetKey(KeyCode.A))
        {
            move.x -= playerMoveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W))
        {
            move.z += playerMoveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            move.z -= playerMoveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            move.x += playerMoveSpeed * Time.deltaTime;
        }

        transform.position += move;
    }
}
