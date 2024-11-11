using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    //--------------------------------------------------//
    //�ړ�
    //--------------------------------------------------//
    [SerializeField, Header("�ړ����x")]
    float speed;

    [SerializeField, Header("�ړ����x�{��")]
    float multiplySpeed;

    [SerializeField, Header("�ړ���b���x")]
    float basicSpeed = 1f;

    [SerializeField, Header("���b���ƂɈړ����x�����Z�����邩")]
    float freamAddTime;

    [SerializeField, Header("��������Ă���̎���")]
    float initTime;

    //--------------------------------------------------//
    //��]
    //--------------------------------------------------//
    [SerializeField, Header("��]���Ǘ����鎞��")]
    float rotTime;

    [SerializeField, Header("�����̕ύX�ɂ�����ő厞��")]
    float ChangeRotTime = 5f;

    [SerializeField, Header("�V������]����")]
    Vector3 targetRotation;

    [SerializeField,Header("���̉�]��")]
    Vector3 nextRotate;

    [SerializeField, Header("�����̉�]���")]
    Quaternion startRotation;

    [SerializeField, Header("�ŏI�I�ȉ�]���")]
    Quaternion endRotation;

    [SerializeField, Header("��]�̐i�s�x�i0�`1)")]
    float rotationProgress = 0f;

    [SerializeField, Header("��]���J�n������")]
    bool isRotationStartFlg;

    [SerializeField, Header("��]�ɂ����鎞�ԁi�b")]
    float rotationDuration = 3f;

    //--------------------------------------------------//
    //�Ǐ]
    //--------------------------------------------------//
    [SerializeField, Header("�ǂ�������Ώ�")]
    GameObject targetObject;

    [SerializeField, Header("�Ǐ]���邩")]
    bool isAdulation = false;

    // Update is called once per frame
    void Update()
    {
        //�ړ�
        Move();

        //�Ǐ]���Ă��Ȃ��ꍇ��]���ĒT��������
        if (!isAdulation)
        {
            //��]
            Rotate();
        }
        //�Ǐ]�t���O�������Ă���ꍇ�̓v���C���[��Ǐ]����
        else
        {
            //�Ǐ]
            PlayerAdulation();
        }
    }
    /// <summary>
    /// �L�����N�^�̈ړ�
    /// </summary>
    void Move()
    {
        //��������Ă���̎��Ԃ����Z
        initTime += Time.deltaTime;

        //�ړ����x�����Z�����鐧�����Ԃ𒴂��Ă������������
        if (initTime > freamAddTime)
        {
            initTime = 0;
            basicSpeed *= multiplySpeed;
        }

        //�����ʒu
        //var pos = new Vector3();
    }
    /// <summary>
    /// ��]
    /// </summary>
    void Rotate()
    {
        //�t���O�������ĂȂ��΂��C
        if (!isRotationStartFlg)
        {
            //��]�̎��Ԃ����Z
            rotTime += Time.deltaTime;
            if (rotTime > ChangeRotTime)
            {
                //��]���Ԃ̏������ƃt���O�𗧂Ă�
                rotTime = 0;
                isRotationStartFlg = true;

                //�ݒ�
                SetNewRotate();
            }

        }
        //�������ꍇ
        else
        {
            //���Ԍo�߂����܂�
            if (rotationProgress < 1f)
            {
                //��]���x���v�Z���ĉ��Z
                rotationProgress += Time.deltaTime / rotationDuration;

                //��]
                transform.rotation = Quaternion.Slerp(startRotation, endRotation, rotationProgress);
            }
            //��]����
            else
            {
                //�t���O���L��
                isRotationStartFlg = false;
                targetRotation += nextRotate;

            }
        }
    }
    /// <summary>
    /// ��]��ݒ�
    /// </summary>
    void SetNewRotate()
    {
        rotationProgress = 0f;

        // ������]�ƃ^�[�Q�b�g��]�̐ݒ�
        startRotation = this.transform.rotation;
        endRotation = Quaternion.Euler(targetRotation);
    }
    /// <summary>
    /// �v���C���[�̒Ǐ]
    /// </summary>
    void PlayerAdulation()
    {
        //�Ǐ]����I�u�W�F�N�g���擾
        var targetPos = targetObject.transform.position;

        //�^�[�Q�b�g�Ɍ������������v�Z
        var direction = (targetPos - transform.position).normalized;

        //�^�[�Q�b�g�Ɍ����Ĉړ�
        transform.position += direction * speed * Time.deltaTime;

        //�L�����N�^�̌������擾
        var targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            isAdulation = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("player"))
        {
            isAdulation = false;
        }
    }
}
