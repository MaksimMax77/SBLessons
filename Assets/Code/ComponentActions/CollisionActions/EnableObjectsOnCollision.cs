using UnityEngine;

namespace Code.ComponentActions.CollisionActions
{
    public class EnableObjectsOnCollision : CollisionAction
    {
        [SerializeField] private GameObject[] _objects;
        private bool _positionIsSet;

        public override void Execute()
        {
            for (int i = 0, len = _objects.Length; i < len; ++i)
            {
                var effect = _objects[i];
                SetObjectPosition(_objects[i], _collisions[0].transform.position);
                effect.transform.SetParent(null);
                effect.SetActive(true);
            }
        }

        private void SetObjectPosition(GameObject obj, Vector3 pos)
        {
            if (_positionIsSet)
            {
                return;
            }
            obj.transform.position = pos;
            Debug.LogError(pos);
            _positionIsSet = true;
        }
    }
}
