using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageResultUISlot : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI nameText;

    public void Init(ItemData itemData)
    {
        icon.sprite = itemData.Icon;
        nameText.text = itemData.Name;
    }
}
