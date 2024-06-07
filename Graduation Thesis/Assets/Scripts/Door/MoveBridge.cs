using UnityEngine;

public class MoveBridge : MonoBehaviour
{
    [Header("Location to move to")]
    [SerializeField] private Vector3 targetMove;

    Vector3 startPos;

    private void Start(){
        startPos = transform.localPosition;
    }

    public void MoveToTarget(){
        LeanTween.moveLocal(this.gameObject, targetMove, 2f).setEase(LeanTweenType.linear);
    }

    public void ReturnStartPos(){
        this.transform.localPosition = startPos;
    }
}
