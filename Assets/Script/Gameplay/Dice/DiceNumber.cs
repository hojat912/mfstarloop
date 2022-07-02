using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Gameplay.DiceManagment
{
    public class DiceNumber : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _diceNumberText;
        [SerializeField]
        private Image _bg;

        private int _diceNumberValue;
        [SerializeField]
        private Color _unslecetedColor;
        [SerializeField]
        private Color _selectedColor;

        public int DiceNumberValue => _diceNumberValue;
        public Vector3 Position => transform.position;

        public void Init(int number)
        {
            _diceNumberValue = number;
            _diceNumberText.text = number.ToString();
        }

        public void SetAsUnselected()
        {
            _bg.color = _unslecetedColor;
        }

        public void SetAsSelected()
        {
            _bg.color = _selectedColor;
        }
    }
}