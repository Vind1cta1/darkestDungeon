using UnityEngine;

public class BackGroundScroll : MonoBehaviour
{
    public float scrollSpeed;
    public ChestController chestController;
    private Renderer backGroundRenderer;

    private void Start()
    {
        backGroundRenderer = GetComponent<Renderer>();
        backGroundRenderer.material.mainTexture.wrapMode = TextureWrapMode.Repeat;
    }

    private void Update()
    {
        if (!chestController.isOpen)
        {
            float offset = Time.time * scrollSpeed;
            Vector2 offsetVector = new Vector2(offset, 0);
            backGroundRenderer.material.SetTextureOffset("_MainTex", offsetVector);
            
        }
    }
}