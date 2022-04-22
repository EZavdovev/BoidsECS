using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

[UpdateAfter(typeof(BoidMoveSystem))]
public class TransformSystem : SystemBase {
    protected override void OnUpdate() {

        var dt = Time.DeltaTime;
        Entities.ForEach((ref Translation pos, ref BoidVelocityComponent boidVelocity, ref Rotation boidRotation) => {
            
            var dist = math.sqrt(math.pow(pos.Value.x, 2) + math.pow(pos.Value.y, 2) + math.pow(pos.Value.z, 2));
            if (dist > boidVelocity.boardMove) {
                boidVelocity.velocity +=  -math.normalize(pos.Value);
            }
            boidVelocity.velocity = math.normalize(boidVelocity.velocity);
            boidRotation.Value = math.slerp(boidRotation.Value, quaternion.LookRotation(boidVelocity.velocity, math.up()), dt * boidVelocity.speedRotation);
            pos.Value += boidVelocity.velocity * dt * boidVelocity.speed;

        }).Run();
    }
}
