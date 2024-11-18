using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySearch : MonoBehaviour
{

    [SerializeField,Header("追従させるオブジェクト")]
    EnemyController enemyPrefab;

    /// <summary>
    /// コライダーに入ったら
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            enemyPrefab.IsAdulation = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("player"))
        {
            enemyPrefab.IsAdulation = false;
        }
    }
}
