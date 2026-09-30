using UnityEngine;

public class ScrapItem : MonoBehaviour
{
    public ScrapType type;
    // Картинки одного типа: покрышка и доска, ванна и холодильник
    public Sprite[] spriteVariants;

    private SpriteRenderer spriteRenderer;

    public Bounds bounds {
        get { return spriteRenderer.bounds; }
    }

    public void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteVariants.Length > 0) {
            spriteRenderer.sprite = spriteVariants[Random.Range(0, spriteVariants.Length)];
        }
    }
}
