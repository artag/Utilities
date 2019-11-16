using System;
using System.IO;
using Common;

namespace Dcp
{
    /// <summary>
    /// Счетчик количества букв в текстовом файле.
    /// </summary>
    internal class FileLettersCounter : IFileHandler<Result<ulong>>
    {
        /// <inheritdoc />
        public Result<ulong> HandleFile(string fullPath)
        {
            ulong numberOfLetters;

            try
            {
                using (var sr = new StreamReader(fullPath))
                {
                    numberOfLetters = CountLetters(sr);
                }
            }
            catch (Exception ex)
            {
                return Result<ulong>.Fail(ex.Message);
            }

            return Result<ulong>.Ok(numberOfLetters);
        }

        private static ulong CountLetters(StreamReader sr)
        {
            var numberOfChars = 0UL;

            var intValue = sr.Read();
            while (intValue > 0)
            {
                var chr = TryConvertToChar(intValue);

                if (char.IsLetter(chr))
                {
                    numberOfChars++;
                }

                intValue = sr.Read();
            }

            return numberOfChars;
        }

        private static char TryConvertToChar(int intValue)
        {
            var chr = ' ';

            try
            {
                chr = Convert.ToChar(intValue);
            }
            catch (OverflowException)
            {
                // Никак не обрабатываем ошибку, просто не считаем символ за букву.
            }

            return chr;
        }
    }
}
