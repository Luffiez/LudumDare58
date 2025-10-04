using UnityEngine;

public class SpriteOrderByY : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        spriteRenderer.sortingOrder = -(int)(transform.position.y * 100);
    }
}
