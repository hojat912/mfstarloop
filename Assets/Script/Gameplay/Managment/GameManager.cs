using UnityEngine;
using DependencyInjection;
using EventSystem;
using Gameplay.DiceManagment;
using Gameplay.BoardManagment;

namespace Gameplay.Managment
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private Board _board;
        [SerializeField]
        private DiceSelector _diceSelector;
        [SerializeField]
        private GamePlayUI _gamePlayUI;

        private IServiceLocator _serviceLocator;
        private void Awake()
        {
            _serviceLocator = new ServiceLocator();
            _serviceLocator.AddService(typeof(IEvent), new EventController());
            _serviceLocator.AddService(typeof(IBoard), _board);
            _serviceLocator.AddService(typeof(IDiceSelector), _diceSelector);

            _board.Init(_serviceLocator);
            _gamePlayUI.Init(_serviceLocator);
        }
    }
}