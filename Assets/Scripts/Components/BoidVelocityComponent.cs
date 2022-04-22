using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct BoidVelocityComponent : IComponentData {
    public float3 velocity;
    public float speed;
    public float speedRotation;
    public float boardMove;
}

