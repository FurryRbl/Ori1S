using System;
using System.Collections;

namespace System.Xml.XPath
{
	// Token: 0x02000144 RID: 324
	internal class XPathNavigatorComparer : IComparer, IEqualityComparer
	{
		// Token: 0x06000F23 RID: 3875 RVA: 0x000493DC File Offset: 0x000475DC
		private XPathNavigatorComparer()
		{
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x000493F0 File Offset: 0x000475F0
		bool IEqualityComparer.Equals(object o1, object o2)
		{
			XPathNavigator xpathNavigator = o1 as XPathNavigator;
			XPathNavigator xpathNavigator2 = o2 as XPathNavigator;
			return xpathNavigator != null && xpathNavigator2 != null && xpathNavigator.IsSamePosition(xpathNavigator2);
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x00049424 File Offset: 0x00047624
		int IEqualityComparer.GetHashCode(object obj)
		{
			return obj.GetHashCode();
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x0004942C File Offset: 0x0004762C
		public int Compare(object o1, object o2)
		{
			XPathNavigator xpathNavigator = o1 as XPathNavigator;
			XPathNavigator xpathNavigator2 = o2 as XPathNavigator;
			if (xpathNavigator == null)
			{
				return -1;
			}
			if (xpathNavigator2 == null)
			{
				return 1;
			}
			XmlNodeOrder xmlNodeOrder = xpathNavigator.ComparePosition(xpathNavigator2);
			if (xmlNodeOrder == XmlNodeOrder.After)
			{
				return 1;
			}
			if (xmlNodeOrder != XmlNodeOrder.Same)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x040006AE RID: 1710
		public static XPathNavigatorComparer Instance = new XPathNavigatorComparer();
	}
}
