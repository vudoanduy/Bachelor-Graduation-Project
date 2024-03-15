using UnityEngine;

public class MovePage : MonoBehaviour
{
    [Header("Set Target Page")]
    [SerializeField] GameObject currentPage;
    [SerializeField] GameObject targetPage;

    [Header("Check if u want keep current Page")]
    public bool isKeepCurrentPage;

    public void SetPage(){
        if(!isKeepCurrentPage){
            currentPage.gameObject.SetActive(false);
        } else {
            currentPage.gameObject.SetActive(true);
        }
        targetPage.gameObject.SetActive(true);
    }
}
