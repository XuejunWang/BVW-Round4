using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickFallingGameState : MonoBehaviour {
    //States
    public bool IsActivated;
    public bool StateEntered;
    public bool StateQuitted;

    public BoxCollider wifeHeadCollider;
    public Rigidbody emmaRigidBody;
    public BoxCollider fakeCollision;

    //Character
    [SerializeField] private WifeNavManager m_characterController;
    private string CHARACTER_TAG = "Wife";
    [SerializeField] private Transform m_characterGoal;

    //Events
    [SerializeField] private ObjectFallDown m_brickController;

    //nextState
    [SerializeField] CrewTalkingGameState m_nextState;


    // Use this for initialization
    void Start()
    {
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
            if (m_characterController.Interacted)
            {
                QuitState();
            }
        }
    }

    public void ActivateState()
    {
        IsActivated = true;
        //wifeHeadCollider.enabled = true;
        m_characterController.SetDestination(m_characterGoal.position);
        //Debug.Log(m_characterGoal.position);
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
        fakeCollision.enabled = false;
        StateQuitted = true;
        IsActivated = false;
        //emmaRigidBody.constraints = RigidbodyConstraints.None;
        //wifeHeadCollider.enabled = false;
        //Reset
        m_characterController.CharacterPlayingNextAnimAfter(0.2f,"Emma-StandUp");
        m_characterController.CharacterEnterNextState();
    }

    public void TriggerStateEvents()
    {
        //emmaRigidBody.constraints = RigidbodyConstraints.FreezePositionX & RigidbodyConstraints.FreezePositionZ;
        m_characterController.CharacterTalkingNextLine();
        m_characterController.CharacterPlayingNextAnimAfter(0.5f, "Emma-CatchBreath");
        m_brickController.FallDownAfter(3.0f);
        m_characterController.CharacterPlayingNextAnimAfter(3.6f, "Emma-hit");
        m_characterController.CharacterTalkingNextLineAfter(3.6f);
        //m_characterController.Interactable = true;
    }

}
