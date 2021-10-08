# Stringly-typing workshop

What is stringly-typed code? It's a play on words, derived from strongly-typed. Stringly-typed code is characterised by a failure to take full advantage of the strong type system that a language provides. In extreme cases, strings may be used to represent integers, floating-point values or dates, when there are other more appropriate types to represent these concept. See https://www.techopedia.com/definition/31876/stringly-typed for a more detailed description *(or Google "stringly typed"!)*.

In less extreme cases, it's still common to use built-in types to represent concepts where the chosen type is not sufficiently constrained to provide guarantees about the data being represented. For example:

- We often use `DateTime` or `DateTimeOffset` to represent a UTC date-time. But the `DateTime` can have a `Kind` other than UTC *(such as Local or Unspecified)* and `DateTimeOffset` isn't guaranteed to have a zero offset. Time offset bugs can therefore creep into software where we assume that a `DateTime` or `DateTimeOffset` represents a UTC date-time, when in fact it does not.
- We often see `string` used instead of `Uri`. If the variable's value is not a valid URI, we wouldn't know that's the case until we try to use the value. Bugs can be found earlier if `Uri` is used, so that an invalid value can be identified closer to its source, when we fail to construct a `Uri` from that value.

# The exercise

The `StringlyTyping.sln` solution contains a console application that takes an input file containing a series of purchases that have been made in 2021. See this [example input](PurchasedItems.csv). Each purchase was made at a specific point in time and in a specific currency. The point of the program is to convert all of the purchases to the same currency, and then add up the cost of each purchase to determine the total cost.

The program takes two command-line arguments, the path to the input purchases file and the common currency to convert each purchase to. We recommend setting the following as the program arguments when running the application via your IDE, otherwise you'll get a usage error message.

```
..\..\..\..\PurchasedItems.csv USD
```

You can vary the currency code to calculate the result in different currencies. The supported currencies are USD, GBP, EUR, JPY and CNY.

The point of this exercise is to try to eliminate the use of stringly typing. It's recommended that you take the following approach:

1. Explore the existing code and try to get a feel of how it all works. Consider some simple mechanical refactoring to help manage the readability. As you progress further down this list, consider adding more automated tests.
2. Replace the use of strings to represent currencies with an actual `Currency` type. What type is appropriate? An enum? A simple wrapper around the three digit currency codes? Maybe another `record` type? You decide.
3. An amount of money is currently represented by two separate values, a `decimal` for the numeric value, and a `Currency` for the associated currency. Try introducing a new `Money` type that combines these two concepts, so that you can pass around a `Money` object that represents an amount of money in a specific currency. You can hopefully use this to simplify the `ICurrencyConverter.ConvertCurrency` API.
4. Replace the use of strings to represent dates with `DateTimeOffset` *(this will be simpler that using `DateTime`!)*. Make sure you fully understand how dates are currently being used in the software before you make this change.
5. Introduce a `UtcDateTime` to represent actual UTC date-times. The timestamps of the purchases are genuinely in local times, where `DateTimeOffset` is appropriate. But the timestamps used by `ExchangeRateRecord` and `CurrencyConverter` should always be in UTC.

_**NOTE:** `DateTimeOffset` is a useful built-in type that we frequently avoid in favour of `DateTime`. When converting a `DateTimeOffset` to UTC, please note that you cannot simply take it's `Ticks` value. Two instances that represent the same instant in time but with different offsets don't actually have the same `Ticks` value, which can seem counterintuitive. Consider using the `DateTimeOffset.UtcTicks` property, which would be the same for those two instances._

When you complete the exercise, try to find examples in your own product's code where introducing stronger types could help improve the clarity of the code and reduce the risk of bugs.

Finally, there is an [example solution](../stringly-typing-example/README.md) for this exercise that you can consider if you get stuck.
