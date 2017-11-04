using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WifeInteractiongHelper : MonoBehaviour {
    public WifeNavManager wnm;
    private string HAND_TAG = "Hand";
    private bool m_onTriggerStay;
    private float Timer = 0;
    private float HoldingTime = 4.0f;
    public GameObject waterSplashes;

    private void OnTriggerEnter(Collider other)
    {
        if (wnm.Interactable)
        {
            if (other.gameObject.tag == HAND_TAG)
            {
                m_onTriggerStay = true;
                wnm.Interacted = true;
            }
        }
        if (other.gameObject.tag == "LiftWaterSplashes")
        {
            Vector3 oldPosition = waterSplashes.transform.position;
            oldPosition.y += 0.3f;
            waterSplashes.transform.position = oldPosition;
            other.gameObject.SetActive(false);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == HAND_TAG)
        {
            m_onTriggerStay = false;
        }
        //Timer = 0;
        // if(!wnm.StayInteracted)
        // {
        //     wnm.UploadAnimBlendTree(1.0f);
        // }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!wnm.StayInteracted)
        {
            if(wnm.Interactable)
            {
                if (m_onTriggerStay)
                {
                    Timer += Time.deltaTime;
                    float remainingTime = HoldingTime - Timer > 0? HoldingTime - Timer:0;
                    wnm.UploadAnimBlendTree(remainingTime/HoldingTime);
                    if (Timer > HoldingTime)
                    {
                        
                        wnm.StayInteracted = true;
                    }
                }
            }
        }
	}
}
