namespace Code.ComponentActions.CollisionActions
{
    public abstract class CollisionAction : ComponentAction
    {
        protected UnityEngine.Collision collision;

        public void SetCollision(UnityEngine.Collision collision)
        {
            this.collision = collision;
        }
    }
}
