using UnityEngine;

public class Column : MonoBehaviour {
    public float minY;
    public float maxY;
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<Bird>() != null) {
            GameController.instance.BirdScored();
        }
    }
}
