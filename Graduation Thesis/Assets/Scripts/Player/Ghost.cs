using UnityEngine;

public class Ghost : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] private float timeAppearGhost = 0.2f;

    public void CreateGhost(Sprite sprite, Transform parent, bool isFlip){
        this.GetComponent<SpriteRenderer>().sprite = sprite;
        if(isFlip){
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        this.transform.position = parent.transform.position;
        Invoke(nameof(DestroyGhost), timeAppearGhost);
    }

    public void DestroyGhost(){
        Destroy(this.gameObject);
    }
}
