using FluentAssertions;
using Platform.Domain.Common;
using Xunit;

namespace Platform.Domain.UnitTests;

public class Money(decimal amount, string currency) : ValueObject
{
    public decimal Amount { get; } = amount;
    public string Currency { get; } = currency;
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }
}

public class ValueObjectTests
{
    [Fact]
    public void SameValues_AreEqual()
    {
        var a = new Money(10, "USD");
        var b = new Money(10, "USD");
        a.Should().Be(b);
    }

    [Fact]
    public void DifferentValues_AreNotEqual()
    {
        var a = new Money(10, "USD");
        var b = new Money(20, "USD");
        a.Should().NotBe(b);
    }
}