using System;
using System.Collections.Generic;

[Serializable]
public class SaveManageData
{
    public List<int> stateSkins = new List<int>{1,0,0,0};

    public List<int> quantityItems = new List<int>{0,0}; 

    public List<int> posSlots = new List<int>(){};

    public List<int> quantitySlots = new List<int>(){0};

    public List<int> markSlots = new List<int>(){0};

    public int coinCurrent = 0;

    public int idSkinSelected = 0;

}
