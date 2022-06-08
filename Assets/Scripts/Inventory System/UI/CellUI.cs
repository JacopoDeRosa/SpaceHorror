using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace SpaceHorror.InventorySystem.UI
{
    public class CellUI : MonoBehaviour
    {

        private Cell _targetCell;

        public Cell TargetCell { get => _targetCell; }

        public void SetPosition(Cell targetCell)
        {
            _targetCell = targetCell;
        }

    }
}

