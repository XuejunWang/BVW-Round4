using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingAnimation : MonoBehaviour {

    private Animator m_animator;
    private bool m_triggered;
    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        m_animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (!m_triggered)
        {
            if (other.tag == "WifeHand")
            {
                m_animator.SetTrigger("PillarFalls");
                m_triggered = true;
                if (audioSource)
                {
                    audioSource.Play();
                }
            }
        }

    }
}
