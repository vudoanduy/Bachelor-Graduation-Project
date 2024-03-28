using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class ChangeLocale : MonoBehaviour
{
    private int localeID;

    void Start(){
        SaveManage.Instance.LoadGame();

        localeID = SaveManage.Instance.GetLocaleID();

        SwitchLocale(localeID);
    }


    IEnumerator SetLocale(int localeID){
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
        yield break;
    }

    public void SwitchLocale(int localeID){
        Debug.Log("Thay doi ngon ngu sang " + localeID);
        StartCoroutine(SetLocale(localeID));
        SaveManage.Instance.SetLocaleID(localeID);
    }
}
