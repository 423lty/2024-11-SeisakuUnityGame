using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContorller : MonoBehaviour
{
    [SerializeField, Header("追従するオブジェクト")]
    GameObject compliance;

    [SerializeField,Header("補正する量")]
    Vector3 correction;

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles=compliance.transform.eulerAngles;
        transform.position = compliance.transform.position + correction;
    }
}
