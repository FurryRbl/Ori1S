using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Linq
{
	// Token: 0x0200001B RID: 27
	public static class Enumerable
	{
		// Token: 0x06000175 RID: 373 RVA: 0x000086E0 File Offset: 0x000068E0
		public static TSource Aggregate<TSource>(this IEnumerable<TSource> source, Func<TSource, TSource, TSource> func)
		{
			Check.SourceAndFunc(source, func);
			TSource result;
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					throw new InvalidOperationException("No elements in source list");
				}
				TSource tsource = enumerator.Current;
				while (enumerator.MoveNext())
				{
					TSource arg = enumerator.Current;
					tsource = func(tsource, arg);
				}
				result = tsource;
			}
			return result;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000876C File Offset: 0x0000696C
		public static TAccumulate Aggregate<TSource, TAccumulate>(this IEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func)
		{
			Check.SourceAndFunc(source, func);
			TAccumulate taccumulate = seed;
			foreach (TSource arg in source)
			{
				taccumulate = func(taccumulate, arg);
			}
			return taccumulate;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000087D8 File Offset: 0x000069D8
		public static TResult Aggregate<TSource, TAccumulate, TResult>(this IEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func, Func<TAccumulate, TResult> resultSelector)
		{
			Check.SourceAndFunc(source, func);
			if (resultSelector == null)
			{
				throw new ArgumentNullException("resultSelector");
			}
			TAccumulate arg = seed;
			foreach (TSource arg2 in source)
			{
				arg = func(arg, arg2);
			}
			return resultSelector(arg);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000885C File Offset: 0x00006A5C
		public static bool All<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			Check.SourceAndPredicate(source, predicate);
			foreach (TSource arg in source)
			{
				if (!predicate(arg))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x000088D0 File Offset: 0x00006AD0
		public static bool Any<TSource>(this IEnumerable<TSource> source)
		{
			Check.Source(source);
			ICollection<TSource> collection = source as ICollection<TSource>;
			if (collection != null)
			{
				return collection.Count > 0;
			}
			bool result;
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				result = enumerator.MoveNext();
			}
			return result;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000893C File Offset: 0x00006B3C
		public static bool Any<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			Check.SourceAndPredicate(source, predicate);
			foreach (TSource arg in source)
			{
				if (predicate(arg))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x000089B0 File Offset: 0x00006BB0
		public static IEnumerable<TSource> AsEnumerable<TSource>(this IEnumerable<TSource> source)
		{
			return source;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x000089B4 File Offset: 0x00006BB4
		public static double Average(this IEnumerable<int> source)
		{
			return source.Average((long a, int b) => a + (long)b, (long a, long b) => (double)a / (double)b);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00008A04 File Offset: 0x00006C04
		public static double Average(this IEnumerable<long> source)
		{
			return source.Average((long a, long b) => a + b, (long a, long b) => (double)a / (double)b);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00008A54 File Offset: 0x00006C54
		public static double Average(this IEnumerable<double> source)
		{
			return source.Average((double a, double b) => a + b, (double a, long b) => a / (double)b);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00008AA4 File Offset: 0x00006CA4
		public static float Average(this IEnumerable<float> source)
		{
			return source.Average((double a, float b) => a + (double)b, (double a, long b) => (float)a / (float)b);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00008AF4 File Offset: 0x00006CF4
		public static decimal Average(this IEnumerable<decimal> source)
		{
			return source.Average((decimal a, decimal b) => a + b, (decimal a, long b) => a / b);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00008B44 File Offset: 0x00006D44
		private static TResult Average<TElement, TAggregate, TResult>(this IEnumerable<TElement> source, Func<TAggregate, TElement, TAggregate> func, Func<TAggregate, long, TResult> result) where TElement : struct where TAggregate : struct where TResult : struct
		{
			Check.Source(source);
			TAggregate arg = default(TAggregate);
			long num = 0L;
			foreach (TElement arg2 in source)
			{
				arg = func(arg, arg2);
				num += 1L;
			}
			if (num == 0L)
			{
				throw new InvalidOperationException();
			}
			return result(arg, num);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00008BD4 File Offset: 0x00006DD4
		private static TResult? AverageNullable<TElement, TAggregate, TResult>(this IEnumerable<TElement?> source, Func<TAggregate, TElement, TAggregate> func, Func<TAggregate, long, TResult> result) where TElement : struct where TAggregate : struct where TResult : struct
		{
			Check.Source(source);
			TAggregate arg = default(TAggregate);
			long num = 0L;
			foreach (TElement? telement in source)
			{
				if (telement != null)
				{
					arg = func(arg, telement.Value);
					num += 1L;
				}
			}
			if (num == 0L)
			{
				return null;
			}
			return new TResult?(result(arg, num));
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00008C84 File Offset: 0x00006E84
		public static double? Average(this IEnumerable<int?> source)
		{
			Check.Source(source);
			return source.AverageNullable((long a, int b) => a + (long)b, (long a, long b) => (double)a / (double)b);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00008CD8 File Offset: 0x00006ED8
		public static double? Average(this IEnumerable<long?> source)
		{
			Check.Source(source);
			return source.AverageNullable((long a, long b) => a + b, (long a, long b) => (double)a / (double)b);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00008D2C File Offset: 0x00006F2C
		public static double? Average(this IEnumerable<double?> source)
		{
			Check.Source(source);
			return source.AverageNullable((double a, double b) => a + b, (double a, long b) => a / (double)b);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00008D80 File Offset: 0x00006F80
		public static decimal? Average(this IEnumerable<decimal?> source)
		{
			Check.Source(source);
			return source.AverageNullable((decimal a, decimal b) => a + b, (decimal a, long b) => a / b);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00008DD4 File Offset: 0x00006FD4
		public static float? Average(this IEnumerable<float?> source)
		{
			Check.Source(source);
			return source.AverageNullable((double a, float b) => a + (double)b, (double a, long b) => (float)a / (float)b);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00008E28 File Offset: 0x00007028
		public static double Average<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
		{
			Check.SourceAndSelector(source, selector);
			return source.Select(selector).Average((long a, int b) => a + (long)b, (long a, long b) => (double)a / (double)b);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00008E60 File Offset: 0x00007060
		public static double? Average<TSource>(this IEnumerable<TSource> source, Func<TSource, int?> selector)
		{
			Check.SourceAndSelector(source, selector);
			return source.Select(selector).AverageNullable((long a, int b) => a + (long)b, (long a, long b) => (double)a / (double)b);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00008E98 File Offset: 0x00007098
		public static double Average<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector)
		{
			Check.SourceAndSelector(source, selector);
			return source.Select(selector).Average((long a, long b) => a + b, (long a, long b) => (double)a / (double)b);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00008ED0 File Offset: 0x000070D0
		public static double? Average<TSource>(this IEnumerable<TSource> source, Func<TSource, long?> selector)
		{
			Check.SourceAndSelector(source, selector);
			return source.Select(selector).AverageNullable((long a, long b) => a + b, (long a, long b) => (double)a / (double)b);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00008F08 File Offset: 0x00007108
		public static double Average<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
		{
			Check.SourceAndSelector(source, selector);
			return source.Select(selector).Average((double a, double b) => a + b, (double a, long b) => a / (double)b);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00008F40 File Offset: 0x00007140
		public static double? Average<TSource>(this IEnumerable<TSource> source, Func<TSource, double?> selector)
		{
			Check.SourceAndSelector(source, selector);
			return source.Select(selector).AverageNullable((double a, double b) => a + b, (double a, long b) => a / (double)b);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00008F78 File Offset: 0x00007178
		public static float Average<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector)
		{
			Check.SourceAndSelector(source, selector);
			return source.Select(selector).Average((double a, float b) => a + (double)b, (double a, long b) => (float)a / (float)b);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00008FB0 File Offset: 0x000071B0
		public static float? Average<TSource>(this IEnumerable<TSource> source, Func<TSource, float?> selector)
		{
			Check.SourceAndSelector(source, selector);
			return source.Select(selector).AverageNullable((double a, float b) => a + (double)b, (double a, long b) => (float)a / (float)b);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00008FE8 File Offset: 0x000071E8
		public static decimal Average<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector)
		{
			Check.SourceAndSelector(source, selector);
			return source.Select(selector).Average((decimal a, decimal b) => a + b, (decimal a, long b) => a / b);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00009020 File Offset: 0x00007220
		public static decimal? Average<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal?> selector)
		{
			Check.SourceAndSelector(source, selector);
			return source.Select(selector).AverageNullable((decimal a, decimal b) => a + b, (decimal a, long b) => a / b);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00009058 File Offset: 0x00007258
		public static IEnumerable<TResult> Cast<TResult>(this IEnumerable source)
		{
			Check.Source(source);
			IEnumerable<TResult> enumerable = source as IEnumerable<TResult>;
			if (enumerable != null)
			{
				return enumerable;
			}
			return Enumerable.CreateCastIterator<TResult>(source);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00009080 File Offset: 0x00007280
		private static IEnumerable<TResult> CreateCastIterator<TResult>(IEnumerable source)
		{
			foreach (object obj in source)
			{
				TResult element = (TResult)((object)obj);
				yield return element;
			}
			yield break;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x000090AC File Offset: 0x000072AC
		public static IEnumerable<TSource> Concat<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
		{
			Check.FirstAndSecond(first, second);
			return Enumerable.CreateConcatIterator<TSource>(first, second);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x000090BC File Offset: 0x000072BC
		private static IEnumerable<TSource> CreateConcatIterator<TSource>(IEnumerable<TSource> first, IEnumerable<TSource> second)
		{
			foreach (TSource element in first)
			{
				yield return element;
			}
			foreach (TSource element2 in second)
			{
				yield return element2;
			}
			yield break;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x000090F4 File Offset: 0x000072F4
		public static bool Contains<TSource>(this IEnumerable<TSource> source, TSource value)
		{
			ICollection<TSource> collection = source as ICollection<TSource>;
			if (collection != null)
			{
				return collection.Contains(value);
			}
			return source.Contains(value, null);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00009120 File Offset: 0x00007320
		public static bool Contains<TSource>(this IEnumerable<TSource> source, TSource value, IEqualityComparer<TSource> comparer)
		{
			Check.Source(source);
			if (comparer == null)
			{
				comparer = EqualityComparer<TSource>.Default;
			}
			foreach (TSource x in source)
			{
				if (comparer.Equals(x, value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x000091A4 File Offset: 0x000073A4
		public static int Count<TSource>(this IEnumerable<TSource> source)
		{
			Check.Source(source);
			ICollection<TSource> collection = source as ICollection<TSource>;
			if (collection != null)
			{
				return collection.Count;
			}
			int num = 0;
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00009218 File Offset: 0x00007418
		public static int Count<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> selector)
		{
			Check.SourceAndSelector(source, selector);
			int num = 0;
			foreach (TSource arg in source)
			{
				if (selector(arg))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000928C File Offset: 0x0000748C
		public static IEnumerable<TSource> DefaultIfEmpty<TSource>(this IEnumerable<TSource> source)
		{
			return source.DefaultIfEmpty(default(TSource));
		}

		// Token: 0x0600019B RID: 411 RVA: 0x000092A8 File Offset: 0x000074A8
		public static IEnumerable<TSource> DefaultIfEmpty<TSource>(this IEnumerable<TSource> source, TSource defaultValue)
		{
			Check.Source(source);
			return Enumerable.CreateDefaultIfEmptyIterator<TSource>(source, defaultValue);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x000092B8 File Offset: 0x000074B8
		private static IEnumerable<TSource> CreateDefaultIfEmptyIterator<TSource>(IEnumerable<TSource> source, TSource defaultValue)
		{
			bool empty = true;
			foreach (TSource item in source)
			{
				empty = false;
				yield return item;
			}
			if (empty)
			{
				yield return defaultValue;
			}
			yield break;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x000092F0 File Offset: 0x000074F0
		public static IEnumerable<TSource> Distinct<TSource>(this IEnumerable<TSource> source)
		{
			return source.Distinct(null);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x000092FC File Offset: 0x000074FC
		public static IEnumerable<TSource> Distinct<TSource>(this IEnumerable<TSource> source, IEqualityComparer<TSource> comparer)
		{
			Check.Source(source);
			if (comparer == null)
			{
				comparer = EqualityComparer<TSource>.Default;
			}
			return Enumerable.CreateDistinctIterator<TSource>(source, comparer);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00009318 File Offset: 0x00007518
		private static IEnumerable<TSource> CreateDistinctIterator<TSource>(IEnumerable<TSource> source, IEqualityComparer<TSource> comparer)
		{
			HashSet<TSource> items = new HashSet<TSource>(comparer);
			foreach (TSource element in source)
			{
				if (!items.Contains(element))
				{
					items.Add(element);
					yield return element;
				}
			}
			yield break;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00009350 File Offset: 0x00007550
		private static TSource ElementAt<TSource>(this IEnumerable<TSource> source, int index, Enumerable.Fallback fallback)
		{
			long num = 0L;
			foreach (TSource result in source)
			{
				long num2 = (long)index;
				long num3 = num;
				num = num3 + 1L;
				if (num2 == num3)
				{
					return result;
				}
			}
			if (fallback == Enumerable.Fallback.Throw)
			{
				throw new ArgumentOutOfRangeException();
			}
			return default(TSource);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x000093D8 File Offset: 0x000075D8
		public static TSource ElementAt<TSource>(this IEnumerable<TSource> source, int index)
		{
			Check.Source(source);
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			IList<TSource> list = source as IList<TSource>;
			if (list != null)
			{
				return list[index];
			}
			return source.ElementAt(index, Enumerable.Fallback.Throw);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00009418 File Offset: 0x00007618
		public static TSource ElementAtOrDefault<TSource>(this IEnumerable<TSource> source, int index)
		{
			Check.Source(source);
			if (index < 0)
			{
				return default(TSource);
			}
			IList<TSource> list = source as IList<TSource>;
			if (list != null)
			{
				return (index >= list.Count) ? default(TSource) : list[index];
			}
			return source.ElementAt(index, Enumerable.Fallback.Default);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00009474 File Offset: 0x00007674
		public static IEnumerable<TResult> Empty<TResult>()
		{
			return new TResult[0];
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000947C File Offset: 0x0000767C
		public static IEnumerable<TSource> Except<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
		{
			return first.Except(second, null);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00009488 File Offset: 0x00007688
		public static IEnumerable<TSource> Except<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
		{
			Check.FirstAndSecond(first, second);
			if (comparer == null)
			{
				comparer = EqualityComparer<TSource>.Default;
			}
			return Enumerable.CreateExceptIterator<TSource>(first, second, comparer);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x000094A8 File Offset: 0x000076A8
		private static IEnumerable<TSource> CreateExceptIterator<TSource>(IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
		{
			HashSet<TSource> items = new HashSet<TSource>(second, comparer);
			foreach (TSource element in first)
			{
				if (!items.Contains(element, comparer))
				{
					yield return element;
				}
			}
			yield break;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x000094F0 File Offset: 0x000076F0
		private static TSource First<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate, Enumerable.Fallback fallback)
		{
			foreach (TSource tsource in source)
			{
				if (predicate(tsource))
				{
					return tsource;
				}
			}
			if (fallback == Enumerable.Fallback.Throw)
			{
				throw new InvalidOperationException();
			}
			return default(TSource);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00009574 File Offset: 0x00007774
		public static TSource First<TSource>(this IEnumerable<TSource> source)
		{
			Check.Source(source);
			IList<TSource> list = source as IList<TSource>;
			if (list == null)
			{
				using (IEnumerator<TSource> enumerator = source.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						return enumerator.Current;
					}
				}
				throw new InvalidOperationException();
			}
			if (list.Count != 0)
			{
				return list[0];
			}
			throw new InvalidOperationException();
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00009600 File Offset: 0x00007800
		public static TSource First<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			Check.SourceAndPredicate(source, predicate);
			return source.First(predicate, Enumerable.Fallback.Throw);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00009614 File Offset: 0x00007814
		public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source)
		{
			Check.Source(source);
			return source.First(Enumerable.PredicateOf<TSource>.Always, Enumerable.Fallback.Default);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00009628 File Offset: 0x00007828
		public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			Check.SourceAndPredicate(source, predicate);
			return source.First(predicate, Enumerable.Fallback.Default);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000963C File Offset: 0x0000783C
		private static List<T> ContainsGroup<K, T>(Dictionary<K, List<T>> items, K key, IEqualityComparer<K> comparer)
		{
			IEqualityComparer<K> equalityComparer = comparer ?? EqualityComparer<K>.Default;
			foreach (KeyValuePair<K, List<T>> keyValuePair in items)
			{
				if (equalityComparer.Equals(keyValuePair.Key, key))
				{
					return keyValuePair.Value;
				}
			}
			return null;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x000096C8 File Offset: 0x000078C8
		public static IEnumerable<IGrouping<TKey, TSource>> GroupBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			return source.GroupBy(keySelector, null);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x000096D4 File Offset: 0x000078D4
		public static IEnumerable<IGrouping<TKey, TSource>> GroupBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			Check.SourceAndKeySelector(source, keySelector);
			return source.CreateGroupByIterator(keySelector, comparer);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x000096E8 File Offset: 0x000078E8
		private static IEnumerable<IGrouping<TKey, TSource>> CreateGroupByIterator<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			Dictionary<TKey, List<TSource>> groups = new Dictionary<TKey, List<TSource>>();
			List<TSource> nullList = new List<TSource>();
			int counter = 0;
			int nullCounter = -1;
			foreach (TSource element in source)
			{
				TKey key = keySelector(element);
				if (key == null)
				{
					nullList.Add(element);
					if (nullCounter == -1)
					{
						nullCounter = counter;
						counter++;
					}
				}
				else
				{
					List<TSource> group = Enumerable.ContainsGroup<TKey, TSource>(groups, key, comparer);
					if (group == null)
					{
						group = new List<TSource>();
						groups.Add(key, group);
						counter++;
					}
					group.Add(element);
				}
			}
			counter = 0;
			foreach (KeyValuePair<TKey, List<TSource>> group2 in groups)
			{
				if (counter == nullCounter)
				{
					yield return new Grouping<TKey, TSource>(default(TKey), nullList);
					counter++;
				}
				yield return new Grouping<TKey, TSource>(group2.Key, group2.Value);
				counter++;
			}
			if (counter == nullCounter)
			{
				yield return new Grouping<TKey, TSource>(default(TKey), nullList);
				counter++;
			}
			yield break;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00009730 File Offset: 0x00007930
		public static IEnumerable<IGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
		{
			return source.GroupBy(keySelector, elementSelector, null);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000973C File Offset: 0x0000793C
		public static IEnumerable<IGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
		{
			Check.SourceAndKeyElementSelectors(source, keySelector, elementSelector);
			return source.CreateGroupByIterator(keySelector, elementSelector, comparer);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00009750 File Offset: 0x00007950
		private static IEnumerable<IGrouping<TKey, TElement>> CreateGroupByIterator<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
		{
			Dictionary<TKey, List<TElement>> groups = new Dictionary<TKey, List<TElement>>();
			List<TElement> nullList = new List<TElement>();
			int counter = 0;
			int nullCounter = -1;
			foreach (TSource item in source)
			{
				TKey key = keySelector(item);
				TElement element = elementSelector(item);
				if (key == null)
				{
					nullList.Add(element);
					if (nullCounter == -1)
					{
						nullCounter = counter;
						counter++;
					}
				}
				else
				{
					List<TElement> group = Enumerable.ContainsGroup<TKey, TElement>(groups, key, comparer);
					if (group == null)
					{
						group = new List<TElement>();
						groups.Add(key, group);
						counter++;
					}
					group.Add(element);
				}
			}
			counter = 0;
			foreach (KeyValuePair<TKey, List<TElement>> group2 in groups)
			{
				if (counter == nullCounter)
				{
					yield return new Grouping<TKey, TElement>(default(TKey), nullList);
					counter++;
				}
				yield return new Grouping<TKey, TElement>(group2.Key, group2.Value);
				counter++;
			}
			if (counter == nullCounter)
			{
				yield return new Grouping<TKey, TElement>(default(TKey), nullList);
				counter++;
			}
			yield break;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x000097A4 File Offset: 0x000079A4
		public static IEnumerable<TResult> GroupBy<TSource, TKey, TElement, TResult>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, Func<TKey, IEnumerable<TElement>, TResult> resultSelector)
		{
			return source.GroupBy(keySelector, elementSelector, resultSelector, null);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x000097B0 File Offset: 0x000079B0
		public static IEnumerable<TResult> GroupBy<TSource, TKey, TElement, TResult>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, Func<TKey, IEnumerable<TElement>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
		{
			Check.GroupBySelectors(source, keySelector, elementSelector, resultSelector);
			return source.CreateGroupByIterator(keySelector, elementSelector, resultSelector, comparer);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x000097C8 File Offset: 0x000079C8
		private static IEnumerable<TResult> CreateGroupByIterator<TSource, TKey, TElement, TResult>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, Func<TKey, IEnumerable<TElement>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
		{
			IEnumerable<IGrouping<TKey, TElement>> groups = source.GroupBy(keySelector, elementSelector, comparer);
			foreach (IGrouping<TKey, TElement> group in groups)
			{
				yield return resultSelector(group.Key, group);
			}
			yield break;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000982C File Offset: 0x00007A2C
		public static IEnumerable<TResult> GroupBy<TSource, TKey, TResult>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TKey, IEnumerable<TSource>, TResult> resultSelector)
		{
			return source.GroupBy(keySelector, resultSelector, null);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00009838 File Offset: 0x00007A38
		public static IEnumerable<TResult> GroupBy<TSource, TKey, TResult>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TKey, IEnumerable<TSource>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
		{
			Check.SourceAndKeyResultSelectors(source, keySelector, resultSelector);
			return source.CreateGroupByIterator(keySelector, resultSelector, comparer);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000984C File Offset: 0x00007A4C
		private static IEnumerable<TResult> CreateGroupByIterator<TSource, TKey, TResult>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TKey, IEnumerable<TSource>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
		{
			IEnumerable<IGrouping<TKey, TSource>> groups = source.GroupBy(keySelector, comparer);
			foreach (IGrouping<TKey, TSource> group in groups)
			{
				yield return resultSelector(group.Key, group);
			}
			yield break;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x000098A0 File Offset: 0x00007AA0
		public static IEnumerable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, IEnumerable<TInner>, TResult> resultSelector)
		{
			return outer.GroupJoin(inner, outerKeySelector, innerKeySelector, resultSelector, null);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x000098B0 File Offset: 0x00007AB0
		public static IEnumerable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, IEnumerable<TInner>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
		{
			Check.JoinSelectors(outer, inner, outerKeySelector, innerKeySelector, resultSelector);
			if (comparer == null)
			{
				comparer = EqualityComparer<TKey>.Default;
			}
			return outer.CreateGroupJoinIterator(inner, outerKeySelector, innerKeySelector, resultSelector, comparer);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x000098E4 File Offset: 0x00007AE4
		private static IEnumerable<TResult> CreateGroupJoinIterator<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, IEnumerable<TInner>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
		{
			ILookup<TKey, TInner> innerKeys = inner.ToLookup(innerKeySelector, comparer);
			foreach (TOuter element in outer)
			{
				TKey outerKey = outerKeySelector(element);
				if (innerKeys.Contains(outerKey))
				{
					yield return resultSelector(element, innerKeys[outerKey]);
				}
				else
				{
					yield return resultSelector(element, Enumerable.Empty<TInner>());
				}
			}
			yield break;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00009958 File Offset: 0x00007B58
		public static IEnumerable<TSource> Intersect<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
		{
			return first.Intersect(second, null);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00009964 File Offset: 0x00007B64
		public static IEnumerable<TSource> Intersect<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
		{
			Check.FirstAndSecond(first, second);
			if (comparer == null)
			{
				comparer = EqualityComparer<TSource>.Default;
			}
			return Enumerable.CreateIntersectIterator<TSource>(first, second, comparer);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00009984 File Offset: 0x00007B84
		private static IEnumerable<TSource> CreateIntersectIterator<TSource>(IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
		{
			HashSet<TSource> items = new HashSet<TSource>(second, comparer);
			foreach (TSource element in first)
			{
				if (items.Remove(element))
				{
					yield return element;
				}
			}
			yield break;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000099CC File Offset: 0x00007BCC
		public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, IEqualityComparer<TKey> comparer)
		{
			Check.JoinSelectors(outer, inner, outerKeySelector, innerKeySelector, resultSelector);
			if (comparer == null)
			{
				comparer = EqualityComparer<TKey>.Default;
			}
			return outer.CreateJoinIterator(inner, outerKeySelector, innerKeySelector, resultSelector, comparer);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00009A00 File Offset: 0x00007C00
		private static IEnumerable<TResult> CreateJoinIterator<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, IEqualityComparer<TKey> comparer)
		{
			ILookup<TKey, TInner> innerKeys = inner.ToLookup(innerKeySelector, comparer);
			foreach (TOuter element in outer)
			{
				TKey outerKey = outerKeySelector(element);
				if (innerKeys.Contains(outerKey))
				{
					foreach (TInner innerElement in innerKeys[outerKey])
					{
						yield return resultSelector(element, innerElement);
					}
				}
			}
			yield break;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00009A74 File Offset: 0x00007C74
		public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector)
		{
			return outer.Join(inner, outerKeySelector, innerKeySelector, resultSelector, null);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00009A84 File Offset: 0x00007C84
		private static TSource Last<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate, Enumerable.Fallback fallback)
		{
			bool flag = true;
			TSource result = default(TSource);
			foreach (TSource tsource in source)
			{
				if (predicate(tsource))
				{
					result = tsource;
					flag = false;
				}
			}
			if (!flag)
			{
				return result;
			}
			if (fallback == Enumerable.Fallback.Throw)
			{
				throw new InvalidOperationException();
			}
			return result;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00009B14 File Offset: 0x00007D14
		public static TSource Last<TSource>(this IEnumerable<TSource> source)
		{
			Check.Source(source);
			ICollection<TSource> collection = source as ICollection<TSource>;
			if (collection != null && collection.Count == 0)
			{
				throw new InvalidOperationException();
			}
			IList<TSource> list = source as IList<TSource>;
			if (list != null)
			{
				return list[list.Count - 1];
			}
			return source.Last(Enumerable.PredicateOf<TSource>.Always, Enumerable.Fallback.Throw);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00009B70 File Offset: 0x00007D70
		public static TSource Last<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			Check.SourceAndPredicate(source, predicate);
			return source.Last(predicate, Enumerable.Fallback.Throw);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00009B84 File Offset: 0x00007D84
		public static TSource LastOrDefault<TSource>(this IEnumerable<TSource> source)
		{
			Check.Source(source);
			IList<TSource> list = source as IList<TSource>;
			if (list != null)
			{
				return (list.Count <= 0) ? default(TSource) : list[list.Count - 1];
			}
			return source.Last(Enumerable.PredicateOf<TSource>.Always, Enumerable.Fallback.Default);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00009BDC File Offset: 0x00007DDC
		public static TSource LastOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			Check.SourceAndPredicate(source, predicate);
			return source.Last(predicate, Enumerable.Fallback.Default);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00009BF0 File Offset: 0x00007DF0
		public static long LongCount<TSource>(this IEnumerable<TSource> source)
		{
			Check.Source(source);
			TSource[] array = source as TSource[];
			if (array != null)
			{
				return array.LongLength;
			}
			long num = 0L;
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					num += 1L;
				}
			}
			return num;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00009C68 File Offset: 0x00007E68
		public static long LongCount<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> selector)
		{
			Check.SourceAndSelector(source, selector);
			long num = 0L;
			foreach (TSource arg in source)
			{
				if (selector(arg))
				{
					num += 1L;
				}
			}
			return num;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00009CDC File Offset: 0x00007EDC
		public static int Max(this IEnumerable<int> source)
		{
			Check.Source(source);
			return Enumerable.Iterate<int, int>(source, int.MinValue, (int a, int b) => Math.Max(a, b));
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00009D18 File Offset: 0x00007F18
		public static long Max(this IEnumerable<long> source)
		{
			Check.Source(source);
			return Enumerable.Iterate<long, long>(source, long.MinValue, (long a, long b) => Math.Max(a, b));
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00009D58 File Offset: 0x00007F58
		public static double Max(this IEnumerable<double> source)
		{
			Check.Source(source);
			return Enumerable.Iterate<double, double>(source, double.MinValue, (double a, double b) => Math.Max(a, b));
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00009D98 File Offset: 0x00007F98
		public static float Max(this IEnumerable<float> source)
		{
			Check.Source(source);
			return Enumerable.Iterate<float, float>(source, float.MinValue, (float a, float b) => Math.Max(a, b));
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00009DD4 File Offset: 0x00007FD4
		public static decimal Max(this IEnumerable<decimal> source)
		{
			Check.Source(source);
			return Enumerable.Iterate<decimal, decimal>(source, decimal.MinValue, (decimal a, decimal b) => Math.Max(a, b));
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00009E0C File Offset: 0x0000800C
		public static int? Max(this IEnumerable<int?> source)
		{
			Check.Source(source);
			return Enumerable.IterateNullable<int>(source, (int a, int b) => Math.Max(a, b));
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00009E38 File Offset: 0x00008038
		public static long? Max(this IEnumerable<long?> source)
		{
			Check.Source(source);
			return Enumerable.IterateNullable<long>(source, (long a, long b) => Math.Max(a, b));
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00009E64 File Offset: 0x00008064
		public static double? Max(this IEnumerable<double?> source)
		{
			Check.Source(source);
			return Enumerable.IterateNullable<double>(source, (double a, double b) => Math.Max(a, b));
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00009E90 File Offset: 0x00008090
		public static float? Max(this IEnumerable<float?> source)
		{
			Check.Source(source);
			return Enumerable.IterateNullable<float>(source, (float a, float b) => Math.Max(a, b));
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00009EBC File Offset: 0x000080BC
		public static decimal? Max(this IEnumerable<decimal?> source)
		{
			Check.Source(source);
			return Enumerable.IterateNullable<decimal>(source, (decimal a, decimal b) => Math.Max(a, b));
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00009EE8 File Offset: 0x000080E8
		private static T? IterateNullable<T>(IEnumerable<T?> source, Func<T, T, T> selector) where T : struct
		{
			bool flag = true;
			T? result = null;
			foreach (T? t in source)
			{
				if (t != null)
				{
					if (result == null)
					{
						result = new T?(t.Value);
					}
					else
					{
						result = new T?(selector(t.Value, result.Value));
					}
					flag = false;
				}
			}
			if (flag)
			{
				return null;
			}
			return result;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00009FAC File Offset: 0x000081AC
		private static TRet? IterateNullable<TSource, TRet>(IEnumerable<TSource> source, Func<TSource, TRet?> source_selector, Func<TRet?, TRet?, bool> selector) where TRet : struct
		{
			bool flag = true;
			TRet? tret = null;
			foreach (TSource arg in source)
			{
				TRet? tret2 = source_selector(arg);
				if (tret == null)
				{
					tret = tret2;
				}
				else if (selector(tret2, tret))
				{
					tret = tret2;
				}
				flag = false;
			}
			if (flag)
			{
				return null;
			}
			return tret;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000A054 File Offset: 0x00008254
		private static TSource IterateNullable<TSource>(IEnumerable<TSource> source, Func<TSource, TSource, bool> selector)
		{
			TSource tsource = default(TSource);
			foreach (TSource tsource2 in source)
			{
				if (tsource2 != null)
				{
					if (tsource == null || selector(tsource2, tsource))
					{
						tsource = tsource2;
					}
				}
			}
			return tsource;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000A0E0 File Offset: 0x000082E0
		private static TSource IterateNonNullable<TSource>(IEnumerable<TSource> source, Func<TSource, TSource, bool> selector)
		{
			TSource tsource = default(TSource);
			bool flag = true;
			foreach (TSource tsource2 in source)
			{
				if (flag)
				{
					tsource = tsource2;
					flag = false;
				}
				else if (selector(tsource2, tsource))
				{
					tsource = tsource2;
				}
			}
			if (flag)
			{
				throw new InvalidOperationException();
			}
			return tsource;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000A170 File Offset: 0x00008370
		public static TSource Max<TSource>(this IEnumerable<TSource> source)
		{
			Check.Source(source);
			Comparer<TSource> comparer = Comparer<TSource>.Default;
			Func<TSource, TSource, bool> selector = (TSource a, TSource b) => comparer.Compare(a, b) > 0;
			if (default(TSource) == null)
			{
				return Enumerable.IterateNullable<TSource>(source, selector);
			}
			return Enumerable.IterateNonNullable<TSource>(source, selector);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000A1C4 File Offset: 0x000083C4
		public static int Max<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
		{
			Check.SourceAndSelector(source, selector);
			return Enumerable.Iterate<TSource, int>(source, int.MinValue, (TSource a, int b) => Math.Max(selector(a), b));
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000A204 File Offset: 0x00008404
		public static long Max<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector)
		{
			Check.SourceAndSelector(source, selector);
			return Enumerable.Iterate<TSource, long>(source, long.MinValue, (TSource a, long b) => Math.Max(selector(a), b));
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000A248 File Offset: 0x00008448
		public static double Max<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
		{
			Check.SourceAndSelector(source, selector);
			return Enumerable.Iterate<TSource, double>(source, double.MinValue, (TSource a, double b) => Math.Max(selector(a), b));
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000A28C File Offset: 0x0000848C
		public static float Max<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector)
		{
			Check.SourceAndSelector(source, selector);
			return Enumerable.Iterate<TSource, float>(source, float.MinValue, (TSource a, float b) => Math.Max(selector(a), b));
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000A2CC File Offset: 0x000084CC
		public static decimal Max<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector)
		{
			Check.SourceAndSelector(source, selector);
			return Enumerable.Iterate<TSource, decimal>(source, decimal.MinValue, (TSource a, decimal b) => Math.Max(selector(a), b));
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000A310 File Offset: 0x00008510
		private static U Iterate<T, U>(IEnumerable<T> source, U initValue, Func<T, U, U> selector)
		{
			bool flag = true;
			foreach (T arg in source)
			{
				initValue = selector(arg, initValue);
				flag = false;
			}
			if (flag)
			{
				throw new InvalidOperationException();
			}
			return initValue;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000A384 File Offset: 0x00008584
		public static int? Max<TSource>(this IEnumerable<TSource> source, Func<TSource, int?> selector)
		{
			Check.SourceAndSelector(source, selector);
			return Enumerable.IterateNullable<TSource, int>(source, selector, (int? a, int? b) => a != null && b != null && a.Value > b.Value);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000A3A0 File Offset: 0x000085A0
		public static long? Max<TSource>(this IEnumerable<TSource> source, Func<TSource, long?> selector)
		{
			Check.SourceAndSelector(source, selector);
			return Enumerable.IterateNullable<TSource, long>(source, selector, (long? a, long? b) => a != null && b != null && a.Value > b.Value);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000A3BC File Offset: 0x000085BC
		public static double? Max<TSource>(this IEnumerable<TSource> source, Func<TSource, double?> selector)
		{
			Check.SourceAndSelector(source, selector);
			return Enumerable.IterateNullable<TSource, double>(source, selector, (double? a, double? b) => a != null && b != null && a.Value > b.Value);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000A3D8 File Offset: 0x000085D8
		public static float? Max<TSource>(this IEnumerable<TSource> source, Func<TSource, float?> selector)
		{
			Check.SourceAndSelector(source, selector);
			return Enumerable.IterateNullable<TSource, float>(source, selector, (float? a, float? b) => a != null && b != null && a.Value > b.Value);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000A3F4 File Offset: 0x000085F4
		public static decimal? Max<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal?> selector)
		{
			Check.SourceAndSelector(source, selector);
			return Enumerable.IterateNullable<TSource, decimal>(source, selector, (decimal? a, decimal? b) => a != null && b != null && a.Value > b.Value);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000A410 File Offset: 0x00008610
		public static TResult Max<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
		{
			Check.SourceAndSelector(source, selector);
			return source.Select(selector).Max<TResult>();
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000A428 File Offset: 0x00008628
		public static int Min(this IEnumerable<int> source)
		{
			Check.Source(source);
			return Enumerable.Iterate<int, int>(source, int.MaxValue, (int a, int b) => Math.Min(a, b));
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000A464 File Offset: 0x00008664
		public static long Min(this IEnumerable<long> source)
		{
			Check.Source(source);
			return Enumerable.Iterate<long, long>(source, long.MaxValue, (long a, long b) => Math.Min(a, b));
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000A4A4 File Offset: 0x000086A4
		public static double Min(this IEnumerable<double> source)
		{
			Check.Source(source);
			return Enumerable.Iterate<double, double>(source, double.MaxValue, (double a, double b) => Math.Min(a, b));
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000A4E4 File Offset: 0x000086E4
		public static float Min(this IEnumerable<float> source)
		{
			Check.Source(source);
			return Enumerable.Iterate<float, float>(source, float.MaxValue, (float a, float b) => Math.Min(a, b));
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000A520 File Offset: 0x00008720
		public static decimal Min(this IEnumerable<decimal> source)
		{
			Check.Source(source);
			return Enumerable.Iterate<decimal, decimal>(source, decimal.MaxValue, (decimal a, decimal b) => Math.Min(a, b));
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000A558 File Offset: 0x00008758
		public static int? Min(this IEnumerable<int?> source)
		{
			Check.Source(source);
			return Enumerable.IterateNullable<int>(source, (int a, int b) => Math.Min(a, b));
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000A584 File Offset: 0x00008784
		public static long? Min(this IEnumerable<long?> source)
		{
			Check.Source(source);
			return Enumerable.IterateNullable<long>(source, (long a, long b) => Math.Min(a, b));
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000A5B0 File Offset: 0x000087B0
		public static double? Min(this IEnumerable<double?> source)
		{
			Check.Source(source);
			return Enumerable.IterateNullable<double>(source, (double a, double b) => Math.Min(a, b));
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000A5DC File Offset: 0x000087DC
		public static float? Min(this IEnumerable<float?> source)
		{
			Check.Source(source);
			return Enumerable.IterateNullable<float>(source, (float a, float b) => Math.Min(a, b));
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000A608 File Offset: 0x00008808
		public static decimal? Min(this IEnumerable<decimal?> source)
		{
			Check.Source(source);
			return Enumerable.IterateNullable<decimal>(source, (decimal a, decimal b) => Math.Min(a, b));
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000A634 File Offset: 0x00008834
		public static TSource Min<TSource>(this IEnumerable<TSource> source)
		{
			Check.Source(source);
			Comparer<TSource> comparer = Comparer<TSource>.Default;
			Func<TSource, TSource, bool> selector = (TSource a, TSource b) => comparer.Compare(a, b) < 0;
			if (default(TSource) == null)
			{
				return Enumerable.IterateNullable<TSource>(source, selector);
			}
			return Enumerable.IterateNonNullable<TSource>(source, selector);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000A688 File Offset: 0x00008888
		public static int Min<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
		{
			Check.SourceAndSelector(source, selector);
			return Enumerable.Iterate<TSource, int>(source, int.MaxValue, (TSource a, int b) => Math.Min(selector(a), b));
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000A6C8 File Offset: 0x000088C8
		public static long Min<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector)
		{
			Check.SourceAndSelector(source, selector);
			return Enumerable.Iterate<TSource, long>(source, long.MaxValue, (TSource a, long b) => Math.Min(selector(a), b));
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000A70C File Offset: 0x0000890C
		public static double Min<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
		{
			Check.SourceAndSelector(source, selector);
			return Enumerable.Iterate<TSource, double>(source, double.MaxValue, (TSource a, double b) => Math.Min(selector(a), b));
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000A750 File Offset: 0x00008950
		public static float Min<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector)
		{
			Check.SourceAndSelector(source, selector);
			return Enumerable.Iterate<TSource, float>(source, float.MaxValue, (TSource a, float b) => Math.Min(selector(a), b));
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000A790 File Offset: 0x00008990
		public static decimal Min<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector)
		{
			Check.SourceAndSelector(source, selector);
			return Enumerable.Iterate<TSource, decimal>(source, decimal.MaxValue, (TSource a, decimal b) => Math.Min(selector(a), b));
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000A7D4 File Offset: 0x000089D4
		public static int? Min<TSource>(this IEnumerable<TSource> source, Func<TSource, int?> selector)
		{
			Check.SourceAndSelector(source, selector);
			return Enumerable.IterateNullable<TSource, int>(source, selector, (int? a, int? b) => a != null && b != null && a.Value < b.Value);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000A7F0 File Offset: 0x000089F0
		public static long? Min<TSource>(this IEnumerable<TSource> source, Func<TSource, long?> selector)
		{
			Check.SourceAndSelector(source, selector);
			return Enumerable.IterateNullable<TSource, long>(source, selector, (long? a, long? b) => a != null && b != null && a.Value < b.Value);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000A80C File Offset: 0x00008A0C
		public static float? Min<TSource>(this IEnumerable<TSource> source, Func<TSource, float?> selector)
		{
			Check.SourceAndSelector(source, selector);
			return Enumerable.IterateNullable<TSource, float>(source, selector, (float? a, float? b) => a != null && b != null && a.Value < b.Value);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000A828 File Offset: 0x00008A28
		public static double? Min<TSource>(this IEnumerable<TSource> source, Func<TSource, double?> selector)
		{
			Check.SourceAndSelector(source, selector);
			return Enumerable.IterateNullable<TSource, double>(source, selector, (double? a, double? b) => a != null && b != null && a.Value < b.Value);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000A844 File Offset: 0x00008A44
		public static decimal? Min<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal?> selector)
		{
			Check.SourceAndSelector(source, selector);
			return Enumerable.IterateNullable<TSource, decimal>(source, selector, (decimal? a, decimal? b) => a != null && b != null && a.Value < b.Value);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000A860 File Offset: 0x00008A60
		public static TResult Min<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
		{
			Check.SourceAndSelector(source, selector);
			return source.Select(selector).Min<TResult>();
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000A878 File Offset: 0x00008A78
		public static IEnumerable<TResult> OfType<TResult>(this IEnumerable source)
		{
			Check.Source(source);
			return Enumerable.CreateOfTypeIterator<TResult>(source);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000A888 File Offset: 0x00008A88
		private static IEnumerable<TResult> CreateOfTypeIterator<TResult>(IEnumerable source)
		{
			foreach (object element in source)
			{
				if (element is TResult)
				{
					yield return (TResult)((object)element);
				}
			}
			yield break;
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000A8B4 File Offset: 0x00008AB4
		public static IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			return source.OrderBy(keySelector, null);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000A8C0 File Offset: 0x00008AC0
		public static IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
		{
			Check.SourceAndKeySelector(source, keySelector);
			return new OrderedSequence<TSource, TKey>(source, keySelector, comparer, SortDirection.Ascending);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000A8D4 File Offset: 0x00008AD4
		public static IOrderedEnumerable<TSource> OrderByDescending<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			return source.OrderByDescending(keySelector, null);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000A8E0 File Offset: 0x00008AE0
		public static IOrderedEnumerable<TSource> OrderByDescending<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
		{
			Check.SourceAndKeySelector(source, keySelector);
			return new OrderedSequence<TSource, TKey>(source, keySelector, comparer, SortDirection.Descending);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000A8F4 File Offset: 0x00008AF4
		public static IEnumerable<int> Range(int start, int count)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			long num = (long)start + (long)count - 1L;
			if (num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException();
			}
			return Enumerable.CreateRangeIterator(start, (int)num);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000A938 File Offset: 0x00008B38
		private static IEnumerable<int> CreateRangeIterator(int start, int upto)
		{
			for (int i = start; i <= upto; i++)
			{
				yield return i;
			}
			yield break;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000A970 File Offset: 0x00008B70
		public static IEnumerable<TResult> Repeat<TResult>(TResult element, int count)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			return Enumerable.CreateRepeatIterator<TResult>(element, count);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000A988 File Offset: 0x00008B88
		private static IEnumerable<TResult> CreateRepeatIterator<TResult>(TResult element, int count)
		{
			for (int i = 0; i < count; i++)
			{
				yield return element;
			}
			yield break;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000A9C0 File Offset: 0x00008BC0
		public static IEnumerable<TSource> Reverse<TSource>(this IEnumerable<TSource> source)
		{
			Check.Source(source);
			return Enumerable.CreateReverseIterator<TSource>(source);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000A9D0 File Offset: 0x00008BD0
		private static IEnumerable<TSource> CreateReverseIterator<TSource>(IEnumerable<TSource> source)
		{
			IList<TSource> list = source as IList<TSource>;
			if (list == null)
			{
				list = new List<TSource>(source);
			}
			for (int i = list.Count - 1; i >= 0; i--)
			{
				yield return list[i];
			}
			yield break;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000A9FC File Offset: 0x00008BFC
		public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
		{
			Check.SourceAndSelector(source, selector);
			return Enumerable.CreateSelectIterator<TSource, TResult>(source, selector);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000AA0C File Offset: 0x00008C0C
		private static IEnumerable<TResult> CreateSelectIterator<TSource, TResult>(IEnumerable<TSource> source, Func<TSource, TResult> selector)
		{
			foreach (TSource element in source)
			{
				yield return selector(element);
			}
			yield break;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000AA44 File Offset: 0x00008C44
		public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, int, TResult> selector)
		{
			Check.SourceAndSelector(source, selector);
			return Enumerable.CreateSelectIterator<TSource, TResult>(source, selector);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000AA54 File Offset: 0x00008C54
		private static IEnumerable<TResult> CreateSelectIterator<TSource, TResult>(IEnumerable<TSource> source, Func<TSource, int, TResult> selector)
		{
			int counter = 0;
			foreach (TSource element in source)
			{
				yield return selector(element, counter);
				counter++;
			}
			yield break;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000AA8C File Offset: 0x00008C8C
		public static IEnumerable<TResult> SelectMany<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TResult>> selector)
		{
			Check.SourceAndSelector(source, selector);
			return Enumerable.CreateSelectManyIterator<TSource, TResult>(source, selector);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000AA9C File Offset: 0x00008C9C
		private static IEnumerable<TResult> CreateSelectManyIterator<TSource, TResult>(IEnumerable<TSource> source, Func<TSource, IEnumerable<TResult>> selector)
		{
			foreach (TSource element in source)
			{
				foreach (TResult item in selector(element))
				{
					yield return item;
				}
			}
			yield break;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000AAD4 File Offset: 0x00008CD4
		public static IEnumerable<TResult> SelectMany<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, int, IEnumerable<TResult>> selector)
		{
			Check.SourceAndSelector(source, selector);
			return Enumerable.CreateSelectManyIterator<TSource, TResult>(source, selector);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000AAE4 File Offset: 0x00008CE4
		private static IEnumerable<TResult> CreateSelectManyIterator<TSource, TResult>(IEnumerable<TSource> source, Func<TSource, int, IEnumerable<TResult>> selector)
		{
			int counter = 0;
			foreach (TSource element in source)
			{
				foreach (TResult item in selector(element, counter))
				{
					yield return item;
				}
				counter++;
			}
			yield break;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000AB1C File Offset: 0x00008D1C
		public static IEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> selector)
		{
			Check.SourceAndCollectionSelectors(source, collectionSelector, selector);
			return Enumerable.CreateSelectManyIterator<TSource, TCollection, TResult>(source, collectionSelector, selector);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000AB30 File Offset: 0x00008D30
		private static IEnumerable<TResult> CreateSelectManyIterator<TSource, TCollection, TResult>(IEnumerable<TSource> source, Func<TSource, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> selector)
		{
			foreach (TSource element in source)
			{
				foreach (TCollection collection in collectionSelector(element))
				{
					yield return selector(element, collection);
				}
			}
			yield break;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000AB78 File Offset: 0x00008D78
		public static IEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this IEnumerable<TSource> source, Func<TSource, int, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> selector)
		{
			Check.SourceAndCollectionSelectors(source, collectionSelector, selector);
			return Enumerable.CreateSelectManyIterator<TSource, TCollection, TResult>(source, collectionSelector, selector);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000AB8C File Offset: 0x00008D8C
		private static IEnumerable<TResult> CreateSelectManyIterator<TSource, TCollection, TResult>(IEnumerable<TSource> source, Func<TSource, int, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> selector)
		{
			int counter = 0;
			foreach (TSource element in source)
			{
				TSource arg = element;
				int arg2;
				counter = (arg2 = counter) + 1;
				foreach (TCollection collection in collectionSelector(arg, arg2))
				{
					yield return selector(element, collection);
				}
			}
			yield break;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000ABD4 File Offset: 0x00008DD4
		private static TSource Single<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate, Enumerable.Fallback fallback)
		{
			bool flag = false;
			TSource result = default(TSource);
			foreach (TSource tsource in source)
			{
				if (predicate(tsource))
				{
					if (flag)
					{
						throw new InvalidOperationException();
					}
					flag = true;
					result = tsource;
				}
			}
			if (!flag && fallback == Enumerable.Fallback.Throw)
			{
				throw new InvalidOperationException();
			}
			return result;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000AC70 File Offset: 0x00008E70
		public static TSource Single<TSource>(this IEnumerable<TSource> source)
		{
			Check.Source(source);
			return source.Single(Enumerable.PredicateOf<TSource>.Always, Enumerable.Fallback.Throw);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000AC84 File Offset: 0x00008E84
		public static TSource Single<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			Check.SourceAndPredicate(source, predicate);
			return source.Single(predicate, Enumerable.Fallback.Throw);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000AC98 File Offset: 0x00008E98
		public static TSource SingleOrDefault<TSource>(this IEnumerable<TSource> source)
		{
			Check.Source(source);
			return source.Single(Enumerable.PredicateOf<TSource>.Always, Enumerable.Fallback.Default);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000ACAC File Offset: 0x00008EAC
		public static TSource SingleOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			Check.SourceAndPredicate(source, predicate);
			return source.Single(predicate, Enumerable.Fallback.Default);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000ACC0 File Offset: 0x00008EC0
		public static IEnumerable<TSource> Skip<TSource>(this IEnumerable<TSource> source, int count)
		{
			Check.Source(source);
			return Enumerable.CreateSkipIterator<TSource>(source, count);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000ACD0 File Offset: 0x00008ED0
		private static IEnumerable<TSource> CreateSkipIterator<TSource>(IEnumerable<TSource> source, int count)
		{
			IEnumerator<TSource> enumerator = source.GetEnumerator();
			try
			{
				do
				{
					int num;
					count = (num = count) - 1;
					if (num <= 0)
					{
						goto Block_4;
					}
				}
				while (enumerator.MoveNext());
				yield break;
				Block_4:
				while (enumerator.MoveNext())
				{
					!0 ! = enumerator.Current;
					yield return !;
				}
			}
			finally
			{
				enumerator.Dispose();
			}
			yield break;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000AD08 File Offset: 0x00008F08
		public static IEnumerable<TSource> SkipWhile<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			Check.SourceAndPredicate(source, predicate);
			return Enumerable.CreateSkipWhileIterator<TSource>(source, predicate);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000AD18 File Offset: 0x00008F18
		private static IEnumerable<TSource> CreateSkipWhileIterator<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			bool yield = false;
			foreach (TSource element in source)
			{
				if (yield)
				{
					yield return element;
				}
				else if (!predicate(element))
				{
					yield return element;
					yield = true;
				}
			}
			yield break;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000AD50 File Offset: 0x00008F50
		public static IEnumerable<TSource> SkipWhile<TSource>(this IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
		{
			Check.SourceAndPredicate(source, predicate);
			return Enumerable.CreateSkipWhileIterator<TSource>(source, predicate);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000AD60 File Offset: 0x00008F60
		private static IEnumerable<TSource> CreateSkipWhileIterator<TSource>(IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
		{
			int counter = 0;
			bool yield = false;
			foreach (TSource element in source)
			{
				if (yield)
				{
					yield return element;
				}
				else if (!predicate(element, counter))
				{
					yield return element;
					yield = true;
				}
				counter++;
			}
			yield break;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000AD98 File Offset: 0x00008F98
		public static int Sum(this IEnumerable<int> source)
		{
			Check.Source(source);
			return source.Sum((int a, int b) => checked(a + b));
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000ADC4 File Offset: 0x00008FC4
		public static int? Sum(this IEnumerable<int?> source)
		{
			Check.Source(source);
			return source.SumNullable(new int?(0), (int? total, int? element) => (element == null) ? total : ((total == null || element == null) ? null : new int?(checked(total.Value + element.Value))));
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000ADF8 File Offset: 0x00008FF8
		public static int Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
		{
			Check.SourceAndSelector(source, selector);
			int num = 0;
			checked
			{
				foreach (TSource arg in source)
				{
					num += selector(arg);
				}
				return num;
			}
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000AE64 File Offset: 0x00009064
		public static int? Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, int?> selector)
		{
			Check.SourceAndSelector(source, selector);
			return source.SumNullable(new int?(0), delegate(int? a, TSource b)
			{
				int? num = selector(b);
				return (num == null) ? a : ((a == null) ? null : new int?(checked(a.Value + num.Value)));
			});
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000AEA4 File Offset: 0x000090A4
		public static long Sum(this IEnumerable<long> source)
		{
			Check.Source(source);
			return source.Sum((long a, long b) => checked(a + b));
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000AED0 File Offset: 0x000090D0
		public static long? Sum(this IEnumerable<long?> source)
		{
			Check.Source(source);
			return source.SumNullable(new long?(0L), (long? total, long? element) => (element == null) ? total : ((total == null || element == null) ? null : new long?(checked(total.Value + element.Value))));
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000AF10 File Offset: 0x00009110
		public static long Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector)
		{
			Check.SourceAndSelector(source, selector);
			long num = 0L;
			checked
			{
				foreach (TSource arg in source)
				{
					num += selector(arg);
				}
				return num;
			}
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000AF7C File Offset: 0x0000917C
		public static long? Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, long?> selector)
		{
			Check.SourceAndSelector(source, selector);
			return source.SumNullable(new long?(0L), delegate(long? a, TSource b)
			{
				long? num = selector(b);
				return (num == null) ? a : ((a == null) ? null : new long?(checked(a.Value + num.Value)));
			});
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000AFBC File Offset: 0x000091BC
		public static double Sum(this IEnumerable<double> source)
		{
			Check.Source(source);
			return source.Sum((double a, double b) => a + b);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000AFE8 File Offset: 0x000091E8
		public static double? Sum(this IEnumerable<double?> source)
		{
			Check.Source(source);
			return source.SumNullable(new double?(0.0), (double? total, double? element) => (element == null) ? total : ((total == null || element == null) ? null : new double?(total.Value + element.Value)));
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000B024 File Offset: 0x00009224
		public static double Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
		{
			Check.SourceAndSelector(source, selector);
			double num = 0.0;
			foreach (TSource arg in source)
			{
				num += selector(arg);
			}
			return num;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000B098 File Offset: 0x00009298
		public static double? Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, double?> selector)
		{
			Check.SourceAndSelector(source, selector);
			return source.SumNullable(new double?(0.0), delegate(double? a, TSource b)
			{
				double? num = selector(b);
				return (num == null) ? a : ((a == null) ? null : new double?(a.Value + num.Value));
			});
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000B0E0 File Offset: 0x000092E0
		public static float Sum(this IEnumerable<float> source)
		{
			Check.Source(source);
			return source.Sum((float a, float b) => a + b);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000B10C File Offset: 0x0000930C
		public static float? Sum(this IEnumerable<float?> source)
		{
			Check.Source(source);
			return source.SumNullable(new float?(0f), (float? total, float? element) => (element == null) ? total : ((total == null || element == null) ? null : new float?(total.Value + element.Value)));
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000B144 File Offset: 0x00009344
		public static float Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector)
		{
			Check.SourceAndSelector(source, selector);
			float num = 0f;
			foreach (TSource arg in source)
			{
				num += selector(arg);
			}
			return num;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000B1B4 File Offset: 0x000093B4
		public static float? Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, float?> selector)
		{
			Check.SourceAndSelector(source, selector);
			return source.SumNullable(new float?(0f), delegate(float? a, TSource b)
			{
				float? num = selector(b);
				return (num == null) ? a : ((a == null) ? null : new float?(a.Value + num.Value));
			});
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000B1F8 File Offset: 0x000093F8
		public static decimal Sum(this IEnumerable<decimal> source)
		{
			Check.Source(source);
			return source.Sum((decimal a, decimal b) => a + b);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000B224 File Offset: 0x00009424
		public static decimal? Sum(this IEnumerable<decimal?> source)
		{
			Check.Source(source);
			return source.SumNullable(new decimal?(0m), (decimal? total, decimal? element) => (element == null) ? total : ((total == null || element == null) ? null : new decimal?(total.Value + element.Value)));
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000B268 File Offset: 0x00009468
		public static decimal Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector)
		{
			Check.SourceAndSelector(source, selector);
			decimal num = 0m;
			foreach (TSource arg in source)
			{
				num += selector(arg);
			}
			return num;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000B2DC File Offset: 0x000094DC
		public static decimal? Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal?> selector)
		{
			Check.SourceAndSelector(source, selector);
			return source.SumNullable(new decimal?(0m), delegate(decimal? a, TSource b)
			{
				decimal? num = selector(b);
				return (num == null) ? a : ((a == null) ? null : new decimal?(a.Value + num.Value));
			});
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000B320 File Offset: 0x00009520
		private static TR Sum<TA, TR>(this IEnumerable<TA> source, Func<TR, TA, TR> selector)
		{
			TR tr = default(TR);
			long num = 0L;
			foreach (TA arg in source)
			{
				tr = selector(tr, arg);
				num += 1L;
			}
			return tr;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000B394 File Offset: 0x00009594
		private static TR SumNullable<TA, TR>(this IEnumerable<TA> source, TR zero, Func<TR, TA, TR> selector)
		{
			TR tr = zero;
			foreach (TA arg in source)
			{
				tr = selector(tr, arg);
			}
			return tr;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000B3F8 File Offset: 0x000095F8
		public static IEnumerable<TSource> Take<TSource>(this IEnumerable<TSource> source, int count)
		{
			Check.Source(source);
			return Enumerable.CreateTakeIterator<TSource>(source, count);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000B408 File Offset: 0x00009608
		private static IEnumerable<TSource> CreateTakeIterator<TSource>(IEnumerable<TSource> source, int count)
		{
			if (count <= 0)
			{
				yield break;
			}
			int counter = 0;
			foreach (TSource element in source)
			{
				yield return element;
				if (++counter == count)
				{
					yield break;
				}
			}
			yield break;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000B440 File Offset: 0x00009640
		public static IEnumerable<TSource> TakeWhile<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			Check.SourceAndPredicate(source, predicate);
			return Enumerable.CreateTakeWhileIterator<TSource>(source, predicate);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000B450 File Offset: 0x00009650
		private static IEnumerable<TSource> CreateTakeWhileIterator<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			foreach (TSource element in source)
			{
				if (!predicate(element))
				{
					yield break;
				}
				yield return element;
			}
			yield break;
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000B488 File Offset: 0x00009688
		public static IEnumerable<TSource> TakeWhile<TSource>(this IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
		{
			Check.SourceAndPredicate(source, predicate);
			return Enumerable.CreateTakeWhileIterator<TSource>(source, predicate);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000B498 File Offset: 0x00009698
		private static IEnumerable<TSource> CreateTakeWhileIterator<TSource>(IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
		{
			int counter = 0;
			foreach (TSource element in source)
			{
				if (!predicate(element, counter))
				{
					yield break;
				}
				yield return element;
				counter++;
			}
			yield break;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000B4D0 File Offset: 0x000096D0
		public static IOrderedEnumerable<TSource> ThenBy<TSource, TKey>(this IOrderedEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			return source.ThenBy(keySelector, null);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000B4DC File Offset: 0x000096DC
		public static IOrderedEnumerable<TSource> ThenBy<TSource, TKey>(this IOrderedEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
		{
			Check.SourceAndKeySelector(source, keySelector);
			return source.CreateOrderedEnumerable<TKey>(keySelector, comparer, false);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000B4F0 File Offset: 0x000096F0
		public static IOrderedEnumerable<TSource> ThenByDescending<TSource, TKey>(this IOrderedEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			return source.ThenByDescending(keySelector, null);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000B4FC File Offset: 0x000096FC
		public static IOrderedEnumerable<TSource> ThenByDescending<TSource, TKey>(this IOrderedEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
		{
			Check.SourceAndKeySelector(source, keySelector);
			return source.CreateOrderedEnumerable<TKey>(keySelector, comparer, true);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000B510 File Offset: 0x00009710
		public static TSource[] ToArray<TSource>(this IEnumerable<TSource> source)
		{
			Check.Source(source);
			ICollection<TSource> collection = source as ICollection<TSource>;
			if (collection != null)
			{
				TSource[] array = new TSource[collection.Count];
				collection.CopyTo(array, 0);
				return array;
			}
			return new List<TSource>(source).ToArray();
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000B554 File Offset: 0x00009754
		public static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
		{
			return source.ToDictionary(keySelector, elementSelector, null);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000B560 File Offset: 0x00009760
		public static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
		{
			Check.SourceAndKeyElementSelectors(source, keySelector, elementSelector);
			if (comparer == null)
			{
				comparer = EqualityComparer<TKey>.Default;
			}
			Dictionary<TKey, TElement> dictionary = new Dictionary<TKey, TElement>(comparer);
			foreach (TSource arg in source)
			{
				dictionary.Add(keySelector(arg), elementSelector(arg));
			}
			return dictionary;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000B5E8 File Offset: 0x000097E8
		public static Dictionary<TKey, TSource> ToDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			return source.ToDictionary(keySelector, null);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000B5F4 File Offset: 0x000097F4
		public static Dictionary<TKey, TSource> ToDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			return source.ToDictionary(keySelector, Enumerable.Function<TSource>.Identity, comparer);
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000B604 File Offset: 0x00009804
		public static List<TSource> ToList<TSource>(this IEnumerable<TSource> source)
		{
			Check.Source(source);
			return new List<TSource>(source);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000B614 File Offset: 0x00009814
		public static ILookup<TKey, TSource> ToLookup<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			return source.ToLookup(keySelector, Enumerable.Function<TSource>.Identity, null);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000B624 File Offset: 0x00009824
		public static ILookup<TKey, TSource> ToLookup<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			return source.ToLookup(keySelector, (TSource element) => element, comparer);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000B63C File Offset: 0x0000983C
		public static ILookup<TKey, TElement> ToLookup<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
		{
			return source.ToLookup(keySelector, elementSelector, null);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000B648 File Offset: 0x00009848
		public static ILookup<TKey, TElement> ToLookup<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
		{
			Check.SourceAndKeyElementSelectors(source, keySelector, elementSelector);
			List<TElement> list = null;
			Dictionary<TKey, List<TElement>> dictionary = new Dictionary<TKey, List<TElement>>(comparer ?? EqualityComparer<TKey>.Default);
			foreach (TSource arg in source)
			{
				TKey tkey = keySelector(arg);
				List<TElement> list2;
				if (tkey == null)
				{
					if (list == null)
					{
						list = new List<TElement>();
					}
					list2 = list;
				}
				else if (!dictionary.TryGetValue(tkey, out list2))
				{
					list2 = new List<TElement>();
					dictionary.Add(tkey, list2);
				}
				list2.Add(elementSelector(arg));
			}
			return new Lookup<TKey, TElement>(dictionary, list);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000B71C File Offset: 0x0000991C
		public static bool SequenceEqual<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
		{
			return first.SequenceEqual(second, null);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000B728 File Offset: 0x00009928
		public static bool SequenceEqual<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
		{
			Check.FirstAndSecond(first, second);
			if (comparer == null)
			{
				comparer = EqualityComparer<TSource>.Default;
			}
			bool result;
			using (IEnumerator<TSource> enumerator = first.GetEnumerator())
			{
				using (IEnumerator<TSource> enumerator2 = second.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (!enumerator2.MoveNext())
						{
							return false;
						}
						if (!comparer.Equals(enumerator.Current, enumerator2.Current))
						{
							return false;
						}
					}
					result = !enumerator2.MoveNext();
				}
			}
			return result;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000B800 File Offset: 0x00009A00
		public static IEnumerable<TSource> Union<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
		{
			Check.FirstAndSecond(first, second);
			return first.Union(second, null);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000B814 File Offset: 0x00009A14
		public static IEnumerable<TSource> Union<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
		{
			Check.FirstAndSecond(first, second);
			if (comparer == null)
			{
				comparer = EqualityComparer<TSource>.Default;
			}
			return Enumerable.CreateUnionIterator<TSource>(first, second, comparer);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000B834 File Offset: 0x00009A34
		private static IEnumerable<TSource> CreateUnionIterator<TSource>(IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
		{
			HashSet<TSource> items = new HashSet<TSource>(comparer);
			foreach (TSource element in first)
			{
				if (!items.Contains(element))
				{
					items.Add(element);
					yield return element;
				}
			}
			foreach (TSource element2 in second)
			{
				if (!items.Contains(element2, comparer))
				{
					items.Add(element2);
					yield return element2;
				}
			}
			yield break;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000B87C File Offset: 0x00009A7C
		public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			Check.SourceAndPredicate(source, predicate);
			return Enumerable.CreateWhereIterator<TSource>(source, predicate);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000B88C File Offset: 0x00009A8C
		private static IEnumerable<TSource> CreateWhereIterator<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			foreach (TSource element in source)
			{
				if (predicate(element))
				{
					yield return element;
				}
			}
			yield break;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000B8C4 File Offset: 0x00009AC4
		public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
		{
			Check.SourceAndPredicate(source, predicate);
			return source.CreateWhereIterator(predicate);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000B8D4 File Offset: 0x00009AD4
		private static IEnumerable<TSource> CreateWhereIterator<TSource>(this IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
		{
			int counter = 0;
			foreach (TSource element in source)
			{
				if (predicate(element, counter))
				{
					yield return element;
				}
				counter++;
			}
			yield break;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000B90C File Offset: 0x00009B0C
		internal static ReadOnlyCollection<TSource> ToReadOnlyCollection<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				return Enumerable.ReadOnlyCollectionOf<TSource>.Empty;
			}
			ReadOnlyCollection<TSource> readOnlyCollection = source as ReadOnlyCollection<TSource>;
			if (readOnlyCollection != null)
			{
				return readOnlyCollection;
			}
			return new ReadOnlyCollection<TSource>(source.ToArray<TSource>());
		}

		// Token: 0x0200001C RID: 28
		private enum Fallback
		{
			// Token: 0x04000096 RID: 150
			Default,
			// Token: 0x04000097 RID: 151
			Throw
		}

		// Token: 0x0200001D RID: 29
		private class PredicateOf<T>
		{
			// Token: 0x04000098 RID: 152
			public static readonly Func<T, bool> Always = (T t) => true;
		}

		// Token: 0x0200001E RID: 30
		private class Function<T>
		{
			// Token: 0x0400009A RID: 154
			public static readonly Func<T, T> Identity = (T t) => t;
		}

		// Token: 0x0200001F RID: 31
		private class ReadOnlyCollectionOf<T>
		{
			// Token: 0x0400009C RID: 156
			public static readonly ReadOnlyCollection<T> Empty = new ReadOnlyCollection<T>(new T[0]);
		}
	}
}
