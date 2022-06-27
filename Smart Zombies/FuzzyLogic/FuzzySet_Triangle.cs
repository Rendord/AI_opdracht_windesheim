namespace SmartZombies.FuzzyLogic
{
    public class FuzzySet_Triangle : FuzzySet
    {
        private double PeakPoint;
        private double LeftOffset;
        private double RightOffset;

        public FuzzySet_Triangle(double peak, double leftOffset, double rightOffset) : base(peak)
        {
            PeakPoint = peak;
            LeftOffset = leftOffset;
            RightOffset = rightOffset;
        }

        public override double CalculateDOM(double val)
        {
            //test for the case where the triangle's left or right offsets are zero
            //(to prevent divide by zero errors below)
            if ((Equals(RightOffset, 0.0) && (Equals(PeakPoint, val))) ||
                (Equals(LeftOffset, 0.0) && (Equals(PeakPoint, val))))
            {
                return 1.0;
            }
            
            //find DOM if left of center
            if ((val <= PeakPoint) && (val >= (PeakPoint - LeftOffset)))
            {
                double grad = 1.0 / LeftOffset;

                return grad * (val - (PeakPoint - LeftOffset));
            }
            //find DOM if right of center
            else if ((val > PeakPoint) && (val < (PeakPoint + RightOffset)))
            {
                double grad = 1.0 / -RightOffset;

                return grad * (val - PeakPoint) + 1.0;
            }
            //out of range of this FLV, return zero
            else
            {
                return 0.0;
            }
        }

        public override double LocationOfMaxima()
        {
            return PeakPoint;
        }
    }
}