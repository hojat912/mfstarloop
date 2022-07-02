using System;
using UnityEngine;

namespace Gameplay.NPC
{
    public class AiDirectionDecision : MonoBehaviour, IDirectionDecision
    {
        public void MakeDecision(CellVector[] directions, Action<CellVector> onDecisionMade)
        {
            onDecisionMade.Invoke(directions[UnityEngine.Random.Range(0, directions.Length)]);
        }
    }
}