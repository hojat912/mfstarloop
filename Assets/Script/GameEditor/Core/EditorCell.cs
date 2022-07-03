using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameEditor.Core
{
    public class EditorCell : MonoBehaviour
    {
        [SerializeField]
        private EditorDirectionArrow _arrowPrefab;

        [SerializeField]
        private Toggle _enableToggle;
        [SerializeField]
        private Button _button;
        [SerializeField]
        private List<EditorDirectionArrow>  _arrows;

        private CellVector _cellIndex;
        private Action<CellVector> _onSelected;


        public bool IsCellEnable => _enableToggle.isOn;

        public List<EditorDirectionArrow> Arrows => _arrows; 

        public void SetData(EditorCellData cellData, CellVector cellIndex)
        {
            _button.onClick.AddListener(OnButtonClicked);
            _enableToggle.onValueChanged.AddListener(OnToggleValueChanged);
            _arrows = new List<EditorDirectionArrow>();
            _cellIndex = cellIndex;
            _enableToggle.isOn = cellData.IsOn;
            int length = cellData.AvalabelDirections.Length;
            for (int i = 0; i < length; i++)
            {
                AddMoveDirection(cellData.AvalabelDirections[i]);
            }
        }

        private void OnToggleValueChanged(bool value)
        {
            if(!value)
                CleareMoveDirections();
        }

        private void OnButtonClicked()
        {
            _onSelected?.Invoke(_cellIndex);
        }

        public void Clear()
        {
            _enableToggle.isOn = false;
            CleareMoveDirections();
        }


        public void SelecCellMode(Action<CellVector> onSelected)
        {
            _onSelected = onSelected;
            _enableToggle.enabled = false;
            _button.enabled = true;

        }

        public void ToggleCellMode()
        {
            _onSelected = null;
            _enableToggle.enabled = true;
            _button.enabled = false;


        }

        public void SelectDirectionMode(Action<CellVector> onSelected)
        {
            _onSelected = onSelected;
            _enableToggle.enabled = false;
            _button.enabled = true;

        }

        public void SetAsStartNode()
        {
            _enableToggle.graphic.color = Color.blue;
        }

        public void SetAsEndNode()
        {
            _enableToggle.graphic.color = Color.red;
        }

        public void SetAsDefult()
        {
            _enableToggle.graphic.color = Color.green;
        }

        public void AddMoveDirection(CellVector direction)
        {
            for (int i = 0; i < _arrows.Count; i++)
            {
                if (_arrows[i].MoveDirection== direction)
                {
                    return;
                }
            }

            EditorDirectionArrow arrow = Instantiate(_arrowPrefab, transform);
            arrow.SetMoveDirection(direction);
            _arrows.Add(arrow);
        }

        public void CleareMoveDirections()
        {
            for (int i = _arrows.Count - 1; i >= 0; i--)
            {
                Destroy(_arrows[i].gameObject);
            }
            _arrows.Clear();
        }

        public void SetAsDirectionSelector()
        {
            _enableToggle.graphic.color = Color.yellow;
        }
    }
}