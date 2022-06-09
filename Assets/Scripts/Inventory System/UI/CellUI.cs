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

        private Cell _targetCell;

        public Cell TargetCell { get => _targetCell; }

        public void SetTargetCell(Cell targetCell)
        {
            _targetCell = targetCell;
            debugText.text = targetCell.Position.ToString();
            gameObject.name = targetCell.Position.ToString();
            if (_targetCell.InUse) GetComponent<Image>().color = Color.red;
        }

    }
}

