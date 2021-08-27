using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image buttonImage;
    [SerializeField] private GameController gameController;
    
    private PlayerType occupiedBy = PlayerType.Empty;
    
    public Button Button => button;
    public PlayerType OccupiedBy => occupiedBy;

    public void Init(PlayerType playerType, Sprite imageSprite)
    {
        Color tempColor = buttonImage.color;
        tempColor.a = 1f;
        buttonImage.color = tempColor;
        
        buttonImage.sprite = imageSprite;
        button.interactable = false;
        Button.onClick.RemoveAllListeners();
        occupiedBy = playerType;
        gameController.MovesCount++;

        gameController.CheckWinner();
        
        if (gameController.IsPlayersTurn)
        {
            gameController.IsPlayersTurn = false;
            if (!gameController.HasWinner)
            {
                gameController.AIController.MakeStep();
            }
        }
    }

    public void ResetButton()
    {
        Color tempColor = buttonImage.color;
        tempColor.a = 0f;
        buttonImage.color = tempColor;
        
        button.interactable = true;
        buttonImage.sprite = null;
        occupiedBy = PlayerType.Empty;
        button.onClick.RemoveAllListeners();
    }
    
    
}
