using System;
using System.Collections;
using System.Xml.Xsl;

namespace System.Xml.XPath
{
	// Token: 0x02000164 RID: 356
	internal static class ExpressionCache
	{
		// Token: 0x06000FCC RID: 4044 RVA: 0x0004B150 File Offset: 0x00049350
		public static XPathExpression Get(string xpath, IStaticXsltContext ctx)
		{
			object key = (ctx == null) ? ExpressionCache.dummy : ctx;
			object obj = ExpressionCache.cache_lock;
			lock (obj)
			{
				WeakReference weakReference = ExpressionCache.table_per_ctx[key] as WeakReference;
				if (weakReference == null)
				{
					return null;
				}
				Hashtable hashtable = weakReference.Target as Hashtable;
				if (hashtable == null)
				{
					ExpressionCache.table_per_ctx[key] = null;
					return null;
				}
				weakReference = (hashtable[xpath] as WeakReference);
				if (weakReference != null)
				{
					XPathExpression xpathExpression = weakReference.Target as XPathExpression;
					if (xpathExpression != null)
					{
						return xpathExpression;
					}
					hashtable[xpath] = null;
				}
			}
			return null;
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x0004B224 File Offset: 0x00049424
		public static void Set(string xpath, IStaticXsltContext ctx, XPathExpression exp)
		{
			object key = (ctx == null) ? ExpressionCache.dummy : ctx;
			Hashtable hashtable = null;
			object obj = ExpressionCache.cache_lock;
			lock (obj)
			{
				WeakReference weakReference = ExpressionCache.table_per_ctx[key] as WeakReference;
				if (weakReference != null && weakReference.IsAlive)
				{
					hashtable = (Hashtable)weakReference.Target;
				}
				if (hashtable == null)
				{
					hashtable = new Hashtable();
					ExpressionCache.table_per_ctx[key] = new WeakReference(hashtable);
				}
				hashtable[xpath] = new WeakReference(exp);
			}
		}

		// Token: 0x040006CF RID: 1743
		private static readonly Hashtable table_per_ctx = new Hashtable();

		// Token: 0x040006D0 RID: 1744
		private static object dummy = new object();

		// Token: 0x040006D1 RID: 1745
		private static object cache_lock = new object();
	}
}
