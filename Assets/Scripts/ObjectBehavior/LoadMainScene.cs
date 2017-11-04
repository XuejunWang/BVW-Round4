using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainScene : MonoBehaviour {
    

	// Use this for initialization
	void Start () {
        StartCoroutine(switchToMainScene());
	}

    private IEnumerator switchToMainScene()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("main");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
