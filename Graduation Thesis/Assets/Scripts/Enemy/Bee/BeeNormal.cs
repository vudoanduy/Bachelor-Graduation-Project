using ExitGames.Client.Photon.StructWrapping;
using UnityEngine;

public class BeeNormal : MonoBehaviour
{
    [Header("")]
    [SerializeField] private float speedMoveBee;
    [SerializeField] private int damageBee;

    private Bee beeNormal;

    private void Start(){
        beeNormal = new(){
            Anim = this.GetComponent<Animator>()
        };

    }
}
