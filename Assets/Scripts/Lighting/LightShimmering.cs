using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightShimmering : MonoBehaviour {
    public float shimmeringMinInterval = 0.01f;
    public float shimmeringMaxInterval = 0.05f;

    public float onMin = 0.5f;
    public float onMax = 0.9f;

    private Light controlledLight;

	// Use this for initialization
	void Start () {
        controlledLight = GetComponent<Light>();
        StartCoroutine(ShimmeringManage());
	}

    private IEnumerator ShimmeringManage()
    {
        yield return null;
        while (true)
        {
            float shimmeringInterval = Random.Range(shimmeringMinInterval, shimmeringMaxInterval);
            controlledLight.enabled = false;
            yield return new WaitForSeconds(shimmeringInterval);
            controlledLight.enabled = true;
            float onInterval = Random.Range(onMin, onMax);
            yield return new WaitForSeconds(onInterval);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
