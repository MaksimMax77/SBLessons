using UnityEngine;

namespace Code.ComponentActions.CollisionActions
{
    public class ShowEffectsOnCollision : CollisionAction
    {
        [SerializeField] private GameObject[] _effects;

        public override void Execute()
        {
            for (int i = 0, len = _effects.Length; i < len; ++i)
            {
                var effect = _effects[i];
                effect.SetActive(true);
                effect.transform.position = collision.contacts[0].point;
                effect.transform.rotation = Quaternion.FromToRotation(Vector3.up, collision.contacts[0].normal);
            }
        }

        public override void ResetAction()
        {
            for (int i = 0, len = _effects.Length; i < len; ++i)
            {
                _effects[i].SetActive(false);
            }

            base.ResetAction();
        }
    }
}
