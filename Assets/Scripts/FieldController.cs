using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldController : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    [SerializeField] private RectTransform buttonsContainer;
    [SerializeField] private GridLayoutGroup buttonsGrid;
    [SerializeField] private GameButton buttonToInstantiate;
    
    public void BuildField()
    {
        float newSize = buttonsContainer.rect.width / gameController.FieldSize;
        buttonsGrid.cellSize = new Vector2(newSize, newSize);
        
        for (int i = 1; i <= gameController.FieldSize * gameController.FieldSize; i++)
        {
            GameButton instantiatedButton = Instantiate(buttonToInstantiate, buttonsContainer);
            instantiatedButton.GameController = gameController;
            gameController.Buttons.Add(instantiatedButton);
        }

        CreateCombinations(gameController.FieldSize, gameController.Buttons, gameController.Combinations);
    }

    private void CreateCombinations(int fieldSize, List<GameButton> buttons, List<WinningCombination> combinations)
    {
        combinations.Clear();
        
        GetHorizontalCells(fieldSize, buttons, combinations);
        GetVerticalCells(fieldSize, buttons, combinations);
        GetDiagonalCells(fieldSize, buttons, combinations);
    }
    
    private void GetHorizontalCells(int fieldSize, List<GameButton> buttons, List<WinningCombination> combinations)
    {
        int lastButton = 0;
        
        for (int i = 0; i < fieldSize; i++)
        {
            WinningCombination obj = new WinningCombination();
            
            for (int j = 0; j < fieldSize; j++)
            {
                obj.Elements.Add(buttons[lastButton]);
                lastButton++;
            }
            
            combinations.Add(obj);
        }
    } 
    
    private void GetVerticalCells(int fieldSize, List<GameButton> buttons, List<WinningCombination> combinations)
    {
        for (int i = 0; i < fieldSize; i++)
        {
            WinningCombination obj = new WinningCombination();
            int lastButton = i;
            
            for (int j = 0; j < fieldSize; j++)
            {
                obj.Elements.Add(buttons[lastButton]);
                lastButton += fieldSize;
            }
            
            combinations.Add(obj);
        }
    } 
    
    private void GetDiagonalCells(int fieldSize, List<GameButton> buttons, List<WinningCombination> combinations)
    {
        WinningCombination obj = new WinningCombination();

        int lastButton = 0;
        
        for (int i = 0; i < fieldSize; i++)
        {
            obj.Elements.Add(buttons[lastButton]);
            lastButton += fieldSize + 1;
        }
        
        combinations.Add(obj);
        
        obj = new WinningCombination();
        lastButton = fieldSize - 1;
        
        for (int i = 0; i < fieldSize; i++)
        {
            obj.Elements.Add(buttons[lastButton]);
            lastButton += fieldSize - 1;
        }
        
        combinations.Add(obj);
    } 
}
