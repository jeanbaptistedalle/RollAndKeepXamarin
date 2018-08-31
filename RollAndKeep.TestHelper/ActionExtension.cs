using System;

namespace RollAndKeep.TestHelper
{
    public static class ActionExtension
    {
        /// <summary>
        /// Return the function as an Action (the result is simply ignored)
        /// </summary>
        /// <typeparam name="TParam">Function's parameter type</typeparam>
        /// <typeparam name="TResult">Function's Result type</typeparam>
        /// <param name="function">Fonction à convertir en action</param>
        /// <returns>Function as an action</returns>
        public static Action<TParam> ToAction<TParam, TResult>(this Func<TParam, TResult> function)
        {
            if (function == null) throw new ArgumentNullException(nameof(function));
            return x => function(x);
        }
    }
}
