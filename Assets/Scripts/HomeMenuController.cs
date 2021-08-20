using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeMenuController : MonoBehaviour
{
    [SerializeField] private Button[] chooseSideButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private GameObject gamePanel; 
    [SerializeField] private GameObject menuPanel; 
    
    // Start is called before the first frame update
    void Start()
    {
        FinishGame();
        
        for (int i = 0; i < chooseSideButton.Length; i++)
        {
            chooseSideButton[i].onClick.AddListener(() => StartGame());
        }
        
        exitButton.onClick.AddListener((() => FinishGame()));
    }

    public void StartGame()
    {
        menuPanel.gameObject.SetActive(false);
        gamePanel.gameObject.SetActive(true);
    }

    public void FinishGame()
    {
        menuPanel.gameObject.SetActive(true);
        gamePanel.gameObject.SetActive(false);
    }
}
