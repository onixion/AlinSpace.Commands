﻿using System.Windows.Input;

namespace FluentCommands
{
    /// <summary>
    /// Execution group interface.
    /// </summary>
    public interface IExecutionGroup
    {
        /// <summary>
        /// Register command to the execution group.
        /// </summary>
        /// <param name="command">Command to register.</param>
        /// <returns>Registered command.</returns>
        ICommand RegisterCommand(ICommand command);
    }
}