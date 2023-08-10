using BuySell.Data.Entities;
using BuySell.Data.Resources;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;



namespace BuySell.Data.Extensions
{
    public static class QueryExtensions
    {
        /// <summary>
        /// This method paginates the query.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="query"></param>
        /// <typeparam name="TSource"></typeparam>
        /// <returns></returns>
        public static IQueryable<TSource> Paginate<TSource>(this IQueryable<TSource> source, Query query)
        {
            if (query.PageNumber <= 0 || query.PageSize <= 0)
                return source;

            return source.Skip((query.PageNumber - 1) * query.PageSize).Take(query.PageSize);
        }

        /// <summary>
        /// This method sorts the query
        /// </summary>
        /// <param name="source"></param>
        /// <param name="query"></param>
        /// <typeparam name="TSource"></typeparam>
        /// <returns></returns>
        public static IQueryable<TSource> Sort<TSource>(this IQueryable<TSource> source, Query query)
        {
            if (string.IsNullOrEmpty(query.SortColumn))
                query.SortColumn = "CreatedAtUtc";

            if (string.IsNullOrEmpty(query.SortDirection))
                query.SortDirection = "desc";

            return source.OrderBy(query.SortColumn, query.SortDirection == "desc");
        }

        /// <summary>
        /// Internal method for sorting
        /// </summary>
        /// <param name="source"></param>
        /// <param name="orderByProperty"></param>
        /// <param name="desc"></param>
        /// <typeparam name="TSource"></typeparam>
        /// <returns></returns>
        private static IQueryable<TSource> OrderBy<TSource>(this IQueryable<TSource> source, string orderByProperty,
            bool desc)
        {
            var command = desc ? "OrderByDescending" : "OrderBy";

            var type = typeof(TSource);

            var property = type.GetProperty(orderByProperty,
                BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (property is null)
            {
                var compositeProp = orderByProperty.Split('.');
                if (compositeProp.Length != 2)
                    return source;

                var compositeProperty = type.GetProperty(compositeProp[0],
                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                var compositeType = type.GetProperty(compositeProp[0],
                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)?.PropertyType;

                if (compositeProperty is null || compositeType is null)
                    return source;

                property = compositeType.GetProperty(compositeProp[1],
                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                return property is null ? source : CreateCompositeSort(source, type, compositeProperty, property, command);
            }

            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);

            var lambdaExpression = Expression.Lambda(propertyAccess, parameter);

            var resultExpression = Expression.Call(typeof(Queryable), command,
                new[] { type, lambdaExpression.Body.Type }, source.Expression, Expression.Quote(lambdaExpression));

            return source.Provider.CreateQuery<TSource>(resultExpression);
        }

        private static IQueryable<TSource> CreateCompositeSort<TSource>(IQueryable<TSource> source,
            Type type,
            PropertyInfo property1,
            PropertyInfo property2,
            string command)
        {
            var parameter = Expression.Parameter(type, "p");
            var propertyAccessTmp = Expression.MakeMemberAccess(parameter, property1);
            var propertyAccess = Expression.MakeMemberAccess(propertyAccessTmp, property2);

            var lambdaExpression = Expression.Lambda(propertyAccess, parameter);

            var resultExpression = Expression.Call(typeof(Queryable), command,
                new[] { type, lambdaExpression.Body.Type }, source.Expression, Expression.Quote(lambdaExpression));

            return source.Provider.CreateQuery<TSource>(resultExpression);
        }

        /// <summary>
        /// This method filters with tracking
        /// </summary>
        /// <param name="source"></param>
        /// <param name="query"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IQueryable<T> AddAsNoTracking<T>(this IQueryable<T> source, Query query) where T : class
        {
            return !query.AsNoTracking ? source : source.AsNoTracking();
        }

        /// <summary>
        /// This method filters with tracking
        /// </summary>
        /// <param name="source"></param>
        /// <param name="asNoTracking"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IQueryable<T> AddAsNoTracking<T>(this IQueryable<T> source, bool asNoTracking = false) where T : class
        {
            return !asNoTracking ? source : source.AsNoTracking();
        }

        public static IQueryable<T> FilterBy<T, TQuery>(this IQueryable<T> source, TQuery query) where TQuery : Query
        {
            var props = query
                .GetType()
                .GetProperties(BindingFlags.Public
                               | BindingFlags.Instance
                               | BindingFlags.DeclaredOnly);

            var propsWithoutMinMax = props
                .Where(x => !x.Name.AsSpan().StartsWith("Min")
                            && !x.Name.AsSpan().StartsWith("Max"))
                .ToArray();

            var propsMin = props
                .Where(x => x.Name.AsSpan().StartsWith("Min"))
                .ToArray();
            var propsMax = props
                .Where(x => x.Name.AsSpan().StartsWith("Max"))
                .ToArray();

            var isAnd = query.GetType()
                .GetProperties()
                .First(x => x.Name.Contains("Intersect")).GetValue(query) as bool? ?? false;

            Expression? mainExpression = null;
            var parameter = Expression.Parameter(typeof(T), "y");
            foreach (var prop in propsMin)
            {
                var valueMin = prop.GetValue(query);
                var valueMax = propsMax.FirstOrDefault(x => x.Name[3..] == prop.Name[3..])?.GetValue(query);

                if (valueMin is null
                    && valueMax is null)
                    continue;

                var expression =
                    CreateExpressionBetween<T, object?>(ref parameter, prop.Name[3..], ref valueMin, ref valueMax);

                mainExpression = mainExpression is null ? expression :
                    isAnd ? Expression.AndAlso(mainExpression, expression) : Expression.OrElse(mainExpression, expression);
            }

            foreach (var propertyInfo in propsWithoutMinMax)
            {
                Expression expression;
                if (propertyInfo.PropertyType.ToString().Contains("List"))
                {
                    // TODO: implement Filter by list
                    continue;
                }

                if (propertyInfo.PropertyType.ToString().Contains("String"))
                {
                    var value = propertyInfo.GetValue(query) as string;
                    if (value is null)
                        continue;

                    expression = CreateExpressionContains<T>(ref parameter, propertyInfo.Name, ref value);
                    if (mainExpression is null)
                        mainExpression = expression;
                    else
                        mainExpression = isAnd
                            ? Expression.AndAlso(mainExpression, expression)
                            : Expression.OrElse(mainExpression, expression);
                    continue;
                }

                var valueLong = propertyInfo.GetValue(query) as long?;
                if (valueLong is null)
                    continue;
                expression = CreateExpressionEquals<T, long>(ref parameter, propertyInfo.Name, (long)valueLong);
                mainExpression = mainExpression is null ? expression :
                    isAnd ? Expression.AndAlso(mainExpression, expression) : Expression.OrElse(mainExpression, expression);
            }

            mainExpression ??= Expression.Constant(true);
            var lambdaExpression = Expression.Lambda<Func<T, bool>>(mainExpression, parameter);
            var resultExpression = Expression.Call(typeof(Queryable), "Where", new[] { typeof(T) }, source.Expression,
                Expression.Quote(lambdaExpression));

            return source.Provider.CreateQuery<T>(resultExpression);
        }

        private static Expression CreateExpressionEquals<T, TValue>(ref ParameterExpression parameter,
            string propertyName,
            TValue filterValue)
        {
            var type = typeof(T);
            var property = type.GetProperty(propertyName,
                BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)!;


            Expression memberExpression = Expression.MakeMemberAccess(parameter, property);

            var toStringCall = memberExpression;
            if (memberExpression.Type.Name != "String")
            {
                var toStringMethodInfo = property.PropertyType.GetMethod("ToString", new Type[] { })!;
                toStringCall = Expression.Call(memberExpression, toStringMethodInfo);
            }

            var toLowerMethodInfo = typeof(string).GetMethod("ToLower", new Type[] { })!;
            Expression toStringToLowerCall = Expression.Call(toStringCall, toLowerMethodInfo);

            var containsMethodInfo = typeof(string).GetMethod("Equals", new[] { typeof(string) })!;
            var constantExpression = Expression.Constant(filterValue!.ToString(), typeof(string));

            Expression toStringToLowerContainsCall =
                Expression.Call(toStringToLowerCall, containsMethodInfo, constantExpression);


            return toStringToLowerContainsCall;
        }

        private static Expression CreateExpressionContains<T>(ref ParameterExpression parameter,
            string propertyName,
            ref string filterValue)
        {
            var type = typeof(T);
            var property = type.GetProperty(propertyName,
                BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)!;

            Expression memberExpression = Expression.MakeMemberAccess(parameter, property);

            var value = filterValue.ToLower();
            var toStringCall = memberExpression;
            if (memberExpression.Type.Name != "String")
            {
                var toStringMethodInfo = property.PropertyType.GetMethod("ToString", new Type[] { })!;
                toStringCall = Expression.Call(memberExpression, toStringMethodInfo);
            }

            var toLowerMethodInfo = typeof(string).GetMethod("ToLower", new Type[] { })!;
            Expression toStringToLowerCall = Expression.Call(toStringCall, toLowerMethodInfo);

            var containsMethodInfo = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;
            var constantExpression = Expression.Constant(value, typeof(string));

            Expression toStringToLowerContainsCall =
                Expression.Call(toStringToLowerCall, containsMethodInfo, constantExpression);


            return toStringToLowerContainsCall;
        }

        private static Expression CreateExpressionBetween<T, TValue>(ref ParameterExpression parameter,
            string propertyName,
            ref TValue minValue,
            ref TValue maxValue)
        {
            var type = typeof(T);
            var property = type.GetProperty(propertyName,
                BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)!;

            Expression memberExpression = Expression.MakeMemberAccess(parameter, property);

            Expression? lowerBound = minValue is not null
                ? Expression.GreaterThanOrEqual(memberExpression, Expression.Constant(minValue, memberExpression.Type))
                : null;

            Expression? upperBound = maxValue is not null
                ? Expression.LessThanOrEqual(memberExpression, Expression.Constant(maxValue, memberExpression.Type))
                : null;

            if (lowerBound is not null && upperBound is not null)
            {
                return Expression.AndAlso(lowerBound, upperBound);
            }

            return lowerBound ?? upperBound!;
        }

        /// <summary>
        /// Internal method for response filtering by equals
        /// </summary>
        /// <param name="source"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static IQueryable<T> FilterByEquals<T, M>(this IQueryable<T> source, string propertyName,
            M value)
        {
            var type = typeof(T);
            var property = type.GetProperty(propertyName,
                BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)!;

            var parameter = Expression.Parameter(type, "y");

            Expression memberExpression = Expression.MakeMemberAccess(parameter, property);

            var valueAsString = value != null ? value.ToString() : null;

            EqualsLambda(out Expression<Func<T, bool>> lambda,
                ref memberExpression,
                ref property,
                ref valueAsString,
                ref parameter);

            var resultExpression = Expression.Call(typeof(Queryable), "Where", new[] { type }, source.Expression,
                Expression.Quote(lambda));

            return source.Provider.CreateQuery<T>(resultExpression);
        }

        private static void EqualsLambda<T>(out Expression<Func<T, bool>> lambda,
            ref Expression memberExpression,
            ref PropertyInfo property,
            ref string value,
            ref ParameterExpression parameter)
        {
            var toStringCall = memberExpression;
            if (memberExpression.Type.Name != "String")
            {
                var toStringMethodInfo = property.PropertyType.GetMethod("ToString", new Type[] { })!;
                toStringCall = Expression.Call(memberExpression, toStringMethodInfo);
            }

            var toLowerMethodInfo = typeof(string).GetMethod("ToLower", new Type[] { })!;
            Expression toStringToLowerCall = Expression.Call(toStringCall, toLowerMethodInfo);

            var containsMethodInfo = typeof(string).GetMethod("Equals", new[] { typeof(string) })!;
            var constantExpression = Expression.Constant(value, typeof(string));

            Expression toStringToLowerContainsCall =
                Expression.Call(toStringToLowerCall, containsMethodInfo, constantExpression);

            lambda = Expression.Lambda<Func<T, bool>>(toStringToLowerContainsCall, parameter);
        }
    }
}
