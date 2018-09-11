using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

[UpdateBefore(typeof(EndFrameTransformSystem))]
public class StopRotationCubeSystem : ComponentSystem
{
    public struct Data
    {
        public ComponentDataArray<RotationSpeed> rotationSpeed;
        public ComponentDataArray<Rotation> rotation;
        public EntityArray entity;
        public readonly int Length;
    }

    [Inject] Data data;

    protected override void OnUpdate()
    {
        //int time = (int)Time.time % 10;

        //bool rotate = time < 5;

        //var entityManager = World.Active.GetExistingManager<EntityManager>();

        //for (int i=0;i<data.Length; i++)
        //{
        //    if (rotate == false && entityManager.HasComponent<StopTag>(data.entity[i]) == false)
        //    {
        //        PostUpdateCommands.AddComponent<StopTag>(data.entity[i], new StopTag());
        //    }
        //    else if (rotate && entityManager.HasComponent<StopTag>(data.entity[i]))
        //    {
        //        PostUpdateCommands.RemoveComponent<StopTag>(data.entity[i]);
        //    }
        //}
    }
}
