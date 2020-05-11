namespace Common
{
    /// <summary>
    /// Обертка для создания объектов, для создания которых требуется проверка входных значимых типов.
    /// </summary>
    /// <typeparam name="T">Тип создаваемого объекта.</typeparam>
    public abstract class ValueObject<T> where T : ValueObject<T>
    {
        /// <summary>
        /// Сравнивает два объекта на равенство.
        /// </summary>
        /// <param name="a">Первый сравниваемый объект.</param>
        /// <param name="b">Второй сравниваемый объект.</param>
        /// <returns>true - два объекта равны, false - в противном случае.</returns>
        public static bool operator ==(ValueObject<T> a, ValueObject<T> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        /// <summary>
        /// Сравнивает два объекта на неравенство.
        /// </summary>
        /// <param name="a">Первый сравниваемый объект.</param>
        /// <param name="b">Второй сравниваемый объект.</param>
        /// <returns>true - два объекта не равны, false - в противном случае.</returns>
        public static bool operator !=(ValueObject<T> a, ValueObject<T> b)
        {
            return !(a == b);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            var valueObject = obj as T;

            if (ReferenceEquals(valueObject, null))
                return false;

            return EqualsCore(valueObject);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return GetHashCodeCore();
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="other">The object to compare with the current object.</param>
        /// <returns>rue if the specified object  is equal to the current object; otherwise, false.</returns>
        protected abstract bool EqualsCore(T other);

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        protected abstract int GetHashCodeCore();
    }
}
