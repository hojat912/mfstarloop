using System;
using UnityEngine;
namespace Gameplay.NPC
{
    public class PlayerWaySelector : MonoBehaviour
    {
        [SerializeField]
        private CellVector _direction;
        private Action<CellVector> _onClicked;

        public CellVector Direction => _direction;

        public void Initialization(Action<CellVector> onClicked)
        {
            _onClicked = onClicked;
        }

        private void OnMouseUpAsButton()
        {
            _onClicked.Invoke(_direction);
        }
    }
}