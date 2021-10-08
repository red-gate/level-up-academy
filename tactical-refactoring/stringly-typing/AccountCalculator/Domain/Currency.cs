namespace AccountCalculator.Domain
{
    public record Currency (string Code)
    {
        public override string ToString()
        {
            return Code;
        }
    }
}