using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    private bool isExiting;
    private float timeElapsed;
    private string str;
    private int strLength;
    private int[] trialAngle = { 45, 90, -45, -90 };


    // Start is called before the first frame update
    void Start()
    {
        target.text = "S";
        message.text = "Pinch to Begin";
        numCnt = 0;
        pushCnt = 0;
        pinchCnt = 0;
        isMoving = false;
        canStart = false;
        isExiting = false;
        timeElapsed = 0.0f;
        str = "23456789ABCDEFGHJKLMNPQRSTUVWXYZ";
        strLength = str.Length;

        defPosition = transform.position;
        defRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {// 移動中
            if (transform.position.z > Mathf.Cos(angleLimit))
            {
                // 移動
                transform.RotateAround(new Vector3(0f, defPosition.y, 0f), Vector3.up, speed * Time.deltaTime);

                // 文字の更新
                timeElapsed += Time.deltaTime;
                if (timeElapsed >= timeOut)
                {
                    int index = 0;
                    string nowText = target.text;
                    string newText = target.text;
                    while (newText == nowText)
                    {
                        index = Random.Range(0, strLength);
                        newText = str.Substring(index, 1);
                    }
                    if (index < 8) numCnt += 1;
                    target.text = newText;
                    timeElapsed = 0.0f;
                }
                // Pinch Countの変更
                if (Pinch.GetStateDown(HandType))
                {
                    pinchCnt += 1;
                    Debug.Log("Pinch");
                }
            }
            else
            {// 停止
                message.text = "Pinch to Restart";
                isMoving = false;
                canStart = false;
            }
            // Push Countの変更
            if (Push.GetStateDown(HandType))
            {
                pushCnt += 1;
                Debug.Log("Push");
            }
        }
        else
        {
            if (canStart)
            {
                if (Push.GetStateDown(HandType))
                {
                    canStart = false;
                    isExiting = true;
                    message.text = "You Want to Exit?\nYes : Push, No : Pinch";
                }
                else if (Pinch.GetStateDown(HandType))
                {// 移動開始
                    isMoving = true;
                    message.text = "";
                }
            }
            else
            {
                if (isExiting)
                {
                    if (Pinch.GetStateDown(HandType))
                    {
                        message.text = "Pinch to Start\nPush to Exit";
                        canStart = true;
                        isExiting = false;
                    }
                    else if (Push.GetStateDown(HandType))
                    {
                        SceneManager.LoadScene("Title");
                    }
                }
                else if (Pinch.GetStateDown(HandType))
                {// リスタートのための初期化
                    transform.position = defPosition;
                    transform.rotation = defRotation;

                    angleLimit = trialAngle[Random.Range(0, 4)];
                    if (angleLimit < 0)
                    {
                        speed = Mathf.Abs(speed) * -1;
                    }
                    else
                    {
                        speed = Mathf.Abs(speed);
                    }

                    message.text = "Pinch to Start\n\nPush to Exit";
                    numCnt = 0;
                    pinchCnt = 0;
                    pushCnt = 0;
                    canStart = true;
                }
            }
        }
    }

}
