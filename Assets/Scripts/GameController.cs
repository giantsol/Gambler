using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static GameController instance;
    public GameObject gameOverText;
    public float scrollSpeed = -1.5f;
    public Text scoreText;
    
    [HideInInspector] public bool gameOver = false;

    private int score = 0;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
    }
    
    // Start is called before the first frame update
    private void Start() {
        
    }

    // Update is called once per frame
    private void Update() {
        if (gameOver && Input.GetKeyDown(KeyCode.R)) {
            RestartGame();
        }
    }

    public void BirdScored() {
        if (gameOver) {
            return;
        }

        score++;
        scoreText.text = "Score: " + score;
    }

    public void BirdDied() {
        gameOverText.SetActive(true);
        gameOver = true;
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
