using UnityEngine;

public class FuelStoreDoor : MonoBehaviour
{
    public FuelStoreDoorType doorType = FuelStoreDoorType.normal;

    public void destroyDoor() {
        Destroy(gameObject);
    }
}
