using System;
using UnityEngine;

public class ScrollingObject : MonoBehaviour {

    private Rigidbody2D rigidBody;
    private float cachedScrollSpeed;
    private Vector2 velocity = Vector2.zero;
    
    // Start is called before the first frame update
    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        cachedScrollSpeed = GameController.instance.scrollSpeed;
        velocity.x = cachedScrollSpeed;
        rigidBody.velocity = velocity;
    }

    // Update is called once per frame
    void Update() {
        if (GameController.instance.gameOver) {
            rigidBody.velocity = Vector2.zero;
        } else if (Math.Abs(cachedScrollSpeed - GameController.instance.scrollSpeed) > 0.01f) {
            cachedScrollSpeed = GameController.instance.scrollSpeed;
            velocity.x = cachedScrollSpeed;
            rigidBody.velocity = velocity;
        }
    }
}

