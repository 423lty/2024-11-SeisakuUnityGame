using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField,Header("生成するオブジェクトのプレハブ")]
    GameObject prefab;

    [SerializeField, Header("何秒ごとに生成するか")]
    float genarateTime;

    [SerializeField,Header("ゲームがスタートしてからの時間")]
    float genarateSpeed;

    [SerializeField, Header("生成されるランダム→")]
    GameObject generteRndR;

    [SerializeField, Header("生成されるランダム←")]
    GameObject generteRndL;

    [SerializeField, Header("生成される最大の高さ")]
    float generteMaxHight;

    [SerializeField, Header("生成された数")]
    int generateNum;

    [SerializeField, Header("最大生成数")]
    int maxGenerateNum;

    // Update is called once per frame
    void Update()
    {
        //生成時間を初期化
        genarateSpeed += Time.deltaTime;

        //規定の時間を経過したら生成
        if (genarateSpeed > genarateTime && generateNum < maxGenerateNum)
        {
            //生成時間を初期化する
            genarateSpeed = 0;
            generateNum++;

            //初期位置を

            var z = Random.Range(generteRndL.gameObject.transform.position.z, generteRndR.gameObject.transform.position.z);

            var pos = new Vector3(gameObject.transform.position.x, generteMaxHight, z);

            Instantiate(prefab, pos, Quaternion.identity);
        }
    }
}
