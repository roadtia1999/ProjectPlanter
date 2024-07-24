using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnimation : MonoBehaviour
{
    private bool bubble;
    public float bubbleMove;
    public float stateMove;
    public float waitSeconds;

    private void Awake()
    {
        if (gameObject.name.Contains("Buble"))
        {
            bubble = true;
        }
        else
        {
            bubble = false;
        }

        StartCoroutine(ButtonAni());
    }

    IEnumerator ButtonAni()
    {
        float moveDistance;
        RectTransform objPosition = gameObject.GetComponent<RectTransform>();
        gameObject.transform.SetParent(transform, false);
        Vector2 movePosition;

        if (bubble)
        {
            moveDistance = bubbleMove;
        }
        else
        {
            moveDistance = stateMove;
        }
        movePosition = new Vector2(0, moveDistance);

        while (true)
        {
            objPosition.anchoredPosition += movePosition;
            yield return new WaitForSeconds(waitSeconds);

            objPosition.anchoredPosition -= movePosition;
            yield return new WaitForSeconds(waitSeconds);
        }
    }
}
