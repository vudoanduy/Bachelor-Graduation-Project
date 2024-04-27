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
    public List<int> stateSkins = new() { 1,0,0,0};

    public List<int> quantityItems = new() { 0,0}; 

    public List<int> posSlots = new(){0};

    public List<int> quantitySlots = new(){0};

    public List<int> markSlots = new(){0};

    public int coinCurrent = 1000;

    public int idSkinSelected = 0;

    public int localeID = 0;

    //

    private void Awake(){
        if(instance == null){
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    private void Start(){
        LoadGame();
    }

    #region Set Info
    public void SetStateSkins(List<int> stateSkins){
        this.stateSkins = stateSkins;
        SaveGame();
    }

    public void SetQuantityItems(List<int> quantityItems){
        this.quantityItems = quantityItems;
        SaveGame();
    }

    public void SetCoinCurrent(int coinCurrent){
        this.coinCurrent = coinCurrent;
        SaveGame();
    }

    public void SetPosSlots(List<int> posSlots){
        this.posSlots = posSlots;
        SaveGame();
    }

    public void SetQuantitySlots(List<int> quantitySlots){
        this.quantitySlots = quantitySlots;
        SaveGame();
    }

    public void SetMarkSlots(List<int> markSlots){
        this.markSlots = markSlots;
        SaveGame();
    }

    public void SetIDSkinSelected(int idSkinSelected){
        this.idSkinSelected = idSkinSelected;
        SaveGame();
    }

    public void SetLocaleID(int localeID){
        this.localeID = localeID;
        SaveGame();
    }

    #endregion


    #region Get Info
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

    public int GetLocaleID(){
        return localeID;
    }

    #endregion

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
            localeID = obj.localeID;
        }
    }

    //
    void OnApplicationQuit(){
        SaveGame();
    }
}
