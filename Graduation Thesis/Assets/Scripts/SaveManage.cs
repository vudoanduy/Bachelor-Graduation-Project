using UnityEngine;

public class SaveManage : MonoBehaviour
{
    // Singleton
    private static SaveManage instance;

    public static SaveManage Instance{
        get{return instance;}
        set{}
    }

    //

    void Awake(){
        if(instance == null){
            instance = this;
        } else {
            Destroy(instance);
        }
    }

    //
}
