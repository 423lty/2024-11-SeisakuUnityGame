using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySearch : MonoBehaviour
{

    [SerializeField,Header("�Ǐ]������I�u�W�F�N�g")]
    EnemyController enemyPrefab;

    /// <summary>
    /// �R���C�_�[�ɓ�������
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
