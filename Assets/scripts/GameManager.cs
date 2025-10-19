using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner spawnManager;

    [SerializeField] private float gameTimeInSeconds = 180;
    [SerializeField] private float multipliyer = 2;

    private float first_phase = 60;
    private float second_phase = 20;
    private const float SECOND_STATE = 0.5f;
    private const float THIRD_STATE = 0.3f;

    void Update()
    {
        gameTimeInSeconds -= Time.deltaTime * multipliyer;

        if (gameTimeInSeconds <= first_phase)
        {
            spawnManager.spawnInterval = SECOND_STATE;
        } else if (gameTimeInSeconds <= second_phase)
        {
            spawnManager.spawnInterval = THIRD_STATE;
        }
    }
}
