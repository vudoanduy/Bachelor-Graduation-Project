using System;
using TMPro;
using UnityEditor.Localization.Editor;
using UnityEngine;
using UnityEngine.Localization;

public class AutoSizeText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    void Start(){
        ChangeSize();
    }

    public void ChangeSize(){
        float heightSize = text.GetPreferredValues().y;

        Vector3 currentScale = this.transform.localScale;
        this.transform.localScale = new Vector3(currentScale.x, heightSize/100, currentScale.z);
    }
}
