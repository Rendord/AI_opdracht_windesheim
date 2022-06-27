namespace SmartZombies.FuzzyLogic
{
    class FuzzySet_LeftShoulder : FuzzySet
    {
        private double PeakPoint;
        private double LeftOffset;
        private double RightOffset;

        public FuzzySet_LeftShoulder(double peak, double leftOffset, double rightOffset) : base(((peak - leftOffset) + peak) / 2)
        {
            PeakPoint = peak;
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

            //find DOM if right of center
            else if ((val >= PeakPoint) && (val < (PeakPoint + RightOffset)))
            {
                double grad = 1.0 / -RightOffset;

                return grad * (val - PeakPoint) + 1.0;
            }

            //find DOM if left of center
            else if ((val < PeakPoint) && (val >= PeakPoint - LeftOffset))
            {
                return 1.0;
            }

            //out of range of FLV
            else
            {
                return 0.0;
            }
        }
        public override double LocationOfMaxima()
        {
            return PeakPoint + LeftOffset / 2;
        }
    }
}