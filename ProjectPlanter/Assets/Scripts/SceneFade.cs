using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


// 씬과 씬 사이의 페이드 인/아웃을 구현한 코드입니다


public class SceneFade : MonoBehaviour
{
    private static SceneFade instance = null;
    private Image fadeImage;
    public float fadeDuration;
    AsyncOperation ao;

    private void Awake()
    {

        // 씬 전환용 게임오브젝트는 모든 씬에서 돌려쓴다

        if (instance != null) // 메인 씬으로 돌아와서, SceneFade오브젝트가 중복되어 존재할 경우를 방지
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 변경되도 사운드 오브젝트가 파괴되지 않음
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // 페이드 인/아웃에 사용될 이미지를 가져온다
        fadeImage = GetComponentInChildren<Image>();

        // 씬이 전환되는것을 감지하고, 페이드 아웃 코드를 실행하기 위한 준비
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode mode) // 씬 로드가 감지된 경우
    {
        // 페이드 아웃 완료 전에 씬 로드 금지
        ao = SceneManager.LoadSceneAsync(scene.buildIndex);
        ao.allowSceneActivation = false;

        // 페이드 인/아웃 코드 실행
        StartCoroutine(SceneChangeSequence());
    }

    // 페이드 인/아웃 코드
    IEnumerator SceneChangeSequence()
    {
        float fadeTimeElapse = 0f;
        Color imageColor = fadeImage.color;
        imageColor.a = 0f;

        // 지정된 시간 동안 페이드 아웃
        while (fadeTimeElapse < fadeDuration)
        {
            fadeTimeElapse += Time.deltaTime;
            imageColor.a = Mathf.Clamp01(fadeTimeElapse / fadeDuration);
            fadeImage.color = imageColor;
            yield return null;
        }

        // 다음 씬 로드 완료까지 대기
        while(!ao.isDone)
        {
            yield return null;
        }
        ao.allowSceneActivation = true;

        yield return null;
    }
}
