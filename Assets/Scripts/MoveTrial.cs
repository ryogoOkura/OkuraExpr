using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Valve.VR;


public class MoveTrial : MonoBehaviour
{
    public float speed;
    public float radius = 2;
    public float angleLimit;

    public SteamVR_Input_Sources HandType;
    public SteamVR_Action_Boolean Pinch;
    public SteamVR_Action_Boolean Push;

    private Vector3 defPosition;
    private Quaternion defRotation;

    [SerializeField] private TextMeshPro target;
    [SerializeField] private TextMeshProUGUI message;
    public int numCnt;
    public int pushCnt;
    public int pinchCnt;
    public float timeOut;

    private bool isMoving;
    private bool canStart;
    private float timeElapsed;
    private string str;
    private int strLength;

    // Start is called before the first frame update
    void Start()
    {
        target.text = "S";
        numCnt = 0;
        isMoving = false;
        canStart = true;
        timeElapsed = 0f;
        str = "23456789ABCDEFGHJKLMNPQRSTUVWXYZ";
        strLength = str.Length;
        defPosition = transform.position;
        defRotation = transform.rotation;
        if (Random.Range(-1, 1) < 0) speed *= -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            if (transform.position.z > Mathf.Cos(angleLimit))
            {
                // 移動
                transform.RotateAround(new Vector3(0f, defPosition.y, 0f), Vector3.up, speed * Time.deltaTime);

                // 文字の更新
                timeElapsed += Time.deltaTime;
                if (timeElapsed >= timeOut)
                {
                    int index = Random.Range(0, strLength);
                    if (index < 8) numCnt += 1;
                    target.text = str.Substring(index, 1);
                    timeElapsed = 0.0f;
                }
                // countの変更
                if (Pinch.GetStateDown(HandType))
                {
                    pinchCnt += 1;
                    Debug.Log("Pinch");
                }
                if (Push.GetStateDown(HandType))
                {
                    pushCnt += 1;
                    Debug.Log("Push");
                }
            }
            else
            {// 停止
                message.text = "Push to Restart";
                isMoving = false;
                canStart = false;
            }
        }
        else
        {
            if (canStart)
            {
                if (Push.GetStateDown(HandType))
                {// 移動開始
                    isMoving = true;
                    message.text = "";
                }
            }
            else
            {
                if (Push.GetStateDown(HandType))
                {// リスタートのための初期化
                    transform.position = defPosition;
                    transform.rotation = defRotation;
                    if (Random.Range(-1, 1) < 0) speed *= -1;
                    canStart = true;
                    message.text = "Push to Start";
                    numCnt = 0;
                    pinchCnt = 0;
                    pushCnt = 0;
                }
            }
        }
    }

}
