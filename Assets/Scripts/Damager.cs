using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour {
    
    public interface OnDoneDamageCallback {
        void OnDoneDamage(GameObject self);
    }

    public int damage = 1;
    
    private int enemiesLayer;

    private readonly List<OnDoneDamageCallback> onDoneDamageCallbacks = new List<OnDoneDamageCallback>();

    private void Start() {
        enemiesLayer = LayerMask.NameToLayer("Enemies");
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == enemiesLayer) {
            GameObject otherObject = other.gameObject;
            Damageable damageable = otherObject.GetComponent<Damageable>();
            if (damageable != null) {
                damageable.DoDamage(damage);

                DispatchOnDoneDamage();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.layer == enemiesLayer) {
            GameObject otherObject = other.gameObject;
            Damageable damageable = otherObject.GetComponent<Damageable>();
            if (damageable != null) {
                damageable.DoDamage(damage);

                DispatchOnDoneDamage();
            }
        }
    }

    private void DispatchOnDoneDamage() {
        foreach (var callback in onDoneDamageCallbacks) {
            callback.OnDoneDamage(gameObject);
        }
    }

    public void RegisterOnDoneDamageCallback(OnDoneDamageCallback callback) {
        onDoneDamageCallbacks.Add(callback);
    }
    
    public void UnregisterOnDoneDamageCallback(OnDoneDamageCallback callback) {
        onDoneDamageCallbacks.Remove(callback);
    }
}
