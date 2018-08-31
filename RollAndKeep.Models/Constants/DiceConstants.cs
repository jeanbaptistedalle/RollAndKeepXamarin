namespace RollAndKeep.Models.Constants
{
    public sealed class DiceConstants
    {
        /// <summary>
        /// Minimum of the dice (with 1 inclusive)
        /// </summary>
        public const int DiceMinimum = 1;
        /// <summary>
        /// Maximum of the dice for check if maximum
        /// </summary>
        public const int DiceMaximum = 10;
        /// <summary>
        /// Maximum of the dice for the random (because max is exclusive)
        /// </summary>
        public const int DiceMaximumForRandom = 11;
        /// <summary>
        /// Minimum of dice that should be roll or keep (it's impossible to roll or keep 0 or less dice)
        /// </summary>
        public const int NumberOfDiceMinimum = 1;
        /// <summary>
        /// Maximum of dice that should be roll or keep (exceeding dices has to be converted to bonus)
        /// </summary>
        public const int NumberOfDiceMaximum = 10;
        /// <summary>
        /// Theorical maximum result for a throw (in order to avoid infinite throw)
        /// </summary>
        public const int TheoricalMaximumResult = 1_000;
    }
}
