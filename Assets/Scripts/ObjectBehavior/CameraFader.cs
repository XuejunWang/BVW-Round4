using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Virtuix;

public class CameraFader : MonoBehaviour {
    public BGMController bgmController;
    public OmniController omniController;


    public GameObject shipAmbientSound;
    public GameObject letter;

    public UnityEvent event_FadeInComplete;
    public UnityEvent event_FadeOutComplete;

    private float playerMovingSpeed = 2.2f;
    private float hornDelayed = 0.1f;

    SpriteRenderer m_fadeSprite;
    
    [SerializeField] float m_fadingTime = 0.1f; 
    private float m_timer;

    // Use this for initialization
    void Start ()
    {
        bgmController.event_HornFinished.AddListener(ResponceAfterHorn);

        if (!m_fadeSprite)
        {
            m_fadeSprite = GetComponent<SpriteRenderer>();
        }
        //CameraFadeIn();
    }

    private void ResponceAfterHorn()
    {
        StartCoroutine(ResponseCoroutine());
       
    }

    private IEnumerator ResponseCoroutine()
    {
        yield return new WaitForSeconds(hornDelayed);
        letter.SetActive(false);
        CameraFadeIn();
        omniController.speed = playerMovingSpeed;
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void CameraFadeIn()
    {
        FadeAlphaFromTo(1, 0, m_fadingTime, true);
    }

    public void CameraFadeInAfter(float time)
    {
        StartCoroutine(CameraFadeInCoroutine(time));
    }

    public void CameraFadeOutAfter(float time)
    {
        StartCoroutine(CameraFadeOutCoroutine(time));
    }

    private IEnumerator CameraFadeInCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        CameraFadeIn();
    }

    private IEnumerator CameraFadeOutCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        CameraFadeOut();
    }

    public void CameraFadeOut()
    {
        FadeAlphaFromTo(0, 1, m_fadingTime, false);
    }

    private void FadeAlphaFromTo(float from, float end, float time, bool fadeInOrOut)
    {
        m_timer = Time.time;
        StartCoroutine(FadeCoroutine(from, end, time, fadeInOrOut));
    }
    IEnumerator FadeCoroutine(float from, float end, float time, bool fadeInOrOut)
    {
        float t = 0;
        while (t < time)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(from, end, t / time);
            //print(alpha);
            // set alpha to the image
            Color color = m_fadeSprite.color;
            m_fadeSprite.color = new Vector4(color.r, color.g, color.b, alpha);
            yield return new WaitForEndOfFrame();
        }
        //shipAmbientSound.SetActive(true);
        if (fadeInOrOut)
        {
            event_FadeInComplete.Invoke();
        }
        else
        {
            event_FadeOutComplete.Invoke();
        }
    }
}
