using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTriggeredSound : MonoBehaviour {
    public AudioClip soundToBePlayed;

    private AudioSource audioSource;
    private Collider boxCollider;
	// Use this for initialization
	void Start () {
        boxCollider = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "WifeHand")
        {
            boxCollider.enabled = false;
            audioSource.clip = soundToBePlayed;
            audioSource.Play();
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
