using UnityEngine;

public class CannotDestroy : MonoBehaviour
{
    private void Start(){
        DontDestroyOnLoad(this.gameObject);
    }
}
