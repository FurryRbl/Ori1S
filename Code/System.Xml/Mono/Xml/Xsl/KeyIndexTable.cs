using System;
using System.Collections;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Mono.Xml.Xsl
{
	// Token: 0x02000092 RID: 146
	internal class KeyIndexTable
	{
		// Token: 0x060004E7 RID: 1255 RVA: 0x0001EF58 File Offset: 0x0001D158
		public KeyIndexTable(XsltCompiledContext ctx, ArrayList keys)
		{
			this.ctx = ctx;
			this.keys = keys;
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x0001EF70 File Offset: 0x0001D170
		public ArrayList Keys
		{
			get
			{
				return this.keys;
			}
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0001EF78 File Offset: 0x0001D178
		private void CollectTable(XPathNavigator doc, XsltContext ctx, Hashtable map)
		{
			for (int i = 0; i < this.keys.Count; i++)
			{
				this.CollectTable(doc, ctx, map, (XslKey)this.keys[i]);
			}
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0001EFBC File Offset: 0x0001D1BC
		private void CollectTable(XPathNavigator doc, XsltContext ctx, Hashtable map, XslKey key)
		{
			XPathNavigator xpathNavigator = doc.Clone();
			xpathNavigator.MoveToRoot();
			XPathNavigator xpathNavigator2 = doc.Clone();
			bool matchesAttributes = false;
			XPathNodeType evaluatedNodeType = key.Match.EvaluatedNodeType;
			if (evaluatedNodeType == XPathNodeType.Attribute || evaluatedNodeType == XPathNodeType.All)
			{
				matchesAttributes = true;
			}
			do
			{
				if (key.Match.Matches(xpathNavigator, ctx))
				{
					xpathNavigator2.MoveTo(xpathNavigator);
					this.CollectIndex(xpathNavigator, xpathNavigator2, map);
				}
			}
			while (this.MoveNavigatorToNext(xpathNavigator, matchesAttributes));
			if (map != null)
			{
				foreach (object obj in map.Values)
				{
					ArrayList arrayList = (ArrayList)obj;
					arrayList.Sort(XPathNavigatorComparer.Instance);
				}
			}
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0001F0B0 File Offset: 0x0001D2B0
		private bool MoveNavigatorToNext(XPathNavigator nav, bool matchesAttributes)
		{
			if (matchesAttributes)
			{
				if (nav.NodeType != XPathNodeType.Attribute && nav.MoveToFirstAttribute())
				{
					return true;
				}
				if (nav.NodeType == XPathNodeType.Attribute)
				{
					if (nav.MoveToNextAttribute())
					{
						return true;
					}
					nav.MoveToParent();
				}
			}
			if (nav.MoveToFirstChild())
			{
				return true;
			}
			while (!nav.MoveToNext())
			{
				if (!nav.MoveToParent())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0001F124 File Offset: 0x0001D324
		private void CollectIndex(XPathNavigator nav, XPathNavigator target, Hashtable map)
		{
			for (int i = 0; i < this.keys.Count; i++)
			{
				this.CollectIndex(nav, target, map, (XslKey)this.keys[i]);
			}
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0001F168 File Offset: 0x0001D368
		private void CollectIndex(XPathNavigator nav, XPathNavigator target, Hashtable map, XslKey key)
		{
			switch (key.Use.ReturnType)
			{
			case XPathResultType.NodeSet:
			{
				XPathNodeIterator xpathNodeIterator = nav.Select(key.Use);
				while (xpathNodeIterator.MoveNext())
				{
					XPathNavigator xpathNavigator = xpathNodeIterator.Current;
					this.AddIndex(xpathNavigator.Value, target, map);
				}
				return;
			}
			case XPathResultType.Any:
			{
				object obj = nav.Evaluate(key.Use);
				XPathNodeIterator xpathNodeIterator = obj as XPathNodeIterator;
				if (xpathNodeIterator != null)
				{
					while (xpathNodeIterator.MoveNext())
					{
						XPathNavigator xpathNavigator2 = xpathNodeIterator.Current;
						this.AddIndex(xpathNavigator2.Value, target, map);
					}
				}
				else
				{
					this.AddIndex(XPathFunctions.ToString(obj), target, map);
				}
				return;
			}
			}
			string key2 = nav.EvaluateString(key.Use, null, null);
			this.AddIndex(key2, target, map);
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0001F248 File Offset: 0x0001D448
		private void AddIndex(string key, XPathNavigator target, Hashtable map)
		{
			ArrayList arrayList = map[key] as ArrayList;
			if (arrayList == null)
			{
				arrayList = new ArrayList();
				map[key] = arrayList;
			}
			for (int i = 0; i < arrayList.Count; i++)
			{
				if (((XPathNavigator)arrayList[i]).IsSamePosition(target))
				{
					return;
				}
			}
			arrayList.Add(target.Clone());
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0001F2B4 File Offset: 0x0001D4B4
		private ArrayList GetNodesByValue(XPathNavigator nav, string value, XsltContext ctx)
		{
			if (this.mappedDocuments == null)
			{
				this.mappedDocuments = new Hashtable();
			}
			Hashtable hashtable = (Hashtable)this.mappedDocuments[nav.BaseURI];
			if (hashtable == null)
			{
				hashtable = new Hashtable();
				this.mappedDocuments.Add(nav.BaseURI, hashtable);
				this.CollectTable(nav, ctx, hashtable);
			}
			return hashtable[value] as ArrayList;
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0001F324 File Offset: 0x0001D524
		public bool Matches(XPathNavigator nav, string value, XsltContext ctx)
		{
			ArrayList nodesByValue = this.GetNodesByValue(nav, value, ctx);
			if (nodesByValue == null)
			{
				return false;
			}
			for (int i = 0; i < nodesByValue.Count; i++)
			{
				if (((XPathNavigator)nodesByValue[i]).IsSamePosition(nav))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0001F374 File Offset: 0x0001D574
		public BaseIterator Evaluate(BaseIterator iter, Expression valueExpr)
		{
			XPathNodeIterator xpathNodeIterator = iter;
			if (iter.CurrentPosition == 0)
			{
				xpathNodeIterator = iter.Clone();
				xpathNodeIterator.MoveNext();
			}
			XPathNavigator xpathNavigator = xpathNodeIterator.Current;
			object obj = valueExpr.Evaluate(iter);
			XPathNodeIterator xpathNodeIterator2 = obj as XPathNodeIterator;
			XsltContext nsm = iter.NamespaceManager as XsltContext;
			BaseIterator baseIterator = null;
			if (xpathNodeIterator2 != null)
			{
				while (xpathNodeIterator2.MoveNext())
				{
					XPathNavigator xpathNavigator2 = xpathNodeIterator2.Current;
					ArrayList nodesByValue = this.GetNodesByValue(xpathNavigator, xpathNavigator2.Value, nsm);
					if (nodesByValue != null)
					{
						ListIterator listIterator = new ListIterator(nodesByValue, nsm);
						if (baseIterator == null)
						{
							baseIterator = listIterator;
						}
						else
						{
							baseIterator = new UnionIterator(iter, baseIterator, listIterator);
						}
					}
				}
			}
			else if (xpathNavigator != null)
			{
				ArrayList nodesByValue2 = this.GetNodesByValue(xpathNavigator, XPathFunctions.ToString(obj), nsm);
				if (nodesByValue2 != null)
				{
					baseIterator = new ListIterator(nodesByValue2, nsm);
				}
			}
			return (baseIterator == null) ? new NullIterator(iter) : baseIterator;
		}

		// Token: 0x04000323 RID: 803
		private XsltCompiledContext ctx;

		// Token: 0x04000324 RID: 804
		private ArrayList keys;

		// Token: 0x04000325 RID: 805
		private Hashtable mappedDocuments;
	}
}
