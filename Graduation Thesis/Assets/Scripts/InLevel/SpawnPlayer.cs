using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] private GameObject playerResources;

    [Header("")]
    [SerializeField] private Vector3 startPos;

    private void Start(){
        GameObject newPlayer = Instantiate(playerResources);
        newPlayer.transform.localPosition = startPos;
        newPlayer.gameObject.name = "Player";
    }
}
