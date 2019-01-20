using UnityEngine;

public class Damageable : MonoBehaviour {

    public int totalHealth = 1;
    public bool isDead = false;
    
    private Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    public void DoDamage(int damage) {
        totalHealth -= damage;

        if (totalHealth <= 0) {
            animator.SetTrigger("Die");
            isDead = true;
            gameObject.layer = LayerMask.NameToLayer("Dead");
        }
    }
    
    public void DestroySelf() {
        Destroy(gameObject);
    }
}
