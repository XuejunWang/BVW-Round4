using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingCeilling : MonoBehaviour {
    private Animator m_animator;
	// Use this for initialization
	void Start () {
        m_animator = GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "WifeHand")
        {
            m_animator.SetTrigger("Trigger");
            GetComponent<BoxCollider>().enabled = false;
        }
    }
}
