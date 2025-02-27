using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource bgmSource; // �����
    public AudioSource sfxSource; // ȿ����

    public AudioClip bgmClip; // ����� Ŭ��
    public AudioClip[] sfxClips; // ���� ���� ȿ���� ����

    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(gameObject); // ���� �ٲ� ����
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    private void Start()
    {
        PlayBGM();
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

