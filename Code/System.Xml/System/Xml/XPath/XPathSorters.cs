using System;
using System.Collections;

namespace System.Xml.XPath
{
	// Token: 0x02000167 RID: 359
	internal class XPathSorters : IComparer
	{
		// Token: 0x06000FE0 RID: 4064 RVA: 0x0004B4F8 File Offset: 0x000496F8
		int IComparer.Compare(object o1, object o2)
		{
			XPathSortElement xpathSortElement = (XPathSortElement)o1;
			XPathSortElement xpathSortElement2 = (XPathSortElement)o2;
			for (int i = 0; i < this._rgSorters.Count; i++)
			{
				XPathSorter xpathSorter = (XPathSorter)this._rgSorters[i];
				int num = xpathSorter.Compare(xpathSortElement.Values[i], xpathSortElement2.Values[i]);
				if (num != 0)
				{
					return num;
				}
			}
			XmlNodeOrder xmlNodeOrder = xpathSortElement.Navigator.ComparePosition(xpathSortElement2.Navigator);
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

		// Token: 0x06000FE1 RID: 4065 RVA: 0x0004B590 File Offset: 0x00049790
		public void Add(object expr, IComparer cmp)
		{
			this._rgSorters.Add(new XPathSorter(expr, cmp));
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x0004B5A8 File Offset: 0x000497A8
		public void Add(object expr, XmlSortOrder orderSort, XmlCaseOrder orderCase, string lang, XmlDataType dataType)
		{
			this._rgSorters.Add(new XPathSorter(expr, orderSort, orderCase, lang, dataType));
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x0004B5C4 File Offset: 0x000497C4
		public void CopyFrom(XPathSorter[] sorters)
		{
			this._rgSorters.Clear();
			this._rgSorters.AddRange(sorters);
		}

		// Token: 0x06000FE4 RID: 4068 RVA: 0x0004B5E0 File Offset: 0x000497E0
		public BaseIterator Sort(BaseIterator iter)
		{
			ArrayList rgElts = this.ToSortElementList(iter);
			return this.Sort(rgElts, iter.NamespaceManager);
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x0004B604 File Offset: 0x00049804
		private ArrayList ToSortElementList(BaseIterator iter)
		{
			ArrayList arrayList = new ArrayList();
			int count = this._rgSorters.Count;
			while (iter.MoveNext())
			{
				XPathSortElement xpathSortElement = new XPathSortElement();
				xpathSortElement.Navigator = iter.Current.Clone();
				xpathSortElement.Values = new object[count];
				for (int i = 0; i < this._rgSorters.Count; i++)
				{
					XPathSorter xpathSorter = (XPathSorter)this._rgSorters[i];
					xpathSortElement.Values[i] = xpathSorter.Evaluate(iter);
				}
				arrayList.Add(xpathSortElement);
			}
			return arrayList;
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x0004B6A0 File Offset: 0x000498A0
		public BaseIterator Sort(ArrayList rgElts, IXmlNamespaceResolver nsm)
		{
			rgElts.Sort(this);
			XPathNavigator[] array = new XPathNavigator[rgElts.Count];
			for (int i = 0; i < rgElts.Count; i++)
			{
				XPathSortElement xpathSortElement = (XPathSortElement)rgElts[i];
				array[i] = xpathSortElement.Navigator;
			}
			return new ListIterator(array, nsm);
		}

		// Token: 0x040006D8 RID: 1752
		private readonly ArrayList _rgSorters = new ArrayList();
	}
}
