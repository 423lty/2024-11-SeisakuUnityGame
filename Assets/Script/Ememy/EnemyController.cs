using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //--------------------------------------------------//
    //移動
    //--------------------------------------------------//
    [SerializeField, Header("移動速度")]
    float speed;

    [SerializeField, Header("移動速度倍率")]
    float multiplySpeed;

    [SerializeField, Header("移動基礎速度")]
    float basicSpeed = 1f;

    [SerializeField, Header("何秒ごとに移動速度を加算させるか")]
    float freamAddTime;

    [SerializeField, Header("生成されてからの時間")]
    float initTime;

    //--------------------------------------------------//
    //回転
    //--------------------------------------------------//
    [SerializeField, Header("回転を管理する時間")]
    float rotTime;

    [SerializeField, Header("向きの変更にかかる最大時間")]
    float ChangeRotTime = 5f;

    [SerializeField, Header("新しい回転方向")]
    Vector3 targetRotation;

    [SerializeField, Header("次の回転量")]
    Vector3 nextRotate;

    [SerializeField, Header("初期の回転状態")]
    Quaternion startRotation;

    [SerializeField, Header("最終的な回転状態")]
    Quaternion endRotation;

    [SerializeField, Header("回転の進行度（0～1)")]
    float rotationProgress = 0f;

    [SerializeField, Header("回転を開始したか")]
    bool isRotationStartFlg;

    [SerializeField, Header("回転にかける時間（秒")]
    float rotationDuration = 3f;

    //--------------------------------------------------//
    //追従
    //--------------------------------------------------//
    [SerializeField, Header("追いかける対象")]
    GameObject targetObject;

    [SerializeField, Header("追従するか")]
    bool isAdulation = false;

    [SerializeField, Header("最低速度")]
    float minSpeed;

    [SerializeField, Header("最大速度")]
    float maxSpeed;

    //--------------------------------------------------//
    //アニメーション
    //--------------------------------------------------//
    [SerializeField, Header("攻撃しているか")]
    bool isAttack = false;

    [SerializeField, Header("歩行")]
    bool isWalk = false;

    [SerializeField, Header("はしる")]
    bool isRun = false;

    [SerializeField, Header("アニメーター")]
    Animator animator;

    /// <summary>
    /// 追いかけるかのプロパティ
    /// </summary>
    public bool IsAdulation { get; set; }

    /// <summary>
    /// 攻撃するか
    /// </summary>
    public bool IsAttack { get; set; }

    // Update is called once per frame
    void Update()
    {
        //行動フラグの設定
        ActionFlgReset();

        //行動
        Action();

        //アニメータのセット
        SetAnimator();
    }

    /// <summary>
    /// 行動
    /// </summary>
    void Action()
    {
        //追従していない場合回転して探索をする
        if (!IsAdulation)
        {
            //回転
            Rotate();

            //移動
            Move();
        }
        //追従フラグが立っている場合はプレイヤーを追従する
        else
        {
            //追従
            PlayerAdulation();
        }
    }

    /// <summary>
    /// 行動に使うフラグの初期化
    /// </summary>
    void ActionFlgReset()
    {
        isRun = false;
        isWalk = false;
        IsAttack = IsAttack;
        isAdulation = IsAdulation;
    }

    /// <summary>
    /// キャラクタの移動
    /// </summary>
    void Move()
    {
        //生成されてからの時間を加算
        initTime += Time.deltaTime;

        //移動速度を加算させる制限時間を超えていたら加速する
        if (initTime > freamAddTime)
        {
            initTime = 0;
            this.speed *= multiplySpeed;
        }

        //歩いている状態にする
        isWalk = true;

        //移動速度を計算
        var speed = this.speed * basicSpeed * Time.deltaTime;

        //計算した結果を加算
        transform.position += transform.forward * speed;


    }

    /// <summary>
    /// 回転を設定
    /// </summary>
    void SetNewRotate()
    {
        rotationProgress = 0f;

        // 初期回転とターゲット回転の設定
        startRotation = this.transform.rotation;
        endRotation = Quaternion.Euler(targetRotation);
    }

    /// <summary>
    /// 回転
    /// </summary>
    void Rotate()
    {
        //フラグが立ってないばあイ
        if (!isRotationStartFlg)
        {
            //回転の時間を加算
            rotTime += Time.deltaTime;
            if (rotTime > ChangeRotTime)
            {
                //回転時間の初期化とフラグを立てる
                rotTime = 0;
                isRotationStartFlg = true;

                //設定
                SetNewRotate();
            }

        }
        //立った場合
        else
        {
            //時間経過されるまで
            if (rotationProgress < 1f)
            {
                //回転速度を計算して加算
                rotationProgress += Time.deltaTime / rotationDuration;

                //回転
                transform.rotation = Quaternion.Slerp(startRotation, endRotation, rotationProgress);
            }
            //回転完了
            else
            {
                //フラグをキル
                isRotationStartFlg = false;
                targetRotation += nextRotate;

            }
        }
    }

    /// <summary>
    /// プレイヤーの追従
    /// </summary>
    void PlayerAdulation()
    {
        isRun = true;

        //playerのtransformを設定
        var player = targetObject.transform;

        //playerとの距離を取得
        float distance = Vector3.Distance(transform.position, player.position);

        //距離を取得する
        float speed = Mathf.Lerp(minSpeed, maxSpeed, distance / maxSpeed);

        //敵とplayerの位置を正規化して取得
        var direction = (player.position - transform.position).normalized;

        //攻撃立っていない場合移動する
        if (!isAttack)
        {
            //playerの方向に向かって移動
            transform.position += direction * speed * Time.deltaTime;
        }

        //方向を取得してその方向に無垢
        Quaternion tragetQuaternion = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, tragetQuaternion, Time.deltaTime * 5f);

    }

    /// <summary>
    /// アニメーションのセット
    /// </summary>
    void SetAnimator()
    {
        animator.SetBool("run", isRun);
        animator.SetBool("walk", isWalk);
        animator.SetBool("attack", isAttack);
    }


    //当たり判定
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("wall"))
        {
            var rot = new Vector3(0, transform.eulerAngles.y * -1, 0);

            transform.eulerAngles = rot;
            
        }
    }

}
