using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPool : MonoBehaviour {
    
    public static AmmoPool instance;

    private readonly Dictionary<string, List<Ammo>> pools = new Dictionary<string, List<Ammo>>();
    private readonly Dictionary<string, int> poolsCurrentItemIndexes = new Dictionary<string, int>();
    private readonly Vector2 poolsPosition = new Vector2(-20f, -20f);

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
    }

    public void CreateAmmoPool(string key, GameObject ammoObject, int poolSize) {
        if (poolSize <= 0) {
            return;
        }

        pools[key] = new List<Ammo>(poolSize);
        poolsCurrentItemIndexes[key] = 0;
        List<Ammo> pool = pools[key];

        for (int i = 0; i < poolSize; i++) {
            pool.Add(CreateAmmo(ammoObject, poolsPosition, Quaternion.identity));
        }
    }

    private Ammo CreateAmmo(GameObject ammoObject, Vector2 position, Quaternion quaternion) {
        GameObject go = Instantiate(ammoObject, position, quaternion);
        Ammo ammo = go.GetComponent<Ammo>();
        ammo.SetRecycledPosition(poolsPosition);
        return ammo;
    }

    public Ammo GetAmmo(string key) {
        if (pools.ContainsKey(key)) {
            List<Ammo> pool = pools[key];
            int currentIndex = poolsCurrentItemIndexes[key];
            Ammo item = pool[currentIndex];
            // 가져온 애가 아직 recylced되지 않고 화면에 남아있는 애면 바로 새로운 ammo 만들어서 걔로 대체함.
            if (!item.IsRecycled()) {
                Ammo newItem = CreateAmmo(item.gameObject, poolsPosition, Quaternion.identity);
                pool.Insert(currentIndex, newItem);
                item = newItem;
            }
            
            currentIndex++;
            if (currentIndex < pool.Count) {
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
}
