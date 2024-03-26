using UnityEngine;

public class Skin
{
    private string name;
    private int state;

    private int cost;
    public int Cost{get{return cost;} set{cost = value;}}
    
    private int hpSkin;
    public int HPSkin{get{return hpSkin;} set{hpSkin = value;}}
    
    private int timeImmortalSkin;
    public int TimeImmortalSkin{get{return timeImmortalSkin;} set{timeImmortalSkin = value;}}

    protected Sprite imageSkin;
    public Sprite ImageSkin{get {return imageSkin;} set{imageSkin = value;}}

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

    public void ChangeState(int state){
        this.state = state;
    }  
}
