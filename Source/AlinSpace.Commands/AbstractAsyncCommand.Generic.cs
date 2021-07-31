﻿using System.Threading.Tasks;

namespace AlinSpace.Exceptions
{
    /// <summary>
    /// Abstract implementation of <see cref="IAsyncCommand{TParameter}"/>.
    /// </summary>
    public abstract class AbstractAsyncCommand<TParameter> : AbstractAsyncCommand
    {
        /// <summary>
        /// Can command execute.
        /// </summary>
        /// <param name="parameter">Command parameter.</param>
        /// <returns>True, if command can be executed; false otherwise.</returns>
        public override bool CanExecute(object parameter = default)
        {
            return CanExecute((TParameter)parameter);
        }

        /// <summary>
        /// Can command execute.
        /// </summary>
        /// <param name="parameter">Command parameter.</param>
        /// <returns>True, if command can be executed; false otherwise.</returns>
        public virtual bool CanExecute(TParameter parameter = default)
        {
            return true;
        }

        /// <summary>
        /// Execute command asynchronously.
        /// </summary>
        /// <param name="parameter">Command parameter.</param>
        public override Task ExecuteAsync(object parameter = null)
        {
            return ExecuteAsync((TParameter)parameter);
        }

        /// <summary>
        /// Execute command asynchronously.
        /// </summary>
        /// <param name="parameter">Command parameter.</param>
        public abstract Task ExecuteAsync(TParameter parameter = default);
    }
}
