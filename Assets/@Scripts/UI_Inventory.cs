using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

public class UI_Inventory : MonoBehaviour
{
    private TextMeshProUGUI _inventoryItemCountText;
    private int _inventoryItemCount = 0;

    void Start()
    {
        _inventoryItemCountText = GetComponent<TextMeshProUGUI>();       

        DropItem.ItemPickedUp += HandleItemPickedUp;
    }

    private void HandleItemPickedUp()
    {
        _inventoryItemCount += 1;

        _inventoryItemCountText.text = _inventoryItemCount.ToString();
    }
}
