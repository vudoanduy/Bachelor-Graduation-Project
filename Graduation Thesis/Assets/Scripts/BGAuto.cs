using UnityEngine;

public class BGAuto : MonoBehaviour
{
    Renderer render;

    float speedAuto = 0.5f;

    //
    void Start(){
        render = this.GetComponent<Renderer>();
    }

    void Update(){
        RunBG();
    }

    //
    void RunBG(){
        render.material.mainTextureOffset += Vector2.left * speedAuto * Time.deltaTime;
    }
}
