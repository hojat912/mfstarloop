using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Gameplay.DiceManagment
{
    public class DiceSelector : MonoBehaviour, IDiceSelector
    {
        [SerializeField]
        private Button _diceButtonPrefab;
        [SerializeField]
        private Transform _root;

        private Action<IDice> _onSelected;
        private List<Dice> _diceList;
        private List<GameObject> _lastButtons = new List<GameObject>();

        public void SelectDice(Action<IDice> onSelected, List<Dice> diceList)
        {
            _diceList = diceList;
            int count = diceList.Count;
            for (int i = 0; i < count; i++)
            {
                Button diceButton = Instantiate(_diceButtonPrefab, _root);
                _lastButtons.Add(diceButton.gameObject);
                int[] diceNimber = _diceList[i].Numbers;
                string diceNumbersStr = string.Empty;
                for (int j = 0; j < diceNimber.Length; j++)
                {
                    diceNumbersStr += $"{diceNimber[j]} ";
                }
                diceButton.GetComponentInChildren<TextMeshProUGUI>().text = diceNumbersStr;
                int index = i;
                diceButton.onClick.AddListener(() => OnDiceButtonClicked(index));
            }

            _onSelected = onSelected;
        }


        private void OnDiceButtonClicked(int diceIndex)
        {
            IDice selectedDice = _diceList[diceIndex];
            _onSelected.Invoke(selectedDice);
            int count = _lastButtons.Count;
            for (int i = count - 1; i >= 0; i--)
            {
                Destroy(_lastButtons[i]);
            }
            _lastButtons.Clear();
        }

    }
}