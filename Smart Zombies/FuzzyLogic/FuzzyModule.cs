using System.Collections.Generic;
using Godot;

namespace SmartZombies.FuzzyLogic
{
    public class FuzzyModule : Object
    {
        private List<FuzzyRule> Rules;
            private Dictionary<string, FuzzyVariable> Variables;

            public FuzzyModule()
            {
                Rules = new List<FuzzyRule>();
                Variables = new Dictionary<string, FuzzyVariable>();
            }

            public void AddRule(FuzzyTerm antecedent, FuzzyTerm consequence)
            {
                Rules.Add(new FuzzyRule(antecedent, consequence));
            }

            public FuzzyVariable CreateFLV(string VarName)
            {
                Variables[VarName] = new FuzzyVariable();
                return Variables[VarName];
            }

            public void Fuzzify (string name, double val)
            {
                Variables[name].Fuzzify(val);
            }

            public void SetConfidencesOfConsequentsToZero()
            {
                foreach(FuzzyRule rule in Rules)
                {
                    rule.Consequence.ClearDOM();
                }
            }

            public double DeFuzzify(string name)
            {
                SetConfidencesOfConsequentsToZero();
                foreach (FuzzyRule rule in Rules)
                {
                    rule.Calculate();
                }
                return Variables[name].DeFuzzifyMaxAv();
            }
    }
}