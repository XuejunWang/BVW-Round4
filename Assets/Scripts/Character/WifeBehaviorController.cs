using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class WifeBehaviorController : MonoBehaviour {
    //Nav Mesh
    private NavMeshAgent m_navMeshAgent;
    [SerializeField] private Transform m_player;
    private float m_distance;
    [SerializeField] private float m_stoppingDistance = 5.0f;
    [SerializeField] private float m_slowingDistance = 3.0f;
    [SerializeField] private float m_destinationReachedTreshold = 0.5f;

    //States
    public bool DestinationReached;
    public bool Interacted;
    public bool Interactable;

    //User Interaction
    private string HAND_TAG = "Hand";

    //Animation
    public Animator m_animtor;

    //Lines
    private AudioSource m_audioSource;
    [SerializeField] private AudioClip[] m_audioClipStrings;
    private int m_linesIndex = 0;

    //Effects
    public AudioSource footSound;

    //Envents
    public UnityEvent event_ShouldReachOut;
    public UnityEvent event_Line3Finished;
    public UnityEvent event_LifeBoat1Finished;
    public UnityEvent event_LifeBoat2Finished;
    public UnityEvent event_LifeBoat3Finished;

    //Crew
    public CrewBehaviorController Crew;

    private void Awake()
    {
        m_navMeshAgent = GetComponent<NavMeshAgent>();
        m_animtor = GetComponent<Animator>();
        m_audioSource = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start () {
        if (!m_navMeshAgent)
        {
            print("Missing NavMesh Agent");
        }

        if (!m_player)
        {
            m_player = GameObject.FindGameObjectWithTag(HAND_TAG).transform;
        }
        Crew.event_CrewTalk1Finished.AddListener(CharacterTalkingNextLine);
        Crew.event_CrewTalk2Finished.AddListener(CharacterTalkingNextLine);
    }
	
	// Update is called once per frame
	void Update () {
        UpdateDistance();
        UpdateDestinationReached();
        float speed = m_navMeshAgent.velocity.magnitude;
        if (speed >= 0.5f)
        {
            if (footSound.isPlaying == false)
            {
                footSound.Play();
            }
        }
        else
        {
            footSound.Stop();
        }
        m_animtor.SetFloat("runningSpeed", speed / m_navMeshAgent.speed);
    }

    private void UpdateDistance()
    {
        m_distance = Vector3.Distance(transform.position, m_player.position);
        if (m_distance >= m_stoppingDistance)
        {
            m_navMeshAgent.isStopped = true;
        }
        else
        {
            m_navMeshAgent.isStopped = false;
        }
    }

    private void UpdateDestinationReached()
    {
        float distance = Vector3.Distance(m_navMeshAgent.transform.position, m_navMeshAgent.destination);
        if (distance < m_destinationReachedTreshold)
        {
            DestinationReached = true;
        }
        else
        {
            DestinationReached = false;
        }
    }

    public void SetDestination(Vector3 destination)
    {
        //need to determine if the destination is on the NavMesh
        m_animtor.SetBool("walking", true);
        m_navMeshAgent.destination = destination;
    }

    public void CharacterStartsWalking()
    {
        if (m_navMeshAgent.isOnNavMesh)
        {
            m_navMeshAgent.isStopped = false;
            m_animtor.SetBool("walking", true);
            m_animtor.SetBool("StartTalking", false);
        }
    }

    public void StopCharacter()
    {
        m_navMeshAgent.isStopped = true;
        m_animtor.SetBool("StartTalking", false);
        m_animtor.SetBool("walking", false);
    }

    public void CharacterEnterNextState()
    {
        Interactable = false;
        Interacted = false;
        DestinationReached = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Interactable)
        {
            if (collision.collider.tag == HAND_TAG)
            {
                Interacted = true;
                print("Interacted");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Interactable)
        {
            if (other.gameObject.tag == HAND_TAG)
            {
                Interacted = true;
                print("Interacted");
            }
        }
    }

    public void CharacterTalkingNextLine()
    {
        if (m_linesIndex < m_audioClipStrings.Length)
        {
            AudioClip clip = m_audioClipStrings[m_linesIndex];
            m_audioSource.clip = clip;
            //m_animator.SetBool("StartTalking", true
            if (m_linesIndex == 3)
            {
                m_animtor.Play("GetStriked");
            }
            else if (m_linesIndex == 7)
            {
                m_animtor.Play("EndLine");
            }
            // PROBLEM HERE
            else
            {
                m_animtor.Play("StartLine");
            }

            if (!(m_linesIndex == 4 || m_linesIndex == 5 || m_linesIndex == 6))
            {
                transform.LookAt(new Vector3(m_player.position.x, transform.position.y, m_player.position.z));
            }
            m_audioSource.Play();
            m_linesIndex++;
            StartCoroutine(CharacterLineEndsAfter(clip.length));
        }
        else
        {
            print("Index out of range");
        }
    }

    private IEnumerator CharacterLineEndsAfter(float clipLength)
    {
        yield return new WaitForSeconds(clipLength);
        m_animtor.SetBool("Talking", false);
    }

    public void CharacterTalkingNextLineAfter(float waitingTime)
    {
        StartCoroutine(CharacterTalkingNextLineCoroutine(waitingTime));
    }

    IEnumerator CharacterTalkingNextLineCoroutine(float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);
        CharacterTalkingNextLine();
    }

    //play animation
    public void PlayAnimation(string animation)
    {
        m_animtor.Play(animation);
    }

    public void PlayAnimationAfter(string animation, float time)
    {
        StartCoroutine(PlayAnimationAfterTimeIEnumerator(animation, time));
    }

    IEnumerator PlayAnimationAfterTimeIEnumerator(string animation, float time)
    {
        yield return new WaitForSeconds(time);
        PlayAnimation(animation);
    }
}
                                          