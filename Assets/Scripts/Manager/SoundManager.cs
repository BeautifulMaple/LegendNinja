using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    private AudioSource bgmSource; // �����
    private AudioSource sfxSource; // ȿ����

    public AudioClip[] bgmClips; // ����� Ŭ��
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

        SceneManager.sceneLoaded += OnSceneLoaded; 
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

        bgmClips = new AudioClip[3];
        sfxClips = new AudioClip[3];

        bgmClips[0] = Resources.Load<AudioClip>("Audio/Rain");
        bgmClips[1] = Resources.Load<AudioClip>("Audio/TestBGM2");
        bgmClips[2] = Resources.Load<AudioClip>("Audio/TestBGM");

        sfxClips[0] = Resources.Load<AudioClip>("Audio/Shuriken");
        sfxClips[1] = Resources.Load<AudioClip>("Audio/Click2");
        //sfxClips[2] = Resources.Load<AudioClip>("Audio/");


        // ���� ����
        bgmSource.volume = 0.4f;
        sfxSource.volume = 0.5f;

        PlayBGM(0); // ����� ����
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "StartScene")
        {
            PlayBGM(0); 
        }
        else if (scene.name == "SampleScene")
        {
            PlayBGM(1); 
        }
        else if (scene.name == "EndScene")
        {
            PlayBGM(2);
        }
    }

    public void PlayBGM(int index)
    {
        if (bgmSource != null && index >= 0 && index < bgmClips.Length)
        {
            bgmSource.clip = bgmClips[index];
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

