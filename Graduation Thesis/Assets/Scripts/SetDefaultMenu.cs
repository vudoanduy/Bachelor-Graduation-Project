using UnityEngine;

public class SetDefaultMenu : MonoBehaviour
{
    [Header("List Gameobject need set default")]
    [SerializeField] GameObject[] gameObjects;

    [Header("Check if u want turn off first Object")]
    public bool isTurnOffFirstObject;

    void Start(){
        int count = gameObjects.Length;
        if(isTurnOffFirstObject){
            gameObjects[0].gameObject.SetActive(false);
        } else {
            gameObjects[0].gameObject.SetActive(true);
        }

        for(int i = 1; i < count; i++){
            gameObjects[i].gameObject.SetActive(false);
        }
    }
}
