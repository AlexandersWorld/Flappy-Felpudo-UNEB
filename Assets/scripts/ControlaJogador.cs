using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControlaJogador : MonoBehaviour {

    public static ControlaJogador _instance { get; private set; }

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    public float fireRate = 0.25f;

    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private int maxHealth = 3;

    private Vector2 moveInput;
    private float nextFireTime = 1f;
    private Rigidbody2D rb;
    private int health;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }

    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        health = maxHealth;

        healthText.text = "x" + health;
    }
  
  void Update ()
    {
        if (health >= 0)
        {
            healthText.text = "x" + health;
        }

        if (isGameOver()) return;

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        moveInput = new Vector2(moveHorizontal, moveVertical);

        if (Input.GetKey(KeyCode.Space) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = moveInput.normalized * moveSpeed;
        SetCameraBounds();
    }

    void Shoot()
    {
        if (bulletPrefab && firePoint)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
            if (rbBullet != null)
            {
                rbBullet.velocity = firePoint.up * bulletSpeed;
            }
            Destroy(bullet, 3f);
        }
    }


    void SetCameraBounds()
    {
        Vector3 pos = transform.position;

        Vector3 viewPos = Camera.main.WorldToViewportPoint(pos);
        viewPos.x = Mathf.Clamp01(viewPos.x);
        viewPos.y = Mathf.Clamp01(viewPos.y);
        pos = Camera.main.ViewportToWorldPoint(viewPos);

        transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "Uruca")
        {
            if (health >= 0) health--;
        }
    }

    public bool isGameOver()
    {
       return health <= 0;
    }
}
