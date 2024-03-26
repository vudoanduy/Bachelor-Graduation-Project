using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization;

public class SetLevelName : MonoBehaviour
{
    [Header("Set text")]
    [SerializeField] LocalizedString localizedString;
    [SerializeField] TextMeshProUGUI text;

    [Header("Button appear after click btn_level")]
    [SerializeField] GameObject btn_Start;

    string s;

    bool isAppearStart = true;

    void OnEnable(){
        localizedString.Arguments = new object[]{s};
        localizedString.StringChanged += UpdateText;
    }

    void OnDisable(){
        localizedString.StringChanged -= UpdateText;
    }

    void Start(){
        btn_Start.SetActive(false);
    }

    private void UpdateText(string value)
    {
        text.text = value;
    }

    public void SetText(){
        if(isAppearStart){
            btn_Start.SetActive(true);
            isAppearStart = false;
        }

        s = EventSystem.current.currentSelectedGameObject.name;
        localizedString.Arguments[0] = s;
        localizedString.RefreshString();
    }
}
