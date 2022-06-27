using Godot;

namespace SmartZombies.FuzzyLogic
{
    public abstract class FuzzyTerm : Object
    {
        public abstract FuzzyTerm Clone();

        public abstract double GetDOM();

        public abstract void ClearDOM();

        public abstract void ORwithDOM(double val);
    }
}
