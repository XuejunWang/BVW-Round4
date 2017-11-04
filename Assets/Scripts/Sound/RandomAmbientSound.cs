using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAmbientSound : MonoBehaviour {
    public AudioClip[] randomClips;

    public BGMController bgmController;
    public ShakeObject sobj;

    private float minInterval = 17f;
    private float maxInterval = 25f;

    private AudioSource randomAmbientSource;
	// Use this for initialization
	void Start () {
        //bgmController.event_HornFinished.AddListener(responceAfterHorn);
        randomAmbientSource = GetComponent<AudioSource>();
	}

    public void PlayTankExplosion()
    {
        randomAmbientSource.clip = randomClips[1];
        randomAmbientSource.Play();
    }

    void responceAfterHorn()
    {
        //Debug.Log("MSG ACK");
        StartCoroutine(PlayRandomSound());
    }

    private IEnumerator PlayRandomSound()
    {

        while (true)
        {
            float interval = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(interval);

            sobj.StartShortShake();
            int index = Random.Range(0, randomClips.Length);
            randomAmbientSource.clip = randomClips[index];
            randomAmbientSource.Play();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
