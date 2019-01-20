using UnityEngine;

public class Ammo : MonoBehaviour, Damager.OnDoneDamageCallback {

    private Damager damager;

    private Vector2 recycledPosition;
    
    public float fireRate;
    public int ammoPoolSize;
    public string ammoPoolKey;
    
    private float leftmostX;
    private float rightmostX;
    
    private Collider2D collider;
    private float halfWidth;

    private void Start() {
        damager = GetComponent<Damager>();
        damager.RegisterOnDoneDamageCallback(this);
        
        Camera mainCamera = Camera.main;
        leftmostX = mainCamera.ScreenToWorldPoint(Vector2.zero).x;
        rightmostX = mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x;
        
        collider = GetComponent<Collider2D>();
        halfWidth = collider.bounds.size.x / 2;
        
    }

    private void Update() {
        Vector2 currentPos = transform.position;
        if (currentPos.x + halfWidth <= leftmostX || currentPos.x - halfWidth >= rightmostX) {
            Recycle();
        }
    }

    public void OnDoneDamage(GameObject self) {
        Recycle();
    }

    private void Recycle() {
        transform.position = recycledPosition;
    }

    public void SetRecycledPosition(Vector2 pos) {
        recycledPosition = pos;
    }

    public bool IsRecycled() {
        return transform.position.y <= recycledPosition.y;
    }

    private void OnDestroy() {
        damager.UnregisterOnDoneDamageCallback(this);
    }
}
