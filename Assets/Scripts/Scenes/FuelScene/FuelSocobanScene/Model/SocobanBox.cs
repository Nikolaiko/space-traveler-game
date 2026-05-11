using UnityEngine;

public class SocobanBox : MonoBehaviour
{
    public delegate void ArrivedHandler(SocobanBoxType boxType);
    public event ArrivedHandler onBoxArrived;

    public bool arrived;
    public SocobanBoxType boxType;

    public bool Move(Vector2 direction) {
        if (IsBoxBlocked(transform.position, direction)) { return false; } 
        else {
            transform.Translate(direction);            
            ArrivedOnCross();
            return true;
        }
    }

    public bool IsBoxBlocked(Vector3 position, Vector2 direction) {
        Vector2 newPos = new Vector2(position.x, position.y) + direction;
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        foreach (var wall in walls) {
            if (wall.transform.position.x == newPos.x && wall.transform.position.y == newPos.y) {
                return true;
            }
        }

        GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box");
        foreach (var box in boxes) {
            if (box.transform.position.x == newPos.x && box.transform.position.y == newPos.y) {
                Debug.Log("Blocked by another Box");
                return true;
            }
        }
        return false;
    }

    public void ArrivedOnCross() {
        GameObject[] slots = GameObject.FindGameObjectsWithTag("Slot");
        SpriteRenderer boxColor = GetComponent<SpriteRenderer>();
        foreach(var slot in slots) {
            if (transform.position.x == slot.transform.position.x && transform.position.y == slot.transform.position.y) {
                boxColor.color = Color.green;
                boxColor.sortingOrder = 100;
                arrived = true;
                onBoxArrived?.Invoke(boxType);
                return;
            }
        }
        boxColor.color = Color.white;
        arrived = false;
    }
}
