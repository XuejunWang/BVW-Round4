using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CrewBehaviorController : MonoBehaviour {
    public UnityEvent event_CrewTalk1Finished;
    public UnityEvent event_CrewTalk2Finished;

    public WifeNavManager wife;

    //Lines
    private AudioSource m_audioSource;
    [SerializeField] private string[] m_audioClipStrings;
    private int m_linesIndex = 0;

    //Animation
    private Animator m_animator;

    // Use this for initialization
    void Start () {
        m_audioSource = GetComponent<AudioSource>();
        m_animator = GetComponent<Animator>();
        //wife.event_LifeBoat1Finished.AddListener(CharacterTalkingNextLine);
        //wife.event_LifeBoat2Finished.AddListener(CharacterTalkingNextLine);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CrewTalk()
    {
        print("Crew Talks");
    }

    public void CharacterTalkingNextLine()
    {
        if (m_linesIndex < m_audioClipStrings.Length)
        {
            string clipString = m_audioClipStrings[m_linesIndex];
            AudioClip clip = Resources.Load<AudioClip>(clipString);
            m_audioSource.clip = clip;
            m_audioSource.Play();
            m_linesIndex++;
            //StartCoroutine(UniversalQuitLine(clip.length));
        }
        else
        {
            print("Lines index out of range");
        }
    }

    public void CharaterPlayAnimation(string animation)
    {
        m_animator.Play(animation);
    }

    //private IEnumerator UniversalQuitLine(float clipLength)
    //{
    //    yield return new WaitForSeconds(clipLength);
    //    if (m_linesIndex == 1)
    //    {
    //        event_CrewTalk1Finished.Invoke();
    //    }
    //    else if (m_linesIndex == 2)
    //    {
    //        event_CrewTalk2Finished.Invoke();
    //    }
    //}
}
