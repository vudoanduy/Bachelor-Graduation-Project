using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannotDestroy : MonoBehaviour
{
    void Start(){
        DontDestroyOnLoad(this.gameObject);
        Debug.Log(this.gameObject.name);
    }
}
