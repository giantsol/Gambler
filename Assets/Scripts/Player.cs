using UnityEngine;

public class Player : MonoBehaviour {

    public void PlayerDied() {
        GameController.instance.PlayerDied();
    }
}
