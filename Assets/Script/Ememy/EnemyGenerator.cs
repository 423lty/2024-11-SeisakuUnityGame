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
        //生成時間を初期化
        genarateSpeed += Time.deltaTime;

        //規定の時間を経過したら生成
        if (genarateSpeed > genarateTime)
        {
            //生成時間を初期化する
            genarateSpeed = 0;

            //初期位置を
            var pos = this.transform.position;

            Instantiate(prefab, pos, Quaternion.identity);
        }
    }
}
