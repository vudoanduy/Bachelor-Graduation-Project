using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


// Xet anim nhanh chong cho cac button khi click vao
// Khi nguoi choi an giu thi anim hold va tha ra se la anim exit

public class AnimButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Header("Select Style Button")]
    [SerializeField] protected bool isRectangle;
    [SerializeField] protected bool isCircle;
    [SerializeField] protected bool isOptions;

    Sprite btn_Hold, btn_Exit;

    readonly string btnCircle_ExitPath = "Menu/layer1";
    readonly string btnRectangle_ExitPath = "Menu/layer3";
    readonly string btnOptions_ExitPath = "Menu/layer5";

    readonly string btnCircle_HoldPath = "Menu/layer2";
    readonly string btnRectangle_HoldPath = "Menu/layer4";
    readonly string btnOptions_HoldPath = "Menu/layer6";

    private void Start(){
        CheckStyleButton();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        this.GetComponent<Image>().sprite = btn_Hold;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        this.GetComponent<Image>().sprite = btn_Exit;
    }

    // Xet sprite dau vao cua button
    protected void CheckStyleButton(){
        if(isCircle){
            SetPathButton(btnCircle_HoldPath, btnCircle_ExitPath);
        } else if(isRectangle){
            SetPathButton(btnRectangle_HoldPath, btnRectangle_ExitPath);
        } else if(isOptions){
            SetPathButton(btnOptions_HoldPath, btnOptions_ExitPath);
        }
    }

    protected void SetPathButton(string pathHold, string pathExit){
        this.btn_Hold = Resources.Load<Sprite>(pathHold);   
        this.btn_Exit = Resources.Load<Sprite>(pathExit);   
    }

}
