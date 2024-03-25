using UnityEngine;

public class Skin
{
    private string name;
    private int cost;
    private int state;
    private int hpSkin;
    public int HPSkin{get{return hpSkin;} set{hpSkin = value;}}
    private int timeImmortalSkin;
    public int TimeImmortalSkin{get{return timeImmortalSkin;} set{timeImmortalSkin = value;}}

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

    public Sprite GetSpriteSkin(){
        return this.imageSkin;
    }

    
}
