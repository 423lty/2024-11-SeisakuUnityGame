using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Header("移動速度")]
    float playerMoveSpeed;

    [SerializeField, Header("捕まったか")]
    bool isGet = false;

    [SerializeField, Header("回転速度")]
    float rotSpeed;

    [SerializeField, Header("体力")]
    float energy;

    [SerializeField, Header("最大体力")]
    float maxEnergy;

    [SerializeField, Header("体力減少量")]
    float reduceEnergy;

    [SerializeField, Header("体力回復量")]
    float healEnergy;

    [SerializeField, Header("体力管理画像")]
    GameObject energyImage; 

    [SerializeField, Header("移動速度アップ補正")]
    float moveSpeedUp;

    [SerializeField, Header("リスポーン地点にいる時間")]
    float respornTime;

    [SerializeField, Header("リスポーン地点に入れる最大時間")]
    float MaxRespornTime;

    [SerializeField, Header("リスポーン地点にいるかどうか")]
    bool isStayResporn;

    [SerializeField, Header("リスポーンに居続けたときの体力減少")]
    float penalty;

    [SerializeField, Header("タイトルにイルカ")]
    bool isTitle;


    // Update is called once per frame
    void Update()
    {
        if (!isTitle)
        {
            //移動等の処理
            Action();

            //判定処理
            TriggerProcess();

            //体力
            UpdateEnergy();
        }
    }

    /// <summary>
    /// 行動
    /// </summary>
    void Action()
    {
        //マウスの入力で移動
        InputKey();

        //マウス処理
        MouseOperation();

    }

    /// <summary>
    /// キーボードの入力で移動
    /// </summary>
    void InputKey()
    {

        //水平方向
        float horizontal = Input.GetAxis("Horizontal");

        //縦方向
        float vertical = Input.GetAxis("Vertical");

        //移動量を計算
        Vector3 moveDirec = (transform.forward * vertical + transform.right * horizontal).normalized;

        //移動速度
        var speedUp = SpeedCorrection();

        //移動量
        var speed = moveDirec * playerMoveSpeed * Time.deltaTime * speedUp;

        //移動
        transform.Translate(speed, Space.World);

        //向きの補正
        if (horizontal != 0 || vertical != 0)
        {
            Quaternion target = Quaternion.LookRotation(moveDirec);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target, rotSpeed * Time.deltaTime);
        }
    
    }

    /// <summary>
    /// シフトを押した時移動速度を上昇させる
    /// </summary>
    /// <returns></returns>
    float SpeedCorrection()
    {
        //体力が0以上ならダッシュできる
        if (energy > 0)
        {
            //押している間
            if (Input.GetKey(KeyCode.LeftShift))
            {
                //体力を減少させる
                energy -= reduceEnergy;

                //移動速度をあげる
                return moveSpeedUp;
            }
        }
        return 1;
    }

    /// <summary>
    /// マウス操作
    /// </summary>
    void MouseOperation()
    {
        //水平方向の移動量
        float mouseX = Input.GetAxis("Mouse X");

        //移動量をもとにして回転
        transform.Rotate(Vector3.up, mouseX * rotSpeed);
    }

    /// <summary>
    /// あたり判定処理s
    /// </summary>
    void TriggerProcess()
    {
        //捕まった時
        if (isGet)
        {
            Time.timeScale = 0;
        }

        //スポーン地点    
        if (isStayResporn)
        {
            respornTime += Time.deltaTime;

            if (respornTime >= MaxRespornTime)
            {
                Debug.Log("リスポーン地点から出てください");
                if (energy >= 0)
                    energy -= penalty;
            }
        }
    }

    /// <summary>
    /// エネル儀―の更新
    /// </summary>
    void UpdateEnergy()
    {
        //エネルギーが最大ではないかつLShiftを押していないとき
        if (energy < maxEnergy && !Input.GetKey(KeyCode.LeftShift))
        {
            energy += healEnergy;
        }

        //体力の表維持
        Debug.Log("現在の体力" + energy.ToString("F1"));

        //体力管理
        var energyImage = energy / maxEnergy;
        this.energyImage.GetComponent<Image>().fillAmount = energyImage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("weapon"))
        {
            isGet = true;
        }
        //リスポーン地点に居続けた場合
        if (other.gameObject.CompareTag("playerResporn"))
        {
            //加算
            isStayResporn = true;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        //リスポーン地点から出た場合
        if (other.gameObject.CompareTag("playerResporn"))
        {
            //加算
            isStayResporn = false;
            respornTime = 0f;

        }
    }

}
