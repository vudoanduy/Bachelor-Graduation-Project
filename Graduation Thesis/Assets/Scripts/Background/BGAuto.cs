using UnityEngine;

// Background tu dong di chuyen khong phu thuoc vao nguoi choi.
// Su dung lam background nen
public class BGAuto : MonoBehaviour
{
    Renderer render;

    readonly float speedAuto = 0.5f;

    private void Start(){
        render = this.GetComponent<Renderer>();
    }

    private void Update(){
        RunBG();
    }

    private void RunBG(){
        render.material.mainTextureOffset += speedAuto * Time.deltaTime * Vector2.left;
    }
}
