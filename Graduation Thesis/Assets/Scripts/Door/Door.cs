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
        }
    }

    IEnumerator DisAppearPlayer(GameObject player){
        anim.SetBool("isOpen", true);
        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);
        yield return new WaitForSeconds(0.5f);

        LeanTween.move(player, this.transform.position, 0.5f);
        yield return new WaitUntil(() => player.transform.position == this.transform.position);
        yield return new WaitForSeconds(0.5f);

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
}
