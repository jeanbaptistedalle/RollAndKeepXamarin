namespace RollAndKeep.Business.Model
{
    public class RkDice
    {
        public int LastResult { get; set; }
        public int SummedResult { get; set; }

        public RkDice(int firstResult)
        {
            AddToDice(firstResult);
        }

        public void AddToDice(int result)
        {
            LastResult = result;
            SummedResult += result;
        }
    }
}
