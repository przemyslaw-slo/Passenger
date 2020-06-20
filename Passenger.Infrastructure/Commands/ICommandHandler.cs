using System.Threading.Tasks;

namespace Passenger.Infrastructure.Commands
{
    public interface ICommandHandler<in T> where T : ICommand
    {
        Task HandleAsync(T command);
    }
}
