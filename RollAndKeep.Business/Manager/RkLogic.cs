using RollAndKeep.Business.Model;
using RollAndKeep.Models.Constants;
using RollAndKeep.Models.Dto;
using System;
using System.Linq;

namespace RollAndKeep.Business
{
    public sealed class RkLogic
    {
        public Random Random { get; set; }
        public RkConfigurationDto Configuration { get; set; }

        public RkLogic(int roll, int keep, int addToResult)
        {
            Random = new Random();
            Configuration = BuildConfiguration(roll, keep, addToResult);
        }
        public RkLogic(RkConfigurationDto configuration)
        {
            Random = new Random();
            Configuration = configuration;
        }

        public RkConfigurationDto BuildConfiguration(int roll, int keep, int addToResult)
        {
            var configuration = new RkConfigurationDto(roll, keep, addToResult);
            return configuration;
        }

        public int RollADice()
        {
            return Random.Next(DiceConstants.DiceMinimum, DiceConstants.DiceMaximumForRandom);
        }

        public int RollAThrow()
        {
            var dices = Enumerable.Range(1, Configuration.Roll).Select(x => new RkDice(RollADice())).ToList();
            var explosionToCancel = 0;
            if (Configuration.MinimumResultCancelExplosion)
            {
                explosionToCancel = dices.Where(x => x.LastResult == DiceConstants.DiceMinimum).Count();
            }
            if (Configuration.IsDiceExplosive)
            {
                foreach (var dice in dices.OrderBy(x => x.LastResult))
                {
                    if (IsExplosive(dice))
                    {
                        if (explosionToCancel > 0)
                        {
                            explosionToCancel--;
                        }
                        else
                        {
                            do
                            {
                                dice.AddToDice(RollADice());
                            } while (IsExplosive(dice) ||
                                //We fix a maximum result in order to avoid theorical infinite launch
                                dice.SummedResult >= DiceConstants.TheoricalMaximumResult);
                        }
                    }
                }
            }
            var keepedResults = dices.OrderByDescending(x => x.SummedResult).Take(Configuration.Keep).Sum(x => x.SummedResult);
            return keepedResults + Configuration.AddToResult;
        }

        public int RollThrowsKeepBest(int nbOfThrow)
        {
            if (nbOfThrow < 1) throw new ArgumentException(nameof(nbOfThrow), $"{nameof(nbOfThrow)} has to be equal to or greater than 1.");
            return Enumerable.Range(1, nbOfThrow - 1).Select(x => RollAThrow()).OrderBy(x => x).FirstOrDefault();
        }

        public bool IsExplosive(RkDice dice)
        {
            return Configuration.ExplodeOn.Contains(dice.LastResult);
        }
    }
}
