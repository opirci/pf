using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Expr = System.Linq.Expressions;

//namespace Pars.Core
namespace Pars.Util.Puma.Domain
{
    [DataContract(Name = "Lookup")]
    public class LookupItem
    {
        [DataMember]
        public int Ref { get; set; }
        [DataMember]
        public string Value { get; set; }

        [DataMember]
        public string Name
        {
            get { return this.Value; }
            set { this.Value = value; }
        }

        internal static Expr.Expression<Func<TSource, LookupItem>> GetExpression<TSource>()
        {
            if (!maps.ContainsKey(typeof(TSource)))
            {
                throw new ArgumentOutOfRangeException($"Mapping for type {typeof(TSource).Name} is not set. Please do not use parameterless method unless setting default mapping using LookupItem.SetDefaultMap method.");
            }
            var map = maps[typeof(TSource)];
            return Expr.Expression.Lambda<Func<TSource, LookupItem>>(map.Item1, map.Item2);
        }

        internal static Expr.Expression<Func<TSource, LookupItem<K, V>>> GetExpression<TSource, K, V>()
        {
            if (!maps.ContainsKey(typeof(TSource)))
            {
                throw new ArgumentOutOfRangeException($"Mapping for type {typeof(TSource).Name} is not set. Please do not use parameterless method unless setting default mapping using LookupItem.SetDefaultMap method.");
            }
            var map = maps[typeof(TSource)];
            return Expr.Expression.Lambda<Func<TSource, LookupItem<K, V>>>(map.Item1, map.Item2);

        }

        internal static Expr.Expression<Func<TSource, LookupItem<K, V, V1>>> GetExpression<TSource, K, V, V1>()
        {
            if (!maps.ContainsKey(typeof(TSource)))
            {
                throw new ArgumentOutOfRangeException($"Mapping for type {typeof(TSource).Name} is not set. Please do not use parameterless method unless setting default mapping using LookupItem.SetDefaultMap method.");
            }
            var map = maps[typeof(TSource)];
            return Expr.Expression.Lambda<Func<TSource, LookupItem<K, V, V1>>>(map.Item1, map.Item2);
        }

        internal static Expr.Expression<Func<TSource, LookupItem<K, V, V1, V2>>> GetExpression<TSource, K, V, V1, V2>()
        {
            if (!maps.ContainsKey(typeof(TSource)))
            {
                throw new ArgumentOutOfRangeException($"Mapping for type {typeof(TSource).Name} is not set. Please do not use parameterless method unless setting default mapping using LookupItem.SetDefaultMap method.");
            }
            var map = maps[typeof(TSource)];
            return Expr.Expression.Lambda<Func<TSource, LookupItem<K, V, V1, V2>>>(map.Item1, map.Item2);
        }

        internal static Expr.Expression<Func<TSource, LookupItem<K, V, V1, V2, V3>>> GetExpression<TSource, K, V, V1, V2, V3>()
        {
            if (!maps.ContainsKey(typeof(TSource)))
            {
                throw new ArgumentOutOfRangeException($"Mapping for type {typeof(TSource).Name} is not set. Please do not use parameterless method unless setting default mapping using LookupItem.SetDefaultMap method.");
            }
            var map = maps[typeof(TSource)];
            return Expr.Expression.Lambda<Func<TSource, LookupItem<K, V, V1, V2, V3>>>(map.Item1, map.Item2);
        }

        internal static Expr.Expression<Func<TSource, LookupItem<K, V, V1, V2, V3, V4>>> GetExpression<TSource, K, V, V1, V2, V3, V4>()
        {
            if (!maps.ContainsKey(typeof(TSource)))
            {
                throw new ArgumentOutOfRangeException($"Mapping for type {typeof(TSource).Name} is not set. Please do not use parameterless method unless setting default mapping using LookupItem.SetDefaultMap method.");
            }
            var map = maps[typeof(TSource)];
            return Expr.Expression.Lambda<Func<TSource, LookupItem<K, V, V1, V2, V3, V4>>>(map.Item1, map.Item2);
        }


        public static void SetDefaultMap<TSource, T, V, V1, V2, V3, V4>(Expr.Expression<Func<TSource, T>> key,
         Expr.Expression<Func<TSource, V>> value,
         Expr.Expression<Func<TSource, V1>> value1,
         Expr.Expression<Func<TSource, V2>> value2,
         Expr.Expression<Func<TSource, V3>> value3,
         Expr.Expression<Func<TSource, V4>> value4)
        {
            maps[typeof(T)] = GenerateLookupInitializer<TSource, T, V, V1, V2, V3, V4>(key, value, value1, value2, value3, value4);
        }

