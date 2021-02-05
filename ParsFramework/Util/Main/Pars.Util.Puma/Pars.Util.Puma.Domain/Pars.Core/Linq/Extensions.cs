using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Expr = System.Linq.Expressions;
using System.Reflection;
using Pars.Util.Puma.Domain;

namespace Pars.Core.Linq
{
    public static class Extensions
    {
        public static bool Match(this string item, string value, TextSearchAs search = TextSearchAs.Equals)
        {
            bool result = false;
            if (item != null)
                if (search == TextSearchAs.Equals)
                    result = item.Equals(value);
                else if (search == TextSearchAs.Contains)
                    result = item.Contains(value);
                else if (search == TextSearchAs.StartsWith)
                    result = item.StartsWith(value);
                else if (search == TextSearchAs.EndsWith)
                    result = item.EndsWith(value);

            return result;
        }

        public static bool Match(this string item, TextSearch search)
            => Match(item, search.Text, search.SearchAs);        

        public static ITQueryable<T> AsTQueryable<T>(this IEnumerable<T> query, IPagedList paged = null)
            => new TEnumerable<T>(query, paged);

        public static ITQueryable<T> AsTQueryable<T>(this ITQueryable<T> query)
            => query;

        public static ITQueryable<T> Where<T>(this ITQueryable<T> query, Expression<Func<T, bool>> func)
            => Queryable.Where(query, Convert(func)).AsTQueryable(query); // AsTQueryable();

        public static ITQueryable<T> WithPaging<T>(this ITQueryable<T> query, IPaging paging = null)
        {
            int queryCnt = 0;
            if (paging != null)
            {
                queryCnt = query.Count();
                //query = query.WithPaging(paging);
                int pageNumber = paging.PageNumber;
                int pageSize = paging.PageSize;

                if (pageNumber == 0)
                    pageNumber = 1;
                if (pageSize == 0)
                    pageSize = 100000;
                query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).AsTQueryable();
                query.TotalCount = queryCnt;
                query.PageNumber = paging.PageNumber;
                query.PageSize = paging.PageSize;

            }

            return query;
        }

        public static IQueryable<T> WithPaging<T>(this IQueryable<T> query, IPaging paging)
            => WithPaging(query, paging.PageNumber, paging.PageSize);

