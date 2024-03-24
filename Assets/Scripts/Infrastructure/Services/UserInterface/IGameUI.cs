using Assets.Scripts.UserInterface;

namespace Assets.Scripts.Infrastructure.Services.UserInterface
{
    public interface IGameUI : IService
    {
        void InitializeScreens(ServiceLocator serviceLocator);

        void OpenScreen(ScreenID screenID);

        void OpenScreen<TPayload>(ScreenID screenID, TPayload payload) where TPayload : class;
    }
}