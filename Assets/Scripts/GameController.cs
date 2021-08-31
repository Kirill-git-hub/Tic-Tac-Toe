using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Sprite[] playerIcon;
    [SerializeField] private AIController aiController;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject winnerX;
    [SerializeField] private GameObject winnerO;
    [SerializeField] private List<GameButton> buttons = new List<GameButton>();
    [SerializeField] private List<WinningCombination> combinations = new List<WinningCombination>();
    [SerializeField] private FieldController fieldController;

    private const int DEFAULT_FIELD_SIZE = 3;
    
    private PlayerType playerSide;
    private bool hasWinner;
    private bool isPlayersTurn = false;
    private int totalMovesAvailable;
    private int fieldSize = DEFAULT_FIELD_SIZE;
    private int movesCount;

    #region Getters and Setters

    public Sprite[] PlayerIcon => playerIcon;
    
    public AIController AIController => aiController;
    
    public int MovesCount
    {
        get => movesCount;
        set => movesCount = value;
    }
    public List<GameButton> Buttons => buttons;
    
    public List<WinningCombination> Combinations => combinations;
    
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

    public int FieldSize
    {
        get => fieldSize;
        set => fieldSize = value < DEFAULT_FIELD_SIZE ? DEFAULT_FIELD_SIZE : value; 
    }
    
    #endregion
    
    public void SetupFirstPlayer()
    {
        movesCount = 0;
        hasWinner = false;
        restartButton.SetActive(false);
    }

    public void InitGame(PlayerType startingSide)
    {
        DestroyButtons();
        
        fieldController.BuildField();

        foreach (GameButton button in buttons)
        {
            button.ResetButton();
            button.Button.onClick.AddListener(() => button.Init(PlayerSide, playerIcon[(int) playerSide]));
        }

        PlayerSide = startingSide;

        totalMovesAvailable = fieldSize * fieldSize;

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
            if (c.Elements[0].OccupiedBy != PlayerType.Empty)
            {
                PlayerType winner = c.Elements[0].OccupiedBy;
                bool combinationWon = true;
                
                foreach (var el in c.Elements)
                {
                    if (el.OccupiedBy != winner)
                    {
                        combinationWon = false;
                    }
                }

                if (combinationWon)
                {
                    GameOver(winner);
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

        foreach (var item in buttons)
        {
            item.Button.interactable = false;
        }

        restartButton.SetActive(true);
        
        winnerX.SetActive(winner == PlayerType.Cross);
        winnerO.SetActive(winner == PlayerType.Zero);
    }

    private void DestroyButtons()
    {
        foreach (var item in buttons)
        {
            Destroy(item.gameObject);
        }
        
        buttons.Clear();
    }
    
    public void RestartGame()
    {
        InitGame(PlayerSide);
    }

    public void FinishGame()
    {
        playerSide = PlayerType.Empty;
    }

    public void DecreaseDifficulty()
    {
        FieldSize--;
    }

    public void IncreaseDifficulty()
    {
        FieldSize++;
    }
}
