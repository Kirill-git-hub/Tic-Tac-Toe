using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AIController : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    
    public PlayerType computerSide;

    public void MakeStep()
    {
        if (gameController.MovesCount < 9 && !gameController.IsPlayersTurn)
        {
            GameButton randomButton = gameController.ButtonList[Random.Range(0, gameController.ButtonList.Count)];
            
            if (randomButton.OccupiedBy == PlayerType.Empty)
            {
                randomButton.Init(computerSide, gameController.PlayerIcon[(int)computerSide]);
                randomButton.Button.onClick.RemoveAllListeners();
            }
            else
            {
                MakeStep();
            }

            gameController.IsPlayersTurn = true;
        }
    }
}
