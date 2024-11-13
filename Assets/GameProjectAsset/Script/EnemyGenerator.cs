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

    [SerializeField, Header("�G���~���Ă��鍂��")]
    float GenerateY;

    [SerializeField, Header("�ő吶����")]
    int maxGenerate = 10;

    [SerializeField,Header("����������")]
    int generateNum;

    [SerializeField, Header("X���W�̍Œ�ʒu")]
    GameObject spawnMinAreaX;

    [SerializeField, Header("X���W�̍ő�ʒu")]
    GameObject spawnMaxAreaX;

    [SerializeField, Header("Z���W�̍Œ�ʒu")]
    GameObject spawnMinAreaZ;

    [SerializeField, Header("Z���W�̍ő�ʒu")]
    GameObject spawnMaxAreaZ;


    // Update is called once per frame
    void Update()
    {
        if (generateNum < maxGenerate)
        {
            genarateSpeed += Time.deltaTime;

            if (genarateSpeed > genarateTime)
            {
                genarateSpeed = 0;

                //��������
                Generate();


                //
                Debug.Log("��������");
            }
        }
    }
    void Generate()
    {
        float rndX = Random.Range(spawnMinAreaX.transform.position.x, spawnMaxAreaX.transform.position.x);
        float rndZ = Random.Range(spawnMinAreaZ.transform.position.z, spawnMaxAreaZ.transform.position.z);

        var gameObject = new Vector3(rndX, GenerateY, rndZ);

        //�����_���Ȉʒu�Ő���
        Instantiate(prefab, gameObject, Quaternion.identity);

        generateNum++;

    }
}
