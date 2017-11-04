using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hand : MonoBehaviour {
    bool hold;
    bool point;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Animator>().SetBool("hold", hold);
        GetComponent<Animator>().SetBool("point", point);
        if (Input.GetKey(KeyCode.A))
        {
            hold = true;
        }else if (!Input.GetKey(KeyCode.A))
        {
            hold = false;
        }
        if (Input.GetKey(KeyCode.S))
        {
            point = true;
        }
        else if (!Input.GetKey(KeyCode.S))
        {
            point = false;
        }

    }
}
