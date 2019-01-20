using UnityEngine;

public class AmmoShooter : MonoBehaviour {
    
	public GameObject ammoObject;
	public Transform ammoSpawnPoint;
    private float ammoFireRate;
    private float lastFiredTime;
    private string ammoPoolKey;
    private int ammoPoolSize;

    private void Start() {
        InitiateAmmo(ammoObject);
        
        AmmoPool.instance.CreateAmmoPool(ammoPoolKey, ammoObject, ammoPoolSize);
    }

    private void InitiateAmmo(GameObject ammoObject) {
        Ammo ammo = ammoObject.GetComponent<Ammo>();
        ammoFireRate = ammo.fireRate;
        lastFiredTime = ammoFireRate;
        ammoPoolKey = ammo.ammoPoolKey;
        ammoPoolSize = ammo.ammoPoolSize;
    }

    private void Update() {
        lastFiredTime += Time.deltaTime;
        if (lastFiredTime >= ammoFireRate) {
            Transform ammoInstanceTransform = AmmoPool.instance.GetAmmo(ammoPoolKey).transform;
            ammoInstanceTransform.position = ammoSpawnPoint.position;
            ammoInstanceTransform.rotation = transform.rotation;
            lastFiredTime = 0f;
        }
    }

    private void OnDestroy() {
//        AmmoPool.instance.RemoveAmmoPool(ammoPoolKey);
    }
}
