using CodeBase.Infrastructure.Services;

namespace CodeBase.Services.Input
{
    public interface IInputService: IService
    {
        bool isMoveForwardButtonUp();
    }
}