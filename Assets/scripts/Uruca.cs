using UnityEngine;

public class Uruca : MonoBehaviour
{
    public static Uruca _instance { get; private set; }

    [Header("Settings")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float minAttackCooldown = .5f;
    [SerializeField] private float maxAttackCooldown = 1.5f;
    [SerializeField] private float attackDuration = 2f;
    [SerializeField] private HealthBar healthbar;

    private Transform player;
    private Animator animator;
    private Vector3 startPosition;

    private float attackTimer = 0f;
    private float currentCooldown;
    private float attackTimeElapsed = 0f;
    private int maxHealth = 100;
    private int currentHealth;

    private float deathTimer = 0f;
    private float deathDelay = 2f;
    private bool isDying = false;

    private enum BossState { Idle, Attacking, Returning, Dead }
    private BossState currentState = BossState.Idle;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        startPosition = transform.position;
        currentHealth = maxHealth;

        healthbar.SetHealth(maxHealth);

        ResetAttackTimer();
        animator.SetTrigger("Idle");
    }

    void Update()
    {
        if (isDying)
        {
            deathTimer += Time.deltaTime;
            if (deathTimer >= deathDelay)
            {
                gameObject.SetActive(false);
            }
            return;
        }

        attackTimer += Time.deltaTime;

        switch (currentState)
        {
            case BossState.Idle:
                HandleIdleState();
                break;

            case BossState.Attacking:
                HandleAttackState();
                break;

            case BossState.Returning:
                HandleReturningState();
                break;
        }

        if (currentHealth <= 0 && !isDying)
        {
            Die();
        }
    }

    void HandleIdleState()
    {
        if (attackTimer >= currentCooldown)
        {
            animator.ResetTrigger("Idle");
            animator.SetTrigger("Attack");
            attackTimeElapsed = 0f;
            currentState = BossState.Attacking;
        }
    }

    void HandleAttackState()
    {
        attackTimeElapsed += Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

        if (attackTimeElapsed >= attackDuration)
        {
            animator.ResetTrigger("Attack");
            animator.SetTrigger("Idle");
            currentState = BossState.Returning;
        }
    }

    void HandleReturningState()
    {
        transform.position = Vector3.MoveTowards(transform.position, startPosition, (moveSpeed * 0.8f) * Time.deltaTime);

        if (Vector3.Distance(transform.position, startPosition) < 0.2f)
        {
            currentState = BossState.Idle;
            ResetAttackTimer();
        }
    }

    void ResetAttackTimer()
    {
        attackTimer = 0f;
        currentCooldown = Random.Range(minAttackCooldown, maxAttackCooldown);
    }

    void TakeDamage(int damage)
    {
        if (isDying) return;
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);
    }

    void Die()
    {
        currentState = BossState.Dead;
        isDying = true;
        deathTimer = 0f;
        PlayDead();
    }

    void PlayDead()
    {
        animator.ResetTrigger("Idle");
        animator.ResetTrigger("Attack");
        animator.SetTrigger("PlayDead");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            TakeDamage(2);
        }
    }

    public bool isDead()
    {
        return currentHealth <= 0;
    }
}
