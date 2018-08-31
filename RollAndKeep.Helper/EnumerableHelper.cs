using System;
using System.Collections.Generic;
using System.Linq;

namespace RollAndKeep.Helper
{
    public class EnumerableHelper
    {
        /// <summary>
        /// Generate numbers from number to number, including thoses numbers
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static IEnumerable<int> GenerateNumbers(int from, int to)
        {
            if (from > to) throw new ArgumentException(nameof(from), $"'{nameof(from)}' has to be less than '{nameof(to)}'");
            return Enumerable.Range(from, to - (from - 1));
        }
    }
}
