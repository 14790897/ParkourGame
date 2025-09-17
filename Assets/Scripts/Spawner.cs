using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float spawnX = 10f;
    public Vector2 yRange = new Vector2(-2.2f, -1.6f);
    public Vector2 spawnIntervalRange = new Vector2(1.0f, 1.8f);
    public float obstacleSpeed = 6f;
    public float speedIncreasePerSecond = 0.05f;

    private float timer;
    private float nextInterval;
    private bool active;

    public void StartSpawn()
    {
        active = true;
        timer = 0f;
        nextInterval = Random.Range(spawnIntervalRange.x, spawnIntervalRange.y);
    }

    public void StopSpawn() => active = false;

    void Update()
    {
        if (!active) return;

        timer += Time.deltaTime;
        obstacleSpeed += speedIncreasePerSecond * Time.deltaTime;

        if (timer >= nextInterval)
        {
            timer = 0f;
            nextInterval = Random.Range(spawnIntervalRange.x, spawnIntervalRange.y);
            SpawnOne();
        }
    }

    private void SpawnOne()
    {
        var pos = new Vector3(spawnX, Random.Range(yRange.x, yRange.y), 0f);
        var go = Instantiate(obstaclePrefab, pos, Quaternion.identity);
        go.AddComponent<ObstacleMover>().speed = obstacleSpeed;
    }
}
