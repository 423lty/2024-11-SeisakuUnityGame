using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Header("�ړ����x")]
    float playerMoveSpeed;

    [SerializeField, Header("�_�b�V���X�s�[�h")]
    float dashSpeed;

    [SerializeField, Header("��]���x")]
    float rotSpeed;

    [SerializeField, Header("player�̃J����")]
    Camera camera;

    [SerializeField, Header("����area")]
    List<GameObject> limitArea;

    [SerializeField, Header("�x�������o������")]
    float warningDistance;

    [SerializeField, Header("�x�������o���e�L�X�g�I�u�W�F�N�g")]
    GameObject warningTextObject;

    [SerializeField, Header("�߂܂�����")]
    bool isGet = false;

    // Update is called once per frame
    void Update()
    {
        //�L�[�{�[�h�̓��͂ňړ�
        InputKey();

        //�}�E�X����
        MouseOperation();

        Bumping();

        LimiteArea();

        DebugMode();

    }

    /// <summary>
    /// �L�[�{�[�h�̓��͂ňړ�
    /// </summary>
    void InputKey()
    {
        //�ړ��ʂɎg�p
        var move = new Vector3();
        var rot = new Vector3();

        //�ړ�
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

        //��]
        if (Input.GetKey(KeyCode.Q))
        {
            rot.y -= rotSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.E))
        {
            rot.y += rotSpeed * Time.deltaTime;
        }
        //�_�b�V��
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            playerMoveSpeed *= dashSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            playerMoveSpeed /= dashSpeed;
        }

        //�ړ�
        transform.position += move;
        transform.eulerAngles += rot;
    }
    
    /// <summary>
    /// �}�E�X�̑���
    /// </summary>
    void MouseOperation()
    {
        //ray���΂��ăI�u�W�F�N�g���擾
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
           
            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit))
            {
                //�f�o�b�N�ŃI�u�W�F�N�g������
                Debug.Log("Hit" + raycastHit.collider.gameObject.name);
            }

        }

        //Gizmos.DrawSphere(ray.origin + ray.direction * 100f, 0.1f);
    }
    /// <summary>
    /// �����������̏���
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
    /// �������ꂽ�G���A�̈ʒu
    /// </summary>
    void LimiteArea()
    {
        float minArea = 5000;
        
        //�Œ�area�̎Z�o
        foreach(var area in limitArea)
        {
            //area�Ǝ��g�̈ʒu
            var areaPos = area.gameObject.transform.position;
            var playPos = this.transform.position;

            //�������v�Z
            var distance=Vector3.Distance(playPos, areaPos);

            //�Œ�area�Ɠ�_�Ԃ̋������r����
            if (minArea > distance)
            {
                minArea = distance;
            }

        }

        //���̋����܂ŋ߂Â�����x�����o��
        if (minArea < warningDistance)
        {
            warningTextObject.GetComponent<Text>().text = "�G���A�̌��E�ʒu�܂�" + (warningDistance - minArea).ToString();
        }
        else
        {

        }

        Debug.Log("�G���A�̌��E�ʒu�܂�:"+ (warningDistance - minArea));
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
