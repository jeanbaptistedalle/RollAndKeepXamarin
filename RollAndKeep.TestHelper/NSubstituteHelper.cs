using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RollAndKeep.TestHelper
{
    public class NSubstituteHelper
    {

        /// <summary>
        /// Substitute the method in order to return the given result
        /// </summary>
        /// <typeparam name="TSubstitute">Type of object to substitute</typeparam>
        /// <typeparam name="TResult">Type of the given result</typeparam>
        /// <param name="substitute">object to substitute</param>
        /// <param name="method">Lambda for method to substitute</param>
        /// <param name="result">Return for the method</param>
        public static void SubstituteMethodForResult<TSubstitute, TResult>(TSubstitute substitute, Func<TSubstitute, TResult> method, TResult result)
            where TSubstitute : class
        {
            var action = method.ToAction();
            substitute.When(x => method(x)).DoNotCallBase();
            method(substitute).Returns(result);
        }

        /// <summary>
        /// Substitute the method in order to return the given result
        /// </summary>
        /// <typeparam name="TSubstitute">Type of object to substitute</typeparam>
        /// <typeparam name="TResult">Type of the given result</typeparam>
        /// <param name="substitute">object to substitute</param>
        /// <param name="method">Lambda for method to substitute</param>
        /// <param name="results">Return for the method</param>
        public static void SubstituteMethodForResult<TSubstitute, TResult>(TSubstitute substitute, Func<TSubstitute, TResult> method, List<TResult> results)
            where TSubstitute : class
        {
            var action = method.ToAction();
            substitute.When(x => method(x)).DoNotCallBase();
            method(substitute).Returns(results.FirstOrDefault(), results.Skip(1).ToArray());
        }
    }
}
