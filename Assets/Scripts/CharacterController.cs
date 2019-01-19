using UnityEngine;

public class CharacterController : MonoBehaviour {
    private static readonly string STRING_HORIZONTAL = "Horizontal";
    
    private Rigidbody2D rigidBody;
    
    public Vector2 jumpForce = new Vector2(0, 200f);
    
    // 카메라 시야 안에서만 움직일 수 있는지
    public bool isConstrainedInsideCamera = true;
    private float leftmostX;
    private float rightmostX;
    
    private Collider2D collider;
    private float halfWidth;
    
    public float maxSpeed = 5f;
    
    private bool isFlipped = false;

    public bool jumpOnlyOnGround = true;
    private bool isGrounded;
    private int groundLayer;
    
    private void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        
        Camera mainCamera = Camera.main;
        if (mainCamera != null) {
            leftmostX = mainCamera.ScreenToWorldPoint(Vector2.zero).x;
            rightmostX = mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x;
        } else {
            isConstrainedInsideCamera = false;
        }

        collider = GetComponent<Collider2D>();
        halfWidth = collider.bounds.size.x / 2;
        
        groundLayer = LayerMask.NameToLayer("Ground");
    }

    private void FixedUpdate() {
        Vector2 currentPos = transform.position;
        Vector2 velocity = rigidBody.velocity;
        
        // 일단 키 input 받은거대로 velocity에 적용
        velocity.x = Input.GetAxis(STRING_HORIZONTAL) * maxSpeed;
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) {
            if (!jumpOnlyOnGround || (jumpOnlyOnGround && isGrounded)) {
                velocity.y = 0f;
                rigidBody.AddForce(jumpForce);
            }
        }
        
        // flipping 일단 조절하고
        bool flip = isFlipped ? velocity.x > 0.01f : velocity.x < -0.01f;
        if (flip) {
            if (isFlipped) {
                transform.rotation = Quaternion.AngleAxis(0f, Vector3.up);
            } else {
                transform.rotation = Quaternion.AngleAxis(180f, Vector3.up);
            }
            isFlipped = !isFlipped;
        }

        if (isConstrainedInsideCamera) {
            if (velocity.x < -0.01f && currentPos.x - halfWidth <= leftmostX) {
                velocity.x = 0f;
            } else if (velocity.x > 0.01f && currentPos.x + halfWidth >= rightmostX) {
                velocity.x = 0f;
            }
        }

        rigidBody.velocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.layer == groundLayer) {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.layer == groundLayer) {
            isGrounded = false;
        }
    }
}
