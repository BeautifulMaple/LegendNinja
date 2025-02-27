using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    private AudioSource bgmSource; // �����
    private AudioSource sfxSource; // ȿ����

    public AudioClip bgmClip; // ����� Ŭ��
    public AudioClip[] sfxClips; // ���� ���� ȿ���� ����
    private void Awake()
    {
        if (instance == null)
        {
            instance = this; 
            DontDestroyOnLoad(gameObject); // ���� �ٲ� �� �����
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        if (bgmSource == null)
        {
            bgmSource = gameObject.AddComponent<AudioSource>();
        }

        if (sfxSource == null)
        {
            sfxSource = gameObject.AddComponent<AudioSource>();
        }

        bgmClip = Resources.Load<AudioClip>("Audio/TestBGM2"); 
        sfxClips = new AudioClip[3]; 

        sfxClips[0] = Resources.Load<AudioClip>("Audio/Shuriken");
        //sfxClips[1] = Resources.Load<AudioClip>("Sound/");
        //sfxClips[2] = Resources.Load<AudioClip>("Sound/");


        // ���� ����
        bgmSource.volume = 0.4f;
        sfxSource.volume = 0.25f;

        PlayBGM(); // ����� ����
    }

    public void PlayBGM()
    {
        if (bgmSource != null && bgmClip != null)
        {
            bgmSource.clip = bgmClip;
            bgmSource.loop = true;
            bgmSource.Play();
        }
    }

    public void PlaySFX(int index)
    {
        if (sfxSource != null && index >= 0 && index < sfxClips.Length)
        {
            sfxSource.PlayOneShot(sfxClips[index]);
        }
    }
}

