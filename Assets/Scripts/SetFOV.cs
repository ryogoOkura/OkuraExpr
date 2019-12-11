using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFOV : MonoBehaviour
{
    [SerializeField] private Camera cameraR;
    [SerializeField] private Camera cameraL;
    [SerializeField] private Camera m_camera;
    public float fov;
    // Start is called before the first frame update
    void Start()
    {
        m_camera.rect = new Rect(0f, 0f, 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
