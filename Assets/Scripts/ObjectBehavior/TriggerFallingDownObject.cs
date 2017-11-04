using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFallingDownObject : MonoBehaviour {
    public ObjectFallDown obj;
    [SerializeField] private AudioSource m_audioSource;

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "WifeHand")
        {
            obj.FallDown();
            m_audioSource.Play();
            GetComponent<BoxCollider>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
