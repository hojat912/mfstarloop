using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Gameplay.DiceManagment
{
    public interface IDiceSelector
    {
        void SelectDice(Action<IDice> onSelected, List<Dice> diceList);
    }
}