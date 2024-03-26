using UnityEngine;

public class CannotDestroy : MonoBehaviour
{
    void Start(){
        DontDestroyOnLoad(this.gameObject);
    }
}
