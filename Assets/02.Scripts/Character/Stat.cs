using UnityEngine.Events;

public class Stat
{
    public StatType type;
    public float value;

    public event UnityAction OnStatModified;

    public Stat(StatData data)
    {
        value = data.baseValue;
    }
    
    public void ModifyStatValue(float amount)
    {
        value += amount;
        
        OnStatModified?.Invoke();
    }
}