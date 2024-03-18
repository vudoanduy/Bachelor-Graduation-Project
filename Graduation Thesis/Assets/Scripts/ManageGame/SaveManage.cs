using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class SaveManage : MonoBehaviour
{
    // Singleton
    private static SaveManage instance;

    public static SaveManage Instance{
        get{return instance;}
        set{}
    }

    //
    public static string SAVEDATA = "SAVEDATA";

    //
    public List<int> stateSkins = new List<int>{1,0,0,0};

    public List<int> quantityItems = new List<int>{0,0}; 

    public List<int> posSlots = new List<int>(){0};

    public List<int> quantitySlots = new List<int>(){0};

    public List<int> markSlots = new List<int>(){0};

    public int coinCurrent = 0;

    public int idSkinSelected = 0;

    //

    void Awake(){
        if(instance == null){
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    void Start(){
        LoadGame();
    }

    //
    public void SetStateSkins(List<int> stateSkins){
        this.stateSkins = stateSkins;
    }

    public void SetQuantityItems(List<int> quantityItems){
        this.quantityItems = quantityItems;
    }

    public void SetCoinCurrent(int coinCurrent){
        this.coinCurrent = coinCurrent;
    }

    public void SetPosSlots(List<int> posSlots){
        this.posSlots = posSlots;
    }

    public void SetQuantitySlots(List<int> quantitySlots){
        this.quantitySlots = quantitySlots;
    }

    public void SetMarkSlots(List<int> markSlots){
        this.markSlots = markSlots;
    }

    public void SetIDSkinSelected(int idSkinSelected){
        this.idSkinSelected = idSkinSelected;
    }

    //
    public List<int> GetStateSkins(){
        return stateSkins;
    }

    public List<int> GetQuantityItems(){
        return quantityItems;
    }

    public int GetCoinCurrent(){
        return coinCurrent;
    }

    public List<int> GetPosSlots(){
        return posSlots;
    }

    public List<int> GetQuantitySlots(){
        return quantitySlots;
    }

    public List<int> GetMarkSlots(){
        return markSlots;
    }

    public int GetIDSkinSelected(){
        return idSkinSelected;
    }

    //
    public void SaveGame(){
        string s = JsonUtility.ToJson(SaveManage.Instance);
        Debug.Log(s);
        SaveSystem.SetString(SAVEDATA, s);
    }

    public void LoadGame(){
        string s = SaveSystem.GetString(SAVEDATA);
        Debug.Log(s);
        if(s != string.Empty){
            SaveManageData obj = JsonUtility.FromJson<SaveManageData>(s);
            stateSkins = obj.stateSkins;
            quantityItems = obj.quantityItems;
            coinCurrent = obj.coinCurrent;
            posSlots = obj.posSlots;
            quantitySlots = obj.quantitySlots;
            markSlots = obj.markSlots;
            idSkinSelected = obj.idSkinSelected;
        }
    }

    //
    void OnApplicationQuit(){
        SaveGame();
    }
}
