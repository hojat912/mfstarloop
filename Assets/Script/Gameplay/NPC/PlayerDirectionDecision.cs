using System;
using UnityEngine;
namespace Gameplay.NPC
{
    public class PlayerDirectionDecision : MonoBehaviour, IDirectionDecision
    {
        [SerializeField]
        private PlayerWaySelector[] _playerWaySelector;
        private Action<CellVector> _onDecisionMade;

        private void Awake()
        {
            int length = _playerWaySelector.Length;
            for (int i = 0; i < length; i++)
            {
                _playerWaySelector[i].Initialization(OnClicked);
            }
        }

        private void OnClicked(CellVector deirection)
        {
            _onDecisionMade.Invoke(deirection);
            int length = _playerWaySelector.Length;
            for (int i = 0; i < length; i++)
            {
                _playerWaySelector[i].gameObject.SetActive(false);
            }
        }

        public void MakeDecision(CellVector[] directions, Action<CellVector> onDecisionMade)
        {
            int waySelectorLength = _playerWaySelector.Length;
            int directionLength = directions.Length;

            for (int i = 0; i < waySelectorLength; i++)
            {
                bool haveDirection = false;
                for (int j = 0; j < directionLength; j++)
                {
                    if (_playerWaySelector[i].Direction == directions[j])
                    {
                        haveDirection = true;
                        break;
                    }
                }
                _playerWaySelector[i].gameObject.SetActive(haveDirection);
            }

            _onDecisionMade = onDecisionMade;
        }
    }
}