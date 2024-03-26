using UnityEngine;

public class SetDefaultMenu : MonoBehaviour
{
    [Header("List Gameobject need set default")]
    [SerializeField] GameObject[] gameObjects;

    [Header("Check if u want turn off first Object")]
    public bool isTurnOffFirstObject;

    // ham nay de cai mac dinh cac object khac off
    void OnEnable(){
        int count = gameObjects.Length;
        if(isTurnOffFirstObject){
            gameObjects[0].SetActive(false);
        } else {
            gameObjects[0].SetActive(true);
        }

        for(int i = 1; i < count; i++){
            gameObjects[i].SetActive(false);
        }
    }
}
