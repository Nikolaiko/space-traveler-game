using UnityEngine;

public class SocobanPlayer : MonoBehaviour
{    
    public bool Move(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) < 0.5) {
            direction.x = 0;
        } else {
            direction.y = 0;
        }

        direction.Normalize();

        if (IsBlocked(transform.position, direction).isBlockedByBox) {
            if (IsBlocked(transform.position, direction).isBlocked) {                
                return false;
            } else {                
                transform.Translate(direction);                
            }
        } else {
            if (IsBlocked(transform.position, direction).isBlocked) {                
                return false;
            } else {                
                transform.Translate(direction);                
            }
        }
        return true;
    }

    BlockedBy IsBlocked(Vector3 position, Vector2 direction)
    {
        Vector2 newPos = new Vector2(position.x, position.y) + direction;
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        BlockedBy blocked;

        foreach (var wall in walls)
        {
            if (wall.transform.position.x == newPos.x && wall.transform.position.y == newPos.y)
            {
                blocked.isBlocked = true;
                blocked.isBlockedByBox = false;
                return blocked;
            }
        }

        GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box");

        foreach (var box in boxes) {
            if (box.transform.position.x == newPos.x && box.transform.position.y == newPos.y) {
                SocobanBox theBox = box.GetComponent<SocobanBox>();
                if (theBox && theBox.Move(direction)) {
                    blocked.isBlocked = false;
                    blocked.isBlockedByBox = true;                
                    return blocked;
                } else {
                    blocked.isBlocked = true;
                    blocked.isBlockedByBox = true;
                    return blocked;
                }
            }
        }
        blocked.isBlocked = false;
        blocked.isBlockedByBox = false;
        
        return blocked;
    }
}
