using DependencyInjection;
using EventSystem;
using Gameplay.DiceManagment;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Gameplay.Managment
{
    public class GamePlayUI : MonoBehaviour
    {
        [SerializeField]
        private Dice _dice6;
        [SerializeField]
        private Dice _dice10;

        [SerializeField]
        private TextMeshProUGUI _gameStateText;

        [SerializeField]
        private Button _replayButton;

        [SerializeField]
        private Button _gotoEditor;

        [SerializeField]
        private GameObject _gameResultScreen;

        [SerializeField]
        private TextMeshProUGUI _gameResultText;

        private IEvent _event;
        private IDiceSelector _diceSelector;
        private EPlayerType _currentPlayer;

        private int _playerDiceCounter;

        private int _aiDiceCounter;


        public void Init(IServiceLocator serviceLocator)
        {
            _event = serviceLocator.GetService<IEvent>();
            _diceSelector = serviceLocator.GetService<IDiceSelector>();
            _currentPlayer = EPlayerType.Player;

            WaitForPlayerDice();

            _gameStateText.text = $"WaitFor{_currentPlayer}Dice";
            _replayButton.onClick.AddListener(ReloadScene);
            _gotoEditor.onClick.AddListener(GotoEditorScene);
            _event.ListenToEvent<EPlayerType>(EEventType.OnPlayerWin, OnPlayerWin);
        }


        private void OnPlayerMoveDone()
        {
            _currentPlayer = _currentPlayer == EPlayerType.AI ? EPlayerType.Player : EPlayerType.AI;
            _gameStateText.text = $"WaitFor{_currentPlayer}Dice";

            if (_currentPlayer == EPlayerType.AI)
            {
                WaitForAiDice();

            }
            else
            {
                WaitForPlayerDice();
            }
        }

        private void WaitForAiDice()
        {
            _aiDiceCounter++;

            if (_aiDiceCounter >= 3)
            {
                OnDiceSelected(_dice10);
                _aiDiceCounter = 0;
            }
            else
            {
                OnDiceSelected(_dice6);
            }
        }

        private void WaitForPlayerDice()
        {
            List<Dice> diceList = new List<Dice>();
            _playerDiceCounter++;
            diceList.Add(_dice6);
            if (_playerDiceCounter >= 3)
            {
                diceList.Add(_dice10);
                _playerDiceCounter = 0;
            }
            _diceSelector.SelectDice(OnDiceSelected, diceList);
        }

        private void OnDiceSelected(IDice dice)
        {
            dice.Roll(OnDiceRollded);
        }

        private void OnDiceRollded(int diceNumber)
        {
            _gameStateText.text = $"{_currentPlayer}Moving{diceNumber}";
            _event.BroadcastEvent<EPlayerType, int, Action>(EEventType.OnPlayerTurn, _currentPlayer, diceNumber, OnPlayerMoveDone);
        }

        private void OnPlayerWin(EPlayerType winner)
        {
            _gameResultScreen.SetActive(true);
            _gameResultText.text = $"{winner} Win";
        }

        private void ReloadScene()
        {
            SceneManager.LoadScene(0);
        }
        private void GotoEditorScene()
        {
            SceneManager.LoadScene(1);
        }
    }
}