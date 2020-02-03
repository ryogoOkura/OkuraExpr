using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFOV : MonoBehaviour
{
    public bool is60 = false;
    public bool is30 = false;
    public GameObject quad60R;
    public GameObject quad60L;
    public GameObject quad60;
    public GameObject quad30R;
    public GameObject quad30L;
    public GameObject quad30;

    // Start is called before the first frame update
    void Start()
    {
        is60 = SceneChange.Is60;
        is30 = SceneChange.Is30;
        quad60.SetActive(is60);
        quad60R.SetActive(is60);
        quad60L.SetActive(is60);
        quad30.SetActive(is30);
        quad30R.SetActive(is30);
        quad30L.SetActive(is30);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
