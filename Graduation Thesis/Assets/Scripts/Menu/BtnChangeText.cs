using System.Linq;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class BtnChangeText : MonoBehaviour
{
    // Y tuong la neu set Script vao 1 nut luu trang thai thi nguoi choi hoan toan co the bo qua Set Button
    //      VD: Music: (On/Off)
    // Neu nguoi choi su dung 2 nut bam de set trang thai thi hay su dung set Button
    //      VD: Language:  <Prev> (English/Vietnamese/France) <Next>
    //      + Neu nguoi choi khong muon gioi han ve 2 dau, tuc la khi dang o English an prev quay ve France thi hay tich vao gia tri bool
    //      + Nguoc lai, nguoi choi muon gioi han 2 dau thi khong can tich vao bool

    [Header("List localizedString")]
    [SerializeField] LocalizedString[] textChange;
    [Header("List button impact on text (use if have 2 btn, otherwise can ignore it)")]
    [SerializeField] Button[] button;

    LocalizeStringEvent localizeStringEvent;

    int idText;
    int countText;

    [Header("Check if u don't want button to loop endlessly")]
    public bool isSetOffButton; 


    private void Start(){
        idText = 0;
        countText = textChange.Count();

        localizeStringEvent = this.GetComponent<LocalizeStringEvent>();

        CheckText();
    } 

    // Xet dieu kien de thay doi text lap lai hay khong
    public void ChangeText(int num){
        idText += num;
        CheckText();
    }

    public void CheckText(){
        if(isSetOffButton){
            TurnOffButton();
        } else {
            if(idText < 0){
                idText = countText - 1;
            } else if(idText == countText){
                idText = 0;
            }
        }
        localizeStringEvent.StringReference = textChange[idText];
    }

    // Set trang thai khi nguoi choi tich vao bool
    public void TurnOffButton(){
        if(idText == 0){
            button[0].interactable = false;
            button[1].interactable = true;
        } else if(idText == countText - 1){
            button[0].interactable = true;
            button[1].interactable = false;
        } else {
            button[0].interactable = true;
            button[1].interactable = true;
        }
    }
    
    public void DefautText(){
        idText = 0;
        CheckText();
    }

    //
}
