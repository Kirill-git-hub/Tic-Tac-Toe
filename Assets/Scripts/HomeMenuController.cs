using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HomeMenuController : MonoBehaviour
{
    [SerializeField] private Button ButtonX;
    [SerializeField] private Button ButtonO;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private GameObject gamePanel; 
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameController gameController;
    [SerializeField] private string fieldSizeFormat;
    [SerializeField] private TextMeshProUGUI fieldSizeText;
    [SerializeField] private Button decreaseDifficulty;
    [SerializeField] private Button increaseDifficulty;

    // Start is called before the first frame update
    void Start()
    {
        gameController.PlayerSide = PlayerType.Empty;
        
        SwitchPanels();
        UpdateFieldSizeText();
        
        exitButton.onClick.AddListener(() =>
        {
            gameController.FinishGame();
            SwitchPanels();
        });
        
        ButtonX.onClick.AddListener(() =>
        {
            gameController.InitGame(PlayerType.Cross);
            SwitchPanels();
        });
        
        ButtonO.onClick.AddListener(() =>
        {
            gameController.InitGame(PlayerType.Zero);
            SwitchPanels();
        });
        
        decreaseDifficulty.onClick.AddListener(() =>
        {
            gameController.DecreaseDifficulty();
            UpdateFieldSizeText();
        });
        
        increaseDifficulty.onClick.AddListener(() =>
        {
            gameController.IncreaseDifficulty();
            UpdateFieldSizeText();
        });
        
        restartButton.onClick.AddListener(gameController.RestartGame);
    }

    public void SwitchPanels()
    {
        menuPanel.SetActive(gameController.PlayerSide == PlayerType.Empty);
        gamePanel.SetActive(gameController.PlayerSide != PlayerType.Empty);
    }

    public void UpdateFieldSizeText()
    {
        fieldSizeText.SetText(String.Format(fieldSizeFormat, gameController.FieldSize, gameController.FieldSize));
    }
}
