using UnityEngine;

namespace Code.Settings
{
    public class UserSettingsDestination
    {
        private IUserUnitSettings _settings;

        public UserSettingsDestination(IUserUnitSettings settings)
        {
            _settings = settings;
        }
    }
}
