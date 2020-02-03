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
    [Header("Use Each Eyes")]
    public bool isUsingEachEyes;
    [SerializeField] private Camera l_camera;
    [SerializeField] private Camera r_camera;

    private Vector3 pos;
    private Vector3 rot;
    private string writeStr;
    private float defTime;
    private string filePath;
    private string folderPath;

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
                string date = System.DateTime.Now.ToString("yyyy_MM_dd");
                folderPath = $"C:\\Users\\shiraishi\\Desktop\\okura expr\\{date}";
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                folderPath = $"C:\\Users\\shiraishi\\Desktop\\okura expr\\{date}\\data";
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                filePath = $"C:\\Users\\shiraishi\\Desktop\\okura expr\\{date}\\data\\{TrialName}.csv";
                writeStr = $"time,posx,posy,posz,rotx,roty,rotz,markCnt,pushCnt,pinchCnt,angle:{angle},mark:{MoveEx.mark}\n";
                File.WriteAllText(filePath, writeStr);
                
                defTime = Time.time;
                isRecording = true;
            }
            // ファイルに書き込み
            if (isUsingEachEyes)
            {// 右目と左目の座標は同じ
                pos = l_camera.transform.position;
                rot = l_camera.transform.rotation.eulerAngles;
            }
            else
            {
                pos = m_camera.transform.position;
                rot = m_camera.transform.rotation.eulerAngles;
            }
            writeStr = $"{Time.time - defTime}, {pos.x}, {pos.y}, {pos.z}, {rot.x}, {rot.y}, {rot.z},{MoveEx.markCnt}, {MoveEx.pushCnt}, {MoveEx.pinchCnt}\n";
            File.AppendAllText(filePath, writeStr);
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
