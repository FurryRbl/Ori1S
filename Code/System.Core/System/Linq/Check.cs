using System;

namespace System.Linq
{
	// Token: 0x0200001A RID: 26
	internal static class Check
	{
		// Token: 0x06000167 RID: 359 RVA: 0x000083DC File Offset: 0x000065DC
		public static void Source(object source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000083F0 File Offset: 0x000065F0
		public static void Source1AndSource2(object source1, object source2)
		{
			if (source1 == null)
			{
				throw new ArgumentNullException("source1");
			}
			if (source2 == null)
			{
				throw new ArgumentNullException("source2");
			}
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00008420 File Offset: 0x00006620
		public static void SourceAndFuncAndSelector(object source, object func, object selector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (func == null)
			{
				throw new ArgumentNullException("func");
			}
			if (selector == null)
			{
				throw new ArgumentNullException("selector");
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00008458 File Offset: 0x00006658
		public static void SourceAndFunc(object source, object func)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (func == null)
			{
				throw new ArgumentNullException("func");
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00008488 File Offset: 0x00006688
		public static void SourceAndSelector(object source, object selector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (selector == null)
			{
				throw new ArgumentNullException("selector");
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000084B8 File Offset: 0x000066B8
		public static void SourceAndPredicate(object source, object predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000084E8 File Offset: 0x000066E8
		public static void FirstAndSecond(object first, object second)
		{
			if (first == null)
			{
				throw new ArgumentNullException("first");
			}
			if (second == null)
			{
				throw new ArgumentNullException("second");
			}
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00008518 File Offset: 0x00006718
		public static void SourceAndKeySelector(object source, object keySelector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00008548 File Offset: 0x00006748
		public static void SourceAndKeyElementSelectors(object source, object keySelector, object elementSelector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			if (elementSelector == null)
			{
				throw new ArgumentNullException("elementSelector");
			}
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00008580 File Offset: 0x00006780
		public static void SourceAndKeyResultSelectors(object source, object keySelector, object resultSelector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			if (resultSelector == null)
			{
				throw new ArgumentNullException("resultSelector");
			}
		}

		// Token: 0x06000171 RID: 369 RVA: 0x000085B8 File Offset: 0x000067B8
		public static void SourceAndCollectionSelectorAndResultSelector(object source, object collectionSelector, object resultSelector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (collectionSelector == null)
			{
				throw new ArgumentNullException("collectionSelector");
			}
			if (resultSelector == null)
			{
				throw new ArgumentNullException("resultSelector");
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x000085F0 File Offset: 0x000067F0
		public static void SourceAndCollectionSelectors(object source, object collectionSelector, object selector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (collectionSelector == null)
			{
				throw new ArgumentNullException("collectionSelector");
			}
			if (selector == null)
			{
				throw new ArgumentNullException("selector");
			}
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00008628 File Offset: 0x00006828
		public static void JoinSelectors(object outer, object inner, object outerKeySelector, object innerKeySelector, object resultSelector)
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
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000868C File Offset: 0x0000688C
		public static void GroupBySelectors(object source, object keySelector, object elementSelector, object resultSelector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			if (elementSelector == null)
			{
				throw new ArgumentNullException("elementSelector");
			}
			if (resultSelector == null)
			{
				throw new ArgumentNullException("resultSelector");
			}
		}
	}
}
