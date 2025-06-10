using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StatusUIItemSlot : MonoBehaviour
{
    [field: SerializeField] public ItemType ItemType { get; private set; }

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    public void Init(UnityAction<StatusUIItemSlot> onClickAction)
    {
        _button.onClick.AddListener(()=>onClickAction?.Invoke(this));
    }
}
