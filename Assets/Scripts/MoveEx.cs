using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public static int numCnt;
    public static int pinchCnt;
    public static int pushCnt;
    public float timeOut;

    public static bool isMoving;
    public static bool canStart;
    private float timeElapsed;
    private string str;
    private int strLength;
    private int[] trialAngle = { 45, 90, -45, 90 };
    private List<int> notFinishTrial = new List<int>{ 0, 1, 2, 3 };
    private int trialNum;
    private int exprNum;

    // Start is called before the first frame update
    void Start()
    {
        target.text = "S";
        message.text = "Push to Expr Start";
        numCnt = 0;
        pinchCnt = 0;
        pushCnt = 0;
        isMoving = false;
        canStart = false;
        timeElapsed = 0f;
        str = "23456789ABCDEFGHJKLMNPQRSTUVWXYZ";
        strLength = str.Length;
        trialNum = 0;

        defPosition = transform.position;
        defRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            if (transform.position.z > radius * Mathf.Cos(angleLimit/180 * Mathf.PI))
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
            {
                message.text = "Push to Restart";
                isMoving = false;
                canStart = false;
            }
        }
        else
        {
            if (canStart)
            {// 移動開始
                if (Push.GetStateDown(HandType))
                {
                    isMoving = true;
                    message.text = $"expr{trialNum}_angle{trialAngle[exprNum]}";
                    // message.text = "";
                }
            }
            else
            {
                if (Push.GetStateDown(HandType) && DrainData.isRecording == false)
                {// リスタートのための初期化
                    transform.position = defPosition;
                    transform.rotation = defRotation;

                    if (notFinishTrial.Count == 0)
                    {
                        message.text = "Finish";
                    }
                    else
                    {// 実験の種類決定
                        trialNum += 1;
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
                        // データ書き込みへ
                        DrainData.TrialName = $"expr{trialNum}";
                        DrainData.angle = angleLimit;

                        message.text = "Push to Start";
                        numCnt = 0;
                        pinchCnt = 0;
                        pushCnt = 0;
                        canStart = true;
                    }
                }
            }
        }
    }
}
