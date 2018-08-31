using NSubstitute;
using NUnit.Framework;
using RollAndKeep.Models.Constants;
using RollAndKeep.TestHelper;
using System;
using System.Collections.Generic;

namespace RollAndKeep.Business.Tests.Manager
{
    [TestFixture]
    public class RkLogicTest
    {
        static IEnumerable<TestCaseData> RkLogicConstructorTestCaseSource()
        {
            yield return new TestCaseData(4, 4, 2, 2, 0, 0);//Normal case : 4k2
            yield return new TestCaseData(4, 4, 2, 2, 5, 5);//Normal case with bonus: 4k2 +5
            yield return new TestCaseData(10, 10, 2, 2, 0, 0);//Normal case :  10k2
            yield return new TestCaseData(2, 2, 10, 2, 0, 0);//Case with too much keep : 2k10 -> 2k2
            yield return new TestCaseData(15, 10, 4, 6, 0, 0);//Case with more than 10 roll : 15k4 -> 10k6
            yield return new TestCaseData(15, 10, 15, 10, 0, 35);//Case with more than 10 roll and keep : 15k15 -> 10k10 + 35
            yield return new TestCaseData(15, 10, 15, 10, 5, 40);//Case with more than 10 roll and keep and bonus : 15k15 + 5 -> 10k10 + 40
        }
        [TestCaseSource(nameof(RkLogicConstructorTestCaseSource))]
        public void RkLogicConstructorTest(int roll, int expectedRoll, int keep, int expectedKeep, int addToResult, int expectedAddToResult)
        {
            var rkLogic = new RkLogic(roll, keep, addToResult);
            Assert.That(rkLogic.Configuration.Roll, Is.EqualTo(expectedRoll));
            Assert.That(rkLogic.Configuration.Keep, Is.EqualTo(expectedKeep));
            Assert.That(rkLogic.Configuration.AddToResult, Is.EqualTo(expectedAddToResult));
        }

        static IEnumerable<TestCaseData> RollAThrowTestCaseSource()
        {
            yield return new TestCaseData(new List<int> { 8, 7, 6, 5 }, 15);//Normal case without explosion
            yield return new TestCaseData(new List<int> { 10, 8, 6, 5, 3 }, 21);//Case with 10 explosion
            yield return new TestCaseData(new List<int> { 10, 8, 6, 5, 10, 3 }, 31);//Case with 10 double explosion
            yield return new TestCaseData(new List<int> { 10, 10, 6, 5, 10, 3, 6 }, 39);//Case with two 10 explosion
        }
        [TestCaseSource(nameof(RollAThrowTestCaseSource))]
        public void RollAThrowTest(List<int> diceResults, int expectedResult)
        {
            var rkLogic = new RkLogic(roll: 4, keep: 2, addToResult: 0);
            rkLogic.Random = Substitute.ForPartsOf<Random>();
            NSubstituteHelper.SubstituteMethodForResult(rkLogic.Random, x => x.Next(DiceConstants.DiceMinimum, DiceConstants.DiceMaximumForRandom), diceResults);
            var result = rkLogic.RollAThrow();
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        static IEnumerable<TestCaseData> RollAThrowWithoutExplosionTestCaseSource()
        {
            yield return new TestCaseData(new List<int> { 8, 7, 6, 5 }, 15);//Normal case
            yield return new TestCaseData(new List<int> { 10, 8, 6, 5, 3 }, 18);//Case with 10
            yield return new TestCaseData(new List<int> { 10, 10, 6, 1, 3 }, 20);//Case with two 10
        }
        [TestCaseSource(nameof(RollAThrowWithoutExplosionTestCaseSource))]
        public void RollAThrowWithoutExplosionTest(List<int> diceResults, int expectedResult)
        {
            var rkLogic = new RkLogic(roll: 4, keep: 2, addToResult: 0);
            rkLogic.Configuration.IsDiceExplosive = false;
            rkLogic.Random = Substitute.ForPartsOf<Random>();
            NSubstituteHelper.SubstituteMethodForResult(rkLogic.Random, x => x.Next(DiceConstants.DiceMinimum, DiceConstants.DiceMaximumForRandom), diceResults);
            var result = rkLogic.RollAThrow();
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        static IEnumerable<TestCaseData> RollAThrowWithCancelTestCaseSource()
        {
            yield return new TestCaseData(new List<int> { 8, 7, 6, 5 }, 15);//Normal case
            yield return new TestCaseData(new List<int> { 10, 8, 6, 5, 3 }, 21);//Case with 10 explosion
            yield return new TestCaseData(new List<int> { 10, 8, 6, 1, 3, 6 }, 18);//Case with 10 and 1
            yield return new TestCaseData(new List<int> { 10, 10, 6, 1, 10, 3, 6 }, 33);//Case with two 10 and 1
            yield return new TestCaseData(new List<int> { 10, 10, 6, 3, 1, 10, 10, 1 }, 42);//Case with two 10 and 1 on explosion
        }
        [TestCaseSource(nameof(RollAThrowWithCancelTestCaseSource))]
        public void RollAThrowWithCancelTest(List<int> diceResults, int expectedResult)
        {
            var rkLogic = new RkLogic(roll: 4, keep: 2, addToResult: 0);
            rkLogic.Configuration.MinimumResultCancelExplosion = true;
            rkLogic.Random = Substitute.ForPartsOf<Random>();
            NSubstituteHelper.SubstituteMethodForResult(rkLogic.Random, x => x.Next(DiceConstants.DiceMinimum, DiceConstants.DiceMaximumForRandom), diceResults);
            var result = rkLogic.RollAThrow();
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
