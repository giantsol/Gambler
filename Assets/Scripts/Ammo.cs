using UnityEngine;

public class Ammo : MonoBehaviour, Damager.OnDoneDamageCallback {

    private Damager damager;

    public Transform ammoPoolPosition;

    private void Start() {
        damager = GetComponent<Damager>();
        damager.RegisterOnDoneDamageCallback(this);
    }

    public void OnDoneDamage(GameObject self) {
        transform.position = ammoPoolPosition.position;
    }

    private void OnDestroy() {
        damager.UnregisterOnDoneDamageCallback(this);
    }
}
