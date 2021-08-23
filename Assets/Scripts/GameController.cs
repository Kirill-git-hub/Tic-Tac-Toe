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
    [SerializeField] private int movesCount = 0;
    [SerializeField] private List<GameButton> buttonList = new List<GameButton>();
    [SerializeField] private List<WinningCombination> combinations = new List<WinningCombination>();
    
    private PlayerType playerSide;
    private bool hasWinner;
    private bool isPlayersTurn = false;


    public PlayerType PlayerSide
    {
        get => playerSide;
        set => playerSide = value;
    }

    public bool HasWinner => hasWinner;

    public AIController AIController => aiController;

    public List<GameButton> ButtonList
    {
        get => buttonList;
        set => buttonList = value;
    }

    public Sprite[] PlayerIcon
    {
        get => playerIcon;
        set => playerIcon = value;
    }

    public bool IsPlayersTurn
    {
        get => isPlayersTurn;
        set => isPlayersTurn = value;
    }

    public int MovesCount
    {
        get => movesCount;
        set => movesCount = value;
    }

    public void SetupFirstPlayer()
    {
        isPlayersTurn = PlayerSide == PlayerType.Cross;
        movesCount = 0;
        hasWinner = false;
        restartButton.SetActive(false);
    }

    public void SetStartingSide(PlayerType startingSide)
    {
        SetupFirstPlayer();

        foreach (var button in ButtonList)
        {
            button.ResetButton();
            button.Button.onClick.AddListener(() => button.Init(PlayerSide, playerIcon[(int) playerSide]));
        }

        PlayerSide = startingSide;

        if (PlayerSide == PlayerType.Cross)
        {
            aiController.computerSide = PlayerType.Zero;
            
            isPlayersTurn = true;
        }
        else
        {
            aiController.computerSide = PlayerType.Cross;
            
            aiController.MakeStep();
        }
        
        winnerX.gameObject.SetActive(false);
        winnerO.gameObject.SetActive(false);
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
        if (movesCount >= 9 && !hasWinner)
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
            winnerX.gameObject.SetActive(true);
        }
        else
        {
            winnerO.gameObject.SetActive(true);
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
