using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head_Direction : MonoBehaviour {
    Ray ray;
    RaycastHit hit;
    int layer = 1 << 9;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray,out hit,layer))
        {
            hit.collider.GetComponent<Target>().hit = true;
        }
     

	}

 

}
