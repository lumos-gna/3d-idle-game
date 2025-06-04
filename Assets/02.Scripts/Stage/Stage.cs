using System.Collections.Generic;

public class Stage
{
    private StageData _stageData;
    private List<Room> _rooms;
    
    public Stage(StageData stageData, List<Room> rooms)
    {
        _stageData = stageData;
        _rooms = rooms;
    }
}