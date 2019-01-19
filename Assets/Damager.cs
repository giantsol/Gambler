using UnityEngine;

public class Damager : MonoBehaviour {
    
    public interface DoDamageCallback {
        void OnDoneDamage();
    }

    public int damage = 1;
    
    private int enemiesLayer;

    public DoDamageCallback doDamageCallback;

    private void Start() {
        enemiesLayer = LayerMask.NameToLayer("Enemies");
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.layer == enemiesLayer) {
            GameObject otherObject = other.gameObject;
            Damageable damageable = otherObject.GetComponent<Damageable>();
            if (damageable != null) {
                damageable.DoDamage(damage);

                doDamageCallback?.OnDoneDamage();
            }
        }
    }
}
