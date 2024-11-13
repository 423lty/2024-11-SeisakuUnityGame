using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Header("移動速度")]
    float playerMoveSpeed;

    [SerializeField, Header("ダッシュスピード")]
    float dashSpeed;

    [SerializeField, Header("回転速度")]
    float rotSpeed;

    [SerializeField, Header("playerのカメラ")]
    Camera camera;

    [SerializeField, Header("制限area")]
    List<GameObject> limitArea;

    [SerializeField, Header("警告文を出す距離")]
    float warningDistance;

    [SerializeField, Header("警告文を出すテキストオブジェクト")]
    GameObject warningTextObject;

    [SerializeField, Header("捕まったか")]
    bool isGet = false;

    // Update is called once per frame
    void Update()
    {
        //キーボードの入力で移動
        InputKey();

        //マウス操作
        MouseOperation();

        Bumping();

        LimiteArea();

        DebugMode();

    }

    /// <summary>
    /// キーボードの入力で移動
    /// </summary>
    void InputKey()
    {
        //移動量に使用
        var move = new Vector3();
        var rot = new Vector3();

        //移動
        if (Input.GetKey(KeyCode.A))
        {
            move.x -= playerMoveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W))
        {
            move.z += playerMoveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            move.z -= playerMoveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            move.x += playerMoveSpeed * Time.deltaTime;
        }

        //回転
        if (Input.GetKey(KeyCode.Q))
        {
            rot.y -= rotSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.E))
        {
            rot.y += rotSpeed * Time.deltaTime;
        }
        //ダッシュ
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            playerMoveSpeed *= dashSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            playerMoveSpeed /= dashSpeed;
        }

        //移動
        transform.position += move;
        transform.eulerAngles += rot;
    }
    
    /// <summary>
    /// マウスの操作
    /// </summary>
    void MouseOperation()
    {
        //rayを飛ばしてオブジェクトを取得
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
           
            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit))
            {
                //デバックでオブジェクトをする
                Debug.Log("Hit" + raycastHit.collider.gameObject.name);
            }

        }

        //Gizmos.DrawSphere(ray.origin + ray.direction * 100f, 0.1f);
    }
    /// <summary>
    /// 当たった時の処理
    /// </summary>
    void Bumping()
    {
        if (isGet)
        {
            this.GetComponent<CapsuleCollider>().isTrigger = isGet;
            this.GetComponent<Rigidbody>().isKinematic = isGet;
        }
    }
    /// <summary>
    /// 制限されたエリアの位置
    /// </summary>
    void LimiteArea()
    {
        float minArea = 5000;
        
        //最低areaの算出
        foreach(var area in limitArea)
        {
            //areaと自身の位置
            var areaPos = area.gameObject.transform.position;
            var playPos = this.transform.position;

            //距離を計算
            var distance=Vector3.Distance(playPos, areaPos);

            //最低areaと二点間の距離を比較して
            if (minArea > distance)
            {
                minArea = distance;
            }

        }

        //一定の距離まで近づいたら警告を出す
        if (minArea < warningDistance)
        {
            warningTextObject.GetComponent<Text>().text = "エリアの限界位置まで" + (warningDistance - minArea).ToString();
        }
        else
        {

        }

        Debug.Log("エリアの限界位置まで:"+ (warningDistance - minArea));
    }

    void DebugMode()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("weapon"))
        {
            isGet = true;
        }
    }
}
