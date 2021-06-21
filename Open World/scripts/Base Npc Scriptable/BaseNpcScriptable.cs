using UnityEngine;

public enum GenderType { male,Female }

[CreateAssetMenu(fileName = "Base Npc", menuName = "ScriptableObjects/AI/BaseNpc", order = 0)]
public class BaseNpcScriptable : ScriptableObject
{
    public string NPC_Name;
    public GenderType Gender;
    public BaseDialogueScriptable Dialogue;
}