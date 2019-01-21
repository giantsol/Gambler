using System.Collections.Generic;
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

    public List<GameObject> enemies = new List<GameObject>();
    public Transform enemySpawnPoint;
    public float spawnRate = 2f;
    private float timer;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
    }

    private void Start() {
        timer = spawnRate;
    }

    // Update is called once per frame
    private void Update() {
        if (gameOver) {
            if (Input.GetKeyDown(KeyCode.R)) {
                RestartGame();
            }
        } else {
            timer += Time.deltaTime;
            if (timer >= spawnRate) {
                timer = 0f;
                SpawnEnemy(enemies[0]);
            }
        }
    }

    public void BirdScored() {
        if (gameOver) {
            return;
        }

        score++;
        scoreText.text = "Score: " + score;
    }

    public void PlayerDied() {
        gameOverText.SetActive(true);
        gameOver = true;
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame() {
        Application.Quit();
    }

    private void SpawnEnemy(GameObject enemy) {
        Instantiate(enemy, enemySpawnPoint.position, Quaternion.identity);
    }

}
