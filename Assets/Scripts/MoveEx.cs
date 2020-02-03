using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Valve.VR;


public class MoveEx : MonoBehaviour
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
    public static string mark;
    public static int markCnt;
    public static int pinchCnt;
    public static int pushCnt;
    public float timeOut;

    public static bool isPreparing;
    public static bool isMoving;
    public static bool canStart;
    private bool isExiting;
    private bool finishNonMoving;
    private float timeElapsed;
    private float timeSum;
    private string str;
    private int strLength;
    private int[] trialAngle = { 45, 90, -45, -90 };
    private List<int> notFinishTrial = new List<int> { 0, 1, 2, 3 };
    private int trialNum;
    private int exprNum;

    // Start is called before the first frame update
    void Start()
    {
        target.text = "+";
        message.text = "Pinch to Begin";
        markCnt = 0;
        pinchCnt = 0;
        pushCnt = 0;
        isPreparing = false;
        isMoving = false;
        canStart = false;
        isExiting = false;
        finishNonMoving = false;
        timeElapsed = 0.0f;
        timeSum = 0.0f;
        str = "23456789ABCDEFGHJKLMNPQRSTUVWXYZ";
        strLength = str.Length;
        trialNum = -1;

        defPosition = transform.position;
        defRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {// 移動中
            if (finishNonMoving)
            {
                if (timeSum < 18)
                {
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
                        timeSum += timeElapsed;
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
                {
                    message.text = $"Push:{pushCnt}, Mark:{markCnt}\n\nFace forward\nPinch to Restart";
                    target.text = "+";
                    isMoving = false;
                    canStart = false;
                    isPreparing = false;
                }
            }
            else
            {
                if (transform.position.z > radius * Mathf.Cos(angleLimit / 180 * Mathf.PI))
                {
                    // 原点を中心に、毎秒speed°で回転移動
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
                {
                    message.text = $"Push:{pushCnt}, Mark:{markCnt}\n\nFace forward\nPinch to Restart";
                    target.text = "+";
                    isMoving = false;
                    canStart = false;
                    isPreparing = false;
                }
            }
        }
        else
        {
            if (canStart)
            {
                if (isPreparing)
                {
                    if (Pinch.GetStateDown(HandType))
                    {// 移動開始 連動して記録開始
                        isMoving = true;
                        // message.text = $"expr{trialNum}_angle{trialAngle[exprNum]}";
                        message.text = "";
                    }
                }
                else
                {// message.text = "Pinch to Go Next\nPush to Exit";
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
            {// !canStart
                if (isExiting)
                {// message.text = "You Want to Exit?\n\nYes : Push, No : Pinch";
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
                else if (Pinch.GetStateDown(HandType) && DrainData.isRecording == false)
                {// リスタートのための初期化
                    transform.position = defPosition;
                    transform.rotation = defRotation;

                    if ((notFinishTrial.Count == 0) && finishNonMoving)
                    {// 実験終了
                        message.text = "Finish\n\nPinch & Push to Exit";
                        if (Push.GetStateDown(HandType))
                        {
                            SceneManager.LoadScene("Title");
                        }
                    }
                    else
                    {// 実験の種類決定
                        trialNum += 1;
                        if (notFinishTrial.Count == 0)
                        {
                            finishNonMoving = true;
                            angleLimit = 0;
                        }
                        else
                        {
                            exprNum = notFinishTrial[Random.Range(0, notFinishTrial.Count)];
                            Debug.Log($"{notFinishTrial.Count}");
                            notFinishTrial.Remove(exprNum);
                            angleLimit = trialAngle[exprNum];
                            if (angleLimit < 0)
                            {
                                speed = Mathf.Abs(speed) * -1;
                            }
                            else
                            {
                                speed = Mathf.Abs(speed);
                            }
                        }
                        // データ書き込み
                        DrainData.TrialName = $"expr{trialNum}";
                        DrainData.angle = angleLimit;

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
}
