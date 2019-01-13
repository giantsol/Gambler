using UnityEngine;

public class ColumnPool : MonoBehaviour {
    private static readonly int TYPE_WIDE = 0;
    private static readonly int TYPE_NARROW = 1;

    public GameObject wideColumnPrefab;
    public GameObject narrowColumnPrefab;
    public float spawnRate = 4f;
    
    private GameObject[] wideColumnPrefabs;
    private int wideColIndex;
    private GameObject[] narrowColumnPrefabs;
    private int narrowColIndex;
    private readonly int columnPoolSize = 5;

    private float wideMinY;
    private float wideMaxY;
    private float narrowMinY;
    private float narrowMaxY;
    
    // Some random position off the screen
    private readonly Vector2 objectPoolPosition = new Vector2(-20f, -20f);
    private readonly float spawnXPosition = 13f;
    private float timeSinceLastSpawn;
    
    // Start is called before the first frame update
    void Start() {
        wideColumnPrefabs = new GameObject[columnPoolSize];
        narrowColumnPrefabs = new GameObject[columnPoolSize];
        for (int i = 0; i < columnPoolSize; i++) {
            wideColumnPrefabs[i] = Instantiate(wideColumnPrefab, objectPoolPosition, Quaternion.identity);
            narrowColumnPrefabs[i] = Instantiate(narrowColumnPrefab, objectPoolPosition, Quaternion.identity);
        }

        Column columnScript = wideColumnPrefab.GetComponent<Column>();
        wideMinY = columnScript.minY;
        wideMaxY = columnScript.maxY;
        columnScript = narrowColumnPrefab.GetComponent<Column>();
        narrowMinY = columnScript.minY;
        narrowMaxY = columnScript.maxY;

        timeSinceLastSpawn = spawnRate / 2f;
    }

    // Update is called once per frame
    void Update() {
        timeSinceLastSpawn += Time.deltaTime;
        spawnRate = 5.5f + GameController.instance.scrollSpeed;

        if (!GameController.instance.gameOver && timeSinceLastSpawn >= spawnRate) {
            timeSinceLastSpawn = 0;

            int columnType = ChooseNarrowOrWideColumn();
            if (columnType == TYPE_WIDE) {
                SpawnWideColumn();
            } else {
                SpawnNarrowColumn();
            }
        }
    }

    private int ChooseNarrowOrWideColumn() {
        return Random.Range(TYPE_WIDE, TYPE_NARROW + 1);
    }

    private void SpawnWideColumn() {
        float spawnYPosition = Random.Range(wideMinY, wideMaxY);
        wideColumnPrefabs[wideColIndex].transform.position = new Vector2(spawnXPosition, spawnYPosition);
            
        wideColIndex++;
        if (wideColIndex >= columnPoolSize) {
            wideColIndex = 0;
        }
    }
    
    private void SpawnNarrowColumn() {
        float spawnYPosition = Random.Range(narrowMinY, narrowMaxY);
        narrowColumnPrefabs[narrowColIndex].transform.position = new Vector2(spawnXPosition, spawnYPosition);
            
        narrowColIndex++;
        if (narrowColIndex >= columnPoolSize) {
            narrowColIndex = 0;
        }
    }
}
