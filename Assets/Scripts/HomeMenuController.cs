using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        gameController.PlayerSide = PlayerType.Empty;
        
        SwitchPanels();
        
        exitButton.onClick.AddListener(() =>
        {
            gameController.FinishGame();
            SwitchPanels();
        });
        
        ButtonX.onClick.AddListener(() =>
        {
            gameController.SetStartingSide(PlayerType.Cross);
            SwitchPanels();
        });
        
        ButtonO.onClick.AddListener(() =>
        {
            gameController.SetStartingSide(PlayerType.Zero);
            SwitchPanels();
        });
        
        restartButton.onClick.AddListener(gameController.RestartGame);
    }

    public void SwitchPanels()
    {
        menuPanel.gameObject.SetActive(gameController.PlayerSide == PlayerType.Empty);
        gamePanel.gameObject.SetActive(gameController.PlayerSide != PlayerType.Empty);
    }
}
