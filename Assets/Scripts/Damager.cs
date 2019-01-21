using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour {
    
    public interface OnDoneDamageCallback {
        void OnDoneDamage(GameObject self);
    }

    public int damage = 1;

    public string opponentLayerName;
    private int opponentLayer;

    private readonly List<OnDoneDamageCallback> onDoneDamageCallbacks = new List<OnDoneDamageCallback>();

    private void Start() {
        opponentLayer = LayerMask.NameToLayer(opponentLayerName);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == opponentLayer) {
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
