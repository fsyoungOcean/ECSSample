using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

////// 使用IJobProcessComponentData去遍历所有符合这个组件类型的Entity
////// Entity的处理时并行的。主线程只负责安排Job。
//public class RotationSpeedSystem : JobComponentSystem
//{
//    //IJobProcessComponentData是用来遍历所有带有所需Compoenent类型Enity的简单方法
//    //它也比IJobParallelFor更高效更便捷。
//    [BurstCompile]
//    struct RotationSpeedRotation : IJobProcessComponentData<Rotation, RotationSpeed>
//    {
//        public float dt;

//        public void Execute(ref Rotation rotation, [ReadOnly]ref RotationSpeed speed)
//        {
//            rotation.Value = math.mul(math.normalize(rotation.Value), quaternion.axisAngle(math.up(), speed.Value * dt));
//        }
//    }

//    // 我们继承JobComponentSystem，这样System就可以自动提供给我们所需Job之间的依赖关系了。
//    // IJobProcessComponentData声明了它要对RotationSpeed读操作，并且对Rotation写操作。
//    // 这样声明以后，JobComponentSystem就连可以给我们Job之间的依赖关系了，包括之前已经安排好的要写Rotation或RotationSpeed的那些Job。
//    // 我们要把这个依赖关系renturn出来，这样，依据类型我们已经安排好的Job就能注册到下一个可能会运行的System里去了。 
//    // 这么做意味着:
//    // * 主线程不发生等待, 主线程只需要根据依赖关系去安排Job (只有依赖关系被确定以后，Job才会被启动)。
//    // * 依赖关系为我们自动计算出来了, 这样我们就只写一些模块化的多线程代码就可以了。
//    protected override JobHandle OnUpdate(JobHandle inputDeps)
//    {
//        var job = new RotationSpeedRotation() { dt = Time.deltaTime };
//        return job.Schedule(this, 64, inputDeps);
//    }
//}

public class RotationSpeedSystem : ComponentSystem
{
    struct EnemyGroup
    {
        public ComponentDataArray<Rotation> rotations;
        [ReadOnly] public ComponentDataArray<RotationSpeed> moveSpeeds;
        [ReadOnly] public SubtractiveComponent<StopTag> stopRotate;
        public readonly int Length;
    }
    [Inject]
    EnemyGroup enemies;

    protected override void OnUpdate()
    {
        float dt = Time.deltaTime;

        for (int i = 0; i < enemies.Length; i++)
        {
            Rotation rotation = enemies.rotations[i];
            RotationSpeed speed = enemies.moveSpeeds[i];

            var asixangle = quaternion.axisAngle(math.up(), speed.Value * dt);
            rotation.Value = math.mul(math.normalize(rotation.Value), asixangle);

            enemies.rotations[i] = rotation;
        }
    }
}
