using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Recenterer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SteamVR.instance.hmd.ResetSeatedZeroPose();
        }
    }
}
