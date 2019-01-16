using UnityEngine;

public class MovingForwardObject : MonoBehaviour {

    public float speed;

    private Rigidbody2D rigidBody;
    
    // Start is called before the first frame update
    private void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        rigidBody.velocity = transform.right * speed;
    }

}
