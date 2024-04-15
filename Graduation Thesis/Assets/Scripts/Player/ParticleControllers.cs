using System;
using UnityEngine;

public class ParticleControllers : MonoBehaviour
{
    [SerializeField] ParticleSystem movementParticle;
    [SerializeField] ParticleSystem fallParticle;
    [SerializeField] ParticleSystem slidingParticle;
    [SerializeField] ParticleSystem jumpParticle;

    [SerializeField] Rigidbody2D rbPlayer;

    [Range(0,1)]
    [SerializeField] private float timeSpawn = 0.1f;

    private bool isGround;

    private float countTime = 0;

    private void Update(){
        countTime += Time.deltaTime;

        if(Math.Abs(rbPlayer.velocity.x) > 1){  
            if(countTime > timeSpawn && isGround){
                movementParticle.Play();
                countTime = 0;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Ground")){
            Debug.Log("Fall Particle called");
            fallParticle.Play();
            isGround = true;
        }
    }

    public void SlidingParticlePlay(){
        if(countTime > timeSpawn){

        }
        slidingParticle.Play();
    }

    public void JumpParticlePlay(){
        jumpParticle.Play();
    }

    void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.CompareTag("Ground")){
            isGround = false;
        }
    }
}
