using UnityEngine;

public class MovePage : MonoBehaviour
{
    [Header("Set Target Page")]
    [SerializeField] GameObject currentPage;
    [SerializeField] GameObject targetPage;

    [Header("Check if u want keep current Page")]
    public bool isKeepCurrentPage;

    // ham nay muc dich de di chuyen qua lai khi chuyen cac man hinh lien tuc
    public void SetPage(){
        if(!isKeepCurrentPage){
            currentPage.SetActive(false);
        } else {
            currentPage.SetActive(true);
        }
        targetPage.SetActive(true);
    }
}
