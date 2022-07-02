
using System;
namespace Gameplay.DiceManagment
{
    public interface IDice
    {
        void Roll(Action<int> onDiceRollded);
    }
}