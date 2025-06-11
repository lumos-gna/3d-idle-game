using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI stageText;
    
    [SerializeField] private StageResultUI stageResultUI;
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private StatusUI statusUI;

    public void Init()
    {
        inventoryUI.Init();
        statusUI.Init();
    }

    public void UpdateGoldText(BigInteger amount)
    {
        if (amount < 1000)
        {
            goldText.text = "Gold : " + amount;

            return;
        }

        string[] units = { "", "k", "m", "b", "t", "q", "Q", "s", "S", "o", "n" };
        int unitIndex = 0;
        BigInteger divisor = 1000;

        while (amount >= divisor && unitIndex < units.Length - 1)
        {
            amount /= divisor;
            unitIndex++;
        }

        string formatString = unitIndex == 0 ? "{0}" : "{0:0.#}{1}";
        
        goldText.text = "Gold : " + string.Format(formatString, amount, units[unitIndex]);
    }

    public void UpdateStageText(int stageLevel)
    {
        stageText.text = "Stage : " + (stageLevel + 1);
    }

    public void ShowStageResult(List<Item> rewardItems)
    {
        stageResultUI.Show(rewardItems);
    }
}
