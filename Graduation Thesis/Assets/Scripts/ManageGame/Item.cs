public class Item
{
    private string name;
    private int cost;
    private int quantity;
    private int maxQuantity;

    public Item(string name, int cost, int quantity, int maxQuantity){
        this.name = name;
        this.cost = cost;
        this.quantity = quantity;
        this.maxQuantity = maxQuantity;
    }

    public void ChangeName(string name, ref string nameItems){
        this.name = name;
        nameItems = name;
    }

    public void ChangeCost(int cost, ref int costItems){
        this.cost = cost;
        costItems = cost;
    }

    public void ChangeQuantity(int quantity){
        this.quantity = quantity;
    }

    public void ChangeMaxQuantity(int maxQuantity, ref int maxQuantityItems){
        this.maxQuantity = maxQuantity;
        maxQuantityItems = maxQuantity;
    }
}
