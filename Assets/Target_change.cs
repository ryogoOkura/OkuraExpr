using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_change : MonoBehaviour {
    public bool a;
    float x;

    public float input_time = 0.5f;
    public float t;
    public GameObject can;
    public enum direction
    {
        up,right,down,left
    }
    public direction dir;
	// Use this for initialization
	void Start () {
        dir = direction.up;
        StartCoroutine("kaitou");
	
	}
	
	// Update is called once per frame
	void Update () {
        if (a)
        {
            can.SetActive(true);
            t -= Time.deltaTime;
            if (t > 0)
            {
               
                switch (dir)
                {
                    case direction.up:
                        x = 0;
                        this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, x);
                        
                        a = false;
                        StartCoroutine("kaitou");
                        break;
                    case direction.right:
                        x = 270;
                        this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, x);
                        
                        a = false;
                        StartCoroutine("kaitou");
                        break;
                    case direction.down:
                        x = 180;
                        this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, x);
                        a = false;
                        StartCoroutine("kaitou");
                        break;
                    case direction.left:
                        x = 90;
                        this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, x);
                        a = false;
                        StartCoroutine("kaitou");
                        break;
                }
            }
            else
            {
                a = false;
                StartCoroutine("kaitou");
            }
        }
        else
        {
            can.SetActive(false);
        }

	}
    IEnumerator kaitou()
    {
        yield return new WaitForSeconds(2f);
        a = true;
        t = input_time;
    }
}
