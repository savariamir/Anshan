using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Anshan.Test.Faker
{
    public static class FakerExtension
    {
        public static T With<T, TProperty>(this T instance, Expression<Func<T, TProperty>> expression, TProperty value)
            where TProperty : notnull
        {
            var memberSelectorExpression = (MemberExpression)expression.Body;

            var property = (PropertyInfo)memberSelectorExpression.Member;

            property.SetValue(instance, value, null);

            return instance;
        }

        public static T WithFutureDate<T>(this T instance,
                                          Expression<Func<T, DateTime>> expression,
                                          int minimumDaysInFuture = 1,
                                          int maximumDaysInFuture = 10)
        {
            var memberSelectorExpression = (MemberExpression)expression.Body;

            var property = (PropertyInfo)memberSelectorExpression.Member;

            var random = new Random();

            var value = DateTime.UtcNow.AddDays(random.Next(minimumDaysInFuture, maximumDaysInFuture));

            property.SetValue(instance, value, null);

            return instance;
        }
        
        public static T WithFutureDate<T>(this T instance,
                                          Expression<Func<T, DateTime?>> expression,
                                          int minimumDaysInFuture = 1,
                                          int maximumDaysInFuture = 10)
        {
            var memberSelectorExpression = (MemberExpression)expression.Body;

            var property = (PropertyInfo)memberSelectorExpression.Member;

            var random = new Random();

            var value = DateTime.UtcNow.AddDays(random.Next(minimumDaysInFuture, maximumDaysInFuture));

            property.SetValue(instance, value, null);

            return instance;
        }

        public static T WithRandom<T, TEnum>(this T instance, Expression<Func<T, string>> expression)
            where TEnum : struct, Enum
        {
            var enumValues = Enum.GetValues<TEnum>().Select(v => v.ToString());

            return WithRandom(instance, expression, enumValues);
        }

        public static T WithRandomEnum<T>(this T instance, Expression<Func<T, string>> expression, Type enumType)
        {
            var enumValues = Enum.GetNames(enumType);

            return WithRandom(instance, expression, enumValues);
        }

        public static T WithRandom<T>(this T instance,
                                      Expression<Func<T, string>> expression,
                                      IEnumerable<string> values)
        {
            var valuesAsList = values.ToList();

            var memberSelectorExpression = (MemberExpression)expression.Body;

            var property = (PropertyInfo)memberSelectorExpression.Member;

            var random = new Random();

            var selectedItemIndex = random.Next(0, valuesAsList.Count);

            property.SetValue(instance, valuesAsList.ElementAt(selectedItemIndex), null);

            return instance;
        }
    }
}