using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    [SerializeField, Header("現在の時間")]
    static float gameTime;

    [SerializeField,Header("timeを描画するオブジェクト")]
    GameObject timeObject;

    [SerializeField,Header("アイテム一覧")]
    List<GameObject> items;

    

    void Start()
    {
        //マウスの初期設定
        //非表示
        Cursor.visible = false;

        //カーソルを画面中央に固定
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;

        timeObject.GetComponent<Text>().text = "Time : " + gameTime.ToString("F1");

    }
    public static float GameTime
    {
        get=> gameTime;
        set => gameTime = value;
    }
}
