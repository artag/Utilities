using System;

namespace Common
{
    /// <summary>
    /// Результат операции.
    /// Содержит статус операции (успешно завершилась или нет) и информацию об ошибке в виде строки.
    /// </summary>
    public partial class Result : ResultBase<string>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Result"/>.
        /// </summary>
        /// <param name="isSuccess">Флаг успешного или неуспешного завершения операции.</param>
        /// <param name="error">Информация об ошибке.</param>
        protected Result(bool isSuccess, string error)
            : base(isSuccess, error)
        {
            if (isSuccess && !error.IsNullOrEmpty())
            {
                throw new InvalidOperationException();
            }

            if (!isSuccess && error.IsNullOrEmpty())
            {
                throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// Создать результат неудачной операции.
        /// </summary>
        /// <param name="message">Сообщение об ошибке.</param>
        /// <returns>Результат неудачной операции.</returns>
        public static Result Fail(string message)
        {
            return new Result(false, message);
        }

        /// <summary>
        /// Создать результат удачной операции.
        /// </summary>
        /// <returns>Результат удачной операции.</returns>
        public static Result Ok()
        {
            return new Result(true, string.Empty);
        }
    }

    /// <summary>
    /// Результат операции.
    /// Содержит возвращаемый результат, статус операции (успешно завершилась или нет) и
    /// информацию об ошибке в виде строки.
    /// </summary>
    /// <typeparam name="TResult">Тип возвращаемого результата операции.</typeparam>
    public partial class Result<TResult> : ResultBase<TResult, string>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Result{TResult}"/>.
        /// </summary>
        /// <param name="value">Результат операции.</param>
        /// <param name="isSuccess">Флаг успешного или неуспешного завершения операции.</param>
        /// <param name="error">Информация об ошибке.</param>
        protected internal Result(TResult value, bool isSuccess, string error)
            : base(value, isSuccess, error)
        {
        }

        /// <summary>
        /// Создать результат неудачной операции.
        /// </summary>
        /// <param name="error">Сообщение об ошибке.</param>
        /// <returns>Результат неудачной операции.</returns>
        public static Result<TResult> Fail(string error)
        {
            return new Result<TResult>(default(TResult), false, error);
        }

        /// <summary>
        /// Создать результат удачной операции.
        /// </summary>
        /// <param name="value">Результат операции.</param>
        /// <returns>Результат удачной операции.</returns>
        public static Result<TResult> Ok(TResult value)
        {
            return new Result<TResult>(value, true, string.Empty);
        }
    }
}
