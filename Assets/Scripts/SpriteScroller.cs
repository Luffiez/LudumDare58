using UnityEngine;

public class SpriteScroller : MonoBehaviour
{
    public float ScrollingSpeed = 1;
    Vector2 CurrentScroll;

    SpriteRenderer SpriteRenderer;
    Material Material;

    void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Material = SpriteRenderer.material;
        float RandomStartingOffset = Random.Range(0, 1);
        CurrentScroll.y = RandomStartingOffset;
    }

    void Update()
    {
        float CurrentFrameScrollingSpeed = ScrollingSpeed * Time.deltaTime;
        float ScrollSpeedInNormalizedTextureOffsetSpace = CurrentFrameScrollingSpeed / SpriteRenderer.bounds.size.y;
        CurrentScroll.y += ScrollSpeedInNormalizedTextureOffsetSpace;
        Material.mainTextureOffset = CurrentScroll;
    }
}
