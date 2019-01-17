using UnityEngine;

public class BlackKitty : MonoBehaviour {

    public Transform target;
    public float maxSpeed = 2f;
    public float force = 50f;

    private Vector2 movePosition;

    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;

    private void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update() {
        bool isTargetLeft = transform.position.x - target.position.x > 0f;
        if (isTargetLeft) {
            rigidBody.AddForce(Vector2.left * force);
            if (rigidBody.velocity.x < -maxSpeed) {
                rigidBody.velocity = new Vector2(-maxSpeed, rigidBody.velocity.y);
            }
        } else {
            rigidBody.AddForce(Vector2.right * force);
            if (rigidBody.velocity.x > maxSpeed) {
                rigidBody.velocity = new Vector2(maxSpeed, rigidBody.velocity.y);
            }
        }

        bool flip = spriteRenderer.flipX ? rigidBody.velocity.x < 0f : rigidBody.velocity.x > 0f;
        if (flip) {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }
}
