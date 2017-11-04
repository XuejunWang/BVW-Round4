using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingGameState : MonoBehaviour {
    //States
    public bool IsActivated;
    public bool StateEntered;
    public bool StateQuitted;

    public CameraFader cameraFader;

    private bool firstFinished = false;

    //Character
    [SerializeField] private WifeNavManager m_characterController;
    private string CHARACTER_TAG = "Wife";
    [SerializeField] private Transform m_characterGoal;

    //Events
    [SerializeField] private ShakeObject m_shakingManager;
    [SerializeField] private BGMController bgmC;

    //nextState
    [SerializeField] private StairsGameState m_nextState;

    // Use this for initialization
    void Start()
    {
        cameraFader.event_FadeInComplete.AddListener(HornFinishedEventsFunction);
        bgmC.event_ShakeHands.AddListener(PlayShakeHands);
        //HornFinishedEventsFunction();

        if (!m_characterController)
        {
            m_characterController = GameObject.FindGameObjectWithTag(CHARACTER_TAG).GetComponent<WifeNavManager>();
        }

        if (!m_characterGoal)
        {
            m_characterGoal = transform;
        }

        if (IsActivated)
        {
            ActivateState();
        }

    } 

    // Update is called once per frame
    void Update()
    {
        if (!StateQuitted && IsActivated)
        {
            CheckState();
        }
    }

    private void CheckState()
    {
        if (!StateEntered)
        {
            //Conditions of Entering the State
            if (m_characterController.DestinationReached)
            {
                EnterState();
            }
        }

        else if (!StateQuitted)
        {
            //Conditions of Quitting the State
            if (m_characterController.Interacted && firstFinished == false)
            {
                firstFinished = true;
                m_characterController.DisableAudioLoop();
                m_characterController.PlayCalmDownSound();
            }
            if (m_characterController.StayInteracted)
            {
                QuitState();
            }
        }
    }

    public void ActivateState()
    {
        m_shakingManager.ShakeObjectAfter(3.0f);
        IsActivated = true;
        m_characterController.SetDestination(m_characterGoal.position);
        m_characterController.CharacterStartsWalking();
    }

    public void PlayShakeHands()
    {
        m_characterController.CharacterPlayingNextAnimAfter(0.2f, "Emma-start-shakyHand");
    }

    public void HornFinishedEventsFunction()
    {
        m_characterController.CharacterTalkingNextLine();
        //m_characterController.m_animator.SetTrigger("StartShakingHand");
        //m_characterController.CharacterPlayingNextAnimAfter(0.2f, "Emma-start-shakyHand");
    }

    public void EnterState()
    {
        StateEntered = true;
        m_characterController.StopCharacter();
    }

    public void QuitState()
    {
        StateQuitted = true; 
        IsActivated = false;
        //Reset
        m_characterController.CharacterEnterNextState();
        m_nextState.ActivateState();
    }
}