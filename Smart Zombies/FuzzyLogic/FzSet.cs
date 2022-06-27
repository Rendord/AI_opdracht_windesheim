namespace SmartZombies.FuzzyLogic
{
    public class FzSet : FuzzyTerm
    {
        private FuzzySet Set;

        public FzSet(FuzzySet fs)
        { 
            Set = fs;
        }
        public override FuzzyTerm Clone()
        {
            return new FzSet(this.Set);
        }

        public override double GetDOM() { return Set.GetDOM(); }
        public override void ClearDOM() { Set.ClearDOM(); }
        public override void ORwithDOM(double val) { Set.ORwithDOM(val); }
    }
}