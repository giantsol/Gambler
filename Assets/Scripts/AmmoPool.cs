using System.Collections.Generic;
using UnityEngine;

public class AmmoPool : MonoBehaviour, Damager.OnDoneDamageCallback {
    
    public static AmmoPool instance;

    private readonly Dictionary<string, GameObject[]> pools = new Dictionary<string, GameObject[]>();
    private readonly Dictionary<string, int> poolsCurrentItemIndexes = new Dictionary<string, int>();
    private readonly Vector2 poolsPosition = new Vector2(-20f, -20f);

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
    }

    public void CreateAmmoPool(string key, GameObject ammo, int poolSize) {
        if (poolSize <= 0) {
            return;
        }
        
        pools[key] = new GameObject[poolSize];
        poolsCurrentItemIndexes[key] = 0;
        GameObject[] pool = pools[key];

        for (int i = 0; i < poolSize; i++) {
            pool[i] = CreateAmmo(ammo, poolsPosition, Quaternion.identity);
        }
    }

    private GameObject CreateAmmo(GameObject ammo, Vector2 position, Quaternion quaternion) {
        GameObject go = Instantiate(ammo, position, quaternion);
        Damager damager = go.GetComponent<Damager>();
        damager.RegisterOnDoneDamageCallback(this);
        return go;
    }

    public GameObject GetAmmo(string key) {
        if (pools.ContainsKey(key)) {
            GameObject[] pool = pools[key];
            int currentIndex = poolsCurrentItemIndexes[key];
            
            GameObject item = pool[currentIndex];
            currentIndex++;
            if (currentIndex < pool.Length) {
                poolsCurrentItemIndexes[key] = currentIndex;
            } else {
                poolsCurrentItemIndexes[key] = 0;
            }

            return item;
        }

        return null;
    }

    public void RemoveAmmoPool(string key) {
        if (pools.ContainsKey(key)) {
            pools.Remove(key);
            poolsCurrentItemIndexes.Remove(key);
        }
    }

    public void OnDoneDamage(GameObject self) {
        self.transform.position = poolsPosition;
    }
}
