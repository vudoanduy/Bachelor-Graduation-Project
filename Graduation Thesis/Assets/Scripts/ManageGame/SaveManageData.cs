using System;
using System.Collections.Generic;

[Serializable]
public class SaveManageData
{
    public List<int> stateSkins = new() {1,0,0,0};

    public List<int> quantityItems = new() {0,0}; 

    public List<int> posSlots = new(){};

    public List<int> quantitySlots = new(){0};

    public List<int> markSlots = new(){0};

    public int coinCurrent = 0;

    public int idSkinSelected = 0;

    public int localeID = 0;

}
