using UnityEngine;
using UnityEngine.SceneManagement;

public class BackMenu : MonoBehaviour
{
    public void Back(){
        SceneManager.LoadScene("Menu");
    }
}
