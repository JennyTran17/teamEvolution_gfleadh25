using UnityEngine;

public class MiniCreatureSpawner : MonoBehaviour
{
    public GameObject miniCreaturePrefab;
    public int numberOfCreatures = 6;
    public float spawnRadius = 3f;

    void Start()
    {
        SpawnCreatures();
    }

    void SpawnCreatures()
    {
        for (int i = 0; i < numberOfCreatures; i++)
        {
            Vector2 spawnPos;
            int count = 0;

            // Try to find a non-overlapping position
            do
            {
                spawnPos = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;
                count++;
            }
            while (Physics2D.OverlapCircle(spawnPos, 0.5f) && count < 10);

            Instantiate(miniCreaturePrefab, spawnPos, Quaternion.identity);
        }
    }
}
