using CodeBase.Infrastructure.Services;
using CodeBase.Services.Input;

namespace CodeBase.Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine;

        public Game()
        {
            StateMachine = new GameStateMachine(new SceneLoader(),AllServices.Container);
        }

    }
}