        public static void SetDefaultMap<TSource, T, V, V1, V2, V3>(Expr.Expression<Func<TSource, T>> key,
            Expr.Expression<Func<TSource, V>> value,
            Expr.Expression<Func<TSource, V1>> value1,
            Expr.Expression<Func<TSource, V2>> value2,
            Expr.Expression<Func<TSource, V3>> value3)
        {
            maps[typeof(T)] = GenerateLookupInitializer<TSource, T, V, V1, V2, V3, object>(key, value, value1, value2, value3);
        }

        public static void SetDefaultMap<TSource, T, V, V1, V2>(Expr.Expression<Func<TSource, T>> key,
            Expr.Expression<Func<TSource, V>> value,
            Expr.Expression<Func<TSource, V1>> value1,
            Expr.Expression<Func<TSource, V2>> value2)
        {
            maps[typeof(T)] = GenerateLookupInitializer<TSource, T, V, V1, V2, object, object>(key, value, value1, value2);
        }

        public static void SetDefaultMap<TSource, T, V, V1>(Expr.Expression<Func<TSource, T>> key,
            Expr.Expression<Func<TSource, V>> value,
            Expr.Expression<Func<TSource, V1>> value1)
        {
            maps[typeof(T)] = GenerateLookupInitializer<TSource, T, V, V1, object, object, object>(key, value, value1);
        }


        public static void SetDefaultMap<TSource, T, V>(Expr.Expression<Func<TSource, T>> key, Expr.Expression<Func<TSource, V>> value)
        {
            maps[typeof(T)] = GenerateLookupInitializer<TSource, T, V, object, object, object, object>(key, value);
        }

        public static void SetDefaultMap<T>(Expr.Expression<Func<T, int>> key, Expr.Expression<Func<T, string>> value)
        {
            maps[typeof(T)] = GenerateLookupInitializer<T, int, string, object, object, object, object>(key, value);
        }

        static Dictionary<Type, Tuple<Expr.MemberInitExpression, Expr.ParameterExpression>> maps =
            new Dictionary<Type, Tuple<Expr.MemberInitExpression, Expr.ParameterExpression>>();


