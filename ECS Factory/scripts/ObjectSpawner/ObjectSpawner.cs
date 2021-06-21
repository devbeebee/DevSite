using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Mathematics;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private float delay=2;
    [SerializeField] private Mesh enemyMesh;
    [SerializeField] private Material enemyMaterial;

    private EntityManager entityManager;
    // Start is called before the first frame update
    void Start()
    {
        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;


        StartCoroutine(enumerator());

    }
    IEnumerator enumerator  () 
    {

        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(1);
        }
        EntityArchetype archetype = entityManager.CreateArchetype(
         typeof(Translation),
         typeof(Rotation),
         typeof(RenderMesh),
                 typeof(RenderBounds),
         typeof(LocalToWorld));
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(delay);
            for (int ii = 0; ii < 250; ii++)
            {
                // 2
                Entity entity = entityManager.CreateEntity(archetype);

                // 3
                entityManager.AddComponentData(entity, new Translation { Value = new float3(i, 0f, ii) });

                entityManager.AddComponentData(entity, new Rotation { Value = quaternion.EulerXYZ(new float3(0f, 0, 0f)) });

                entityManager.AddSharedComponentData(entity, new RenderMesh
                {
                    mesh = enemyMesh,
                    material = enemyMaterial
                });
            }
        }
    }  
}
