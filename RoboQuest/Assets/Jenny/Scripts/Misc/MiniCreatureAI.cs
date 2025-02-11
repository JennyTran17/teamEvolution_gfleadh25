using UnityEngine;
using System.Collections;

public class MiniCreatureAI : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float moveInterval = 2f;
    public float detectionRange = 2f;
    public float fleeSpeed = 4f;
    public float fleeDuration = 2f;

    private Transform player;
    private Rigidbody2D rb;
    private bool isFleeing = false;
    private int moveDirection = 1;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        rb = GetComponent<Rigidbody2D>();

        StartCoroutine(Wander());
    }

    void Update()
    {
        if (!isFleeing)
        {
            rb.velocity = new Vector2(moveSpeed * moveDirection, rb.velocity.y);
        }

        if (player != null && Vector2.Distance(transform.position, player.position) < detectionRange && !isFleeing)
        {
            StartCoroutine(Flee());
        }
    }

    IEnumerator Wander()
    {
        while (true)
        {
            yield return new WaitForSeconds(moveInterval);
            moveDirection *= -1; // Switch direction (left/right)
        }
    }

    IEnumerator Flee()
    {
        isFleeing = true;
        int fleeDirection = (transform.position.x > player.position.x) ? 1 : -1;
        float startTime = Time.time;

        while (Time.time < startTime + fleeDuration)
        {
            rb.velocity = new Vector2(fleeSpeed * fleeDirection, rb.velocity.y);
            yield return null;
        }

        isFleeing = false;
    }
}
