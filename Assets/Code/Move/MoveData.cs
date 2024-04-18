using Unity.Entities;

namespace Code.Move
{
    [GenerateAuthoringComponent]
    public struct MoveData : IComponentData
    {
        public float speed;
    }
}
