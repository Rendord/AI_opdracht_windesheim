namespace SmartZombies.FuzzyLogic
{
    public abstract class FuzzySet : FuzzyTerm
    {
        protected double Dom;
        protected double RepresentativeValue;

        public FuzzySet(double repVal)
        {
            Dom = 0.0;
            RepresentativeValue = repVal;
        }

        public abstract double CalculateDOM(double val);
        public abstract double LocationOfMaxima();

        public override FuzzyTerm Clone() => new FzSet(this);
        public double GetRepresentativeVal()
        {
            return RepresentativeValue;
        }

        public void SetDOM(double val)
        {
            Dom = val;
        }
        
        public override void ORwithDOM(double val)
        {
            if (val > Dom) Dom = val;
        }
        public override void ClearDOM() { Dom = 0.0; }
        public override double GetDOM() { return Dom; }
    }
}