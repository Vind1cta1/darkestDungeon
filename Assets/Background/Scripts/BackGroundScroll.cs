using UnityEngine;

public class BackGroundScroll : MonoBehaviour
{
    [SerializeField] private float scrollSpeed;
    private Renderer backGroundRenderer;

    void Start()
    {
        backGroundRenderer = GetComponent<Renderer>();
        backGroundRenderer.material.mainTexture.wrapMode = TextureWrapMode.Repeat;
    }

    void Update()
    {
        float offset = Time.time * scrollSpeed;

        Vector2 offsetVector = new Vector2(offset, 0);

        backGroundRenderer.material.SetTextureOffset("_MainTex", offsetVector);
    }
}