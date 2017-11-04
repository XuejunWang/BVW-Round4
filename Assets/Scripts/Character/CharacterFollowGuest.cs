using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterFollowGuest : MonoBehaviour {

	private NavMeshAgent m_navMeshAgent;
    [SerializeField] private Transform m_goal;
	[SerializeField] private Transform m_player;

    [SerializeField] private float m_stoppingDistance = 5.0f;
    private float m_distance;

    private string GOAL_TAG = "Goal";
    private string TARGET_TAG = "Target";

    // Use this for initialization
    void Start () {
		m_navMeshAgent = GetComponent<NavMeshAgent>();
        if (!m_navMeshAgent)
        {
            print("Missing NavMesh Agent");
        }

        if (m_goal)
        {
            m_navMeshAgent.destination = m_goal.position;
        }
        else
        {
            m_navMeshAgent.destination = GameObject.FindGameObjectWithTag("Goal").transform.position;
        }

        if (!m_player)
        {
            m_player = GameObject.FindGameObjectWithTag("Goal").transform;
        }
    }
	
	// Update is called once per frame
	void Update () {
		m_distance = Vector3.Distance(transform.position, m_player.position);
		if(m_distance >= m_stoppingDistance)
		{
			m_navMeshAgent.isStopped = true;
		}
		else
		{
			m_navMeshAgent.isStopped = false;
		}
	}
}
