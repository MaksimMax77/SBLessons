using Code.Settings;
using UnityEngine;
using Zenject;

namespace Code.Installer
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private bool _useUserUnitSettingsSo;
        [SerializeField] private UserUnitSettings _userUnitSettings;

        public override void InstallBindings()
        {
            if (_useUserUnitSettingsSo)
            {
                Container.Bind<IUserUnitSettings>()
                    .To<UserUnitSettings>()
                    .FromInstance(_userUnitSettings)
                    .AsSingle();
            }
            else
            {
                Container.Bind<IUserUnitSettings>().To<UserUnitSettings>().AsSingle();
            }

            Container.Bind<UserSettingsDestination>().AsSingle().NonLazy();
        }
    }
}