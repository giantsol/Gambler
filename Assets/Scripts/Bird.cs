using UnityEngine;

public class Bird : MonoBehaviour {

    public string animTriggerFlapName = "Flap";
    public string animTriggerDieName = "Die";
    public Vector2 upForceWhenClick = new Vector2(0, 200f);

    private Animator animator;
    private Rigidbody2D rigidBody;
    private bool isDead = false;
    
    // Start is called before the first frame update
    private void Start() {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update() {
        if (!isDead) {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) {
                animator.SetTrigger(animTriggerFlapName);
                rigidBody.velocity = Vector2.zero;
                rigidBody.AddForce(upForceWhenClick);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        rigidBody.velocity = Vector2.zero;
        isDead = true;
        animator.SetTrigger(animTriggerDieName);

        GameController.instance.BirdDied();
    }
}
