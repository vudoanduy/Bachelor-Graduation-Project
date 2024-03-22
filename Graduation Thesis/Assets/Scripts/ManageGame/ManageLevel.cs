using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageLevel : MonoBehaviour
{
    void Start(){
        if (GameObject.FindFirstObjectByType<ManageLevel>() != this)
        {
            Destroy(GameObject.FindFirstObjectByType<ManageLevel>().gameObject);
            return;
        }
    }

    public void JoinLevel(){
        SceneManager.LoadScene("1");
    }
}
