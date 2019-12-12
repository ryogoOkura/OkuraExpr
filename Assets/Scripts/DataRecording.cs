using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace PupilLabs
{ 
    [RequireComponent(typeof(RecordingController))]
    public class DataRecording : MonoBehaviour
    {
        RecordingController rec;

        [SerializeField] private TextMeshProUGUI message;

        // Start is called before the first frame update
        void Awake()
        {
            rec = GetComponent<RecordingController>();
        }

        // Update is called once per frame
        void Update()
        {
            if (MoveEx.isMoving)
            {
                if (!rec.IsRecording)
                {
                    rec.StartRecording();
                }
            }
            else
            {
                if (rec.IsRecording)
                {
                    rec.StopRecording();
                }
            }

            bool connected = rec.requestCtrl.IsConnected;
            message.text = connected ? "Connected" : "Not connected";

            if (connected)
            {
                var status = rec.IsRecording ? "recording" : "not recording";
                message.text += $"\nStatus: {status}";
            }
        }
    }
}