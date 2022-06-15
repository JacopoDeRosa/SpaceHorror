using SpaceHorror.InventorySystem.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceHorror.InventorySystem.UI
{
    public class InventoryWindow : MonoBehaviour
    {
        public const int cellSizeX = 100;
        public const int cellSizeY = 100;
        public const int cellsBorder = 2;

        [SerializeField] private ItemSlotUI[] _allSlots;
        [SerializeField] private CellUI[] _allCells;
        [SerializeField] private Vector2Int _padding;
        [SerializeField] private bool _dynamicWindow;
        [SerializeField] private RectTransform _cellsContainer, _slotsContainer;
        [SerializeField] private Inventory _starterInventory;
        [SerializeField] private SlotOptionsMenu _optionsMenu;
        [SerializeField] private Inspector _inspector;

        private Queue<ItemSlotUI> _freeSlots;
        private Queue<CellUI> _freeCells;

        private CellUI[,] _cellsGrid;

        private bool _windowStarted;

        private void Awake()
        {
            SubToEvents();
        }

        private void Start()
        {
          if(_starterInventory) ReadInventory(_starterInventory);
        }

        public void ResetWindow()
        {
            foreach (ItemSlotUI slot in _allSlots)
            {
                slot.gameObject.SetActive(false);
            }
            foreach (CellUI cell in _allCells)
            {
                cell.gameObject.SetActive(false);
            }
            _freeSlots = new Queue<ItemSlotUI>(_allSlots);
            _freeCells = new Queue<CellUI>(_allCells);           
        }

        private void SubToEvents()
        {
            foreach (ItemSlotUI slot in _allSlots)
            {
                slot.onSlotDeath += RecycleSlotUI;
            }
        }

        public void SetInventory(Inventory inventory)
        {
            if(inventory == null)
            {
                ResetWindow();
            }
            else
            {
                ReadInventory(inventory);
            }       
        }

        public CellUI GetCell(Vector2Int position)
        {
            if (_cellsGrid.GetLength(0) <= position.x || _cellsGrid.GetLength(1) <= position.y || position.x < 0 || position.y < 0) return null;

            return _cellsGrid[position.x, position.y];
        }

        private void ReadInventory(Inventory inventory)
        {
            if(_windowStarted)
            {
                ResetWindow();
            }
            else
            {
                StartWindow();
            }

            if (_dynamicWindow)
            {
                RectTransform rectTransform = transform as RectTransform;
                if(rectTransform)
                {
                    rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, inventory.Size.x * (cellSizeX + cellsBorder) + _padding.x * 2);
                    rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, inventory.Size.y * (cellSizeY + cellsBorder) + _padding.y * 2);
                }
            }

            _cellsGrid = new CellUI[inventory.Size.x, inventory.Size.y];

            for (int y = 0; y < inventory.Size.y; y++)
            {
                for (int x = 0; x < inventory.Size.x; x++)
                {
                    CellUI cell = _freeCells.Dequeue();

                    cell.gameObject.SetActive(true);
                    cell.SetTargetCell(inventory.GetCell(x, y));

                    int posX = (int)-(_cellsContainer.rect.width / 2);
                    int posY = (int)(_cellsContainer.rect.height / 2);

                    posX += _padding.x;
                    posY -= _padding.y;

                    posX += cellSizeX / 2;
                    posY -= cellSizeY / 2;

                    posX += (cellSizeX + cellsBorder) * x;
                    posY -= (cellSizeY + cellsBorder) * y;

                    cell.transform.localPosition = new Vector3Int(posX, posY);
                    _cellsGrid[x, y] = cell;
                }
            }
            foreach (ItemSlot slot in inventory.AllItems)
            {
                var uiSlot = _freeSlots.Dequeue();
                uiSlot.gameObject.SetActive(true);
                uiSlot.SetItemSlot(slot);
                uiSlot.SetPosition(_cellsGrid[slot.Position.x, slot.Position.y].transform.position);
            }
        }

        private void RecycleSlotUI(ItemSlotUI slot)
        {
            slot.SetOptionsMenu(_optionsMenu);
            slot.SetInspector(_inspector);
            slot.SetItemSlot(null);
            _freeSlots.Enqueue(slot);
            slot.gameObject.SetActive(false);   
        }

        public void StartWindow()
        {
            if (_windowStarted) return;

            foreach (ItemSlotUI slot in _allSlots)
            {
                slot.SetOptionsMenu(_optionsMenu);
                slot.SetInspector(_inspector);
                slot.SetParentWindow(this);
                slot.gameObject.SetActive(false);
            }

            foreach (CellUI cell in _allCells)
            {
                cell.gameObject.SetActive(false);
            }

            _freeSlots = new Queue<ItemSlotUI>(_allSlots);
            _freeCells = new Queue<CellUI>(_allCells);

            _windowStarted = true;
        }       
    }
}