        public static IQueryable<T> WithPaging<T>(this IQueryable<T> query, int pageNumber, int pageSize)
        {
            if (pageNumber == 0)
                pageNumber = 1;
            if (pageSize == 0)
                pageSize = 100000;
            return query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        public static PagedList<T> AsPagedList<T>(this ITQueryable<T> query)
            => new PagedList<T>(query, query.TotalCount, query.PageNumber, query.PageSize);

        public static PagedList<T> AsPagedList<T>(this IQueryable<T> query, IPaging paging)
            => AsPagedList(query, paging.PageNumber, paging.PageSize);

        public static PagedList<T> AsPagedList<T>(this IQueryable<T> query, int pageNumber, int pageSize)
            =>
            //var query1 = query
            //    .WithPaging(pageNumber, pageSize)
            //    .GroupBy(i => new { Total = query.Count() });

            //var query2 = query1.FirstOrDefault();

            //return (query2 == null)
            //    ? new PagedList<T>(Enumerable.Empty<T>(), 0, pageNumber, pageSize)
            //    : new PagedList<T>(query2.Select(i => i).AsEnumerable(), query2.Key.Total, pageNumber, pageSize);

            new PagedList<T>(query.WithPaging(pageNumber, pageSize).ToList(), query.Count(), pageNumber, pageSize);


        public static ITQueryable<LookupItem> AsLookup<TSource, TResult>(this ITQueryable<TSource> source,
            Expression<Func<TSource, TResult>> selector) where TResult : LookupItem
            => source.Select(selector).AsTQueryable(source);

        public static ITQueryable<LookupItem> AsLookup<TSource>(this ITQueryable<TSource> source,
            Expression<Func<TSource, int>> key, Expression<Func<TSource, string>> value)
            => source.Select(GenerateLookupInitializer(key, value)).AsTQueryable(source);

        public static IQueryable<LookupItem> AsLookup<TSource, TResult>(this IQueryable<TSource> source,
            Expression<Func<TSource, TResult>> selector) where TResult : LookupItem
            => source.Select(selector);

        public static IQueryable<LookupItem> AsLookup<TSource>(this IQueryable<TSource> source,
            Expression<Func<TSource, int>> key, Expression<Func<TSource, string>> value)
            => source.Select(GenerateLookupInitializer(key, value));

        public static IQueryable<LookupItem<K, V>> AsLookup<TSource, K, V>(this IQueryable<TSource> source,
            Expression<Func<TSource, K>> key, Expression<Func<TSource, V>> value)
        {
            var tuple = LookupItem.GenerateLookupInitializer<TSource, K, V, object, object, object, object>(key, value);
            return source.Select(Expr.Expression.Lambda<Func<TSource, LookupItem<K, V>>>(tuple.Item1, tuple.Item2));
        }

        public static ITQueryable<LookupItem<K, V>> AsLookup<TSource, K, V>(this ITQueryable<TSource> source,
           Expression<Func<TSource, K>> key, Expression<Func<TSource, V>> value)
            => AsLookup((IQueryable<TSource>)source, key, value).AsTQueryable(source);


        public static IQueryable<LookupItem<K, V, V1>> AsLookup<TSource, K, V, V1>(this IQueryable<TSource> source,
            Expression<Func<TSource, K>> key,
            Expression<Func<TSource, V>> value,
            Expression<Func<TSource, V1>> value1)
        {
            var tuple = LookupItem.GenerateLookupInitializer<TSource, K, V, V1, object, object, object>(key, value, value1);
            return source.Select(Expr.Expression.Lambda<Func<TSource, LookupItem<K, V, V1>>>(tuple.Item1, tuple.Item2));
        }

        public static ITQueryable<LookupItem<K, V, V1>> AsLookup<TSource, K, V, V1>(this ITQueryable<TSource> source,
           Expression<Func<TSource, K>> key,
           Expression<Func<TSource, V>> value,
           Expression<Func<TSource, V1>> value1)
           => AsLookup((IQueryable<TSource>)source, key, value, value1).AsTQueryable(source);

        public static IQueryable<LookupItem<K, V, V1, V2>> AsLookup<TSource, K, V, V1, V2>(this IQueryable<TSource> source,
            Expression<Func<TSource, K>> key,
            Expression<Func<TSource, V>> value,
            Expression<Func<TSource, V1>> value1,
            Expression<Func<TSource, V2>> value2)
        {
            var tuple = LookupItem.GenerateLookupInitializer<TSource, K, V, V1, V2, object, object>(key, value, value1, value2);
            return source.Select(Expr.Expression.Lambda<Func<TSource, LookupItem<K, V, V1, V2>>>(tuple.Item1, tuple.Item2));
        }

        public static ITQueryable<LookupItem<K, V, V1, V2>> AsLookup<TSource, K, V, V1, V2>(this ITQueryable<TSource> source,
           Expression<Func<TSource, K>> key,
           Expression<Func<TSource, V>> value,
           Expression<Func<TSource, V1>> value1,
           Expression<Func<TSource, V2>> value2)
           => AsLookup((IQueryable<TSource>)source, key, value, value1, value2).AsTQueryable();

        public static IQueryable<LookupItem<K, V, V1, V2, V3>> AsLookup<TSource, K, V, V1, V2, V3>(this IQueryable<TSource> source,
            Expression<Func<TSource, K>> key,
            Expression<Func<TSource, V>> value,
            Expression<Func<TSource, V1>> value1,
            Expression<Func<TSource, V2>> value2,
            Expression<Func<TSource, V3>> value3)
        {
            var tuple = LookupItem.GenerateLookupInitializer<TSource, K, V, V1, V2, V3, object>(key, value, value1, value2, value3);
            return source.Select(Expr.Expression.Lambda<Func<TSource, LookupItem<K, V, V1, V2, V3>>>(tuple.Item1, tuple.Item2));
        }

        public static ITQueryable<LookupItem<K, V, V1, V2, V3>> AsLookup<TSource, K, V, V1, V2, V3>(this ITQueryable<TSource> source,
           Expression<Func<TSource, K>> key,
           Expression<Func<TSource, V>> value,
           Expression<Func<TSource, V1>> value1,
           Expression<Func<TSource, V2>> value2,
           Expression<Func<TSource, V3>> value3)
           => AsLookup((IQueryable<TSource>)source, key, value, value1, value2, value3).AsTQueryable(source);

        public static IQueryable<LookupItem> AsLookup<TSource>(this IQueryable<TSource> source)
           => source.Select(LookupItem.GetExpression<TSource>());

        public static ITQueryable<LookupItem> AsLookup<TSource>(this ITQueryable<TSource> source)
            => source.Select(LookupItem.GetExpression<TSource>()).AsTQueryable(source);

        public static ITQueryable<T> GetItems<T>(this IQueryable<T> query, Expression<Func<T, string>> selector)
            => GetItems(query, selector);

        public static ITQueryable<T> GetItems<T>(this IQueryable<T> query, Expression<Func<T, string>> selector, IPaging paging = null)
           => GetItems(query, selector, null, paging);

        public static ITQueryable<T> GetItems<T>(this IQueryable<T> query, Expression<Func<T, string>> selector = null, TextSearch search = null, IPaging paging = null)
        {
            if ((paging != null || search != null) && selector == null)
            {
                throw new ArgumentNullException($"{nameof(selector)} must be provided");
            }
            if (paging != null)
            {
                query = query.OrderBy(selector);
            }
            if (!TextSearch.IsNullOrEmpty(search))
            {
                query = query.AsTQueryable().Where(GenerateMatchExpression(selector, search));
            }

            ITQueryable<T> res = query.AsTQueryable();
            if (paging != null)
            {
                res = res.WithPaging(paging);
            }
            return res;
        }

        static Expression<Func<T, bool>> GenerateMatchExpression<T>(Expression<Func<T, string>> selector, TextSearch search)
        {
            Expr.Expression[] arguments = new Expr.Expression[] {
                    selector.Body,
                    Expr.Expression.Constant(search.Text),
                    Expr.Expression.Constant(search.SearchAs)
                };

            MethodCallExpression method = Expr.Expression.Call(
                typeof(Pars.Core.Linq.Extensions).GetMethod(nameof(Pars.Core.Linq.Extensions.Match)),
                arguments);

            return Expr.Expression.Lambda<Func<T, bool>>(method, selector.Parameters);
        }

        static Expression<Func<TSource, LookupItem>> GenerateLookupInitializer<TSource>(
            Expression<Func<TSource, int>> keySelector, Expression<Func<TSource, string>> valueSelector)
        {
            ParameterExpression param = Expr.Expression.Parameter(typeof(TSource), "p");

            MemberExpression ConvertSelector(LambdaExpression exp)
            {
                MemberExpression kxp = exp.Body as MemberExpression;
                return new ParameterUpdateVisitor(kxp.Expression as ParameterExpression, param)
                    .Visit(kxp) as MemberExpression;
            }

            NewExpression newLookup = Expr.Expression.New(typeof(LookupItem));
            MemberInfo refMember = typeof(LookupItem).GetMember(nameof(LookupItem.Ref))[0];
            MemberInfo nameMember = typeof(LookupItem).GetMember(nameof(LookupItem.Value))[0];

            //MemberExpression keyexp = keySelector.Body as MemberExpression;
            //MemberExpression valExp = valueSelector.Body as MemberExpression;

            //ParameterUpdateVisitor visitor = new ParameterUpdateVisitor(keyexp.Expression as ParameterExpression, param);
            //keyexp = visitor.Visit(keyexp) as MemberExpression;

            //visitor = new ParameterUpdateVisitor(valExp.Expression as ParameterExpression, param);
            //valExp = visitor.Visit(valExp) as MemberExpression;

            MemberBinding refMemberBinding = Expr.Expression.Bind(refMember, ConvertSelector(keySelector)); //keyexp);
            MemberBinding nameMemberBinding = Expr.Expression.Bind(nameMember, ConvertSelector(valueSelector)); // valExp);
            MemberInitExpression memberInitExpression = Expr.Expression.MemberInit(newLookup, refMemberBinding, nameMemberBinding);

            Expression<Func<TSource, LookupItem>> ret = Expr.Expression.Lambda<Func<TSource, LookupItem>>(memberInitExpression, param);
            return ret;

        }

        static Expression<Func<T, bool>> Convert<T>(this Expression<Func<T, bool>> exp)
            => Expr.Expression.Lambda<Func<T, bool>>(CheckExpression(exp.Body), exp.Parameters);

        static Expr.Expression CheckExpression(Expr.Expression exp)
        {
            BinaryExpression binExp = exp as BinaryExpression;

            if (binExp != null)
            {
                Expr.Expression left = binExp.Left;
                Expr.Expression right = binExp.Right;

                left = (left is BinaryExpression) ? CheckExpression(left) : CheckMethodCallExpression(left);
                right = (right is BinaryExpression) ? CheckExpression(right) : CheckMethodCallExpression(right);

                return Expr.Expression.MakeBinary(binExp.NodeType, left, right);
            }

            return CheckMethodCallExpression(exp);
        }

        static Expr.Expression CheckMethodCallExpression(Expr.Expression exp)
        {
            TextSearchAs? GetValue(Expr.Expression param, object item)
            {
                Type type = null;
                object val = null;

                ConstantExpression constant = param as ConstantExpression;

                if (constant != null)
                {
                    type = constant.Type;
                    val = constant.Value;
                }
                else
                {
                    MemberExpression member = param as MemberExpression;
                    if (member != null)
                    {
                        var objectMember = Expr.Expression.Convert(member, typeof(object));
                        var getterLambda = Expr.Expression.Lambda<Func<object>>(objectMember);
                        var getter = getterLambda.Compile();
                        val = getter();
                        type = member.Member.ReflectedType;
                    }
                }

                return ((type == typeof(TextSearch) || type == typeof(TextSearchAs)) && val != null) ? (TextSearchAs?)val : null;
            }

            MethodCallExpression call = exp as MethodCallExpression;
            TextSearchAs? value;
            if (call != null && call.Method.Name == nameof(Extensions.Match)
                && call.Arguments.Count == 3 && (value = GetValue(call.Arguments[2], call)) != null)
            {
                MethodInfo[] mi = typeof(System.String).GetTypeInfo().GetDeclaredMethods(value.ToString()).ToArray();
                return
                    Expr.Expression.Call(
                        call.Arguments[0],
                        mi[0],
                        call.Arguments[1]);
            }
            return exp;
        }
    }
}

