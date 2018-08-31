using RollAndKeep.Models.Constants;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RollAndKeep.Models.Dto
{
    public sealed class RkConfigurationDto : IEquatable<RkConfigurationDto>
    {
        /// <summary>
        /// Instantiate a Roll&Keep configuration
        /// </summary>
        /// <param name="roll">Number of dice to roll (has to be > 0)</param>
        /// <param name="keep">Number of dice to keep (has to be > 0)</param>
        public RkConfigurationDto(int roll, int keep) : this(roll, keep, addToResult: 0)
        {

        }

        /// <summary>
        /// Instantiate a Roll&Keep configuration
        /// </summary>
        /// <param name="roll">Number of dice to roll (has to be > 0)</param>
        /// <param name="keep">Number of dice to keep (has to be > 0)</param>
        /// <param name="addToResult">Bonus or malus that will be add to the result</param>
        public RkConfigurationDto(int roll, int keep, int addToResult)
        {
            if (roll <= 0) throw new ArgumentOutOfRangeException(nameof(roll), roll, "Roll has to be superior to 0");
            if (keep <= 0) throw new ArgumentOutOfRangeException(nameof(keep), keep, "Roll has to be superior to 0");
            OriginalRoll = Roll = roll;
            OriginalKeep = Keep = keep;
            OriginalAddToResult = AddToResult = addToResult;
            if (Roll > DiceConstants.NumberOfDiceMaximum)
            {
                //If more than 10 dice has to be thrown, each pair of dice after 10 is converted to keep
                Keep += (Roll - DiceConstants.NumberOfDiceMaximum) / 2;
                Roll = DiceConstants.NumberOfDiceMaximum;
            }
            if (Keep > DiceConstants.NumberOfDiceMaximum)
            {
                //If more than 10 dice has to be kept, each dice after 10 is convert to +5
                AddToResult += (Keep - DiceConstants.NumberOfDiceMaximum) * 5;
                Keep = DiceConstants.NumberOfDiceMaximum;
            }
            if (Keep > Roll)
            {
                Keep = Roll;
            }
            IsDiceExplosive = true;
            MinimumResultCancelExplosion = false;
            ExplodeOn = new List<int> { DiceConstants.DiceMaximum };
        }

        /// <summary>
        /// Number of dice to roll
        /// </summary>
        public int Roll { get; private set; }
        public int OriginalRoll { get; set; }
        /// <summary>
        /// Number of dice to keep
        /// </summary>
        public int Keep { get; private set; }
        public int OriginalKeep { get; set; }
        /// <summary>
        /// The number that has to be added to the final result
        /// </summary>
        public int AddToResult { get; set; }
        public int OriginalAddToResult { get; set; }
        public string DisplayAddToResult => (Math.Sign(OriginalAddToResult) < 0 ? " - " : " + ") + AddToResult;
        /// <summary>
        /// List of the result that can explode the dice
        /// </summary>
        public List<int> ExplodeOn { get; set; }
        /// <summary>
        /// Show if the dice can explode if the correct result is obtained
        /// </summary>
        public bool IsDiceExplosive { get; set; }
        /// <summary>
        /// Show if a minimum result cancel an explosion or not
        /// </summary>
        public bool MinimumResultCancelExplosion { get; set; }
        public string Display => $"{OriginalRoll}g{OriginalKeep}{(AddToResult != 0 ? DisplayAddToResult : String.Empty)}";

        public bool Equals(RkConfigurationDto other)
        {
            if (other == null) return false;
            var equals = Roll == other.Roll &&
                Keep == other.Keep &&
                AddToResult == other.AddToResult &&
                ExplodeOn.SequenceEqual(other.ExplodeOn) &&
                IsDiceExplosive == other.IsDiceExplosive &&
                MinimumResultCancelExplosion == other.MinimumResultCancelExplosion;
            return equals;
        }
    }
}
