using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField, Header("現在の時間")]
    static float gameTime;

    [SerializeField, Header("timeを描画するオブジェクト")]
    GameObject timeObject;

    [SerializeField, Header("Scoreを描画するオブジェクト")]
    GameObject scoreObject;

    [SerializeField, Header("現在のスコア")]
    float score;

    [SerializeField, Header("増加するスコア")]
    float addScore;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;

        score +=  addScore* Time.deltaTime;

        timeObject.GetComponent<Text>().text = "Time : " + gameTime.ToString("F1");
        scoreObject.GetComponent<Text>().text = "Score : " + score.ToString("F0");

    }
    public static float GameTime
    {
        get => gameTime;
        set => gameTime = value;
    }
}
