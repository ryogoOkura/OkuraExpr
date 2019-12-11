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

    [SerializeField] private TextMeshPro text;
    [SerializeField] private TextMeshProUGUI message;
    public int numCnt;
    public int pinchCnt;
    public int pushCnt;
    public float timeOut;

    public static bool isMoving;
    private float timeElapsed;
    private string str;
    private int strLength;

    // Start is called before the first frame update
    void Start()
    {
        text.text = "S";
        numCnt = 0;
        pinchCnt = 0;
        pushCnt = 0;
        isMoving = false;
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
        if (Push.GetStateDown(HandType))
        {
            isMoving = true;
            message.text = "";
        }
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
                    text.text = str.Substring(index, 1);
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
        }
    }

}
