using System;
namespace Gameplay.NPC
{
    internal interface IDirectionDecision
    {
        void MakeDecision(CellVector[] directions, Action<CellVector> onDecisionMade);
    }
}