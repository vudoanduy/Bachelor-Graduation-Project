using System;
using System.Collections;
using System.Collections.Generic;
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

    public int coinCurrent = 0;
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

    //
    public void SaveGame(){
        string s = JsonUtility.ToJson(SaveManage.Instance);
        SaveSystem.SetString(SAVEDATA, s);
    }

    public void LoadGame(){
        string s = SaveSystem.GetString(SAVEDATA);
        if(s != string.Empty){
            SaveManageData obj = JsonUtility.FromJson<SaveManageData>(s);
            stateSkins = obj.stateSkins;
            quantityItems = obj.quantityItems;
            coinCurrent = obj.coinCurrent;
        }
    }

    //
    void OnApplicationQuit(){
        SaveGame();
    }
}
