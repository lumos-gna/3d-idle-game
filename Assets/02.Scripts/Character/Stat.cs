
public class Stat
{
    public StatType type;
    
    public float curValue;
    public float maxValue;

    public Stat(StatData data)
    {
        type = data.type;
        maxValue = data.baseValue;
    }
}