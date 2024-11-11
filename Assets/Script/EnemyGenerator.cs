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

    // Update is called once per frame
    void Update()
    {
        genarateSpeed += Time.deltaTime;

        if (genarateSpeed > genarateTime)
        {
            genarateSpeed = 0;

            //生成する
            var gameObject = Instantiate(prefab);

            //
            Debug.Log("生成する");
        }
    }
}
