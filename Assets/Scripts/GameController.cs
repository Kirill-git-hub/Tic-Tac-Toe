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
    [SerializeField] private List<GameButton> buttons = new List<GameButton>();
    [SerializeField] private List<WinningCombination> combinations = new List<WinningCombination>();
    [SerializeField] private int step;
    
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
    
    public List<GameButton> Buttons => buttons;
    
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

    private void Start()
    {
        // for (int i = 1; i <= step * step; i++)
        // {
        //     buttons.Add();
        // }

        GetHorizontalCell();
        GetVerticalCell();
        GetDiagonalCell();
    }

    public void GetHorizontalCell()
    {
        int lastButton = 0;
        
        for (int i = 0; i < step; i++)
        {
            WinningCombination obj = new WinningCombination();
            for (int j = 0; j < step; j++)
            {
                obj.elements.Add(buttons[lastButton]);
                lastButton++;
            }
            combinations.Add(obj);
        }
    } 
    
    public void GetVerticalCell()
    {
        for (int i = 0; i < step; i++)
        {
            WinningCombination obj = new WinningCombination();
            int lastButton = i;
            
            for (int j = 0; j < step; j++)
            {
                obj.elements.Add(buttons[lastButton]);
                lastButton += step;
            }
            combinations.Add(obj);
        }
    } 
    
    public void GetDiagonalCell()
    {
        WinningCombination obj = new WinningCombination();

        int lastButton = 0;
        
        for (int i = 0; i < step; i++)
        {
            obj.elements.Add(buttons[lastButton]);
            lastButton += step + 1;
        }
        combinations.Add(obj);
        
        obj = new WinningCombination();
        lastButton = step - 1;
        
        for (int i = 0; i < step; i++)
        {
            obj.elements.Add(buttons[lastButton]);
            lastButton += step - 1;
        }
        combinations.Add(obj);
    } 

    public void SetupFirstPlayer()
    {
        movesCount = 0;
        hasWinner = false;
        restartButton.SetActive(false);
    }

    public void SetStartingSide(PlayerType startingSide)
    {
        foreach (GameButton button in buttons)
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
        foreach (WinningCombination c in combinations)
        {
            // if (c.OccupiedBy != PlayerType.Empty)
            // {
            //     // if (c.firstItem.OccupiedBy == c.secondItem.OccupiedBy && c.firstItem.OccupiedBy == c.thirdItem.OccupiedBy)
            //     // {
            //     //     GameOver(c.firstItem.OccupiedBy);
            //     // }
            //     GameOver(OccupiedBy);
            // }
        }
        if (movesCount >= TotalMovesAvailable && !hasWinner)
        {
            restartButton.SetActive(true);
        }
    }

    public void GameOver(PlayerType winner)
    {
        hasWinner = true;

        foreach (var item in buttons)
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
