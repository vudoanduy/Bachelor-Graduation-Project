using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManageLevel : MonoBehaviour
{
    private static string nameSceneLoad;

    BtnChangeText btnChangeText;
    SetLevelName setLevelName;

    private int idLevelMax = 1;
    private int idStateLevelMax = 0; // 0: de, 1: thuong, 2: kho

    private void Start(){
        SaveManage.Instance.LoadGame();
        idLevelMax = SaveManage.Instance.GetIDLevelMax();
        idStateLevelMax = SaveManage.Instance.GetIDStateLevelMax();
        if (FindFirstObjectByType<ManageLevel>() != this)
        {
            Destroy(FindFirstObjectByType<ManageLevel>().gameObject);
            return;
        }
    }

    // Cai hien thi cac level da choi
    // Neu da qua het man de thi se mo het 15 man choi
    public void SetUpLevel(){
        if(idStateLevelMax == 0){
            if(GameObject.Find("btn_Level") != null){
                GameObject btnLevel = GameObject.Find("btn_Level");
                for(int i = 0; i < 15; i++){
                    if(i < idLevelMax){
                        btnLevel.transform.GetChild(i).GetComponent<Button>().interactable = true;
                    } else {
                        btnLevel.transform.GetChild(i).GetComponent<Button>().interactable = false;
                    }
                }
            }
        }
    }

    #region Check and Unlock New Level
    // Kiem tra xem man choi hien tai co phai moi nhat khong de set up nut vao man choi
    public void CheckLevelCurrent(){
        if(btnChangeText == null){
            btnChangeText = FindObjectOfType<BtnChangeText>();
            setLevelName = FindObjectOfType<SetLevelName>();
        }

        int idLevelCurrent = setLevelName.GetLevelId();
        int idStateLevelCurrent = btnChangeText.GetIDStateLevel();

        if(idStateLevelCurrent > idStateLevelMax){
            GameObject.Find("btn_Start").GetComponent<Button>().interactable = false;
        } else if(idStateLevelCurrent == idStateLevelMax){
            if(idLevelCurrent > idLevelMax){
                GameObject.Find("btn_Start").GetComponent<Button>().interactable = false;
            } else {
                GameObject.Find("btn_Start").GetComponent<Button>().interactable = true;
            }
        }
    }

    // Neu man choi hien tai nho hon man choi max thi bo qua
    // Neu choi toi man 15 de thi mo khoa 1 thuong, 15 thuong thi mo khoa 1 kho, 15 kho thi bo qua
    // 
    public void CheckUnlockNewLevel(int idLevelCurrent, int idStateLevelCurrent){
        if(idStateLevelCurrent == idStateLevelMax){
            if(idLevelCurrent == idLevelMax){
                if(idLevelCurrent == 15 && idStateLevelMax != 2){
                    idLevelMax = 1;
                    idStateLevelMax++;
                    SaveManage.Instance.SetIDLevelMax(idLevelMax);
                    SaveManage.Instance.SetIDStateLevelMax(idStateLevelMax);
                } else if(idLevelCurrent < 15){
                    idLevelMax++;
                    SaveManage.Instance.SetIDLevelMax(idLevelMax);
                }
            }
        }
    }

    #endregion

    // Vao mot man choi nao do
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
