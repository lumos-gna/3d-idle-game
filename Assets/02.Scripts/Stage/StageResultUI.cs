using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageResultUI : MonoBehaviour
{
    [SerializeField] private StageResultUISlot slotPrefab;
    
    [SerializeField] private Transform slotsParent;

    public void Show(List<Item> rewardItems)
    {
        gameObject.SetActive(true);
        
        if (rewardItems.Count == 0)
            return;

        for (int i = 0; i < rewardItems.Count; i++)
        {
            var createdSlot = Instantiate(slotPrefab, slotsParent);
            
            createdSlot.Init(rewardItems[i].itemData);
        }
    }

    public void Hide()
    {
        for (int i = 0; i < slotsParent.childCount; i++)
        {
            Destroy(slotsParent.GetChild(i).gameObject);
        }

        GameManager.Instance.StartNextStage();

        gameObject.SetActive(false);
        
    }
}
