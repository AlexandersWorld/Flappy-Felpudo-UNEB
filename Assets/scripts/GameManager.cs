using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance { get; private set; }

    private const string FADE_IN = "FadeIn";

    [SerializeField] private EnemySpawner spawnManager;
    [SerializeField] private Transform gameOverComponent;
    [SerializeField] private DirectionalLighting directionalLighting;

    [SerializeField] private Button tryAgainButton;
    [SerializeField] private Button quitButton;

    [SerializeField] private float gameTimeInSeconds = 100;
    [SerializeField] private float TimeMultipliyer = 2;
    private Animator animator;


    private float first_phase = 60;
    private float second_phase = 20;
    private const float SECOND_STATE = 0.5f;
    private const float THIRD_STATE = 0.3f;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }


    private void Start()
    {
        animator = gameOverComponent.GetComponent<Animator>();
        gameOverComponent.gameObject.SetActive(false);

        tryAgainButton.onClick.AddListener(OnTryAgainClicked);
        quitButton.onClick.AddListener(OnQuitClicked);
    }

    void Update()
    {
        gameTimeInSeconds -= Time.deltaTime * TimeMultipliyer;

        if (gameTimeInSeconds <= 0)
        {
            spawnManager.StopSpawning();
            directionalLighting.TurnLightRed();
        }

        if (gameTimeInSeconds <= first_phase)
        {
            spawnManager.SetSpawnInterval(SECOND_STATE);
        } else if (gameTimeInSeconds <= second_phase)
        {
            spawnManager.SetSpawnInterval(THIRD_STATE);
        }


        if (ControlaJogador._instance.isGameOver())
        {
            gameOverComponent.gameObject.SetActive(true);
            animator.SetTrigger(FADE_IN);
        }

        if (Uruca._instance && Uruca._instance.isDead())
        {
            gameOverComponent.gameObject.SetActive(true);
            animator.SetTrigger(FADE_IN);
        }
    }


    private void OnTryAgainClicked()
    {
        gameOverComponent.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnQuitClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public float GetGameTime()
    {
        return gameTimeInSeconds;
    }
}
