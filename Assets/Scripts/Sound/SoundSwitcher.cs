using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSwitcher : MonoBehaviour {
    public GameObject soundGroup;

    private Collider boxCollider;

    // Use this for initialization
    void Start () {
        boxCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "WifeHand")
        {
            boxCollider.enabled = false;
            soundGroup.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
