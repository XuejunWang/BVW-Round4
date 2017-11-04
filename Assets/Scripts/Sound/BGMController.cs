using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BGMController : MonoBehaviour {
    public AudioClip horn;
    public AudioClip bgm_AllTheWay;

    private AudioSource bgm;

    public UnityEvent event_HornFinished;
    public UnityEvent event_ShakeHands;

    // Use this for initialization
    void Start () {
        bgm = GetComponent<AudioSource>();
        PlayHorn();
	}

    public void PlayBGM(AudioClip clip)
    {
        bgm.clip = clip;
        bgm.Play();
    }

    public void PlayHorn ()
    {
        bgm.clip = horn;
        bgm.Play();
        StartCoroutine(HornFinishedNotify());
    }

    private IEnumerator HornFinishedNotify()
    {
        yield return new WaitForSeconds(horn.length + 4f);
        event_ShakeHands.Invoke();
        yield return new WaitForSeconds(1f);
        event_HornFinished.Invoke();
        PlayEndingSong();
    }

    public void PlayEndingSong()
    {
        bgm.clip = bgm_AllTheWay;
        bgm.volume = 0.2f;
        bgm.Play();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
