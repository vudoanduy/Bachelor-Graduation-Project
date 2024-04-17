using UnityEngine;

public class InstructionManage : MonoBehaviour
{
    private void Start(){
        OffActiveInstruction();
    }

    private void OffActiveInstruction(){
        GameObject[] instructions = GameObject.FindGameObjectsWithTag("Instruction");
        
        foreach (GameObject instruction in instructions){
            instruction.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
