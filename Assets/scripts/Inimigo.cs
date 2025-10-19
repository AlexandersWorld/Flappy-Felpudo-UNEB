using UnityEngine;

public class EnemyStraightMove : MonoBehaviour
{
    public float speed = 4f;
    public float lifetime = 10f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
}