using UnityEngine;

public class FlappyBird : MonoBehaviour {
    private static readonly string STRING_HORIZONTAL = "Horizontal";
    
    public Vector2 upForceWhenClick = new Vector2(0, 200f);
    
    private Rigidbody2D rigidBody;
    
    private BoxCollider2D collider;
    private float halfColliderScreenSize;
    
    private Camera mainCamera;
    private Vector2 screenPos;

    private Vector2 velocity = Vector2.zero;
    public float maxSpeed = 5f;

    private SpriteRenderer spriteRenderer;
    
    // Start is called before the first frame update
    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        
        collider = GetComponent<BoxCollider2D>();
        
        mainCamera = Camera.main;

        float x2 =  mainCamera.WorldToScreenPoint(new Vector3(collider.size.x / 2, 0f)).x;
        float x1 =  mainCamera.WorldToScreenPoint(new Vector3(0f, 0f)).x;
        halfColliderScreenSize = x2 - x1;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        screenPos = mainCamera.WorldToScreenPoint(transform.position);
        // get current velocity
        velocity = rigidBody.velocity;
        
        // 일단 키 input 받은거대로 velocity에 적용
        velocity.x = Input.GetAxis(STRING_HORIZONTAL) * maxSpeed;
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) {
            velocity.y = 0f;
            rigidBody.AddForce(upForceWhenClick);
        }

        // 스프라이트 flipping 일단 조절하고
        bool flipSprite = spriteRenderer.flipX ? velocity.x > 0.01f : velocity.x < -0.01f;
        if (flipSprite) {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        if (velocity.x < -0.01f && screenPos.x - halfColliderScreenSize <= 0.01f) {
            velocity.x = 0f;
        } else if (velocity.x > 0.01f && screenPos.x + halfColliderScreenSize >= Screen.width - 0.01f) {
            velocity.x = 0f;
        }
        
        rigidBody.velocity = velocity;
    }
}
