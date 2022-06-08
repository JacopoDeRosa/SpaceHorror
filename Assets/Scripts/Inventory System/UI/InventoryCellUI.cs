using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace SpaceHorror.InventorySystem.UI
{
    public class InventoryCellUI : MonoBehaviour
    {

        private InventoryCell _targetCell;

        public InventoryCell TargetCell { get => _targetCell; }

        public void SetPosition(InventoryCell targetCell)
        {
            _targetCell = targetCell;
        }

    }
}

