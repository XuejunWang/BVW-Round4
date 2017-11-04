using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Move : MonoBehaviour {

    public Transform goal;
    private NavMeshAgent m_navMeshAgent;
    public Transform m_target;
    private float m_distance;
    private float m_stoppingDistance = 5f;

    void Start()
    {
        m_navMeshAgent = GetComponent<NavMeshAgent>();
        m_navMeshAgent.destination = goal.position;
    }

    // Update is called once per frame
    void Update()
    {
        m_distance = Vector3.Distance(transform.position, m_target.position);
        if (m_distance >= m_stoppingDistance)
        {
            m_navMeshAgent.isStopped = true;
        }
        else
        {
            m_navMeshAgent.isStopped = false;
        }
    }
}
