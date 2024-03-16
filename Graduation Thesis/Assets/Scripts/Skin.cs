public class Skin
{
    private string name;
    private int cost;
    private int state;

    public Skin(string name, int cost, int state){
        this.name = name;
        this.cost = cost;
        this.state = state;
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
}
