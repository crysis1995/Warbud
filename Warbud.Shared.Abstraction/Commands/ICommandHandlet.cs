﻿using System.Threading.Tasks;

namespace Warbud.Shared.Abstraction.Commands
{
    public interface ICommandHandler<in TCommand> where TCommand : class, ICommand
    {
        Task HandleAsync(TCommand command);
    }
}