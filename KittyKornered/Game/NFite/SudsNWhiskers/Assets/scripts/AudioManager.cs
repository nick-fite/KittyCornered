using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager AudioInstance { get { return _instance; } }
    [SerializeField] AudioSource m_AudioSource;
    [SerializeField] AudioSource m_AudioSourceBack;
    [SerializeField] AudioClip ShootingClip;
    [SerializeField] AudioClip stompingClip;
    [SerializeField] AudioClip ratClip;
    [SerializeField] AudioClip ratDeathClip;
    [SerializeField] AudioClip bubbleClip;
    [SerializeField] float shootTimeStart;
    [SerializeField] float shootTimeStop;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void PlayShoot() {
        m_AudioSource.time = shootTimeStart;
        m_AudioSource.clip = ShootingClip;
        m_AudioSource.Play();
    }

    public void PlayStomp()
    {
        m_AudioSource.clip = stompingClip;
        m_AudioSource.Play();
    }

    public void PlayRatDeath()
    {
        m_AudioSource.clip = ratDeathClip;
        m_AudioSource.Play();
    }

    public void PlayBubblePop()
    {
        m_AudioSource.clip = bubbleClip;
        m_AudioSource.Play();
    }

    public void CleanUpMusic()
    {
    
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
