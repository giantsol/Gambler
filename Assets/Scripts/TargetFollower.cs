using UnityEngine;

public class TargetFollower : MonoBehaviour {
    
    public Transform target;
    public float maxSpeed = 2f;
    public float acceleration = 50f;
    public float directionChangeDrag = 0.5f;
    private bool isHeadingLeft = true; // 실제로 왼쪽으로 움직이고 있든 없든, 왼쪽으로 가려고 노력하고 있는지.
    
    private Rigidbody2D rigidBody;
    private Vector2 accelerationVector = Vector2.zero; // 단순히 빈번히 사용하기 위한 벡터 객체..
    private SpriteRenderer spriteRenderer;

    private Damageable damageable;
    
    private void Start() {
        if (target == null) {
            target = GameObject.FindWithTag("Player").transform;
        }
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        damageable = GetComponent<Damageable>();
    }

    private void FixedUpdate() {
        if (damageable.isDead) {
            return;
        }
        
        Vector2 currentVelocity = rigidBody.velocity;
        Vector2 currentPos = transform.position;
        Vector2 targetPosition = target.position;

        // currently going left and target is at the right more than drag value
        if (isHeadingLeft && targetPosition.x - currentPos.x - directionChangeDrag > 0f) {
            isHeadingLeft = false;
        } else if (!isHeadingLeft && currentPos.x - targetPosition.x - directionChangeDrag > 0f) {
            isHeadingLeft = true;
        }

        if (isHeadingLeft) {
            if (currentVelocity.x > -maxSpeed) {
                accelerationVector.x = -acceleration;
            } else {
                accelerationVector.x = 0f;
            }
        } else {
            if (currentVelocity.x < maxSpeed) {
                accelerationVector.x = acceleration;
            } else {
                accelerationVector.x = 0f;
            }
        }
        rigidBody.AddForce(accelerationVector);
        
        bool flip = spriteRenderer.flipX ? currentVelocity.x < 0f : currentVelocity.x > 0f;
        if (flip) {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }

}
