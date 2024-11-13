using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    [SerializeField, Header("���݂̎���")]
    static float gameTime;

    [SerializeField,Header("time��`�悷��I�u�W�F�N�g")]
    GameObject timeObject;

    [SerializeField,Header("�A�C�e���ꗗ")]
    List<GameObject> items;

    

    void Start()
    {
        //�}�E�X�̏����ݒ�
        //��\��
        Cursor.visible = false;

        //�J�[�\������ʒ����ɌŒ�
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;

        timeObject.GetComponent<Text>().text = "Time : " + gameTime.ToString("F1");

    }
    public static float GameTime
    {
        get=> gameTime;
        set => gameTime = value;
    }
}
