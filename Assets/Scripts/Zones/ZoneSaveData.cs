using System;
using System.Collections.Generic;

[Serializable]
public class ZoneSaveData
{
    public string zoneName;
    public List<ProducerSaveData> producers = new List<ProducerSaveData>();
}
