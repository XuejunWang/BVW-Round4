using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerConcrete : MonoBehaviour {
    public Animator concrete1;
    public Animator concrete2;
	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "WifeHand")
        {
            concrete1.SetTrigger("Trigger");
            concrete2.SetTrigger("Trigger");
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
