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

    [SerializeField, Header("敵が降ってくる高さ")]
    float GenerateY;

    [SerializeField, Header("最大生成数")]
    int maxGenerate = 10;

    [SerializeField,Header("生成した数")]
    int generateNum;

    [SerializeField, Header("X座標の最低位置")]
    GameObject spawnMinAreaX;

    [SerializeField, Header("X座標の最大位置")]
    GameObject spawnMaxAreaX;

    [SerializeField, Header("Z座標の最低位置")]
    GameObject spawnMinAreaZ;

    [SerializeField, Header("Z座標の最大位置")]
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

                //生成する
                Generate();


                //
                Debug.Log("生成する");
            }
        }
    }
    void Generate()
    {
        float rndX = Random.Range(spawnMinAreaX.transform.position.x, spawnMaxAreaX.transform.position.x);
        float rndZ = Random.Range(spawnMinAreaZ.transform.position.z, spawnMaxAreaZ.transform.position.z);

        var gameObject = new Vector3(rndX, GenerateY, rndZ);

        //ランダムな位置で生成
        Instantiate(prefab, gameObject, Quaternion.identity);

        generateNum++;

    }
}
