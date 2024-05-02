using System.Collections;
using UnityEngine;

public class ManageTransitionScene : MonoBehaviour
{
    Animator anim;

    private void Start(){
        anim = GetComponent<Animator>();

        Invoke(nameof(OpenScene), 0.5f);

        if(FindFirstObjectByType<ManageTransitionScene>() != this){
            Destroy(FindFirstObjectByType<ManageTransitionScene>().gameObject);
            return;
        }
    }

    public void OpenScene(){
        anim.Play("Open");
    }

    public void CloseScene(){
        anim.Play("Close");
    }

    public void Transition(float timeTrans){
        StartCoroutine(TransScene(timeTrans));
    }

    IEnumerator TransScene(float timeTrans){
        CloseScene();
        yield return new WaitForSeconds(timeTrans);

        OpenScene();
        yield break;
    }
}
