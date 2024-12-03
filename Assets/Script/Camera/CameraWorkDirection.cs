using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWorkDirection : MonoBehaviour
{
    [SerializeField, Header("���C���̃J����")]
    Camera mainCamera;

    [SerializeField, Header("�G�t�F�N�g�̃J����")]
    Camera EffectCamera;

    [SerializeField, Header("�J�����̈ړ��Ɋ|���鎞��")]
    float transitionTime;

    [SerializeField, Header("�ŏ��̃J�����̈ʒu")]
    Vector3 strCameraPos;

    [SerializeField, Header("�Ō�̃J�����̈ʒu")]
    Vector3 finCameraPos;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CameraTranstion());
    }

    IEnumerator CameraTranstion()
    {
        //�J�����I�u�W�F�N�g�̐ݒ�
        mainCamera.enabled = false;
        EffectCamera.enabled = true;

        EffectCamera.transform.position = strCameraPos;


        float elasedTime = 0f;

        while (elasedTime < transitionTime)
        {

            //�w��̈ʒu�ɂȂ�܂ňʒu���Z
            EffectCamera.transform.position = Vector3.Lerp(strCameraPos, finCameraPos, elasedTime / transitionTime);
            elasedTime += Time.deltaTime;

            yield return null;
        }

        //�J�����I�u�W�F�N�g�̐ݒ�
        mainCamera.enabled = true;
        EffectCamera.enabled = false;


    }
}
