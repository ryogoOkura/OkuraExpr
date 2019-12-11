using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Valve.VR;

public class SceneChange : MonoBehaviour
{
    public Button trial;
    public Button caribration;
    public Button start;
    public SteamVR_Input_Sources HandType;
    public SteamVR_Action_Boolean Right;
    public SteamVR_Action_Boolean Left;
    public SteamVR_Action_Boolean Enter;

    private int nowCursol;

    // Start is called before the first frame update
    void Start()
    {
        nowCursol = 0;
        SelectButton(nowCursol);
    }

    // Update is called once per frame
    void Update()
    {
        if (Right.GetStateDown(HandType))
        {
            Debug.Log("right");
            nowCursol = (nowCursol + 1) % 3;
            SelectButton(nowCursol);
        }
        if (Left.GetStateDown(HandType))
        {
            Debug.Log("left");
            nowCursol = (nowCursol - 1 + 3) % 3;
            SelectButton(nowCursol);
        }
        if (Enter.GetStateDown(HandType))
        {
            Debug.Log("enter");
            ChangeScene(nowCursol);
        }
    }

    private void SelectButton(int num)
    {
        switch (num)
        {
            case 0:
                trial.Select();
                break;
            case 1:
                caribration.Select();
                break;
            case 2:
                start.Select();
                break;
        }
    }

    private void ChangeScene(int num)
    {
        switch (num)
        {
            case 0:
                SceneManager.LoadScene("Trial");
                break;
            case 1:
                SceneManager.LoadScene("Caribration");
                break;
            case 2:
                SceneManager.LoadScene("ExOkura");
                break;
        }
    }
}
