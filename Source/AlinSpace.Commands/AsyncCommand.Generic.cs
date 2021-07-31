﻿using System;
using System.Threading.Tasks;

namespace AlinSpace.Exceptions
{
    /// <summary>
    /// Default implementation of <see cref="IAsyncCommand{TParameter}"/>.
    /// </summary>
    public class AsyncCommand<TParameter> : AbstractAsyncCommand<TParameter>
    {
        readonly bool verifyCanExecuteBeforeExecution;
        readonly bool continueOnCapturedContext;

        Func<TParameter, Task> executeFunc;
        Func<TParameter, bool> canExecuteFunc;

        /// <summary>
        /// Static factory method.
        /// </summary>
        /// <param name="verifyCanExecuteBeforeExecution"></param>
        /// <param name="continueOnCapturedContext"></param>
        /// <returns>Async command.</returns>
        public static AsyncCommand<TParameter> New(
            bool verifyCanExecuteBeforeExecution = false,
            bool continueOnCapturedContext = true)
        {
            return new AsyncCommand<TParameter>(
                verifyCanExecuteBeforeExecution,
                continueOnCapturedContext);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="verifyCanExecuteBeforeExecution"></param>
        /// <param name="continueOnCapturedContext"></param>
        public AsyncCommand(
            bool verifyCanExecuteBeforeExecution = false,
            bool continueOnCapturedContext = true)
        {
            this.verifyCanExecuteBeforeExecution = verifyCanExecuteBeforeExecution;
            this.continueOnCapturedContext = continueOnCapturedContext;
        }

        /// <summary>
        /// On can execute callback.
        /// </summary>
        /// <param name="executeFunc"></param>
        /// <returns>Async command.</returns>
        public AsyncCommand<TParameter> OnCanExecute(Func<TParameter, bool> canExecuteFunc)
        {
            this.canExecuteFunc = canExecuteFunc;
            return this;
        }

        /// <summary>
        /// On execute asynchronously callback.
        /// </summary>
        /// <param name="executeAction"></param>
        /// <returns>Async command.</returns>
        public AsyncCommand<TParameter> OnExecuteAsync(Func<TParameter, Task> executeFunc)
        {
            this.executeFunc = executeFunc;
            return this;
        }

        /// <summary>
        /// Can execute.
        /// </summary>
        /// <param name="parameter">Parameter.</param>
        /// <returns>True if can execute; false otherwise.</returns>
        public override bool CanExecute(TParameter parameter = default)
        {
            if (canExecuteFunc == null)
                return true;

            return canExecuteFunc(parameter);
        }

        /// <summary>
        /// Execute command asynchronously.
        /// </summary>
        /// <param name="parameter">Command parameter.</param>
        /// <returns>Task.</returns>
        public override async Task ExecuteAsync(TParameter parameter = default)
        {
            if (executeFunc == null)
                return;

            if (verifyCanExecuteBeforeExecution)
            {
                if (!CanExecute(parameter))
                    return;
            }

            await executeFunc(parameter).ConfigureAwait(continueOnCapturedContext);
        }
    }
}
