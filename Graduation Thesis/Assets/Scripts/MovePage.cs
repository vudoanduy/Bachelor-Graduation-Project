using UnityEngine;

public class MovePage : MonoBehaviour
{
    [Header("Set Target Page")]
    [SerializeField] GameObject currentPage;
    [SerializeField] GameObject targetPage;

    public void SetPage(){
        currentPage.gameObject.SetActive(false);
        targetPage.gameObject.SetActive(true);
    }
}
