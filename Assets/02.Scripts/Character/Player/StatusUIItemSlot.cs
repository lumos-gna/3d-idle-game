using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StatusUIItemSlot : MonoBehaviour
{
    [field: SerializeField] public ItemType ItemType { get; private set; }

    [SerializeField] private Button button;

    public void Init(UnityAction<StatusUIItemSlot> onClickAction)
    {
        button.onClick.AddListener(()=>onClickAction?.Invoke(this));
    }
}