        //static Tuple<Expr.MemberInitExpression, Expr.ParameterExpression> GenerateLookupInitializer<TSource>(
        //    Expr.Expression<Func<TSource, int>> keySelector, Expr.Expression<Func<TSource, string>> valueSelector)
        internal static Tuple<Expr.MemberInitExpression, Expr.ParameterExpression> GenerateLookupInitializer<TSource, K, V, V1, V2, V3, V4>(
            Expr.Expression<Func<TSource, K>> keySelector,
            Expr.Expression<Func<TSource, V>> valueSelector,
            Expr.Expression<Func<TSource, V1>> valueSelector1 = null,
            Expr.Expression<Func<TSource, V2>> valueSelector2 = null,
            Expr.Expression<Func<TSource, V3>> valueSelector3 = null,
            Expr.Expression<Func<TSource, V4>> valueSelector4 = null)
        {
            Expr.MemberInitExpression memberInitExpression = null;
            Expr.ParameterExpression param = Expr.Expression.Parameter(typeof(TSource), "p");
            try
            {
                List<Expr.MemberBinding> bindings = new List<Expr.MemberBinding>();


                Expr.MemberExpression ConvertSelector(Expr.LambdaExpression exp)
                {
                    Expr.MemberExpression memExp = exp.Body as Expr.MemberExpression;
                    return new ParameterUpdateVisitor(memExp.Expression as Expr.ParameterExpression, param)
                        .Visit(memExp) as Expr.MemberExpression;
                }

                Type type = null;
                if (valueSelector4 != null)
                    type = typeof(LookupItem<K, V, V1, V2, V3, V4>);
                else if (valueSelector3 != null)
                    type = typeof(LookupItem<K, V, V1, V2, V3>);
                else if (valueSelector2 != null)
                    type = typeof(LookupItem<K, V, V1, V2>);
                else if (valueSelector1 != null)
                    type = typeof(LookupItem<K, V, V1>);
                else if (valueSelector != null)
                {
                    if (typeof(K) == typeof(int) && typeof(V) == typeof(string))
                    {
                        type = typeof(LookupItem);
                    }
                    else
                    {
                        type = typeof(LookupItem<K, V>);
                    }
                }
                Expr.NewExpression newLookup = Expr.Expression.New(type);

                bindings.Add(Expr.Expression.Bind(type.GetMember(nameof(LookupItem.Ref))[0], ConvertSelector(keySelector)));

                if (valueSelector != null)
                    bindings.Add(Expr.Expression.Bind(type.GetMember(nameof(LookupItem.Value))[0], ConvertSelector(valueSelector)));
                if (valueSelector1 != null)
                    bindings.Add(Expr.Expression.Bind(type.GetMember(nameof(LookupItem<K, V, V1>.Value1))[0], ConvertSelector(valueSelector1)));
                if (valueSelector2 != null)
                    bindings.Add(Expr.Expression.Bind(type.GetMember(nameof(LookupItem<K, V, V1, V2>.Value2))[0], ConvertSelector(valueSelector2)));
                if (valueSelector3 != null)
                    bindings.Add(Expr.Expression.Bind(type.GetMember(nameof(LookupItem<K, V, V1, V2, V3>.Value3))[0], ConvertSelector(valueSelector3)));
                if (valueSelector4 != null)
                    bindings.Add(Expr.Expression.Bind(type.GetMember(nameof(LookupItem<K, V, V1, V2, V3, V4>.Value4))[0], ConvertSelector(valueSelector4)));

                //MemberInfo refMember = typeof(LookupItem).GetMember(nameof(LookupItem.Ref))[0];
                //MemberInfo value = typeof(LookupItem).GetMember(nameof(LookupItem.Value))[0];

                //Expr.MemberBinding refMemberBinding = Expr.Expression.Bind(refMember, ConvertSelector(keySelector));
                //Expr.MemberBinding nameMemberBinding = Expr.Expression.Bind(nameMember, ConvertSelector(valueSelector1));

                //Expr.MemberInitExpression memberInitExpression = Expr.Expression.MemberInit(newLookup, refMemberBinding, nameMemberBinding);

                memberInitExpression = Expr.Expression.MemberInit(newLookup, bindings);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Tuple.Create(memberInitExpression, param);

        }
    }

    class ParameterUpdateVisitor : Expr.ExpressionVisitor
    {
        private Expr.ParameterExpression _oldParameter;
        private Expr.ParameterExpression _newParameter;

        public ParameterUpdateVisitor(Expr.ParameterExpression oldParameter, Expr.ParameterExpression newParameter)
        {
            _oldParameter = oldParameter;
            _newParameter = newParameter;
        }

        protected override Expr.Expression VisitParameter(Expr.ParameterExpression node)
        {
            if (object.ReferenceEquals(node, _oldParameter))
                return _newParameter;

            return base.VisitParameter(node);
        }
    }


    [DataContract(Name = "LookupG")]
    public class LookupItem<K, V>
    {
        [DataMember]
        public K Ref { get; set; }
        [DataMember]
        public V Value { get; set; }
    }

    [DataContract(Name = "LookupG1")]
    public class LookupItem<K, V, V1>
    {
        [DataMember]
        public K Ref { get; set; }
        [DataMember]
        public V Value { get; set; }
        [DataMember]
        public V1 Value1 { get; set; }
    }

    [DataContract(Name = "LookupG2")]
    public class LookupItem<K, V, V1, V2>
    {
        [DataMember]
        public K Ref { get; set; }
        [DataMember]
        public V Value { get; set; }
        [DataMember]
        public V1 Value1 { get; set; }
        [DataMember]
        public V2 Value2 { get; set; }
    }

    [DataContract(Name = "LookupG3")]
    public class LookupItem<K, V, V1, V2, V3>
    {
        [DataMember]
        public K Ref { get; set; }
        [DataMember]
        public V Value { get; set; }
        [DataMember]
        public V1 Value1 { get; set; }
        [DataMember]
        public V2 Value2 { get; set; }
        [DataMember]
        public V3 Value3 { get; set; }
    }


    [DataContract(Name = "LookupG4")]
    public class LookupItem<K, V, V1, V2, V3, V4>
    {
        [DataMember]
        public K Ref { get; set; }
        [DataMember]
        public V Value { get; set; }
        [DataMember]
        public V1 Value1 { get; set; }
        [DataMember]
        public V2 Value2 { get; set; }
        [DataMember]
        public V3 Value3 { get; set; }
        [DataMember]
        public V4 Value4 { get; set; }
    }
}
