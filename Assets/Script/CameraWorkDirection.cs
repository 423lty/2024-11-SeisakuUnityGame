using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWorkDirection : MonoBehaviour
{
    [SerializeField, Header("メインのカメラ")]
    Camera mainCamera;

    [SerializeField, Header("エフェクトのカメラ")]
    Camera EffectCamera;

    [SerializeField, Header("カメラの移動に掛ける時間")]
    float transitionTime;

    [SerializeField, Header("最初のカメラの位置")]
    Vector3 strCameraPos;

    [SerializeField, Header("最後のカメラの位置")]
    Vector3 finCameraPos;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CameraTranstion());
    }

    IEnumerator CameraTranstion()
    {
        //カメラオブジェクトの設定
        mainCamera.enabled = false;
        EffectCamera.enabled = true;

        EffectCamera.transform.position = strCameraPos;


        float elasedTime = 0f;

        while (elasedTime < transitionTime)
        {

            //指定の位置になるまで位置加算
            EffectCamera.transform.position = Vector3.Lerp(strCameraPos, finCameraPos, elasedTime / transitionTime);
            elasedTime += Time.deltaTime;

            yield return null;
        }

        //カメラオブジェクトの設定
        mainCamera.enabled = true;
        EffectCamera.enabled = false;


    }
}
