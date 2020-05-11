using System;

namespace Common
{
    /// <summary>
    /// Результат операции.
    /// Содержит статус операции (успешно завершилась или нет) и информацию об ошибке.
    /// </summary>
    /// <typeparam name="TError">Тип объекта, содержащего информацию об ошибке.</typeparam>
    public abstract class ResultBase<TError>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ResultBase{TError}"/>.
        /// </summary>
        /// <param name="isSuccess">Флаг успешного или неуспешного завершения операции.</param>
        /// <param name="error">Информация об ошибке.</param>
        protected ResultBase(bool isSuccess, TError error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        /// <summary>
        /// Операция завершена успешно.
        /// </summary>
        public bool IsSuccess { get; }

        /// <summary>
        /// Операция завершена с ошибкой.
        /// </summary>
        public bool IsFailure => !IsSuccess;

        /// <summary>
        /// Информация об ошибке.
        /// </summary>
        public TError Error { get; }
    }

    /// <summary>
    /// Результат операции.
    /// Содержит возвращаемый результат, статус операции (успешно завершилась или нет) и
    /// информацию об ошибке.
    /// </summary>
    /// <typeparam name="TResult">Тип возвращаемого результата операции.</typeparam>
    /// <typeparam name="TError">Тип объекта, содержащего информацию об ошибке.</typeparam>
    public abstract class ResultBase<TResult, TError> : ResultBase<TError>
    {
        private readonly TResult _value;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ResultBase{TResult, TError}"/>.
        /// </summary>
        /// <param name="value">Результат операции.</param>
        /// <param name="isSuccess">Флаг успешного или неуспешного завершения операции.</param>
        /// <param name="error">Информация об ошибке.</param>
        protected internal ResultBase(TResult value, bool isSuccess, TError error)
            : base(isSuccess, error)
        {
            _value = value;
        }

        /// <summary>
        /// Результат операции.
        /// </summary>
        public TResult Value
        {
            get
            {
                if (!IsSuccess)
                {
                    throw new InvalidOperationException();
                }

                return _value;
            }
        }
    }
}
