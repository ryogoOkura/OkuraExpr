using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Valve.VR;

public class HeadTracking : MonoBehaviour
{
    void Update()
    {
        // 着席モードでRキーで位置トラッキングをリセットする
        if (Input.GetKeyDown(KeyCode.R))
        {
            SteamVR.instance.hmd.ResetSeatedZeroPose();
        }
    }
}