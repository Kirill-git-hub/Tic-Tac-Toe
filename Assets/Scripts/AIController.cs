using System;
using System.Collections;
using System.Collections.Generic;
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
            GameButton randomButton = gameController.ButtonList[Random.Range(0, gameController.ButtonList.Count)];
            
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
