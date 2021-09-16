using System;
using System.Text.RegularExpressions;

namespace Tuna.Core
{
    public struct Interval<T> where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>
    {
        public Interval(NumberLinePoint<T> start, NumberLinePoint<T> end)
        {
            Start = start;
            End = end;
        }

        public Interval(string intervalString)
        {
            var (openingBracket, firstNumber, secondNumber, closingBracket) = ExtractIntervalTokens(intervalString);

            Start = new NumberLinePoint<T>(GetTypedValue(firstNumber), openingBracket == "[");
            End = new NumberLinePoint<T>(GetTypedValue(secondNumber), closingBracket == "]");

            static T GetTypedValue(string valueString) => (T)Convert.ChangeType(valueString, typeof(T));
        }

        private static (string openingBracket, string firstNumber, string secondNumber, string closingBracket) ExtractIntervalTokens(string intervalString)
        {
            const string regexPattern = @"^(?'OpeningBracket'[[(])(?'FirstNumber'\d+(\.\d+)?),(?'SecondNumber'\d+(\.\d+)?)(?'ClosingBracket'[])])$";

            var regex = new Regex(regexPattern);
            var match = regex.Match(intervalString);

            if (!match.Success)
            {
                throw new ArgumentException($"Invalid interval '{intervalString}'. " +
                    $"Intervals need to be formated as '<[ OR (><first number>,<second number><] OR )>'. " +
                    $"Square brackets mean that the interval is closed at that point (inclusive of the number) " +
                    $"and rounded brackets mean that the point is open (non-inclusive of that point.)");
            }

            var g = match.Groups;
            return (g["OpeningBracket"].Value, g["FirstNumber"].Value, g["SecondNumber"].Value, g["ClosingBracket"].Value);
        }

        public NumberLinePoint<T> Start { get; set; }
        public NumberLinePoint<T> End { get; set; }
    }

   public struct NumberLinePoint<T> where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>
    {
        public NumberLinePoint(T value, bool isInclusive)
        {
            Value = value;
            IsInclusive = isInclusive;
        }

        public T Value { get; set; }
        public bool IsInclusive { get; set; }
    }
    
}