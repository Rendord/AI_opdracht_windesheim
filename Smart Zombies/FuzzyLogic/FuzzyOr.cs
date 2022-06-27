using System.Collections.Generic;

namespace SmartZombies.FuzzyLogic
{
    public class FuzzyOr : FuzzyTerm
    {
        private List<FuzzyTerm> Terms = new List<FuzzyTerm>();

        public FuzzyOr(FuzzyTerm op1, FuzzyTerm op2)
        {
            Terms.Add(op1);
            Terms.Add(op2);
        }
        
        public FuzzyOr(FuzzyOr fo)
        {
            Terms = new List<FuzzyTerm>();
            foreach (var item in fo.Terms)
            {
                Terms.Add(item.Clone());
            }
        }

        public override FuzzyTerm Clone()
        {
            return new FuzzyOr(this);
        }

        public override double GetDOM()
        {
            double maxValue = 0;
            foreach (FuzzyTerm term in Terms)
            {
                if (term.GetDOM() > maxValue)
                {
                    maxValue = term.GetDOM();
                }
            }

            return maxValue;
        }

        public override void ClearDOM()
        {
            foreach (FuzzyTerm term in Terms)
            {
                term.ClearDOM();
            }
        }

        public override void ORwithDOM(double val)
        {
            foreach (FuzzyTerm term in Terms)
            {
                term.ORwithDOM(val);
            }
        }
    }
}