using UnityEngine;

public class Skin
{
    private string name;
    private int cost;
    private int state;
    private int hpSkin;
    private int timeImmortalSkin;
    private string infoSkin;
    Sprite imageSkin;

    public Skin(string name, int cost, int state, int hpSkin, int timeImmortalSkin, Sprite imageSkin){
        this.name = name;
        this.cost = cost;
        this.state = state;
        this.hpSkin = hpSkin;
        this.timeImmortalSkin = timeImmortalSkin;
        this.imageSkin = imageSkin;
    }

    //
    public void ChangeName(string name, ref string nameSkins){
        this.name = name;
        nameSkins = name;
    }

    public void ChangeCost(int cost, ref int costSkins){
        this.cost = cost;
        costSkins = cost;
    }

    public void ChangeState(int state){
        this.state = state;
    }

    public void SetInfoSkin(string info){
        this.infoSkin = info;
    }

    public Sprite GetSpriteSkin(){
        return this.imageSkin;
    }

    
}
