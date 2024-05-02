using System.Collections;
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

        StartCoroutine(LoadLevelScene(nameSceneLoad));
    }

    IEnumerator LoadLevelScene(string nameSceneLoad){
        FindObjectOfType<ManageTransitionScene>().Transition(2f);
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(nameSceneLoad);
    }
}
