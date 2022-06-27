using System;
using System.Collections.Generic;

namespace SmartZombies.FuzzyLogic
{
    class FuzzyAnd : FuzzyTerm
    {
        private List<FuzzyTerm> Terms = new List<FuzzyTerm>();

        public FuzzyAnd(FuzzyTerm op1, FuzzyTerm op2)
        {
            Terms.Add(op1);
            Terms.Add(op2);
        }
        public FuzzyAnd(FuzzyAnd fa)
        {
            Terms = new List<FuzzyTerm>();
            foreach (var item in fa.Terms)
            {
                Terms.Add(item.Clone());
            }
        }

        public override FuzzyTerm Clone()
        {
            return new FuzzyAnd(this);
        }

        public override double GetDOM()
        {
            double minDom = Double.MaxValue;
            foreach(FuzzyTerm term in Terms)
            {
                if(term.GetDOM() < minDom)
                {
                    minDom = term.GetDOM();
                }
            }
            return minDom;
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