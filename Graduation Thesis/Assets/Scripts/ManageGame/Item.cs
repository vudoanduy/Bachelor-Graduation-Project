public class Item
{
    #region Parameters

    private string name;
    public string Name{get{return name;} set{name = value;} }

    private int cost;
    public int Cost{get{return cost;} set{cost = value;}}

    private int quantity;
    public int Quantity{get{return quantity;} set{quantity = value;}}

    private int maxQuantity;
    public int MaxQuantity{get{return maxQuantity;} set{maxQuantity = value;}}
    #endregion

    public Item(string name, int cost, int quantity, int maxQuantity){
        this.name = name;
        this.cost = cost;
        this.quantity = quantity;
        this.maxQuantity = maxQuantity;
    }

    #region SetInfo
    public void ChangeName(string name, ref string nameItem){
        this.name = name;
        nameItem = name;
    }

    public void ChangeCost(int cost, ref int costItem){
        this.cost = cost;
        costItem = cost;
    }

    public void ChangeQuantity(int quantity){
        this.quantity = quantity;
    }

    public void ChangeMaxQuantity(int maxQuantity, ref int maxQuantityItem){
        this.maxQuantity = maxQuantity;
        maxQuantityItem = maxQuantity;
    }
    #endregion
}
