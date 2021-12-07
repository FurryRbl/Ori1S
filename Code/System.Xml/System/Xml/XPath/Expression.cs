using System;
using System.Globalization;

namespace System.Xml.XPath
{
	// Token: 0x0200016B RID: 363
	internal abstract class Expression
	{
		// Token: 0x06000FF0 RID: 4080 RVA: 0x0004B958 File Offset: 0x00049B58
		public Expression()
		{
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06000FF1 RID: 4081
		public abstract XPathResultType ReturnType { get; }

		// Token: 0x06000FF2 RID: 4082 RVA: 0x0004B960 File Offset: 0x00049B60
		public virtual XPathResultType GetReturnType(BaseIterator iter)
		{
			return this.ReturnType;
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x0004B968 File Offset: 0x00049B68
		public virtual Expression Optimize()
		{
			return this;
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06000FF4 RID: 4084 RVA: 0x0004B96C File Offset: 0x00049B6C
		public virtual bool HasStaticValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06000FF5 RID: 4085 RVA: 0x0004B970 File Offset: 0x00049B70
		public virtual object StaticValue
		{
			get
			{
				switch (this.ReturnType)
				{
				case XPathResultType.Number:
					return this.StaticValueAsNumber;
				case XPathResultType.String:
					return this.StaticValueAsString;
				case XPathResultType.Boolean:
					return this.StaticValueAsBoolean;
				default:
					return null;
				}
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06000FF6 RID: 4086 RVA: 0x0004B9BC File Offset: 0x00049BBC
		public virtual string StaticValueAsString
		{
			get
			{
				return (!this.HasStaticValue) ? null : XPathFunctions.ToString(this.StaticValue);
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06000FF7 RID: 4087 RVA: 0x0004B9DC File Offset: 0x00049BDC
		public virtual double StaticValueAsNumber
		{
			get
			{
				return (!this.HasStaticValue) ? 0.0 : XPathFunctions.ToNumber(this.StaticValue);
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06000FF8 RID: 4088 RVA: 0x0004BA10 File Offset: 0x00049C10
		public virtual bool StaticValueAsBoolean
		{
			get
			{
				return this.HasStaticValue && XPathFunctions.ToBoolean(this.StaticValue);
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06000FF9 RID: 4089 RVA: 0x0004BA30 File Offset: 0x00049C30
		public virtual XPathNavigator StaticValueAsNavigator
		{
			get
			{
				return this.StaticValue as XPathNavigator;
			}
		}

		// Token: 0x06000FFA RID: 4090
		public abstract object Evaluate(BaseIterator iter);

		// Token: 0x06000FFB RID: 4091 RVA: 0x0004BA40 File Offset: 0x00049C40
		public virtual BaseIterator EvaluateNodeSet(BaseIterator iter)
		{
			XPathResultType returnType = this.GetReturnType(iter);
			switch (returnType)
			{
			case XPathResultType.NodeSet:
			case XPathResultType.Navigator:
			case XPathResultType.Any:
			{
				object obj = this.Evaluate(iter);
				XPathNodeIterator xpathNodeIterator = obj as XPathNodeIterator;
				BaseIterator baseIterator = null;
				if (xpathNodeIterator != null)
				{
					baseIterator = (xpathNodeIterator as BaseIterator);
					if (baseIterator == null)
					{
						baseIterator = new WrapperIterator(xpathNodeIterator, iter.NamespaceManager);
					}
					return baseIterator;
				}
				XPathNavigator xpathNavigator = obj as XPathNavigator;
				if (xpathNavigator != null)
				{
					XPathNodeIterator xpathNodeIterator2 = xpathNavigator.SelectChildren(XPathNodeType.All);
					baseIterator = (xpathNodeIterator2 as BaseIterator);
					if (baseIterator == null && xpathNodeIterator2 != null)
					{
						baseIterator = new WrapperIterator(xpathNodeIterator2, iter.NamespaceManager);
					}
				}
				if (baseIterator != null)
				{
					return baseIterator;
				}
				if (obj == null)
				{
					return new NullIterator(iter);
				}
				returnType = Expression.GetReturnType(obj);
				break;
			}
			}
			throw new XPathException(string.Format("expected nodeset but was {1}: {0}", this.ToString(), returnType));
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x0004BB20 File Offset: 0x00049D20
		protected static XPathResultType GetReturnType(object obj)
		{
			if (obj is string)
			{
				return XPathResultType.String;
			}
			if (obj is bool)
			{
				return XPathResultType.Boolean;
			}
			if (obj is XPathNodeIterator)
			{
				return XPathResultType.NodeSet;
			}
			if (obj is double || obj is int)
			{
				return XPathResultType.Number;
			}
			if (obj is XPathNavigator)
			{
				return XPathResultType.Navigator;
			}
			throw new XPathException("invalid node type: " + obj.GetType().ToString());
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06000FFD RID: 4093 RVA: 0x0004BB94 File Offset: 0x00049D94
		internal virtual XPathNodeType EvaluatedNodeType
		{
			get
			{
				return XPathNodeType.All;
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06000FFE RID: 4094 RVA: 0x0004BB98 File Offset: 0x00049D98
		internal virtual bool IsPositional
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06000FFF RID: 4095 RVA: 0x0004BB9C File Offset: 0x00049D9C
		internal virtual bool Peer
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x0004BBA0 File Offset: 0x00049DA0
		public virtual double EvaluateNumber(BaseIterator iter)
		{
			XPathResultType xpathResultType = this.GetReturnType(iter);
			object obj;
			if (xpathResultType == XPathResultType.NodeSet)
			{
				obj = this.EvaluateString(iter);
				xpathResultType = XPathResultType.String;
			}
			else
			{
				obj = this.Evaluate(iter);
			}
			if (xpathResultType == XPathResultType.Any)
			{
				xpathResultType = Expression.GetReturnType(obj);
			}
			switch (xpathResultType)
			{
			case XPathResultType.Number:
				if (obj is double)
				{
					return (double)obj;
				}
				if (obj is IConvertible)
				{
					return ((IConvertible)obj).ToDouble(CultureInfo.InvariantCulture);
				}
				return (double)obj;
			case XPathResultType.String:
				return XPathFunctions.ToNumber((string)obj);
			case XPathResultType.Boolean:
				return (!(bool)obj) ? 0.0 : 1.0;
			case XPathResultType.NodeSet:
				return XPathFunctions.ToNumber(this.EvaluateString(iter));
			case XPathResultType.Navigator:
				return XPathFunctions.ToNumber(((XPathNavigator)obj).Value);
			default:
				throw new XPathException("invalid node type");
			}
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x0004BC90 File Offset: 0x00049E90
		public virtual string EvaluateString(BaseIterator iter)
		{
			object obj = this.Evaluate(iter);
			XPathResultType returnType = this.GetReturnType(iter);
			if (returnType == XPathResultType.Any)
			{
				returnType = Expression.GetReturnType(obj);
			}
			switch (returnType)
			{
			case XPathResultType.Number:
			{
				double d = (double)obj;
				return XPathFunctions.ToString(d);
			}
			case XPathResultType.String:
				return (string)obj;
			case XPathResultType.Boolean:
				return (!(bool)obj) ? "false" : "true";
			case XPathResultType.NodeSet:
			{
				BaseIterator baseIterator = (BaseIterator)obj;
				if (baseIterator == null || !baseIterator.MoveNext())
				{
					return string.Empty;
				}
				return baseIterator.Current.Value;
			}
			case XPathResultType.Navigator:
				return ((XPathNavigator)obj).Value;
			default:
				throw new XPathException("invalid node type");
			}
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x0004BD50 File Offset: 0x00049F50
		public virtual bool EvaluateBoolean(BaseIterator iter)
		{
			object obj = this.Evaluate(iter);
			XPathResultType returnType = this.GetReturnType(iter);
			if (returnType == XPathResultType.Any)
			{
				returnType = Expression.GetReturnType(obj);
			}
			switch (returnType)
			{
			case XPathResultType.Number:
			{
				double num = Convert.ToDouble(obj);
				return num != 0.0 && num != --0.0 && !double.IsNaN(num);
			}
			case XPathResultType.String:
				return ((string)obj).Length != 0;
			case XPathResultType.Boolean:
				return (bool)obj;
			case XPathResultType.NodeSet:
			{
				BaseIterator baseIterator = (BaseIterator)obj;
				return baseIterator != null && baseIterator.MoveNext();
			}
			case XPathResultType.Navigator:
				return ((XPathNavigator)obj).HasChildren;
			default:
				throw new XPathException("invalid node type");
			}
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x0004BE18 File Offset: 0x0004A018
		public object EvaluateAs(BaseIterator iter, XPathResultType type)
		{
			switch (type)
			{
			case XPathResultType.Number:
				return this.EvaluateNumber(iter);
			case XPathResultType.String:
				return this.EvaluateString(iter);
			case XPathResultType.Boolean:
				return this.EvaluateBoolean(iter);
			case XPathResultType.NodeSet:
				return this.EvaluateNodeSet(iter);
			default:
				return this.Evaluate(iter);
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06001004 RID: 4100 RVA: 0x0004BE74 File Offset: 0x0004A074
		public virtual bool RequireSorting
		{
			get
			{
				return false;
			}
		}
	}
}
