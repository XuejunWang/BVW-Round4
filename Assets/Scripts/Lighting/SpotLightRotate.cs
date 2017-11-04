using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotLightRotate : MonoBehaviour {
    private float rotateSpeed = 5f;
    private float rotateInterval = 0.01f;

	// Use this for initialization
	void Start () {
        StartCoroutine(Rotate());	
	}

    private IEnumerator Rotate ()
    {
        yield return null;
        while (true)
        {
            yield return new WaitForSeconds(rotateInterval);
            transform.Rotate(new Vector3(0, rotateSpeed, 0));
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
