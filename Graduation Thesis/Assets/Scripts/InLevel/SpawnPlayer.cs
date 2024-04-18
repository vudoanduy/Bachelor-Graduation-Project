using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] private GameObject playerResources;

    [Header("")]
    [SerializeField] private Vector3 startPos;

    private void Start(){
        Invoke(nameof(Spawn), 0.5f);
    }

    public void Spawn(){
        GameObject newPlayer = Instantiate(playerResources);
        newPlayer.transform.localPosition = startPos;
        newPlayer.name = "Player";
    }
}
