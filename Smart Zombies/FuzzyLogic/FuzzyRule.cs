namespace SmartZombies.FuzzyLogic
{
    class FuzzyRule
    {
        public FuzzyTerm Antecedent;
        public FuzzyTerm Consequence;


        public FuzzyRule(FuzzyTerm ant, FuzzyTerm con)
        {
            Antecedent = ant.Clone();
            Consequence = con.Clone();
        }

        public void SetConfidenceOfConsequentToZero()
        {
            Consequence.ClearDOM();
        }

        public void Calculate()
        {
            Consequence.ORwithDOM(Antecedent.GetDOM());
        }
    }
}