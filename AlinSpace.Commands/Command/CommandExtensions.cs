﻿using System;
using System.Threading.Tasks;

namespace AlinSpace.Commands
{
    /// <summary>
    /// Extensions for <see cref="ICommand"/>.
    /// </summary>
    public static class CommandExtensions
    {
        /// <summary>
        /// Convert to <see cref="global::System.Windows.Input.ICommand"/>.
        /// </summary>
        /// <param name="asyncCommand">Async command to convert.</param>
        /// <param name="fireAndForgetOnExecution">Flag indicates whether or not the async command shall be fire and forget on execution.</param>
        /// <returns>Windows command.</returns>
        public static global::System.Windows.Input.ICommand ToWindowsCommand(this ICommand asyncCommand, bool fireAndForgetOnExecution = true)
        {
            return new ToWindowsCommand(asyncCommand, fireAndForgetOnExecution);
        }

        /// <summary>
        /// Save execute command asynchronously.
        /// </summary>
        /// <param name="command">Async command to execute safely.</param>
        /// <param name="parameter">Command parameter.</param>
        /// <param name="callCanExecuteBeforeExecution">Call CanExecute before command execution.</param>
        /// <param name="ignoreExceptions">Ignore exceptions from command.</param>
        public static async Task SafeExecuteAsync(
            this ICommand command,
            object parameter = null,
            bool callCanExecuteBeforeExecution = true,
            bool ignoreExceptions = true)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            try
            {
                if (callCanExecuteBeforeExecution)
                {
                    if (command.CanExecute(parameter))
                        return;
                }

                await command.ExecuteAsync(parameter);
            }
            catch
            {
                if (!ignoreExceptions)
                    throw;
            }
        }

        /// <summary>
        /// Save execute command.
        /// </summary>
        /// <param name="command">Command to execute safely.</param>
        /// <param name="parameter">Command parameter.</param>
        /// <param name="callCanExecuteBeforeExecution">Call CanExecute before command execution.</param>
        /// <param name="ignoreExceptions">Ignore exceptions from command.</param>
        public static void SafeExecute(
            this global::System.Windows.Input.ICommand command,
            object parameter = null,
            bool callCanExecuteBeforeExecution = true,
            bool ignoreExceptions = true)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            try
            {
                if (callCanExecuteBeforeExecution)
                {
                    if (command.CanExecute(parameter))
                        return;
                }

                command.Execute(parameter);
            }
            catch
            {
                if (!ignoreExceptions)
                    throw;
            }
        }
    }
}
