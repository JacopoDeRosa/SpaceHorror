using UnityEngine;

namespace SpaceHorror.InventorySystem
{
    public delegate void ItemSlotHandler(ItemSlot slot);
    public delegate void CellHandler(Cell cell);
    public delegate void PositionChangeHAndler(Vector2Int position);
 
}

namespace SpaceHorror.InventorySystem.UI
{
    public delegate void ItemSlotUIHandler(ItemSlotUI slot);
    public delegate void CompareWindowHandler(CompareWindow window);
}

namespace SpaceHorror
{
    public delegate void IntHandler(int amount);
}