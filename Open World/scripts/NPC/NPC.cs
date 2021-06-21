using System.IO;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Interaction))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CustomTags))]
[RequireComponent(typeof(ThirdPersonCharacter))]
public class NPC : MonoBehaviour
{
    public BaseNpcScriptable BaseNpc;
    NavMeshAgent NPC_Agent = default;
    ThirdPersonCharacter character;

    public string NPC_DataPath => $"{BaseNpc.NPC_Name}";

    private void Start()
    {
        character = GetComponent<ThirdPersonCharacter>();
        NPC_Agent = GetComponent<NavMeshAgent>();
        GetComponent<CustomTags>().AddTag("npc");

        //NPC_LoadData();
    }

    private void NPC_LoadData()
    {
        if (File.Exists(NPC_DataPath))
        {
            string json = File.ReadAllText(NPC_DataPath);
            NPC fromSaved = JsonUtility.FromJson<NPC>(json);
        }
    }

    private void Update()
    {
        if (NPC_Agent.remainingDistance > NPC_Agent.stoppingDistance)
        {
            character.Move(NPC_Agent.desiredVelocity, false, false);
        }
        else
        {
            character.Move(Vector3.zero, false, false);
            Vector3 pos = new Vector3(Random.Range(-50, 50), 1, Random.Range(-50, 50));
            SetNewDestination(pos);
        }
    }
    public void SetNewDestination(Vector3 dst) => NPC_Agent.SetDestination(dst);

}
