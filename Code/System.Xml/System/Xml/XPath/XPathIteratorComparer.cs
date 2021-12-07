using System;
using System.Collections;

namespace System.Xml.XPath
{
	// Token: 0x02000143 RID: 323
	internal class XPathIteratorComparer : IComparer
	{
		// Token: 0x06000F20 RID: 3872 RVA: 0x00049370 File Offset: 0x00047570
		private XPathIteratorComparer()
		{
		}

		// Token: 0x06000F22 RID: 3874 RVA: 0x00049384 File Offset: 0x00047584
		public int Compare(object o1, object o2)
		{
			XPathNodeIterator xpathNodeIterator = o1 as XPathNodeIterator;
			XPathNodeIterator xpathNodeIterator2 = o2 as XPathNodeIterator;
			if (xpathNodeIterator == null)
			{
				return -1;
			}
			if (xpathNodeIterator2 == null)
			{
				return 1;
			}
			XmlNodeOrder xmlNodeOrder = xpathNodeIterator.Current.ComparePosition(xpathNodeIterator2.Current);
			if (xmlNodeOrder == XmlNodeOrder.After)
			{
				return -1;
			}
			if (xmlNodeOrder != XmlNodeOrder.Same)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x040006AD RID: 1709
		public static XPathIteratorComparer Instance = new XPathIteratorComparer();
	}
}
