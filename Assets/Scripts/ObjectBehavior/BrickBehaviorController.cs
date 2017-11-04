using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBehaviorController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BrickFall()
    {
        print("Brick Fall");
        gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
    }
}
