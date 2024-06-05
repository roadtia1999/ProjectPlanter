using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// �޴� â���� �޹���� �ݺ��ؼ� �������ִ� �ڵ��Դϴ�


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
        // �������� �ϴ� �Ÿ� ���
        float move = Mathf.Repeat(Time.time * scrollSpeed, 1);
        Vector2 offsetMove = new Vector2(move, move);

        // offsetMove�� ������ �� ��ŭ ��� ������
        renderer.sharedMaterial.SetTextureOffset("_MainTex", offsetMove);
    }
}
