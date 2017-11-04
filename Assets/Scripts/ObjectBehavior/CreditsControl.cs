using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsControl : MonoBehaviour {
    public GameObject credits;
	// Use this for initialization
	void Start () {
        StartCoroutine(ShowCredits());
	}

    private IEnumerator ShowCredits()
    {
        yield return new WaitForSeconds(10f);
        credits.SetActive(true);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
