using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewTalkingGameState : MonoBehaviour {
    //States
    public bool IsActivated;
    public bool StateEntered;
    public bool StateQuitted;

    //Character
    [SerializeField] private WifeNavManager m_characterController;
    private string CHARACTER_TAG = "Wife";
    [SerializeField] private Transform m_characterGoal;

    //Events
    [SerializeField] private CrewBehaviorController m_crewController;

    //nextState
    [SerializeField] EndingGameState m_nextState;

    // Use this for initialization

    // Use this for initialization
    void Start()
    {
        m_characterController.event_LifeBoat3Finished.AddListener(QuitState);
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
        //else if (!StateQuitted)
        //{
        //    //Conditions of Quitting the State
        //    if (m_characterController.Interacted)
        //    {
        //        QuitState();
        //    }
        //}
    }

    public void ActivateState()
    {
        IsActivated = true;
        m_characterController.SetDestination(m_characterGoal.position);
        m_characterController.CharacterStartsWalking();
    }

    public void EnterState()
    {
        StateEntered = true;
        //Events to trigger
        m_characterController.StopCharacter();
        TriggerStateEvents();
    }

    public void QuitState()
    {
        StateQuitted = true;
        IsActivated = false;
        //Reset
        m_characterController.CharacterEnterNextState();
        m_nextState.ActivateState();
    }

    public void TriggerStateEvents()
    {
        m_characterController.CharacterTalkingNextLine();
        m_crewController.CharaterPlayAnimation("lifeboatMan-Dialogue");
        m_characterController.CharacterPlayNextAnim("Emma-LifeboatDialogue");
        //m_crewController.CrewTalk();
    }
}
