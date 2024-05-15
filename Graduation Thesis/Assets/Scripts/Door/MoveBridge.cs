using UnityEngine;

public class MoveBridge : MonoBehaviour
{
    [Header("Location to move to")]
    [SerializeField] private Vector3 targetMove;

    public void MoveToTarget(){
        LeanTween.moveLocal(this.gameObject, targetMove, 2f).setEase(LeanTweenType.linear);
    }
}
