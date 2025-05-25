using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public int money;
    public long lastQuitTime;              // Oyun kapanmadan önceki UTC zaman (ticks)
    public List<ZoneSaveData> zones = new List<ZoneSaveData>();
} 
