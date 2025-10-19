using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed = 3f;
    public float stopDistance = 0.5f;

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (!player) return;

        Vector2 direction = (player.position - transform.position).normalized;
        float distance = Vector2.Distance(player.position, transform.position);

        if (distance > stopDistance)
        {
            transform.position += (Vector3)direction * speed * Time.deltaTime;
        }
    }
}
