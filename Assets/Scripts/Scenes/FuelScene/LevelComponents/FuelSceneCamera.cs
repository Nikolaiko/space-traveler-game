using UnityEngine;

public class FuelSceneCamera : MonoBehaviour
{
    public int yOffset = 0;
    public GameObject target;
    public GameObject leftBorder;
    public GameObject rightBorder;
    
    public void Start()
    {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
    }

    public void Update()
    {
        Vector3 newPosition = transform.position;
        newPosition.x = target.transform.position.x;
        newPosition.y = target.transform.position.y + yOffset;
        
        newPosition.x = Mathf.Clamp(newPosition.x, leftBorder.transform.position.x, rightBorder.transform.position.x);

        transform.position = newPosition;
    }
}
