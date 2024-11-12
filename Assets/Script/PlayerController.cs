using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Header("�ړ����x")]
    float playerMoveSpeed;

    [SerializeField, Header("�߂܂�����")]
    bool isGet = false;

    // Update is called once per frame
    void Update()
    {
        //�}�E�X�̓��͂ňړ�
        InputKey();

        if (isGet)
        {
            this.GetComponent<CapsuleCollider>().isTrigger = isGet;
            this.GetComponent<Rigidbody>().isKinematic = isGet;
        }
    }

    /// <summary>
    /// �L�[�{�[�h�̓��͂ňړ�
    /// </summary>
    void InputKey()
    {
        //�ړ��ʂɎg�p
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("weapon"))
        {
            isGet = true;
        }
    }
}
