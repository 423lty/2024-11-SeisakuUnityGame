using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    [SerializeField, Header("�Œᑬ�x")]
    float minSpeed;

    [SerializeField, Header("�ő呬�x")]
    float maxSpeed;

    //--------------------------------------------------//
    //�A�j���[�V����
    //--------------------------------------------------//
    [SerializeField,Header("�U�����Ă��邩")]
    bool isAttack = false;

    [SerializeField, Header("���s")]
    bool isWalk = false;

    [SerializeField, Header("�͂���")]
    bool isRun = false;

    [SerializeField,Header("�A�j���[�^�[")]
    Animator animator;



    void Start()
    {
        //�A�j���[�V�����̐ݒ�
        SetAnimator(false, true, false);
    }

    // Update is called once per frame
    void Update()
    {
        //�U�����Ă��Ȃ�������ړ�����
        if (!isAttack)
        {
            //�ړ�
            Move();
        }

        //�Ǐ]���Ă��Ȃ��ꍇ��]���ĒT��������
        if (!isAdulation)
        {
            //���s��Ԃ�ݒ肷��
            SetAnimator(false, true, false);

            //��]
            Rotate();
        }
        //�Ǐ]�t���O�������Ă���ꍇ�̓v���C���[��Ǐ]����
        else
        {
            //�����Ă����Ԃ�ݒ肷��
            SetAnimator(true, false, false);

            //�Ǐ]
            PlayerAdulation();
        }


        DebugMode();

        //�A�j���[�^�̃Z�b�g
        SetValueAnimatation();
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
            speed *= multiplySpeed;
        }

        //�����ʒu
        var pos = new Vector3();

        pos.z += basicSpeed * speed * Time.deltaTime;

        this.transform.position += pos;
        
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
        //player��transform��ݒ�
        var player = this.targetObject.transform;

        //player�Ƃ̋������擾
        float distance = Vector3.Distance(transform.position, player.position);

        //�������擾����
        float speed = Mathf.Lerp(minSpeed, maxSpeed, distance / maxSpeed);

        //�G��player�̈ʒu�𐳋K�����Ď擾
        var direction = (player.position - transform.position).normalized;

        //�U���̃��[�V���������Ă��Ȃ�������ړ�����
        if (!isAttack)
        {
            //player�̕����Ɍ������Ĉړ�
            transform.position += direction * speed * Time.deltaTime;
        }
        //�������擾���Ă��̕����ɖ��C
        Quaternion tragetQuaternion = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation,tragetQuaternion,Time.deltaTime*5f);

    }

    /// <summary>
    /// �A�j���[�V�����̃Z�b�g
    /// </summary>
    void SetValueAnimatation()
    {
        animator.SetBool("run", isRun);
        animator.SetBool("walk", isWalk);
        animator.SetBool("attack", isAttack);

    }

    /// <summary>
    /// bool�l���g�p�����ݒ�
    /// </summary>
    /// <param name="isRun">isRun</param>
    /// <param name="isWalk">isWalk</param>
    /// <param name="isAttack">isAttack</param>
    void SetAnimator(bool isRun,bool isWalk,bool isAttack)
    {
        this.isWalk = isWalk;
        this.isRun = isRun;
        this.isAttack = isAttack;
    }

    /// <summary>
    /// �f�o�b�O���[�h
    /// </summary>
    void DebugMode()
    {
        if(Input.GetKey(KeyCode.P)) 
        {
            SetAnimator(false, false, true);
        }
    }

    /// <summary>
    /// �R���C�_�[�ɓ�������
    /// </summary>
    /// <param name="other"></param>
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