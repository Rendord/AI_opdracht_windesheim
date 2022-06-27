using Godot.Collections;

namespace SmartZombies.FuzzyLogic
{
    public class FuzzyVariable
    {
        private double MinRange { get; set; }
        private double MaxRange { get; set; }
        private Dictionary<string, FuzzySet> MemberSets { get; set; }

        public FuzzyVariable()
        {
            this.MinRange = 0.0d;
            this.MaxRange = 0.0d;
            MemberSets = new Dictionary<string, FuzzySet>();
        }

        public void Fuzzify(double val)
        {
            foreach (string s in MemberSets.Keys)
            {
                MemberSets[s].SetDOM(MemberSets[s].CalculateDOM(val));
            }

        }


        public void SetConfidencesOfConsequentsToZero()
        {
            foreach (FuzzySet set in MemberSets.Values)
            {
                set.ClearDOM();
            }
        }

        public double DeFuzzifyMaxAv()
        {
            double maxAv = 0;
            double div = 0;
            foreach (var set in MemberSets.Values)
            {
                double locationOfMaxima = set.LocationOfMaxima();
                double heightAtMax = set.GetDOM();
                maxAv += heightAtMax * locationOfMaxima;
                div += heightAtMax;
            }

            return maxAv / div;
        }


        public FzSet AddTriangularSet(string name, double minBound, double peak, double maxBound)
        {
            MemberSets[name] = new FuzzySet_Triangle(peak, peak - minBound, maxBound - peak);
            AdjustRangeToFit(minBound, maxBound);

            return new FzSet(MemberSets[name]);
        }

        public FzSet AddLeftShoulderSet(string name, double minBound, double peak, double maxBound)
        {
            MemberSets[name] = new FuzzySet_LeftShoulder(peak, peak - minBound, maxBound - peak);

            //adjust range if necessary
            AdjustRangeToFit(minBound, maxBound);

            return new FzSet(MemberSets[name]);
        }

        public FzSet AddRightShoulderSet(string name, double minBound, double peak, double maxBound)
        {
            MemberSets[name] = new FuzzySet_RightShoulder(peak, peak - minBound, maxBound - peak);

            //adjust range if necessary
            AdjustRangeToFit(minBound, maxBound);

            return new FzSet(MemberSets[name]);
        }

        public void AdjustRangeToFit(double minBound, double maxBound)
        {
            if (minBound < MinRange) MinRange = minBound;
            if (maxBound > MaxRange) MaxRange = maxBound;
        }
    }
}