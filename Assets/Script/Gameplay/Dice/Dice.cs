using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.DiceManagment
{
    public class Dice : MonoBehaviour, IDice
    {

        [SerializeField]
        private int[] _numbers;


        [SerializeField]
        private DiceNumber _diceNumberPrefab;

        [SerializeField]
        private float _moveSpeed;

        private Action<int> _onChoosed;

        private List<DiceNumber> _diceNumbers;
        private int _currecntDiceIndex;

        public int[] Numbers => _numbers;

        private void Awake()
        {
            _diceNumbers = new List<DiceNumber>();
            int length = _numbers.Length;
            for (int i = 0; i < length; i++)
            {
                DiceNumber item = Instantiate(_diceNumberPrefab, transform);
                _diceNumbers.Add(item);
                item.Init(_numbers[i]);
            }
        }


        private IEnumerator Diceanimation()
        {
            yield return new WaitForSeconds(1);
            for (int i = 0; i < 20; i++)
            {
                _diceNumbers[_currecntDiceIndex].SetAsUnselected();
                _currecntDiceIndex = UnityEngine.Random.Range(0, _diceNumbers.Count);
                _diceNumbers[_currecntDiceIndex].SetAsSelected();
                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(0.5f);
            SetDisable();
            _onChoosed.Invoke(_numbers[_currecntDiceIndex]);

        }


        public void Roll(Action<int> onChoosed)
        {
            _onChoosed = onChoosed;
            gameObject.SetActive(true);
            StartCoroutine(Diceanimation());
        }

        public void SetDisable()
        {
            gameObject.SetActive(false);
        }
    }
}