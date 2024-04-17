using TMPro;
using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textTime;

    [Header("")]
    [SerializeField] private int coin = 1000;
    [Tooltip("Chest cooldown time: x(days) : y(hours) : z(minutes) : w(seconds)")]
    [SerializeField] private Vector4 timeRetrieval;

    Animator anim;
    ManageCoin manageCoin;
    AppearCoins appearCoins;

    private bool isCollected;
    private float day, hour, minute, second;

    private void Start(){
        anim = GetComponent<Animator>();

        SetTime();
    }

    private void Update(){
        UpdateTime();
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Player")){
            if(!isCollected){
                AddCoin();
                isCollected = true;
                anim.SetBool("isCollected", true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.CompareTag("Player")){
            anim.SetBool("isCollected", false);
        }
    }

    public void AddCoin(){
        if(manageCoin == null){
            manageCoin = FindObjectOfType<ManageCoin>();
            appearCoins = FindObjectOfType<AppearCoins>();
        }
        manageCoin.AddCoin(coin);
        appearCoins.AppearNotifi(coin, this.transform);
    }
    
    // Chỉnh thời gian hiện lên tại text để thể hiện thời gian còn lại cho đến lúc rương được mở lần kế tiếp
    #region Set up time

    private void SetTime(){
        day = timeRetrieval.x;
        hour = timeRetrieval.y;
        minute = timeRetrieval.z;
        second = timeRetrieval.w;
    }

    private void UpdateTime(){
        if(second < 0){
            second = 0;
            UpdateText();
        }
        if(second > 0){
            second -= Time.deltaTime;
            UpdateText();
            return;
        }
        if(minute > 0){
            minute--;
            second = 60;
            return;
        }
        if(hour > 0){
            hour--;
            minute = 59;
            second = 60;
        }
        if(day > 0){
            day--;
            hour = 23;
            minute = 59;
            second = 60;
        }
    }

    private void UpdateText(){
        if(day == 0){
            if(hour == 0){
                if(minute == 0){
                    if(second == 0){
                        textTime.text = "Ready";
                        return;
                    }
                }
                NullHour();
            } else {
                NullDay();
            }
        } else {
            NotNullDay();
        }
    }

    // Dinh dang cac loai hien thi gio
    private void NotNullDay(){
        textTime.text = ConsiderText(day) + ":" + ConsiderText(hour) + ":" + ConsiderText(minute) + ":" + ConsiderText(second);
    }

    private void NullDay(){
        textTime.text = ConsiderText(hour) + ":" + ConsiderText(minute) + ":" + ConsiderText(second);
    }

    private void NullHour(){
        textTime.text = ConsiderText(minute) + ":" + ConsiderText(second);
    }

    // Check hien thi cua tung loai thoi gian
    private string ConsiderText(float number){
        if(number < 10) return "0" + (int)number;
        return ((int)number).ToString();
    }

    #endregion

    public void SetTimeRetrieval(Vector4 timeRetrieval){
        this.timeRetrieval = timeRetrieval;
    }

    public Vector4 GetTimeRetrieval(){
        return this.timeRetrieval;
    }
}
