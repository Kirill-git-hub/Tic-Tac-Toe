using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Sprite[] playerIcon;
    [SerializeField] private AIController aiController;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject winnerX;
    [SerializeField] private GameObject winnerO;
    [SerializeField] private int movesCount;
    [SerializeField] private List<GameButton> buttonList = new List<GameButton>();
    [SerializeField] private List<WinningCombination> combinations = new List<WinningCombination>();
    
    private PlayerType playerSide;
    private bool hasWinner;
    private bool isPlayersTurn = false;
    private int totalMovesAvailable = 9;

    #region Getters and Setters

    public Sprite[] PlayerIcon => playerIcon;
    
    public AIController AIController => aiController;
    
    public int MovesCount
    {
        get => movesCount;
        set => movesCount = value;
    }
    
    public List<GameButton> ButtonList => buttonList;
    
    public PlayerType PlayerSide
    {
        get => playerSide;
        set => playerSide = value;
    }
    public bool HasWinner => hasWinner;
    
    public bool IsPlayersTurn
    {
        get => isPlayersTurn;
        set => isPlayersTurn = value;
    }

    public int TotalMovesAvailable => totalMovesAvailable;

    #endregion

    public void SetupFirstPlayer()
    {
        movesCount = 0;
        hasWinner = false;
        restartButton.SetActive(false);
    }

    public void SetStartingSide(PlayerType startingSide)
    {
        foreach (var button in ButtonList)
        {
            button.ResetButton();
            button.Button.onClick.AddListener(() => button.Init(PlayerSide, playerIcon[(int) playerSide]));
        }

        PlayerSide = startingSide;

        SetupFirstPlayer();

        if (PlayerSide == PlayerType.Cross)
        {
            aiController.ComputerSide = PlayerType.Zero;
            isPlayersTurn = true;
        }
        else
        {
            aiController.ComputerSide = PlayerType.Cross;
            isPlayersTurn = false;
            
            aiController.MakeStep();
        }
        
        winnerX.SetActive(false);
        winnerO.SetActive(false);
    }

    public void CheckWinner()
    {
        foreach (var c in combinations)
        {
            if (c.firstItem.OccupiedBy != PlayerType.Empty)
            {
                if (c.firstItem.OccupiedBy == c.secondItem.OccupiedBy && c.firstItem.OccupiedBy == c.thirdItem.OccupiedBy)
                {
                    GameOver(c.firstItem.OccupiedBy);
                }
            }
        }
        if (movesCount >= TotalMovesAvailable && !hasWinner)
        {
            restartButton.SetActive(true);
        }
    }

    public void GameOver(PlayerType winner)
    {
        hasWinner = true;

        foreach (var item in buttonList)
        {
            item.Button.interactable = false;
        }

        restartButton.SetActive(true);

        if (winner == PlayerType.Cross)
        {
            winnerX.SetActive(true);
        }
        else
        {
            winnerO.SetActive(true);
        }
    }

    public void RestartGame()
    {
        SetStartingSide(PlayerSide);
    }

    public void FinishGame()
    {
        playerSide = PlayerType.Empty;
    }
}
