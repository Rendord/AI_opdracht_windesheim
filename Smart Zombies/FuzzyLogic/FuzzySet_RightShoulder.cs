namespace SmartZombies.FuzzyLogic
{
    public class FuzzySet_RightShoulder : FuzzySet
    {
        private double PeakPoint;
        private double LeftOffset;
        private double RightOffset;

        public FuzzySet_RightShoulder(double peak, double leftOffset, double rightOffset) : base(
            (peak + rightOffset + peak) / 2)
        {
            this.PeakPoint = peak;
            this.LeftOffset = leftOffset;
            this.RightOffset = rightOffset;
        }

        public override double CalculateDOM(double val)
        {
            //test for the case where the left or right offsets are zero
            //(to prevent divide by zero errors below)
            if ((Equals(RightOffset, 0.0) && (Equals(PeakPoint, val))) ||
                (Equals(LeftOffset, 0.0) && (Equals(PeakPoint, val))))
            {
                return 1.0;
            }
            //Find DOM if left of center
            else if ((val <= PeakPoint) && (val > (PeakPoint - LeftOffset)))
            {
                double grad = 1.0 / LeftOffset;
                return grad * (val - (PeakPoint - LeftOffset));
            }
            //find DOM if right of center
            else if ((val > PeakPoint) && (val <= PeakPoint + RightOffset))
            {
                return 1.0;
            }
            //out of range of FLV
            else
            {
                return 0;
            }
        }

        public override double LocationOfMaxima()
        {
            return LeftOffset + PeakPoint / 2;
        }
    }
}