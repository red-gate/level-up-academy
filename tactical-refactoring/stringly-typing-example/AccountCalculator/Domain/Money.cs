namespace AccountCalculator.Domain
{
    /// <summary>
    /// Represents an amount of money, having a specific numeric <see cref="Amount"/>and an associated
    /// <see cref="Currency"/>.
    /// </summary>
    /// <param name="Amount">The numeric value of the money.</param>
    /// <param name="Currency">The associated currency.</param>
    public record Money(decimal Amount, Currency Currency)
    {
        public override string ToString() => $"{Amount:F2} {Currency}";
    }
}
