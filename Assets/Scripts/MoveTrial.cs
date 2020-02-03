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
    public string mark;
    public int markCnt;
    public int pushCnt;
    public int pinchCnt;
    public float timeOut;

    private bool isPreparing;
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
        target.text = "+";
        message.text = "Pinch to Begin";
        markCnt = 0;
        pushCnt = 0;
        pinchCnt = 0;
        isPreparing = false;
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
            if (transform.position.z > Mathf.Cos(angleLimit / 180 * Mathf.PI))
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
                    if (newText == mark) markCnt += 1;
                    target.text = newText;
                    timeElapsed = 0.0f;
                }
                // Pinch Countの変更
                if (Pinch.GetStateDown(HandType))
                {
                    pinchCnt += 1;
                    Debug.Log("Pinch");
                }
                // Push Countの変更
                if (Push.GetStateDown(HandType))
                {
                    pushCnt += 1;
                    Debug.Log("Push");
                }
            }
            else
            {// 停止
                message.text = $"Push:{pushCnt}, Mark:{markCnt}\n\nFace forward\nPinch to Restart";
                target.text = "+";
                isMoving = false;
                canStart = false;
                isPreparing = false;
            }
        }
        else
        {
            if (canStart)
            {
                if (isPreparing)
                {
                    if (Pinch.GetStateDown(HandType))
                    {
                        isMoving = true;
                        message.text = "";
                    }
                }
                else
                {// message.text = "Pinch to Start\nPush to Exit";
                    if (Push.GetStateDown(HandType))
                    {
                        canStart = false;
                        isExiting = true;
                        message.text = "You Want to Exit?\n\nYes : Push, No : Pinch";
                    }
                    else if (Pinch.GetStateDown(HandType))
                    {
                        isPreparing = true;
                        int index = Random.Range(0, strLength);
                        mark = str.Substring(index, 1);
                        message.text = $"When \"{mark}\" is Displayed, \nPush to Count\n\nPinch to Start";
                    }
                }
            }
            else
            {
                if (isExiting)
                {// message.text = "You Want to Exit?\nYes : Push, No : Pinch"
                    if (Pinch.GetStateDown(HandType))
                    {
                        message.text = "Pinch to Go Next\n\nPush to Exit";
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

                    message.text = "Pinch to Go Next\n\nPush to Exit";
                    markCnt = 0;
                    pinchCnt = 0;
                    pushCnt = 0;
                    canStart = true;
                }
            }
        }
    }

}
