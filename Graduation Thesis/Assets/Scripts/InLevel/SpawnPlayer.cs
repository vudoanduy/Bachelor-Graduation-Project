using System.Collections;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] private GameObject playerResources;

    [Header("")]
    [Tooltip("The location where the player is spawned")]
    [SerializeField] private Vector3 startPos;

    private void Start(){
        SpawnNewPlayer(startPos);
    }

    public void SpawnNewPlayer(Vector3 spawnPos){
        StartCoroutine(Spawn(startPos));
    }

    IEnumerator Spawn(Vector3 spawnPos){
        GameObject newPlayer = Instantiate(playerResources);

        Animator anim = newPlayer.GetComponent<Animator>();
        Rigidbody2D rbPlayer = newPlayer.GetComponent<Rigidbody2D>();

        newPlayer.transform.localPosition = new Vector3(spawnPos.x, spawnPos.y, 0);
        newPlayer.name = "Player";
        newPlayer.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0);

        rbPlayer.simulated = false;
        yield return new WaitForSeconds(2); // time close + time pause to trans scene

        newPlayer.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
        anim.Play("AppearPlayer");   
        yield return new WaitForSeconds(0.583f);

        anim.SetTrigger("appeared");    
        rbPlayer.simulated = true;
        yield break;
    }

    // Hoi sinh nguoi choi tai checkpoint hoac vi tri ban dau 
    public void RevivalPlayer(){
        StartCoroutine(Revival(2));
    }

    IEnumerator Revival(float time){
        FindObjectOfType<ManageTransitionScene>().Transition(2); // time close (1) + time pause (1) to trans scene
        yield return new WaitForSeconds(1);

        SpawnNewPlayer(startPos);
        yield break;
    }
}
