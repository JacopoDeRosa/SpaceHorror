using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryWindow : MonoBehaviour
{
    [SerializeField] private InventorySlotUI[] _allSlots;

    private Queue<InventorySlotUI> _freeSlots;

    private void Start()
    {
        ResetWindow();
    }


    private void ResetWindow()
    {
        foreach (InventorySlotUI slot in _allSlots)
        {
            slot.gameObject.SetActive(false);
        }

        _freeSlots = new Queue<InventorySlotUI>(_allSlots);
    }
}


