using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Лента от шредера до края: везёт металлолом, доехавшее до края пропадает
public class ScrapConveyor : MonoBehaviour
{
    // Пасть шредера: отсюда выезжают предметы, по ней же высота ленты
    public Transform spawnPoint;
    public Transform beltEnd;
    public ScrapItem[] scrapPrefabs;

    public float speed { get; set; }

    // Самый новый предмет, ближайший к шредеру, в конце списка
    private readonly List<ScrapItem> items = new();
    private ScrapBag bag;
    private float nextGap;
    private bool running;

    public IReadOnlyList<ScrapItem> scrapItems {
        get { return items; }
    }

    public void startBelt(ScrapBag scrapBag) {
        bag = scrapBag;
        fillBelt();
        running = true;
    }

    public void stopBelt() {
        running = false;
        speed = 0f;
    }

    public void Update() {
        if (!running) {
            return;
        }

        Vector3 shift = Vector3.right * speed * Time.deltaTime;
        float rightEdge = beltEnd.position.x;
        for (int i = items.Count - 1; i >= 0; i--) {
            ScrapItem item = items[i];
            item.transform.position += shift;
            if (item.bounds.min.x > rightEdge) {
                items.RemoveAt(i);
                Destroy(item.gameObject);
            }
        }

        ScrapItem newest = items.LastOrDefault();
        if (newest == null || newest.bounds.min.x - spawnPoint.position.x >= nextGap) {
            // Новый предмет стоит целиком внутри шредера и выезжает из пасти
            ScrapItem item = spawn();
            moveLeftEdge(item, spawnPoint.position.x - item.bounds.size.x);
            nextGap = randomGap();
        }
    }

    // В начале раунда лента уже заполнена до правого края
    private void fillBelt() {
        float rightEdge = beltEnd.position.x;
        float x = spawnPoint.position.x + randomGap();
        while (x < rightEdge) {
            ScrapItem item = spawn();
            moveLeftEdge(item, x);
            x = item.bounds.max.x + randomGap();
        }

        items.Reverse();
        nextGap = randomGap();
    }

    private ScrapItem spawn() {
        ScrapType type = bag.next();
        ScrapItem prefab = scrapPrefabs.First(value => value.type == type);
        ScrapItem item = Instantiate(prefab, spawnPoint.position, Quaternion.identity, transform);
        items.Add(item);
        return item;
    }

    private void moveLeftEdge(ScrapItem item, float x) {
        item.transform.position += Vector3.right * (x - item.bounds.min.x);
    }

    private float randomGap() {
        return Random.Range(ArmorCollectionConsts.minItemGap, ArmorCollectionConsts.maxItemGap);
    }
}
