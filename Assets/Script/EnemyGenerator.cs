using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField,Header("��������I�u�W�F�N�g�̃v���n�u")]
    GameObject prefab;

    [SerializeField, Header("���b���Ƃɐ������邩")]
    float genarateTime;

    [SerializeField,Header("�Q�[�����X�^�[�g���Ă���̎���")]
    float genarateSpeed;

    // Update is called once per frame
    void Update()
    {
        genarateSpeed += Time.deltaTime;

        if (genarateSpeed > genarateTime)
        {
            genarateSpeed = 0;

            //��������
            var gameObject = Instantiate(prefab);

            //
            Debug.Log("��������");
        }
    }
}
