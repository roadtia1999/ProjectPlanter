using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStartSceneManager : MonoBehaviour
{
    public Slider loadingSlider;
    public float loadingWaitSequence;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadGame()); // ���� ���� �� Ÿ��Ʋ�� ������ �� ���� �� �ε� ����
    }

    IEnumerator LoadGame() // �� �ε� ���μ���
    {
        SceneFade sf = GameObject.Find("FadeCanvas").GetComponent<SceneFade>();
        yield return new WaitForEndOfFrame();

        AsyncOperation ao = SceneManager.LoadSceneAsync("_MainScene");
        ao.allowSceneActivation = false;

        while (!ao.isDone)
        {
            loadingSlider.value = ao.progress;

            if (ao.progress >= 0.9f)
            {
                break;
            }

            yield return null;
        }
        yield return new WaitForSeconds(loadingWaitSequence);

        loadingSlider.value = 1f;
        yield return new WaitForSeconds(loadingWaitSequence);
        sf.SceneChange(ao);
    }
}
