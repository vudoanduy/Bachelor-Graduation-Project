using UnityEngine;

public class InstructionManage : MonoBehaviour
{
    private void Start(){
        OnActiveInstruction();
    }

    private void OnActiveInstruction(){
        GameObject[] instructions = GameObject.FindGameObjectsWithTag("Instruction");
        
        foreach (GameObject instruction in instructions){
            instruction.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
