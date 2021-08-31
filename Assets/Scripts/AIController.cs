using UnityEngine;
using Random = UnityEngine.Random;

public class AIController : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    
    private PlayerType computerSide;

    public PlayerType ComputerSide
    {
        set => computerSide = value;
    }

    public void MakeStep()
    {
        if (gameController.MovesCount < gameController.TotalMovesAvailable && !gameController.IsPlayersTurn)
        {
            GameButton randomButton = gameController.Buttons[Random.Range(0, gameController.Buttons.Count)];
            
            if (randomButton.OccupiedBy == PlayerType.Empty)
            {
                randomButton.Init(computerSide, gameController.PlayerIcon[(int)computerSide]);
            }
            else
            {
                MakeStep();
            }

            gameController.IsPlayersTurn = true;
        }
    }
}
