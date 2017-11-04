using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeObject : MonoBehaviour {

    [SerializeField] float m_shakingTime = 5.0f;
    [SerializeField] float m_shakingAmplitude = 0.1f;
    [SerializeField] float m_decreasingTime = 1.0f;
    [SerializeField] Transform m_transformToShake;
    private Vector3 m_position;
    private float m_timer;
    public bool m_isShaking;

    // Use this for initialization
    void Start () {
        if (!m_transformToShake)
        {
            m_transformToShake = gameObject.transform;
        }
        m_position = transform.position;
    }

    public void StartShake( )
    {
        m_timer = Time.time;
        m_isShaking = true;
        StartCoroutine(Shaking());
    }

    public void StartShortShake()
    {
        m_timer = Time.time;
        m_isShaking = true;
        m_shakingTime = 3f;
        m_decreasingTime = 1f;
        StartCoroutine(Shaking());
    }

    private IEnumerator Shaking()
    {
        while (m_isShaking)
        {
            Vector3 curPos = m_transformToShake.transform.position;
            float deltaTime = Time.time - m_timer;
            float amplitude = m_shakingAmplitude;
            if (deltaTime > m_decreasingTime && deltaTime < m_shakingTime)
            {
                amplitude = m_shakingAmplitude * ((m_shakingTime - deltaTime) / (m_shakingTime - m_decreasingTime));
            }
            else if (deltaTime > m_shakingTime)
            {
                amplitude = 0;
                m_isShaking = false;
                transform.position = m_position;
            }
            m_transformToShake.transform.position = m_position + Random.insideUnitSphere * amplitude;
            yield return new WaitForEndOfFrame();
        }
    }

    public void ShakeObjectAfter(float waitingTime)
    {
        StartCoroutine(ShakeObjectCoroutine(waitingTime));
    }

    public IEnumerator ShakeObjectCoroutine(float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);
        StartShake();
    }
}
