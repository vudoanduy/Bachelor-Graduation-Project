using UnityEngine;

public class SaveCheckpoint : MonoBehaviour
{
    Animator anim;

    bool isSavePos;

    private void Start(){
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Player")){
            if(!isSavePos){
                isSavePos = true;
                FindObjectOfType<SpawnPlayer>().SetPosSpawnPlayer(this.transform.position);
                PlayAnimFlagOut();
                Invoke(nameof(PlayIdleFlag), 2.167f);
            }
        }
    }

    private void PlayAnimFlagOut(){
        anim.SetTrigger("touchPlayer");
    }

    private void PlayIdleFlag(){
        anim.SetTrigger("outPlayer");
    }
}
