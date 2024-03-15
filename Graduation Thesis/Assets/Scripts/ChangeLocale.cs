using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class ChangeLocale : MonoBehaviour
{
    IEnumerator SetLocale(int localeID){
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
    }

    public void SwitchLocale(int localeID){
        StartCoroutine(SetLocale(localeID));
    }
}
