using Code.Utils;
using UnityEngine;

namespace Code.ComponentActions.AbilityActions
{
    public class ProtectionDomeAbility : ComponentAction
    {
        [SerializeField] private float _workTime; 
        [SerializeField] private GameObject _domeGameObject;
        private Timer _timer;

        private void Awake()
        {
            _timer = new Timer();
            _timer.Init(_workTime);
            _timer.SetMaxValue();
        }

        public void RestartTimerIfIsEnd()
        {
            if (_timer.IsEnd)
            {
                _timer.Restart();
            }
        }

        public override void Execute()
        {
            _domeGameObject.SetActive(!_timer.IsEnd);
            _timer.Update(Time.deltaTime);
        }
    }
}