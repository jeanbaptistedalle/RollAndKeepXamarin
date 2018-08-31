using NUnit.Framework;
using RollAndKeep.Business.Model;
using RollAndKeep.Models.Constants;

namespace RollAndKeep.Business.Tests.Model
{
    [TestFixture]
    public class RkDiceTest
    {

        [Test]
        public void RkDiceConstructorTest(
            [Values(DiceConstants.DiceMinimum, 5, DiceConstants.DiceMaximum)]int result)
        {
            var dice = new RkDice(result);
            Assert.That(dice.LastResult, Is.EqualTo(result));
            Assert.That(dice.SummedResult, Is.EqualTo(result));
        }

        [Test]
        public void AddToDiceTest(
            [Values(DiceConstants.DiceMinimum, 5, DiceConstants.DiceMaximum)]int firstResult,
            [Values(DiceConstants.DiceMinimum, 5, DiceConstants.DiceMaximum)]int secondResult)
        {
            var dice = new RkDice(firstResult);
            Assert.That(dice.LastResult, Is.EqualTo(firstResult));
            Assert.That(dice.SummedResult, Is.EqualTo(firstResult));

            dice.AddToDice(secondResult);
            Assert.That(dice.LastResult, Is.EqualTo(secondResult));
            Assert.That(dice.SummedResult, Is.EqualTo(firstResult + secondResult));
        }
    }
}
