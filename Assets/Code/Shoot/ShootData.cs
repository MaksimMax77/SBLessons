using Unity.Entities;
using UnityEngine;

namespace Code.Shoot
{
    [GenerateAuthoringComponent]
    public struct ShootData : IComponentData
    {
        public float shootDelay;
        [HideInInspector] public float shootTime;
    }
}
