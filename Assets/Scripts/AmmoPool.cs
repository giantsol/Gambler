using System.Collections.Generic;
using UnityEngine;

public class AmmoPool : MonoBehaviour {
    
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
            pool[i] = Instantiate(ammo, poolsPosition, Quaternion.identity);
        }
    }

    public GameObject GetAmmo(string key) {
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
}
