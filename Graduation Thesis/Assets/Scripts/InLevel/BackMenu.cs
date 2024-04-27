using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackMenu : MonoBehaviour
{
    public void Back(){
        StartCoroutine(ReturnHome());
    }

    IEnumerator ReturnHome(){
        FindObjectOfType<ManageTransitionScene>().Transition(2); // time close (1) + time pause (1) to trans scene
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("Menu");
        yield break;
    }
}
