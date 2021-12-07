using System;
using System.Collections;
using System.Xml.XPath;

namespace Mono.Xml.Xsl
{
	// Token: 0x02000096 RID: 150
	internal class XslSortEvaluator
	{
		// Token: 0x06000501 RID: 1281 RVA: 0x0001FB84 File Offset: 0x0001DD84
		public XslSortEvaluator(XPathExpression select, Sort[] sorterTemplates)
		{
			this.select = select;
			this.sorterTemplates = sorterTemplates;
			this.PopulateConstantSorters();
			this.sortRunner = new XPathSorters();
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0001FBAC File Offset: 0x0001DDAC
		private void PopulateConstantSorters()
		{
			this.sorters = new XPathSorter[this.sorterTemplates.Length];
			for (int i = 0; i < this.sorterTemplates.Length; i++)
			{
				Sort sort = this.sorterTemplates[i];
				if (sort.IsContextDependent)
				{
					this.isSorterContextDependent = true;
				}
				else
				{
					this.sorters[i] = sort.ToXPathSorter(null);
				}
			}
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0001FC14 File Offset: 0x0001DE14
		public BaseIterator SortedSelect(XslTransformProcessor p)
		{
			if (this.isSorterContextDependent)
			{
				for (int i = 0; i < this.sorters.Length; i++)
				{
					if (this.sorterTemplates[i].IsContextDependent)
					{
						this.sorters[i] = this.sorterTemplates[i].ToXPathSorter(p);
					}
				}
			}
			BaseIterator baseIterator = (BaseIterator)p.Select(this.select);
			p.PushNodeset(baseIterator);
			p.PushForEachContext();
			ArrayList arrayList = new ArrayList(baseIterator.Count);
			while (baseIterator.MoveNext())
			{
				XPathSortElement xpathSortElement = new XPathSortElement();
				xpathSortElement.Navigator = baseIterator.Current.Clone();
				xpathSortElement.Values = new object[this.sorters.Length];
				for (int j = 0; j < this.sorters.Length; j++)
				{
					xpathSortElement.Values[j] = this.sorters[j].Evaluate(baseIterator);
				}
				arrayList.Add(xpathSortElement);
			}
			p.PopForEachContext();
			p.PopNodeset();
			this.sortRunner.CopyFrom(this.sorters);
			return this.sortRunner.Sort(arrayList, baseIterator.NamespaceManager);
		}

		// Token: 0x04000343 RID: 835
		private XPathExpression select;

		// Token: 0x04000344 RID: 836
		private Sort[] sorterTemplates;

		// Token: 0x04000345 RID: 837
		private XPathSorter[] sorters;

		// Token: 0x04000346 RID: 838
		private XPathSorters sortRunner;

		// Token: 0x04000347 RID: 839
		private bool isSorterContextDependent;
	}
}
