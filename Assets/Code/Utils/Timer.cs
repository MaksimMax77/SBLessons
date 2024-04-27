namespace Code.Utils
{
    public class Timer
    {
        private float _destinationTime;
        private float _currentTime;
        private bool _isInitialized;

        public bool IsEnd => _currentTime >= _destinationTime;

        public void Init(float destinationTime)
        {
            if (_isInitialized)
            {
                return;
            }
            
            _destinationTime = destinationTime;
            _isInitialized = true;
        }
        
        public void Update(float deltaTime)
        {
            if (IsEnd)
            {
                return;
            }
            _currentTime += deltaTime;
        }

        public void SetMaxValue()
        {
            _currentTime = _destinationTime;
        }

        public void Restart()
        {
            _currentTime = 0;
        }
    }
}
