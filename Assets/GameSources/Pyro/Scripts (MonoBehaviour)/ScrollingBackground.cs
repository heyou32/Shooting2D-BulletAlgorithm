using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float speed = 1;
    MeshRenderer meshRenderer;
    Vector2 vec2;
    void Start()
    {
        meshRenderer = gameObject.GetComponentNoAlloc<MeshRenderer>();
    }
    void Update()
    {
        vec2.y = Time.deltaTime * speed;
        meshRenderer.material.mainTextureOffset = vec2;
    }
}
