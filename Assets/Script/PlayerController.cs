using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   [SerializeField, Header("移動速度")]
    float playerMoveSpeed;

    [SerializeField, Header("捕まったか")]
    bool isGet = false;

    [SerializeField, Header("回転速度")]
    float rotSpeed;

    [SerializeField, Header("リスポーン地点にいる時間")]
    float respornTime;

    [SerializeField, Header("リスポーン地点に入れる最大時間")]
    float MaxRespornTime;

    // Update is called once per frame
    void Update()
    {
        //マウスの入力で移動
        InputKey();

        if (isGet)
        {
            this.GetComponent<CapsuleCollider>().isTrigger = isGet;
            this.GetComponent<Rigidbody>().isKinematic = isGet;
        }

        MouseOperation();
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

    void MouseOperation(){
        //水平方向の移動量
        float mouseX = Input.GetAxis("Mouse X");

        //移動量をもとにして回転
        transform.Rotate(Vector3.up, mouseX * rotSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("weapon"))
        {
            isGet = true;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        //リスポーン地点に居続けた場合
        if (collision.gameObject.tag == "playerResporn")
        {
            //加算
            respornTime += Time.deltaTime;

            if (respornTime >= MaxRespornTime)
            {
                Debug.Log("リスポーン地点から出てください");
            }
        }
    }

}
