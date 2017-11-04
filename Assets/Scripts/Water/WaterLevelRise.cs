using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLevelRise : MonoBehaviour {
    private float highLevel = 15f;
    private float riseInterval = 0.01f;
    private float riseHeight = 0.01f;
    private float timeDelayed = 5f;

    public GameObject[] waterSprays;

	// Use this for initialization
	void Start () {
        
        //RiseLevel();
    }
	
    public void RiseLevel ()
    {
        foreach (GameObject go in waterSprays)
        {
            go.SetActive(true);
        }
        StartCoroutine(WaterLevelRiseCoroutine());
    }

    private IEnumerator WaterLevelRiseCoroutine()
    {
        yield return new WaitForSeconds(timeDelayed);
        while (transform.position.y <= highLevel)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + riseHeight, transform.position.z);
            yield return new WaitForSeconds(riseInterval);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
