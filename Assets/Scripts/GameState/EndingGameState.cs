using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Virtuix;
using UnityEngine.SceneManagement;

public class EndingGameState : MonoBehaviour {
    //States
    public bool IsActivated;
    public bool StateEntered;
    public bool StateQuitted;

    public WaterLevelRise waterLevel;
    public Rigidbody door;
    public GameObject ambientBGM;
    public GameObject image;
    public GameObject[] goTobeDisabled;
    public OmniController omniController;
    public CameraFader cameraFader;
    public GameObject underWaterCamera;
    public AudioSource DrowningSound;
    public RandomAmbientSound randomAmbientSoundSource;

    //Character
    [SerializeField] private WifeNavManager m_characterController;
    private string CHARACTER_TAG = "Wife";
    [SerializeField] private Transform m_characterGoal;

    //Events

    // Use this for initialization
    void Start () {
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
	void Update () {
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
        StartCoroutine(ActivateStateAter(1.5f));
    }

    private IEnumerator ActivateStateAter(float time)
    {
        yield return new WaitForSeconds(time);
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
        waterLevel.RiseLevel();
        randomAmbientSoundSource.PlayTankExplosion();
        door.useGravity = true;
        //ambientBGM.SetActive(true);
        StartCoroutine(ActivateImage());
        omniController.speed = 0;
        foreach (GameObject go in goTobeDisabled)
        {
            go.SetActive(false);
        }
        StateQuitted = true;
        IsActivated = false;
        //print("Ending");
        //Trigger Ending 
    }

    private IEnumerator ActivateImage ()
    {
        DrowningSound.Play();
        yield return new WaitForSeconds(15f);
        cameraFader.CameraFadeOut();
        yield return new WaitForSeconds(2);
        //underWaterCamera.SetActive(false);
        //image.SetActive(true);
        SceneManager.LoadScene("ending");
    }

    public void TriggerStateEvents()
    {
        //m_characterController.DestinationReached = true;
        m_characterController.NavMeshAgent.enabled = false;
        m_characterController.CharacterTalkingNextLine();
        m_characterController.CharacterPlayNextAnim("Emma-End-HandHold");
        m_characterController.CharacterLookAtPlayer();
    }
}
