﻿using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Samples.Common
{
    //實體化圓周組成的物件
    public class SpawnRandomCircleSystem : ComponentSystem
    {
        struct Group
        {
            [ReadOnly]
            public SharedComponentDataArray<SpawnRandomCircle> Spawner;
            public ComponentDataArray<Position>                Position;
            public EntityArray                                 Entity;
            public readonly int                                Length;
        }

        [Inject] Group m_Group;


        protected override void OnUpdate()
        {
            while (m_Group.Length != 0)
            {
                var spawner = m_Group.Spawner[0];
                var sourceEntity = m_Group.Entity[0];
                var center = m_Group.Position[0].Value;
                
                //配置預計數量的空間
                var entities = new NativeArray<Entity>(spawner.count, Allocator.Temp);
                //產生預計數量的物件
                EntityManager.Instantiate(spawner.prefab, entities);
                
                //配置位置空間
                var positions = new NativeArray<float3>(spawner.count, Allocator.Temp);

                if (spawner.spawnLocal)
                {
                    GeneratePoints.RandomPointsOnCircle(new float3(), spawner.radius, ref positions);
                    for (int i = 0; i < spawner.count; i++)
                    {
                        var position = new Position
                        {
                            Value = positions[i]
                        };
                        EntityManager.SetComponentData(entities[i],position);
                        
                        // Spawn Attach
                        var attach = EntityManager.CreateEntity();
                        EntityManager.AddComponentData(attach, new Attach
                        {
                            Parent = sourceEntity,
                            Child = entities[i]
                        });
                    }
                }
                else
                {
                    //配置位置
                    GeneratePoints.RandomPointsOnCircle(center, spawner.radius, ref positions);
                    for (int i = 0; i < spawner.count; i++)
                    {
                        var position = new Position
                        {
                            Value = positions[i]
                        };
                        EntityManager.SetComponentData(entities[i],position);
                    }
                }

                entities.Dispose();
                positions.Dispose();
                
                EntityManager.RemoveComponent<SpawnRandomCircle>(sourceEntity);

                // Instantiate & AddComponent & RemoveComponent calls invalidate the injected groups,
                // so before we get to the next spawner we have to reinject them  
                UpdateInjectedComponentGroups();
            }
        }
    }
}
