using UnityEngine;
using UnityEngine.UI;

public class GameButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image buttonImage;

    private GameController gameController;
    private PlayerType occupiedBy = PlayerType.Empty;

    private const float MAX_ALFA_COMPONENT = 1;
    private const float MIN_ALFA_COMPONENT = 0;
    
    public Button Button => button;
    public PlayerType OccupiedBy => occupiedBy;

    public GameController GameController
    {
        get => gameController;
        set => gameController = value;
    }

    public void Init(PlayerType playerType, Sprite imageSprite)
    {
        Color tempColor = buttonImage.color;
        tempColor.a = MAX_ALFA_COMPONENT;
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
        tempColor.a = MIN_ALFA_COMPONENT;
        buttonImage.color = tempColor;
        
        button.interactable = true;
        buttonImage.sprite = null;
        occupiedBy = PlayerType.Empty;
        button.onClick.RemoveAllListeners();
    }
}
