using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardSpawner : MonoBehaviour
{
    public GameObject[] hazardPrefabs; // Asignás bota, lata, bomba
    public float spawnInterval = 2f;
    public float xRange = 2.5f;
    public float minFallSpeed = 2f;
    public float maxFallSpeed = 6f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnHazard), 2f, spawnInterval);
    }

    void SpawnHazard()
    {
        int index = Random.Range(0, hazardPrefabs.Length);
        GameObject hazard = Instantiate(hazardPrefabs[index]);

        float x = Random.Range(-xRange, xRange);
        hazard.transform.position = new Vector2(x, transform.position.y);

        Rigidbody2D rb = hazard.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            float speed = Random.Range(minFallSpeed, maxFallSpeed);
            rb.velocity = Vector2.down * speed;
        }
    }
}