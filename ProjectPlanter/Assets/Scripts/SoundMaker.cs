using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// 배경 음악, 버튼 클릭 사운드 등을 관리하는 코드입니다


public class SoundMaker : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public AudioSource buttonClick;
    public AudioSource backgroundClick;

    // Start is called before the first frame update
    void Start() // 시작 시 저장된 볼륨 설정
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
