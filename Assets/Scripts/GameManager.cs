using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject enemyPrefab;
    public float minSpawnValue = -10.0f;
    public float maxSpawnValue = 10.0f;
    public float enemyDestroyTime = 10.0f;

    [Header("Particle Effects")]
    public GameObject explosion;
    public GameObject muzzleFlash;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 1.0f, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnEnemy()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(minSpawnValue, maxSpawnValue), 6f);
        var gm = Instantiate(enemyPrefab, spawnPosition, Quaternion.Euler(0, 0, 180));
        Destroy(gm, enemyDestroyTime);
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");
    }

    public void InstantiateExplosion(Transform trans, float size)
    {
        var gm = Instantiate(explosion, trans.position, trans.rotation);
        gm.transform.localScale = Vector3.one * size;
        Destroy(gm, 2.0f);
    }
}
