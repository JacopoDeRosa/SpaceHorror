using UnityEngine;

namespace SpaceHorror.InventorySystem
{
    public delegate void ItemSlotHandler(ItemSlot slot);
    public delegate void CellHandler(Cell cell);
    public delegate void PositionChangeHAndler(Vector2Int position);
    public delegate void IntHandler(int amount);
}

namespace SpaceHorror.InventorySystem.UI
{
    public delegate void ItemSlotUIHandler(ItemSlotUI slot);
}