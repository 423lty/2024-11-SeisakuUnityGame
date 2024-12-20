using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class GameTitleController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField, Header("normal背景")]
    GameObject normalBackground;

    [SerializeField, Header("hell背景")]
    GameObject hellBackground;

    [SerializeField, Header("normal")]
    GameObject normalObject;

    [SerializeField, Header("hell")]
    GameObject hellObject;

    [SerializeField, Header("title")]
    GameObject titleObject;

    [SerializeField, Header("カメラ")]
    Camera camera;

    /// -----------モードの変更 -----------///

    public void SelectNormal()
    {
        SceneManager.LoadScene("normal");
    }
    public void SelectHell()
    {
        SceneManager.LoadScene("hell");
    }

    /// -----------ホバーした時 -----------///
    public void NormalHoverTitle()
    {
        normalBackground.SetActive(!normalBackground.activeSelf);
    }
    public void HellHoverTitle()
    {
        hellBackground.SetActive(!hellBackground.activeSelf);
    }

    private void Update()
    {
        if (camera != null && camera.enabled)
        {
            normalObject.SetActive(true);
            hellObject.SetActive(true);
            titleObject.SetActive(true);
        }
    }

}
