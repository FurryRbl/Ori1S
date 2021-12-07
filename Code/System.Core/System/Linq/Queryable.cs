using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Linq
{
	// Token: 0x0200002F RID: 47
	public static class Queryable
	{
		// Token: 0x060002E4 RID: 740 RVA: 0x0000C85C File Offset: 0x0000AA5C
		private static MethodInfo MakeGeneric(MethodBase method, params Type[] parameters)
		{
			return ((MethodInfo)method).MakeGenericMethod(parameters);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000C86C File Offset: 0x0000AA6C
		private static Expression StaticCall(MethodInfo method, params Expression[] expressions)
		{
			return Expression.Call(null, method, expressions);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000C878 File Offset: 0x0000AA78
		private static TRet Execute<TRet, TSource>(this IQueryable<TSource> source, MethodBase current)
		{
			return source.Provider.Execute<TRet>(Queryable.StaticCall(Queryable.MakeGeneric(current, new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000C8C0 File Offset: 0x0000AAC0
		public static TSource Aggregate<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, TSource, TSource>> func)
		{
			Check.SourceAndFunc(source, func);
			return source.Provider.Execute<TSource>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(func)
			}));
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000C91C File Offset: 0x0000AB1C
		public static TAccumulate Aggregate<TSource, TAccumulate>(this IQueryable<TSource> source, TAccumulate seed, Expression<Func<TAccumulate, TSource, TAccumulate>> func)
		{
			Check.SourceAndFunc(source, func);
			return source.Provider.Execute<TAccumulate>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource),
				typeof(TAccumulate)
			}), new Expression[]
			{
				source.Expression,
				Expression.Constant(seed),
				Expression.Quote(func)
			}));
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000C990 File Offset: 0x0000AB90
		public static TResult Aggregate<TSource, TAccumulate, TResult>(this IQueryable<TSource> source, TAccumulate seed, Expression<Func<TAccumulate, TSource, TAccumulate>> func, Expression<Func<TAccumulate, TResult>> selector)
		{
			Check.SourceAndFuncAndSelector(source, func, selector);
			return source.Provider.Execute<TResult>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource),
				typeof(TAccumulate),
				typeof(TResult)
			}), new Expression[]
			{
				source.Expression,
				Expression.Constant(seed),
				Expression.Quote(func),
				Expression.Quote(selector)
			}));
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000CA1C File Offset: 0x0000AC1C
		public static bool All<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			Check.SourceAndPredicate(source, predicate);
			return source.Provider.Execute<bool>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(predicate)
			}));
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000CA78 File Offset: 0x0000AC78
		public static bool Any<TSource>(this IQueryable<TSource> source)
		{
			Check.Source(source);
			return source.Provider.Execute<bool>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000CAC8 File Offset: 0x0000ACC8
		public static bool Any<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			Check.SourceAndPredicate(source, predicate);
			return source.Provider.Execute<bool>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(predicate)
			}));
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000CB24 File Offset: 0x0000AD24
		public static IQueryable<TElement> AsQueryable<TElement>(this IEnumerable<TElement> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			IQueryable<TElement> queryable = source as IQueryable<TElement>;
			if (queryable != null)
			{
				return queryable;
			}
			return new QueryableEnumerable<TElement>(source);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000CB58 File Offset: 0x0000AD58
		public static IQueryable AsQueryable(this IEnumerable source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			IQueryable queryable = source as IQueryable;
			if (queryable != null)
			{
				return queryable;
			}
			Type type = source.GetType();
			if (!type.IsGenericImplementationOf(typeof(IEnumerable<>)))
			{
				throw new ArgumentException("source is not IEnumerable<>");
			}
			return (IQueryable)Activator.CreateInstance(typeof(QueryableEnumerable<>).MakeGenericType(new Type[]
			{
				type.GetFirstGenericArgument()
			}), new object[]
			{
				source
			});
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000CBE0 File Offset: 0x0000ADE0
		public static double Average(this IQueryable<int> source)
		{
			Check.Source(source);
			return source.Provider.Execute<double>(Queryable.StaticCall((MethodInfo)MethodBase.GetCurrentMethod(), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000CC1C File Offset: 0x0000AE1C
		public static double? Average(this IQueryable<int?> source)
		{
			Check.Source(source);
			return source.Provider.Execute<double?>(Queryable.StaticCall((MethodInfo)MethodBase.GetCurrentMethod(), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000CC58 File Offset: 0x0000AE58
		public static double Average(this IQueryable<long> source)
		{
			Check.Source(source);
			return source.Provider.Execute<double>(Queryable.StaticCall((MethodInfo)MethodBase.GetCurrentMethod(), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000CC94 File Offset: 0x0000AE94
		public static double? Average(this IQueryable<long?> source)
		{
			Check.Source(source);
			return source.Provider.Execute<double?>(Queryable.StaticCall((MethodInfo)MethodBase.GetCurrentMethod(), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000CCD0 File Offset: 0x0000AED0
		public static float Average(this IQueryable<float> source)
		{
			Check.Source(source);
			return source.Provider.Execute<float>(Queryable.StaticCall((MethodInfo)MethodBase.GetCurrentMethod(), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000CD0C File Offset: 0x0000AF0C
		public static float? Average(this IQueryable<float?> source)
		{
			Check.Source(source);
			return source.Provider.Execute<float?>(Queryable.StaticCall((MethodInfo)MethodBase.GetCurrentMethod(), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000CD48 File Offset: 0x0000AF48
		public static double Average(this IQueryable<double> source)
		{
			Check.Source(source);
			return source.Provider.Execute<double>(Queryable.StaticCall((MethodInfo)MethodBase.GetCurrentMethod(), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000CD84 File Offset: 0x0000AF84
		public static double? Average(this IQueryable<double?> source)
		{
			Check.Source(source);
			return source.Provider.Execute<double?>(Queryable.StaticCall((MethodInfo)MethodBase.GetCurrentMethod(), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000CDC0 File Offset: 0x0000AFC0
		public static decimal Average(this IQueryable<decimal> source)
		{
			Check.Source(source);
			return source.Provider.Execute<decimal>(Queryable.StaticCall((MethodInfo)MethodBase.GetCurrentMethod(), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000CDFC File Offset: 0x0000AFFC
		public static decimal? Average(this IQueryable<decimal?> source)
		{
			Check.Source(source);
			return source.Provider.Execute<decimal?>(Queryable.StaticCall((MethodInfo)MethodBase.GetCurrentMethod(), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000CE38 File Offset: 0x0000B038
		public static double Average<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int>> selector)
		{
			Check.SourceAndSelector(source, selector);
			return source.Provider.Execute<double>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(selector)
			}));
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000CE94 File Offset: 0x0000B094
		public static double? Average<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int?>> selector)
		{
			Check.SourceAndSelector(source, selector);
			return source.Provider.Execute<double?>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(selector)
			}));
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000CEF0 File Offset: 0x0000B0F0
		public static double Average<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, long>> selector)
		{
			Check.SourceAndSelector(source, selector);
			return source.Provider.Execute<double>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(selector)
			}));
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000CF4C File Offset: 0x0000B14C
		public static double? Average<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, long?>> selector)
		{
			Check.SourceAndSelector(source, selector);
			return source.Provider.Execute<double?>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(selector)
			}));
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000CFA8 File Offset: 0x0000B1A8
		public static float Average<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, float>> selector)
		{
			Check.SourceAndSelector(source, selector);
			return source.Provider.Execute<float>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(selector)
			}));
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000D004 File Offset: 0x0000B204
		public static float? Average<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, float?>> selector)
		{
			Check.SourceAndSelector(source, selector);
			return source.Provider.Execute<float?>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(selector)
			}));
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000D060 File Offset: 0x0000B260
		public static double Average<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, double>> selector)
		{
			Check.SourceAndSelector(source, selector);
			return source.Provider.Execute<double>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(selector)
			}));
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000D0BC File Offset: 0x0000B2BC
		public static double? Average<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, double?>> selector)
		{
			Check.SourceAndSelector(source, selector);
			return source.Provider.Execute<double?>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(selector)
			}));
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000D118 File Offset: 0x0000B318
		public static decimal Average<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, decimal>> selector)
		{
			Check.SourceAndSelector(source, selector);
			return source.Provider.Execute<decimal>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(selector)
			}));
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000D174 File Offset: 0x0000B374
		public static decimal? Average<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, decimal?>> selector)
		{
			Check.SourceAndSelector(source, selector);
			return source.Provider.Execute<decimal?>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(selector)
			}));
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000D1D0 File Offset: 0x0000B3D0
		public static IQueryable<TResult> Cast<TResult>(this IQueryable source)
		{
			Check.Source(source);
			return (IQueryable<TResult>)source.Provider.CreateQuery(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TResult)
			}), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000D224 File Offset: 0x0000B424
		public static IQueryable<TSource> Concat<TSource>(this IQueryable<TSource> source1, IEnumerable<TSource> source2)
		{
			Check.Source1AndSource2(source1, source2);
			return source1.Provider.CreateQuery<TSource>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source1.Expression,
				Expression.Constant(source2)
			}));
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000D280 File Offset: 0x0000B480
		public static bool Contains<TSource>(this IQueryable<TSource> source, TSource item)
		{
			Check.Source(source);
			return source.Provider.Execute<bool>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression,
				Expression.Constant(item)
			}));
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000D2E0 File Offset: 0x0000B4E0
		public static bool Contains<TSource>(this IQueryable<TSource> source, TSource item, IEqualityComparer<TSource> comparer)
		{
			Check.Source(source);
			return source.Provider.Execute<bool>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression,
				Expression.Constant(item),
				Expression.Constant(comparer)
			}));
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000D348 File Offset: 0x0000B548
		public static int Count<TSource>(this IQueryable<TSource> source)
		{
			Check.Source(source);
			return source.Execute(MethodBase.GetCurrentMethod());
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000D35C File Offset: 0x0000B55C
		public static int Count<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			Check.SourceAndPredicate(source, predicate);
			return source.Provider.Execute<int>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(predicate)
			}));
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000D3B8 File Offset: 0x0000B5B8
		public static IQueryable<TSource> DefaultIfEmpty<TSource>(this IQueryable<TSource> source)
		{
			Check.Source(source);
			return source.Provider.CreateQuery<TSource>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000D408 File Offset: 0x0000B608
		public static IQueryable<TSource> DefaultIfEmpty<TSource>(this IQueryable<TSource> source, TSource defaultValue)
		{
			Check.Source(source);
			return source.Provider.CreateQuery<TSource>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression,
				Expression.Constant(defaultValue)
			}));
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000D468 File Offset: 0x0000B668
		public static IQueryable<TSource> Distinct<TSource>(this IQueryable<TSource> source)
		{
			Check.Source(source);
			return source.Provider.CreateQuery<TSource>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000D4B8 File Offset: 0x0000B6B8
		public static IQueryable<TSource> Distinct<TSource>(this IQueryable<TSource> source, IEqualityComparer<TSource> comparer)
		{
			Check.Source(source);
			return source.Provider.CreateQuery<TSource>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression,
				Expression.Constant(comparer)
			}));
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000D510 File Offset: 0x0000B710
		public static TSource ElementAt<TSource>(this IQueryable<TSource> source, int index)
		{
			Check.Source(source);
			return source.Provider.Execute<TSource>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression,
				Expression.Constant(index)
			}));
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000D570 File Offset: 0x0000B770
		public static TSource ElementAtOrDefault<TSource>(this IQueryable<TSource> source, int index)
		{
			Check.Source(source);
			return source.Provider.Execute<TSource>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression,
				Expression.Constant(index)
			}));
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000D5D0 File Offset: 0x0000B7D0
		public static IQueryable<TSource> Except<TSource>(this IQueryable<TSource> source1, IEnumerable<TSource> source2)
		{
			Check.Source1AndSource2(source1, source2);
			return source1.Provider.CreateQuery<TSource>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source1.Expression,
				Expression.Constant(source2)
			}));
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000D62C File Offset: 0x0000B82C
		public static IQueryable<TSource> Except<TSource>(this IQueryable<TSource> source1, IEnumerable<TSource> source2, IEqualityComparer<TSource> comparer)
		{
			Check.Source1AndSource2(source1, source2);
			return source1.Provider.CreateQuery<TSource>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source1.Expression,
				Expression.Constant(source2),
				Expression.Constant(comparer)
			}));
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000D690 File Offset: 0x0000B890
		public static TSource First<TSource>(this IQueryable<TSource> source)
		{
			Check.Source(source);
			return source.Provider.Execute<TSource>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000D6E0 File Offset: 0x0000B8E0
		public static TSource First<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			Check.SourceAndPredicate(source, predicate);
			return source.Provider.Execute<TSource>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(predicate)
			}));
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000D73C File Offset: 0x0000B93C
		public static TSource FirstOrDefault<TSource>(this IQueryable<TSource> source)
		{
			Check.Source(source);
			return source.Provider.Execute<TSource>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000D78C File Offset: 0x0000B98C
		public static TSource FirstOrDefault<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			Check.SourceAndPredicate(source, predicate);
			return source.Provider.Execute<TSource>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(predicate)
			}));
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000D7E8 File Offset: 0x0000B9E8
		public static IQueryable<IGrouping<TKey, TSource>> GroupBy<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector)
		{
			Check.SourceAndKeySelector(source, keySelector);
			return source.Provider.CreateQuery<IGrouping<TKey, TSource>>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource),
				typeof(TKey)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(keySelector)
			}));
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000D850 File Offset: 0x0000BA50
		public static IQueryable<IGrouping<TKey, TSource>> GroupBy<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, IEqualityComparer<TKey> comparer)
		{
			Check.SourceAndKeySelector(source, keySelector);
			return source.Provider.CreateQuery<IGrouping<TKey, TSource>>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource),
				typeof(TKey)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(keySelector),
				Expression.Constant(comparer)
			}));
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000D8C0 File Offset: 0x0000BAC0
		public static IQueryable<IGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, Expression<Func<TSource, TElement>> elementSelector)
		{
			Check.SourceAndKeyElementSelectors(source, keySelector, elementSelector);
			return source.Provider.CreateQuery<IGrouping<TKey, TElement>>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource),
				typeof(TKey),
				typeof(TElement)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(keySelector),
				Expression.Quote(elementSelector)
			}));
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000D940 File Offset: 0x0000BB40
		public static IQueryable<TResult> GroupBy<TSource, TKey, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, Expression<Func<TKey, IEnumerable<TSource>, TResult>> resultSelector)
		{
			Check.SourceAndKeyResultSelectors(source, keySelector, resultSelector);
			return source.Provider.CreateQuery<TResult>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource),
				typeof(TKey),
				typeof(TResult)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(keySelector),
				Expression.Quote(resultSelector)
			}));
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000D9C0 File Offset: 0x0000BBC0
		public static IQueryable<IGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, Expression<Func<TSource, TElement>> elementSelector, IEqualityComparer<TKey> comparer)
		{
			Check.SourceAndKeyElementSelectors(source, keySelector, elementSelector);
			return source.Provider.CreateQuery<IGrouping<TKey, TElement>>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource),
				typeof(TKey),
				typeof(TElement)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(keySelector),
				Expression.Quote(elementSelector),
				Expression.Constant(comparer)
			}));
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000DA48 File Offset: 0x0000BC48
		public static IQueryable<TResult> GroupBy<TSource, TKey, TElement, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, Expression<Func<TSource, TElement>> elementSelector, Expression<Func<TKey, IEnumerable<TElement>, TResult>> resultSelector)
		{
			Check.GroupBySelectors(source, keySelector, elementSelector, resultSelector);
			return source.Provider.CreateQuery<TResult>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource),
				typeof(TKey),
				typeof(TElement),
				typeof(TResult)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(keySelector),
				Expression.Quote(elementSelector),
				Expression.Quote(resultSelector)
			}));
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000DADC File Offset: 0x0000BCDC
		public static IQueryable<TResult> GroupBy<TSource, TKey, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, Expression<Func<TKey, IEnumerable<TSource>, TResult>> resultSelector, IEqualityComparer<TKey> comparer)
		{
			Check.SourceAndKeyResultSelectors(source, keySelector, resultSelector);
			return source.Provider.CreateQuery<TResult>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource),
				typeof(TKey),
				typeof(TResult)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(keySelector),
				Expression.Quote(resultSelector),
				Expression.Constant(comparer)
			}));
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000DB64 File Offset: 0x0000BD64
		public static IQueryable<TResult> GroupBy<TSource, TKey, TElement, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, Expression<Func<TSource, TElement>> elementSelector, Expression<Func<TKey, IEnumerable<TElement>, TResult>> resultSelector, IEqualityComparer<TKey> comparer)
		{
			Check.GroupBySelectors(source, keySelector, elementSelector, resultSelector);
			return source.Provider.CreateQuery<TResult>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource),
				typeof(TKey),
				typeof(TElement),
				typeof(TResult)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(keySelector),
				Expression.Quote(elementSelector),
				Expression.Quote(resultSelector),
				Expression.Constant(comparer)
			}));
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000DC04 File Offset: 0x0000BE04
		public static IQueryable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(this IQueryable<TOuter> outer, IEnumerable<TInner> inner, Expression<Func<TOuter, TKey>> outerKeySelector, Expression<Func<TInner, TKey>> innerKeySelector, Expression<Func<TOuter, IEnumerable<TInner>, TResult>> resultSelector)
		{
			if (outer == null)
			{
				throw new ArgumentNullException("outer");
			}
			if (inner == null)
			{
				throw new ArgumentNullException("inner");
			}
			if (outerKeySelector == null)
			{
				throw new ArgumentNullException("outerKeySelector");
			}
			if (innerKeySelector == null)
			{
				throw new ArgumentNullException("innerKeySelector");
			}
			if (resultSelector == null)
			{
				throw new ArgumentNullException("resultSelector");
			}
			return outer.Provider.CreateQuery<TResult>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TOuter),
				typeof(TInner),
				typeof(TKey),
				typeof(TResult)
			}), new Expression[]
			{
				outer.Expression,
				Expression.Constant(inner),
				Expression.Quote(outerKeySelector),
				Expression.Quote(innerKeySelector),
				Expression.Quote(resultSelector)
			}));
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000DCF0 File Offset: 0x0000BEF0
		public static IQueryable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(this IQueryable<TOuter> outer, IEnumerable<TInner> inner, Expression<Func<TOuter, TKey>> outerKeySelector, Expression<Func<TInner, TKey>> innerKeySelector, Expression<Func<TOuter, IEnumerable<TInner>, TResult>> resultSelector, IEqualityComparer<TKey> comparer)
		{
			if (outer == null)
			{
				throw new ArgumentNullException("outer");
			}
			if (inner == null)
			{
				throw new ArgumentNullException("inner");
			}
			if (outerKeySelector == null)
			{
				throw new ArgumentNullException("outerKeySelector");
			}
			if (innerKeySelector == null)
			{
				throw new ArgumentNullException("innerKeySelector");
			}
			if (resultSelector == null)
			{
				throw new ArgumentNullException("resultSelector");
			}
			return outer.Provider.CreateQuery<TResult>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TOuter),
				typeof(TInner),
				typeof(TKey),
				typeof(TResult)
			}), new Expression[]
			{
				outer.Expression,
				Expression.Constant(inner),
				Expression.Quote(outerKeySelector),
				Expression.Quote(innerKeySelector),
				Expression.Quote(resultSelector),
				Expression.Constant(comparer)
			}));
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000DDE8 File Offset: 0x0000BFE8
		public static IQueryable<TSource> Intersect<TSource>(this IQueryable<TSource> source1, IEnumerable<TSource> source2)
		{
			Check.Source1AndSource2(source1, source2);
			return source1.Provider.CreateQuery<TSource>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source1.Expression,
				Expression.Constant(source2)
			}));
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000DE44 File Offset: 0x0000C044
		public static IQueryable<TSource> Intersect<TSource>(this IQueryable<TSource> source1, IEnumerable<TSource> source2, IEqualityComparer<TSource> comparer)
		{
			Check.Source1AndSource2(source1, source2);
			return source1.Provider.CreateQuery<TSource>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source1.Expression,
				Expression.Constant(source2),
				Expression.Constant(comparer)
			}));
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000DEA8 File Offset: 0x0000C0A8
		public static IQueryable<TResult> Join<TOuter, TInner, TKey, TResult>(this IQueryable<TOuter> outer, IEnumerable<TInner> inner, Expression<Func<TOuter, TKey>> outerKeySelector, Expression<Func<TInner, TKey>> innerKeySelector, Expression<Func<TOuter, TInner, TResult>> resultSelector)
		{
			Check.JoinSelectors(outer, inner, outerKeySelector, innerKeySelector, resultSelector);
			return outer.Provider.CreateQuery<TResult>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TOuter),
				typeof(TInner),
				typeof(TKey),
				typeof(TResult)
			}), new Expression[]
			{
				outer.Expression,
				Expression.Constant(inner),
				Expression.Quote(outerKeySelector),
				Expression.Quote(innerKeySelector),
				Expression.Quote(resultSelector)
			}));
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000DF48 File Offset: 0x0000C148
		public static IQueryable<TResult> Join<TOuter, TInner, TKey, TResult>(this IQueryable<TOuter> outer, IEnumerable<TInner> inner, Expression<Func<TOuter, TKey>> outerKeySelector, Expression<Func<TInner, TKey>> innerKeySelector, Expression<Func<TOuter, TInner, TResult>> resultSelector, IEqualityComparer<TKey> comparer)
		{
			Check.JoinSelectors(outer, inner, outerKeySelector, innerKeySelector, resultSelector);
			return outer.Provider.CreateQuery<TResult>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TOuter),
				typeof(TInner),
				typeof(TKey),
				typeof(TResult)
			}), new Expression[]
			{
				outer.Expression,
				Expression.Constant(inner),
				Expression.Quote(outerKeySelector),
				Expression.Quote(innerKeySelector),
				Expression.Quote(resultSelector),
				Expression.Constant(comparer)
			}));
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000DFF4 File Offset: 0x0000C1F4
		public static TSource Last<TSource>(this IQueryable<TSource> source)
		{
			Check.Source(source);
			return source.Provider.Execute<TSource>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000E044 File Offset: 0x0000C244
		public static TSource Last<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			Check.SourceAndPredicate(source, predicate);
			return source.Provider.Execute<TSource>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(predicate)
			}));
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000E0A0 File Offset: 0x0000C2A0
		public static TSource LastOrDefault<TSource>(this IQueryable<TSource> source)
		{
			Check.Source(source);
			return source.Provider.Execute<TSource>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000E0F0 File Offset: 0x0000C2F0
		public static TSource LastOrDefault<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			Check.SourceAndPredicate(source, predicate);
			return source.Provider.Execute<TSource>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(predicate)
			}));
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000E14C File Offset: 0x0000C34C
		public static long LongCount<TSource>(this IQueryable<TSource> source)
		{
			Check.Source(source);
			return source.Execute(MethodBase.GetCurrentMethod());
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000E160 File Offset: 0x0000C360
		public static long LongCount<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			Check.SourceAndPredicate(source, predicate);
			return source.Provider.Execute<long>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(predicate)
			}));
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000E1BC File Offset: 0x0000C3BC
		public static TSource Max<TSource>(this IQueryable<TSource> source)
		{
			Check.Source(source);
			return source.Provider.Execute<TSource>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000E20C File Offset: 0x0000C40C
		public static TResult Max<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector)
		{
			Check.Source(source);
			return source.Provider.Execute<TResult>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource),
				typeof(TResult)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(selector)
			}));
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000E274 File Offset: 0x0000C474
		public static TSource Min<TSource>(this IQueryable<TSource> source)
		{
			Check.Source(source);
			return source.Provider.Execute<TSource>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000E2C4 File Offset: 0x0000C4C4
		public static TResult Min<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector)
		{
			Check.SourceAndSelector(source, selector);
			return source.Provider.Execute<TResult>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource),
				typeof(TResult)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(selector)
			}));
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000E32C File Offset: 0x0000C52C
		public static IQueryable<TResult> OfType<TResult>(this IQueryable source)
		{
			Check.Source(source);
			return (IQueryable<TResult>)source.Provider.CreateQuery(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TResult)
			}), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000E380 File Offset: 0x0000C580
		public static IOrderedQueryable<TSource> OrderBy<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector)
		{
			Check.SourceAndKeySelector(source, keySelector);
			return (IOrderedQueryable<TSource>)source.Provider.CreateQuery<TSource>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource),
				typeof(TKey)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(keySelector)
			}));
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000E3EC File Offset: 0x0000C5EC
		public static IOrderedQueryable<TSource> OrderBy<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, IComparer<TKey> comparer)
		{
			Check.SourceAndKeySelector(source, keySelector);
			return (IOrderedQueryable<TSource>)source.Provider.CreateQuery<TSource>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource),
				typeof(TKey)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(keySelector),
				Expression.Constant(comparer)
			}));
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000E460 File Offset: 0x0000C660
		public static IOrderedQueryable<TSource> OrderByDescending<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector)
		{
			Check.SourceAndKeySelector(source, keySelector);
			return (IOrderedQueryable<TSource>)source.Provider.CreateQuery<TSource>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource),
				typeof(TKey)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(keySelector)
			}));
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000E4CC File Offset: 0x0000C6CC
		public static IOrderedQueryable<TSource> OrderByDescending<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, IComparer<TKey> comparer)
		{
			Check.SourceAndKeySelector(source, keySelector);
			return (IOrderedQueryable<TSource>)source.Provider.CreateQuery<TSource>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource),
				typeof(TKey)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(keySelector),
				Expression.Constant(comparer)
			}));
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000E540 File Offset: 0x0000C740
		public static IQueryable<TSource> Reverse<TSource>(this IQueryable<TSource> source)
		{
			Check.Source(source);
			return source.Provider.CreateQuery<TSource>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000E590 File Offset: 0x0000C790
		public static IQueryable<TResult> Select<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector)
		{
			Check.SourceAndSelector(source, selector);
			return source.Provider.CreateQuery<TResult>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource),
				typeof(TResult)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(selector)
			}));
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000E5F8 File Offset: 0x0000C7F8
		public static IQueryable<TResult> Select<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, int, TResult>> selector)
		{
			Check.SourceAndSelector(source, selector);
			return source.Provider.CreateQuery<TResult>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource),
				typeof(TResult)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(selector)
			}));
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000E660 File Offset: 0x0000C860
		public static IQueryable<TResult> SelectMany<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, IEnumerable<TResult>>> selector)
		{
			Check.SourceAndSelector(source, selector);
			return source.Provider.CreateQuery<TResult>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource),
				typeof(TResult)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(selector)
			}));
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000E6C8 File Offset: 0x0000C8C8
		public static IQueryable<TResult> SelectMany<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, int, IEnumerable<TResult>>> selector)
		{
			Check.SourceAndSelector(source, selector);
			return source.Provider.CreateQuery<TResult>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource),
				typeof(TResult)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(selector)
			}));
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000E730 File Offset: 0x0000C930
		public static IQueryable<TResult> SelectMany<TSource, TCollection, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, int, IEnumerable<TCollection>>> collectionSelector, Expression<Func<TSource, TCollection, TResult>> resultSelector)
		{
			Check.SourceAndCollectionSelectorAndResultSelector(source, collectionSelector, resultSelector);
			return source.Provider.CreateQuery<TResult>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource),
				typeof(TCollection),
				typeof(TResult)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(collectionSelector),
				Expression.Quote(resultSelector)
			}));
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000E7B0 File Offset: 0x0000C9B0
		public static IQueryable<TResult> SelectMany<TSource, TCollection, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, IEnumerable<TCollection>>> collectionSelector, Expression<Func<TSource, TCollection, TResult>> resultSelector)
		{
			Check.SourceAndCollectionSelectorAndResultSelector(source, collectionSelector, resultSelector);
			return source.Provider.CreateQuery<TResult>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource),
				typeof(TCollection),
				typeof(TResult)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(collectionSelector),
				Expression.Quote(resultSelector)
			}));
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000E830 File Offset: 0x0000CA30
		public static bool SequenceEqual<TSource>(this IQueryable<TSource> source1, IEnumerable<TSource> source2)
		{
			Check.Source1AndSource2(source1, source2);
			return source1.Provider.Execute<bool>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source1.Expression,
				Expression.Constant(source2)
			}));
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000E88C File Offset: 0x0000CA8C
		public static bool SequenceEqual<TSource>(this IQueryable<TSource> source1, IEnumerable<TSource> source2, IEqualityComparer<TSource> comparer)
		{
			Check.Source1AndSource2(source1, source2);
			return source1.Provider.Execute<bool>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source1.Expression,
				Expression.Constant(source2),
				Expression.Constant(comparer)
			}));
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000E8F0 File Offset: 0x0000CAF0
		public static TSource Single<TSource>(this IQueryable<TSource> source)
		{
			Check.Source(source);
			return source.Provider.Execute<TSource>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000E940 File Offset: 0x0000CB40
		public static TSource Single<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			Check.SourceAndPredicate(source, predicate);
			return source.Provider.Execute<TSource>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(predicate)
			}));
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000E99C File Offset: 0x0000CB9C
		public static TSource SingleOrDefault<TSource>(this IQueryable<TSource> source)
		{
			Check.Source(source);
			return source.Provider.Execute<TSource>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000E9EC File Offset: 0x0000CBEC
		public static TSource SingleOrDefault<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			Check.SourceAndPredicate(source, predicate);
			return source.Provider.Execute<TSource>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(predicate)
			}));
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000EA48 File Offset: 0x0000CC48
		public static IQueryable<TSource> Skip<TSource>(this IQueryable<TSource> source, int count)
		{
			Check.Source(source);
			return source.Provider.CreateQuery<TSource>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression,
				Expression.Constant(count)
			}));
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000EAA8 File Offset: 0x0000CCA8
		public static IQueryable<TSource> SkipWhile<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			Check.SourceAndPredicate(source, predicate);
			return source.Provider.CreateQuery<TSource>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(predicate)
			}));
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000EB04 File Offset: 0x0000CD04
		public static IQueryable<TSource> SkipWhile<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int, bool>> predicate)
		{
			Check.SourceAndPredicate(source, predicate);
			return source.Provider.CreateQuery<TSource>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(predicate)
			}));
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000EB60 File Offset: 0x0000CD60
		public static int Sum<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int>> selector)
		{
			Check.SourceAndSelector(source, selector);
			return source.Provider.Execute<int>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(selector)
			}));
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000EBBC File Offset: 0x0000CDBC
		public static int? Sum<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int?>> selector)
		{
			Check.SourceAndSelector(source, selector);
			return source.Provider.Execute<int?>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(selector)
			}));
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000EC18 File Offset: 0x0000CE18
		public static long Sum<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, long>> selector)
		{
			Check.SourceAndSelector(source, selector);
			return source.Provider.Execute<long>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(selector)
			}));
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000EC74 File Offset: 0x0000CE74
		public static long? Sum<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, long?>> selector)
		{
			Check.SourceAndSelector(source, selector);
			return source.Provider.Execute<long?>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(selector)
			}));
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000ECD0 File Offset: 0x0000CED0
		public static float Sum<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, float>> selector)
		{
			Check.SourceAndSelector(source, selector);
			return source.Provider.Execute<float>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(selector)
			}));
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000ED2C File Offset: 0x0000CF2C
		public static float? Sum<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, float?>> selector)
		{
			Check.SourceAndSelector(source, selector);
			return source.Provider.Execute<float?>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(selector)
			}));
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000ED88 File Offset: 0x0000CF88
		public static double Sum<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, double>> selector)
		{
			Check.SourceAndSelector(source, selector);
			return source.Provider.Execute<double>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(selector)
			}));
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000EDE4 File Offset: 0x0000CFE4
		public static double? Sum<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, double?>> selector)
		{
			Check.SourceAndSelector(source, selector);
			return source.Provider.Execute<double?>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(selector)
			}));
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000EE40 File Offset: 0x0000D040
		public static decimal Sum<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, decimal>> selector)
		{
			Check.SourceAndSelector(source, selector);
			return source.Provider.Execute<decimal>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(selector)
			}));
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000EE9C File Offset: 0x0000D09C
		public static decimal? Sum<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, decimal?>> selector)
		{
			Check.SourceAndSelector(source, selector);
			return source.Provider.Execute<decimal?>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(selector)
			}));
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000EEF8 File Offset: 0x0000D0F8
		public static int Sum(this IQueryable<int> source)
		{
			Check.Source(source);
			return source.Provider.Execute<int>(Queryable.StaticCall((MethodInfo)MethodBase.GetCurrentMethod(), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000EF34 File Offset: 0x0000D134
		public static int? Sum(this IQueryable<int?> source)
		{
			Check.Source(source);
			return source.Provider.Execute<int?>(Queryable.StaticCall((MethodInfo)MethodBase.GetCurrentMethod(), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000EF70 File Offset: 0x0000D170
		public static long Sum(this IQueryable<long> source)
		{
			Check.Source(source);
			return source.Provider.Execute<long>(Queryable.StaticCall((MethodInfo)MethodBase.GetCurrentMethod(), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000EFAC File Offset: 0x0000D1AC
		public static long? Sum(this IQueryable<long?> source)
		{
			Check.Source(source);
			return source.Provider.Execute<long?>(Queryable.StaticCall((MethodInfo)MethodBase.GetCurrentMethod(), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000EFE8 File Offset: 0x0000D1E8
		public static float Sum(this IQueryable<float> source)
		{
			Check.Source(source);
			return source.Provider.Execute<float>(Queryable.StaticCall((MethodInfo)MethodBase.GetCurrentMethod(), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000F024 File Offset: 0x0000D224
		public static float? Sum(this IQueryable<float?> source)
		{
			Check.Source(source);
			return source.Provider.Execute<float?>(Queryable.StaticCall((MethodInfo)MethodBase.GetCurrentMethod(), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000F060 File Offset: 0x0000D260
		public static double Sum(this IQueryable<double> source)
		{
			Check.Source(source);
			return source.Provider.Execute<double>(Queryable.StaticCall((MethodInfo)MethodBase.GetCurrentMethod(), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000F09C File Offset: 0x0000D29C
		public static double? Sum(this IQueryable<double?> source)
		{
			Check.Source(source);
			return source.Provider.Execute<double?>(Queryable.StaticCall((MethodInfo)MethodBase.GetCurrentMethod(), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000F0D8 File Offset: 0x0000D2D8
		public static decimal Sum(this IQueryable<decimal> source)
		{
			Check.Source(source);
			return source.Provider.Execute<decimal>(Queryable.StaticCall((MethodInfo)MethodBase.GetCurrentMethod(), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000F114 File Offset: 0x0000D314
		public static decimal? Sum(this IQueryable<decimal?> source)
		{
			Check.Source(source);
			return source.Provider.Execute<decimal?>(Queryable.StaticCall((MethodInfo)MethodBase.GetCurrentMethod(), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000F150 File Offset: 0x0000D350
		public static IQueryable<TSource> Take<TSource>(this IQueryable<TSource> source, int count)
		{
			Check.Source(source);
			return source.Provider.CreateQuery<TSource>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression,
				Expression.Constant(count)
			}));
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000F1B0 File Offset: 0x0000D3B0
		public static IQueryable<TSource> TakeWhile<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			Check.SourceAndPredicate(source, predicate);
			return source.Provider.CreateQuery<TSource>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(predicate)
			}));
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000F20C File Offset: 0x0000D40C
		public static IQueryable<TSource> TakeWhile<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int, bool>> predicate)
		{
			Check.SourceAndPredicate(source, predicate);
			return source.Provider.CreateQuery<TSource>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(predicate)
			}));
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000F268 File Offset: 0x0000D468
		public static IOrderedQueryable<TSource> ThenBy<TSource, TKey>(this IOrderedQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector)
		{
			Check.SourceAndKeySelector(source, keySelector);
			return (IOrderedQueryable<TSource>)source.Provider.CreateQuery(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource),
				typeof(TKey)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(keySelector)
			}));
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000F2D4 File Offset: 0x0000D4D4
		public static IOrderedQueryable<TSource> ThenBy<TSource, TKey>(this IOrderedQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, IComparer<TKey> comparer)
		{
			Check.SourceAndKeySelector(source, keySelector);
			return (IOrderedQueryable<TSource>)source.Provider.CreateQuery(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource),
				typeof(TKey)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(keySelector),
				Expression.Constant(comparer)
			}));
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000F348 File Offset: 0x0000D548
		public static IOrderedQueryable<TSource> ThenByDescending<TSource, TKey>(this IOrderedQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector)
		{
			Check.SourceAndKeySelector(source, keySelector);
			return (IOrderedQueryable<TSource>)source.Provider.CreateQuery(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource),
				typeof(TKey)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(keySelector)
			}));
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000F3B4 File Offset: 0x0000D5B4
		public static IOrderedQueryable<TSource> ThenByDescending<TSource, TKey>(this IOrderedQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, IComparer<TKey> comparer)
		{
			Check.SourceAndKeySelector(source, keySelector);
			return (IOrderedQueryable<TSource>)source.Provider.CreateQuery(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource),
				typeof(TKey)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(keySelector),
				Expression.Constant(comparer)
			}));
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000F428 File Offset: 0x0000D628
		public static IQueryable<TSource> Union<TSource>(this IQueryable<TSource> source1, IEnumerable<TSource> source2)
		{
			Check.Source1AndSource2(source1, source2);
			return source1.Provider.CreateQuery<TSource>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source1.Expression,
				Expression.Constant(source2)
			}));
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000F484 File Offset: 0x0000D684
		public static IQueryable<TSource> Union<TSource>(this IQueryable<TSource> source1, IEnumerable<TSource> source2, IEqualityComparer<TSource> comparer)
		{
			Check.Source1AndSource2(source1, source2);
			return source1.Provider.CreateQuery<TSource>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source1.Expression,
				Expression.Constant(source2),
				Expression.Constant(comparer)
			}));
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000F4E8 File Offset: 0x0000D6E8
		public static IQueryable<TSource> Where<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			Check.SourceAndPredicate(source, predicate);
			return source.Provider.CreateQuery<TSource>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(predicate)
			}));
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000F544 File Offset: 0x0000D744
		public static IQueryable<TSource> Where<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int, bool>> predicate)
		{
			Check.SourceAndPredicate(source, predicate);
			return source.Provider.CreateQuery<TSource>(Queryable.StaticCall(Queryable.MakeGeneric(MethodBase.GetCurrentMethod(), new Type[]
			{
				typeof(TSource)
			}), new Expression[]
			{
				source.Expression,
				Expression.Quote(predicate)
			}));
		}
	}
}
