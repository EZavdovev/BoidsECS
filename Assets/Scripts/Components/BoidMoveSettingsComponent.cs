using Unity.Entities;

[GenerateAuthoringComponent]
public struct BoidMoveSettingsComponent : IComponentData {
    public float alignmentCoeff;
    public float cohesionCoeff;
    public float separationCoeff;
    public float radius;
    public int maxNeighbours;
}

