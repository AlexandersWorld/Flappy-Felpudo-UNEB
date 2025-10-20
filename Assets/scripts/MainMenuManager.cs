using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button quitGameButton;


    private void Start()
    {
        startGameButton.onClick.AddListener(OnTryAgainClicked);
        quitGameButton.onClick.AddListener(OnQuitClicked);
    }

    private void OnTryAgainClicked()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void OnQuitClicked()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
