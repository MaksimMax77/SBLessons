using System.Collections.Generic;
using UnityEngine;

namespace Code.Collision.Actions
{
    [CreateAssetMenu(menuName = "CollisionActions/" + nameof(DisableGameObject), fileName = nameof(DisableGameObject))]
    public class DisableGameObject : CollisionAction
    {
        public override void Execute(List<Collider> collisions)
        {
            _currenGameObject.SetActive(false);
        }
    }
}
