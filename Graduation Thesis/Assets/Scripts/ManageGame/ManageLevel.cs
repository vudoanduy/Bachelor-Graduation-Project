using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageLevel : MonoBehaviour
{
    private static string nameSceneLoad;

    BtnChangeText btnChangeText;
    SetLevelName setLevelName;

    private void Start(){
        if (FindFirstObjectByType<ManageLevel>() != this)
        {
            Destroy(FindFirstObjectByType<ManageLevel>().gameObject);
            return;
        }
    }

    public void JoinLevel(){
        if(btnChangeText == null){
            btnChangeText = FindObjectOfType<BtnChangeText>();
            setLevelName = FindObjectOfType<SetLevelName>();
        }
        nameSceneLoad = setLevelName.GetLevelId().ToString() + "_" + btnChangeText.GetStateLevel();

        Debug.Log(nameSceneLoad);
        SceneManager.LoadScene(nameSceneLoad);
    }
}
