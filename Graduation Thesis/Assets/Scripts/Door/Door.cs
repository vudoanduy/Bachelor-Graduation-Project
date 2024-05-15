using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    Animator anim;

    private void Start(){
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player")){
            Destroy(other.gameObject.GetComponent<PlayerMove>());
            other.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            StartCoroutine(DisAppearPlayer(other.gameObject));

            string nameSceneCurrent = SceneManager.GetActiveScene().name;
            string idLevelCurrent = "";
            string stateLevelCurrent;
            int i = 0;
            int idStateLevelCurrent;

            while(IsNumber(Convert.ToChar(nameSceneCurrent.Substring(i, 1)))){
                idLevelCurrent += nameSceneCurrent.Substring(i, 1);
                i++;
            }
            stateLevelCurrent = nameSceneCurrent[(i + 1)..];
            if(stateLevelCurrent == "Easy"){
                idStateLevelCurrent = 0;
            } else if(stateLevelCurrent == "Medium"){
                idStateLevelCurrent = 1;
            } else {
                idStateLevelCurrent = 2;
            }

            FindObjectOfType<ManageLevel>().CheckUnlockNewLevel(Convert.ToInt32(idLevelCurrent), idStateLevelCurrent);
        }
    }

    IEnumerator DisAppearPlayer(GameObject player){
        anim.SetBool("isOpen", true);
        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);
        yield return new WaitForSeconds(0.5f);

        // keo nguoi choi vao trung tam cua
        LeanTween.move(player, this.transform.position, 0.5f);
        yield return new WaitUntil(() => player.transform.position == this.transform.position);
        yield return new WaitForSeconds(0.5f);

        // Thu nho nguoi choi
        LeanTween.scale(player, Vector2.zero, 1f);
        yield return new WaitUntil(() => player.transform.localScale.x == 0);
        yield return new WaitForSeconds(0.5f);

        anim.SetBool("isOpen", false);
        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);

        FindObjectOfType<ManageTransitionScene>().Transition(2);
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("Menu");
        yield break;
    }

    private bool IsNumber(char value){
        if(value >= 48 && value <= 57){
            return true;
        }
        return false;
    }
}
