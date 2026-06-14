using UnityEngine;
using UnityEngine.Tilemaps;

public class FuelSceneCharacterController : MonoBehaviour
{
    public delegate void OpenDoorHandler(FuelStoreDoor door);
    public event OpenDoorHandler onDoorOpened;

    public TilemapCollider2D tileMapCollider;
    public GameObject upperLeft;
    public GameObject lowerRight;
    public LayerMask groundLayerMask;

    private static float characterSpeed = 3.0f;
    private static float climbDistance = 0.8f;
    private static string walkParameterName = "isWalk";
    private static string climbParameterName = "isClimb";

    private bool isOnGround = false;
    private bool isOnUpperStairs = false;
    private bool isOnLowerStairs = false;
    private bool movingUp = false;
    private bool movingDown = false;
    private bool openForInputs = true;
    private Vector3 upperTarget = Vector3.zero;
    private Vector3 lowerTarget = Vector3.zero;
    private FuelStoreDoor collidedDoor;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D characterBody;

    float maxJumpHeight = 3.0f;
	
    public void Awake() {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        characterBody = GetComponent<Rigidbody2D>();
    }

    public void activate() {
        gameObject.SetActive(true);
    }

    public void deactivate() {
        gameObject.SetActive(false);
    }

    public void FixedUpdate() {               
        isOnGround = Physics2D.OverlapArea(
            upperLeft.transform.position,
            lowerRight.transform.position,
            groundLayerMask
        );

        if (openForInputs) {
            processInput();
        }

        if (movingUp) {
            tileMapCollider.enabled = false;            
            animator.SetBool(climbParameterName, true);

            Vector2 moveVector = Vector2.up;
            characterBody.MovePosition(
                characterBody.position + moveVector * Time.fixedDeltaTime * characterSpeed
            );

            Vector2 posZero = Vector2.zero;
            posZero.y = characterBody.position.y;

            Vector2 targetZero = Vector2.zero;
            targetZero.y = upperTarget.y;

            if (Vector2.Distance(posZero, targetZero) < climbDistance) {
                movingUp = false;
                tileMapCollider.enabled = true;
                openForInputs = true;
                animator.SetBool(climbParameterName, false);
            } 
        }

        if (movingDown) {
            tileMapCollider.enabled = false;
            animator.SetBool(climbParameterName, true);

            Vector2 moveVector = Vector2.down;
            characterBody.MovePosition(
                characterBody.position + moveVector * Time.fixedDeltaTime * characterSpeed
            );

            Vector2 posZero = Vector2.zero;
            posZero.y = characterBody.position.y;

            Vector2 targetZero = Vector2.zero;
            targetZero.y = lowerTarget.y;
            if (Vector2.Distance(posZero, targetZero) < climbDistance) {
                movingDown = false;
                tileMapCollider.enabled = true;
                openForInputs = true;
                animator.SetBool(climbParameterName, false);
            } 
        }
    }

    public void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == GameObjectTags.EASY_DOOR_TAG)
        {
            GameObject triggeredObject = collider.gameObject;
            FuelStoreDoor door = triggeredObject.GetComponent<FuelStoreDoor>();
            if (door != null) {
                collidedDoor = door;
            }
        }
        if (collider.tag == GameObjectTags.NORMAL_DOOR_TAG)
        {
            GameObject triggeredObject = collider.gameObject;
            FuelStoreDoor door = triggeredObject.GetComponent<FuelStoreDoor>();
            if (door != null) {
                collidedDoor = door;
            }
        }
        if (collider.tag == "StairsLower") {            
            isOnLowerStairs = true;
            upperTarget = collider.gameObject.GetComponent<StairsObject>().targetPosition.position;
        }
        if (collider.tag == "StairsUpper") {            
            isOnUpperStairs = true;
            lowerTarget = collider.gameObject.GetComponent<StairsObject>().targetPosition.position;
        }
    }

    public void OnTriggerExit2D(Collider2D collider) { 
        if (collider.tag == GameObjectTags.EASY_DOOR_TAG || collider.tag == GameObjectTags.NORMAL_DOOR_TAG)
        {
            GameObject triggeredObject = collider.gameObject;        
            FuelStoreDoor door = triggeredObject.GetComponent<FuelStoreDoor>();
            if (door == collidedDoor) {
                collidedDoor = null;
            }    
        }
        if (collider.tag == "StairsLower") {            
            isOnLowerStairs = false;            
        }
        if (collider.tag == "StairsUpper") {            
            isOnUpperStairs = false;            
        }        
    }

    private void processInput() {
        if (Input.GetKey(KeyCode.UpArrow) && isOnLowerStairs) {
            openForInputs = false;
            movingUp = true;
        }
        else if (Input.GetKey(KeyCode.DownArrow) && isOnUpperStairs) {
            openForInputs = false;
            movingDown = true;            
        }
        else if (Input.GetKey(KeyCode.LeftArrow)) {
            if (isOnGround) {
                spriteRenderer.flipX = true;
                characterBody.MovePosition(characterBody.position + Vector2.left * Time.fixedDeltaTime * characterSpeed);
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow)) {
            if (isOnGround) {
                spriteRenderer.flipX = false;
                characterBody.MovePosition(characterBody.position + Vector2.right * Time.fixedDeltaTime * characterSpeed);
            }            
        }
        else if (Input.GetKey(KeyCode.E) && collidedDoor != null){
            onDoorOpened?.Invoke(collidedDoor);
            collidedDoor = null;
        }

        bool isWalking = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow);        
        animator.SetBool(walkParameterName, isWalking);
    }
}
