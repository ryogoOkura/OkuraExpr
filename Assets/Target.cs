using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {
    public bool hit;
	// Use this for initialization
	void Start () {
        hit = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (hit)
        {
           GetComponent<MeshRenderer>().material.color = Color.red;
        }
        else
        {
           GetComponent<MeshRenderer>().material.color = Color.white;
        }
        hit = false;
	}
}
