using UnityEngine;

// Растягивает землю и ленту на ширину камеры, ставит шредер к левому краю,
// а конец ленты к правому, чтобы свалка занимала весь экран при любом соотношении сторон
public class ScrapyardLayout : MonoBehaviour
{
    public Camera mainCamera;
    public SpriteRenderer ground;
    public SpriteRenderer belt;
    public Transform shredder;
    public Transform beltEnd;

    private float laidOutAspect;

    public void Awake() {
        layout();
    }

    // Экран могут повернуть, а окно растянуть
    public void Update() {
        if (mainCamera.aspect != laidOutAspect) {
            layout();
        }
    }

    private void layout() {
        laidOutAspect = mainCamera.aspect;

        float left = mainCamera.ViewportToWorldPoint(Vector3.zero).x;
        float right = mainCamera.ViewportToWorldPoint(Vector3.one).x;
        float bottom = mainCamera.ViewportToWorldPoint(Vector3.zero).y;
        float centerX = (left + right) / 2f;

        belt.size = new Vector2(right - left, belt.size.y);
        belt.transform.position = new Vector3(centerX, belt.transform.position.y, belt.transform.position.z);

        float groundTop = ground.transform.position.y;
        ground.size = new Vector2(right - left, groundTop - bottom);
        ground.transform.position = new Vector3(centerX, groundTop, ground.transform.position.z);

        shredder.position = new Vector3(left, shredder.position.y, shredder.position.z);
        beltEnd.position = new Vector3(right, beltEnd.position.y, beltEnd.position.z);
    }
}
