using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacl : MonoBehaviour
{
    [SerializeField,Header("enemy")]
    EnemyController enemyController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player")){
            enemyController.IsAttack = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("player"))
        {
            enemyController.IsAttack = false;
        }
    }

}
