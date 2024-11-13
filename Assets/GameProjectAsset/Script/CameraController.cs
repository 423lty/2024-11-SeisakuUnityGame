using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField, Header("追従するオブジェクト")]
    GameObject compliance;

    [SerializeField, Header("回転速度")]
    float rotSpeed;

    [SerializeField, Header("補正する量")]
    Vector3 correction;

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //水平方向の移動量
        float mouseX = Input.GetAxis("Mouse X");

        //移動量をもとにして回転する前にオブジェクトの回転を取得して傘
        transform.Rotate(Vector3.up, mouseX * rotSpeed);
        //transform.Rotate(Vector3.up, mouseX * rotSpeed);

        transform.position = compliance.transform.position + correction;
    }
}
