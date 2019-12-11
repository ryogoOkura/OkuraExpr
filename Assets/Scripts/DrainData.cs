using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Valve.VR;

public class DrainData : MonoBehaviour
{
    public string TrialName;
    public bool isRecord;
    [SerializeField] private GameObject m_camera;

    private Vector3 pos;
    private Vector3 rot;
    private string writeStr;

    // Start is called before the first frame update
    void Start()
    {
        File.WriteAllText($"C:\\Users\\shiraishi\\Desktop\\okura expr\\data\\{TrialName}.csv", $"posx, posy, posz, rotx, roty, rotz\n");
        isRecord = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (MoveEx.isMoving)
        {
            pos = m_camera.transform.position;
            rot = m_camera.transform.rotation.eulerAngles;
            writeStr = $"{pos.x}, {pos.y}, {pos.z}, {rot.x}, {rot.y}, {rot.z}\n";

            File.AppendAllText($"C:\\Users\\shiraishi\\Desktop\\okura expr\\data\\{TrialName}.csv", writeStr);
        }
    }
}
