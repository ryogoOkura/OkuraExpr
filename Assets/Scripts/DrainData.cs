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
    public GameObject FOVRestrictor;
    [SerializeField] private Camera m_camera;

    private Vector3 pos;
    private Vector3 rot;
    private string writeStr;
    private float defTime;

    // Start is called before the first frame update
    void Start()
    {
        // FOVRestrictor.SetActive(true);
        isRecording = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (MoveEx.isMoving)
        {
            if (isRecording == false)
            {
                File.WriteAllText($"C:\\Users\\shiraishi\\Desktop\\okura expr\\data\\{TrialName}.csv", $"time, posx, posy, posz, rotx, roty, rotz, numCnt, pushCnt, pinchCnt, angle{angle}\n");
                defTime = Time.time;
                isRecording = true;
            }
            // ファイルに書き込み
            pos = m_camera.transform.position;
            rot = m_camera.transform.rotation.eulerAngles;
            writeStr = $"{Time.time - defTime}, {pos.x}, {pos.y}, {pos.z}, {rot.x}, {rot.y}, {rot.z},{MoveEx.numCnt}, {MoveEx.pushCnt}, {MoveEx.pinchCnt}\n";
            File.AppendAllText($"C:\\Users\\shiraishi\\Desktop\\okura expr\\data\\{TrialName}.csv", writeStr);
        }
        else
        {
            if (isRecording)
            {
                isRecording = false;
            }
        }
    }
}
