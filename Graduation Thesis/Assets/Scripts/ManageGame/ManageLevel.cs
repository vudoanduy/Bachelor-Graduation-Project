using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageLevel : MonoBehaviour
{
    private void Start(){
        if (FindFirstObjectByType<ManageLevel>() != this)
        {
            Destroy(FindFirstObjectByType<ManageLevel>().gameObject);
            return;
        }
    }

    public void JoinLevel(){
        SceneManager.LoadScene("LevelDemo");
    }
}
