using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WinningCombination
{
    [SerializeField] private List<GameButton> elements = new List<GameButton>();

    public List<GameButton> Elements => elements;
}
