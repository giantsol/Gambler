using UnityEngine;

public class AmmoShooter : MonoBehaviour {
    
	public GameObject ammo;
	public Transform ammoSpawnPoint;
    private AmmoInfo ammoInfo;
    private float ammoFireRate;
    private float lastFiredTime;
    private string ammoPoolKey;

    private void Start() {
        ammoInfo = ammo.GetComponent<AmmoInfo>();
        ammoFireRate = ammoInfo.fireRate;
        lastFiredTime = ammoFireRate;
        ammoPoolKey = ammoInfo.ammoPoolKey;
        
        AmmoPool.instance.CreateAmmoPool(ammoPoolKey, ammo, ammoInfo.ammoPoolSize);
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
        AmmoPool.instance.RemoveAmmoPool(ammoPoolKey);
    }
}
