using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class WifeNavManager : MonoBehaviour {

    public UnityEvent event_ShouldReachOut;
    public UnityEvent event_Line3Finished;

    public UnityEvent event_LifeBoat1Finished;
    public UnityEvent event_LifeBoat2Finished;
    public UnityEvent event_LifeBoat3Finished;

    public CrewBehaviorController crew;

    public AudioSource footSound;

    public GameObject waterSplashes;

    //Nav Mesh
    private NavMeshAgent m_navMeshAgent;
    [SerializeField] private Transform m_player;
    private string HAND_TAG = "Hand";
    private float m_distance;
    [SerializeField] private float m_stoppingDistanceLow = 5.0f;
    [SerializeField] private float m_stoppingDistanceHigh = 6.5f;
    [SerializeField] private float m_destinationReachedTreshold = 0.5f;

    //Animation
    public Animator m_animator;

    //Lines
    private AudioSource m_audioSource;
    [SerializeField] private AudioClip[] m_audioClipStrings;
    private int m_linesIndex = 0;

    //State Machine
    public bool DestinationReached;
    public bool Interacted;
    public bool StayInteracted;
    public bool Interactable;
    [SerializeField] private CrewTalkingGameState m_brickFallingController;
    [SerializeField] private EndingGameState m_endingState;

    public NavMeshAgent NavMeshAgent
    {
        get
        {
            return m_navMeshAgent;
        }

        set
        {
            m_navMeshAgent = value;
        }
    }

    private void Awake()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        m_animator = GetComponent<Animator>();
        m_audioSource = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start()
    {
        //crew.event_CrewTalk1Finished.AddListener(CharacterTalkingNextLine);
        //crew.event_CrewTalk2Finished.AddListener(CharacterTalkingNextLine);
        m_audioSource = GetComponent<AudioSource>();

        if (!NavMeshAgent)
        {
            print("Missing NavMesh Agent");
        }

        if (!m_player)
        {
            m_player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDistance();
        UpdateDestinationReached();
        UpdateAnimSpeed();
           
        if (NavMeshAgent.velocity.magnitude < 0.5f)
        {
            StartCoroutine(DisableWaterSplashes());
        } else
        {
            StartCoroutine(EnableWaterSplashes());
        }
    }

    private IEnumerator EnableWaterSplashes()
    {
        yield return new WaitForSeconds(0.1f);
        waterSplashes.SetActive(true);
    }

    private IEnumerator DisableWaterSplashes()
    {
        yield return new WaitForSeconds(0.1f);
        waterSplashes.SetActive(false);
    }

    private void UpdateDestinationReached()
    {
        float distance = 0;
        if (NavMeshAgent.enabled == true)
        {
            distance = Vector3.Distance(NavMeshAgent.transform.position, NavMeshAgent.destination);
        }

        if (distance < m_destinationReachedTreshold)
        {
            DestinationReached = true;
            //Debug.Log(NavMeshAgent.velocity);
        }
        else
        {
            DestinationReached = false;
        }
    }

    private void UpdateDistance()
    {
        m_distance = Vector3.Distance(transform.position, m_player.position);
        if (DestinationReached == false)
        {
            if (m_distance >= m_stoppingDistanceHigh)
            {
                NavMeshAgent.isStopped = true;
                m_animator.Play("Guest-signGuestToFollow");
            }
            else if (m_distance <= m_stoppingDistanceLow)
            {
                NavMeshAgent.isStopped = false;
            }
        }
    }

    private void UpdateAnimSpeed()
    {
        float speed = NavMeshAgent.velocity.magnitude;
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
        m_animator.SetFloat("runningSpeed", speed / NavMeshAgent.speed);
    }

    public void UploadAnimBlendTree(float para)
    {
        m_animator.SetFloat("ShakingHand", para);
    }

    public void SetDestination(Vector3 destination)
    {
        m_animator.Play("Walk");
        NavMeshAgent.destination = destination;
    }

    public void CharacterStartsWalking()
    {
        if (NavMeshAgent.isOnNavMesh)
        {
            NavMeshAgent.isStopped = false;
            m_animator.Play("Walk");
        }
    }

    public void StopCharacter()
    {
        NavMeshAgent.isStopped = true;
        m_animator.Play("Idle");
    }

    public void CharacterEnterNextState()
    {
        Interactable = false;
        Interacted = false;
        DestinationReached = false;
    }

    public void CharacterTalkingNextLine()
    {
        if (m_linesIndex < m_audioClipStrings.Length)
        {
            CharacterEventsWhileTalking();
            AudioClip clip = m_audioClipStrings[m_linesIndex];
            m_audioSource.clip = clip;
            m_audioSource.Play();
            m_linesIndex += 1;
            float clipLength = clip.length;
            StartCoroutine(CharacterFinishTalking(clipLength));
        }
        else
        {
            print("Index out of range");
        }
    }

    private void UpdateHoldingBlend()
    {

    }

    public void CharacterPlayNextAnim(string anim)
    {
        m_animator.Play(anim);
    }

    public void CharacterPlayingNextAnimAfter(float time, string anim)
    {
        StartCoroutine(CharacterPlayingNextAnimCoroutine(time, anim));
    }

    private IEnumerator CharacterPlayingNextAnimCoroutine(float time, string anim)
    {
        yield return new WaitForSeconds(time);
        m_animator.Play(anim);
    }

    private void CharacterEventsWhileTalking()
    {
        if (m_linesIndex == 2)
        {
            CharacterTurning();
        }   
    }

    private IEnumerator CharacterFinishTalking(float clipLength)
    {
        yield return new WaitForSeconds(clipLength);
        if (m_linesIndex == 1)
        {
            Interactable = true;
            m_animator.SetFloat("ShakingHand", 1.0f);
            AudioClip clip = Resources.Load<AudioClip>("BreathLoop");
            m_audioSource.clip = clip;
            m_audioSource.loop = true;
            m_audioSource.Play();            
        }
        if (m_linesIndex == 2)
        {
            Interacted = true;
            m_animator.Play("Walk");
        }
    }

    public void DisableAudioLoop()
    {
        m_audioSource.loop = false;
    }

    public void PlayBreathLoopSound()
    {
        m_animator.SetFloat("ShakingHand", 1.0f);
        AudioClip clip = Resources.Load<AudioClip>("Looping");
        m_audioSource.clip = clip;
        m_audioSource.loop = true;
        m_audioSource.Play();
    }

    public void PlayCalmDownSound()
    {
        AudioClip clip = Resources.Load<AudioClip>("CalmingDown");
        m_audioSource.clip = clip;
        m_audioSource.Play();
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

    IEnumerator CharacterTurningToPlayer()
    {
        float turningTime = 1.0f;
        float timer = 0;
        Vector3 direction = m_player.position - transform.position;
        Quaternion fromRotation = transform.rotation;
        Quaternion toRotation = Quaternion.FromToRotation(Vector3.up, direction);
        while (timer < turningTime)
        {
            transform.rotation =  Quaternion.Lerp(fromRotation, toRotation, timer / turningTime);
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    public void CharacterTurning()
    {
        StartCoroutine(CharacterTurningToPlayer());
    }

    public void CharacterLookAtPlayer()
    {
        //Debug.Log("Ending Turn");
        // transform.LookAt( new Vector3 (m_player.position.x, transform.position.y, m_player.position.z));
        transform.LookAt(m_player);
        transform.rotation  = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
    }

    public void ActivateCrewTalkingState()
    {
        m_brickFallingController.ActivateState();
    }

    public void SetInteractable()
    {
        Interactable = true;
    }

    public void ActivateEndingState()
    {
        m_endingState.ActivateState();
    }
}