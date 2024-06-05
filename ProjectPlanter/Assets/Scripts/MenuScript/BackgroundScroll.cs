using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 메뉴 창에서 뒷배경을 반복해서 움직여주는 코드입니다


public class BackgroundScroll : MonoBehaviour
{
    private Renderer renderer;
    public float scrollSpeed;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // 움직여야 하는 거리 계산
        float move = Mathf.Repeat(Time.time * scrollSpeed, 1);
        Vector2 offsetMove = new Vector2(move, move);

        // offsetMove로 설정한 값 만큼 배경 움직임
        renderer.sharedMaterial.SetTextureOffset("_MainTex", offsetMove);
    }
}
