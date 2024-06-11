using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// ��� ����, ��ư Ŭ�� ���� ���� �����ϴ� �ڵ��Դϴ�


public class SoundMaker : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public AudioSource buttonClick;
    public AudioSource backgroundClick;

    // Start is called before the first frame update
    void Start() // ���� �� ����� ���� ����
    {
        float volume = PlayerPrefs.GetFloat("volume");

        backgroundMusic.volume = volume;
        buttonClick.volume = volume;
        backgroundMusic.volume = volume;

        backgroundMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MenuScene")
        {
            float volume = PlayerPrefs.GetFloat("volume");

            backgroundMusic.volume = volume;
            buttonClick.volume = volume;
            backgroundMusic.volume = volume;
        }
    }
}
