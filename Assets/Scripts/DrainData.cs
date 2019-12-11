using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Valve.VR;

public class DrainData : MonoBehaviour
{
    public static string TrialName;
    public static float angle;
    public static bool isRecording;
    [SerializeField] private Camera m_camera;

    private Vector3 pos;
    private Vector3 rot;
    private string writeStr;

    // Start is called before the first frame update
    void Start()
    {
        isRecording = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (MoveEx.isMoving)
        {
            if (isRecording == false)
            {
                File.WriteAllText($"C:\\Users\\shiraishi\\Desktop\\okura expr\\data\\{TrialName}.csv", $"posx, posy, posz, rotx, roty, rotz, numCnt, pushCnt, pinchCnt, angle{angle}\n");
                isRecording = true;
            }
            // ファイルに書き込み
            pos = m_camera.transform.position;
            rot = m_camera.transform.rotation.eulerAngles;
            writeStr = $"{pos.x}, {pos.y}, {pos.z}, {rot.x}, {rot.y}, {rot.z}\n";
            File.AppendAllText($"C:\\Users\\shiraishi\\Desktop\\okura expr\\data\\{TrialName}.csv", writeStr);
        }
        else
        {
            if (isRecording)
            {
                writeStr = $",,,,,,{MoveEx.numCnt}, {MoveEx.pushCnt}, {MoveEx.pinchCnt}";
                File.AppendAllText($"C:\\Users\\shiraishi\\Desktop\\okura expr\\data\\{TrialName}.csv", writeStr);
                isRecording = false;
            }
        }
    }
}
