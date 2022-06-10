using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace SpaceHorror.InventorySystem.UI
{
    public class CellUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text debugText;
        [SerializeField] private Image _background;
        [SerializeField] private Color _filledColor, _emptyColor;

        private Cell _targetCell;

        public Cell TargetCell { get => _targetCell; }

        public void SetTargetCell(Cell targetCell)
        {
            _targetCell = targetCell;

            if(debugText != null) debugText.text = targetCell.Position.ToString();

            gameObject.name = targetCell.Position.ToString();

            UpdateCellValues(targetCell);

            targetCell.onCellUpdated += UpdateCellValues;
        }

        private void UpdateCellValues(Cell targetCell)
        {
            if(targetCell.InUse)
            {
                _background.color = _filledColor;
            }
            else
            {
                _background.color = _emptyColor;
            }
        }

    }
}

