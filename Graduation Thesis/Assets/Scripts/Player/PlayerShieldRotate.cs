using Unity.VisualScripting;
using UnityEngine;

public class PlayerShieldRotate : MonoBehaviour
{
    int angle = 0;
    public int speedRotate = 1;

    private void Update(){
        angle += speedRotate;
        this.transform.Rotate(Vector3.forward, angle);
    }
}
