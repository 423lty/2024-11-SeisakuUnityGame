using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField, Header("�Ǐ]����I�u�W�F�N�g")]
    GameObject compliance;

    [SerializeField, Header("��]���x")]
    float rotSpeed;

    [SerializeField, Header("�␳�����")]
    Vector3 correction;

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //���������̈ړ���
        float mouseX = Input.GetAxis("Mouse X");

        //�ړ��ʂ����Ƃɂ��ĉ�]����O�ɃI�u�W�F�N�g�̉�]���擾���ĎP
        transform.Rotate(Vector3.up, mouseX * rotSpeed);
        //transform.Rotate(Vector3.up, mouseX * rotSpeed);

        transform.position = compliance.transform.position + correction;
    }
}
