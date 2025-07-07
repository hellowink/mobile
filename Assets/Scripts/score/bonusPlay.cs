using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bonusPlay : MonoBehaviour
{
    public GameObject bonusPrefab;
    public float spawnInterval = 10f;
    public float spawnY = 10f;

    private float minX, maxX;

    void Start()
    {
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0));
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, 0));

        minX = leftEdge.x;
        maxX = rightEdge.x;

        InvokeRepeating(nameof(SpawnBonus), 2f, spawnInterval);
    }

    void SpawnBonus()
    {
        float randomX = Random.Range(minX, maxX);
        Vector2 spawnPosition = new Vector2(randomX, spawnY);

        GameObject bonus = Instantiate(bonusPrefab, spawnPosition, Quaternion.identity);

        Rigidbody2D rb = bonus.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.down * Random.Range(2f, 4f);
        }
    }
}
