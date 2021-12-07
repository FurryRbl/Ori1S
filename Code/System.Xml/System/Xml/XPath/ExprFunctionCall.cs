using System;
using System.Collections;
using System.Xml.Xsl;

namespace System.Xml.XPath
{
	// Token: 0x02000190 RID: 400
	internal class ExprFunctionCall : Expression
	{
		// Token: 0x060010D0 RID: 4304 RVA: 0x0004DD5C File Offset: 0x0004BF5C
		public ExprFunctionCall(XmlQualifiedName name, FunctionArguments args, IStaticXsltContext ctx)
		{
			if (ctx != null)
			{
				name = ctx.LookupQName(name.ToString());
				this.resolvedName = true;
			}
			this._name = name;
			if (args != null)
			{
				args.ToArrayList(this._args);
			}
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x0004DDB0 File Offset: 0x0004BFB0
		public static Expression Factory(XmlQualifiedName name, FunctionArguments args, IStaticXsltContext ctx)
		{
			if (name.Namespace != null && name.Namespace != string.Empty)
			{
				return new ExprFunctionCall(name, args, ctx);
			}
			string name2 = name.Name;
			switch (name2)
			{
			case "last":
				return new XPathFunctionLast(args);
			case "position":
				return new XPathFunctionPosition(args);
			case "count":
				return new XPathFunctionCount(args);
			case "id":
				return new XPathFunctionId(args);
			case "local-name":
				return new XPathFunctionLocalName(args);
			case "namespace-uri":
				return new XPathFunctionNamespaceUri(args);
			case "name":
				return new XPathFunctionName(args);
			case "string":
				return new XPathFunctionString(args);
			case "concat":
				return new XPathFunctionConcat(args);
			case "starts-with":
				return new XPathFunctionStartsWith(args);
			case "contains":
				return new XPathFunctionContains(args);
			case "substring-before":
				return new XPathFunctionSubstringBefore(args);
			case "substring-after":
				return new XPathFunctionSubstringAfter(args);
			case "substring":
				return new XPathFunctionSubstring(args);
			case "string-length":
				return new XPathFunctionStringLength(args);
			case "normalize-space":
				return new XPathFunctionNormalizeSpace(args);
			case "translate":
				return new XPathFunctionTranslate(args);
			case "boolean":
				return new XPathFunctionBoolean(args);
			case "not":
				return new XPathFunctionNot(args);
			case "true":
				return new XPathFunctionTrue(args);
			case "false":
				return new XPathFunctionFalse(args);
			case "lang":
				return new XPathFunctionLang(args);
			case "number":
				return new XPathFunctionNumber(args);
			case "sum":
				return new XPathFunctionSum(args);
			case "floor":
				return new XPathFunctionFloor(args);
			case "ceiling":
				return new XPathFunctionCeil(args);
			case "round":
				return new XPathFunctionRound(args);
			}
			return new ExprFunctionCall(name, args, ctx);
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x0004E0B0 File Offset: 0x0004C2B0
		public override string ToString()
		{
			string text = string.Empty;
			for (int i = 0; i < this._args.Count; i++)
			{
				Expression expression = (Expression)this._args[i];
				if (text != string.Empty)
				{
					text += ", ";
				}
				text += expression.ToString();
			}
			return string.Concat(new object[]
			{
				this._name.ToString(),
				'(',
				text,
				')'
			});
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x060010D3 RID: 4307 RVA: 0x0004E14C File Offset: 0x0004C34C
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.Any;
			}
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x0004E150 File Offset: 0x0004C350
		public override XPathResultType GetReturnType(BaseIterator iter)
		{
			return XPathResultType.Any;
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x0004E154 File Offset: 0x0004C354
		private XPathResultType[] GetArgTypes(BaseIterator iter)
		{
			XPathResultType[] array = new XPathResultType[this._args.Count];
			for (int i = 0; i < this._args.Count; i++)
			{
				array[i] = ((Expression)this._args[i]).GetReturnType(iter);
			}
			return array;
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x0004E1AC File Offset: 0x0004C3AC
		public override object Evaluate(BaseIterator iter)
		{
			XPathResultType[] argTypes = this.GetArgTypes(iter);
			IXsltContextFunction xsltContextFunction = null;
			XsltContext xsltContext = iter.NamespaceManager as XsltContext;
			if (xsltContext != null)
			{
				if (this.resolvedName)
				{
					xsltContextFunction = xsltContext.ResolveFunction(this._name, argTypes);
				}
				else
				{
					xsltContextFunction = xsltContext.ResolveFunction(this._name.Namespace, this._name.Name, argTypes);
				}
			}
			if (xsltContextFunction == null)
			{
				throw new XPathException("function " + this._name.ToString() + " not found");
			}
			object[] array = new object[this._args.Count];
			if (xsltContextFunction.Maxargs != 0)
			{
				XPathResultType[] argTypes2 = xsltContextFunction.ArgTypes;
				for (int i = 0; i < this._args.Count; i++)
				{
					XPathResultType type;
					if (argTypes2 == null)
					{
						type = XPathResultType.Any;
					}
					else if (i < argTypes2.Length)
					{
						type = argTypes2[i];
					}
					else
					{
						type = argTypes2[argTypes2.Length - 1];
					}
					Expression expression = (Expression)this._args[i];
					object obj = expression.EvaluateAs(iter, type);
					array[i] = obj;
				}
			}
			return xsltContextFunction.Invoke(xsltContext, array, iter.Current);
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x060010D7 RID: 4311 RVA: 0x0004E2E0 File Offset: 0x0004C4E0
		internal override bool Peer
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0400070A RID: 1802
		protected readonly XmlQualifiedName _name;

		// Token: 0x0400070B RID: 1803
		protected readonly bool resolvedName;

		// Token: 0x0400070C RID: 1804
		protected readonly ArrayList _args = new ArrayList();
	}
}
