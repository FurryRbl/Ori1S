using System;
using System.Collections.Generic;
using System.Xml.XPath;
using Mono.Xml.Xsl.Operations;

namespace Mono.Xml.Xsl
{
	// Token: 0x0200007A RID: 122
	internal class Sort
	{
		// Token: 0x06000415 RID: 1045 RVA: 0x0001AE80 File Offset: 0x00019080
		public Sort(Compiler c)
		{
			c.CheckExtraAttributes("sort", new string[]
			{
				"select",
				"lang",
				"data-type",
				"order",
				"case-order"
			});
			this.expr = c.CompileExpression(c.GetAttribute("select"));
			if (this.expr == null)
			{
				this.expr = c.CompileExpression("string(.)");
			}
			this.langAvt = c.ParseAvtAttribute("lang");
			this.dataTypeAvt = c.ParseAvtAttribute("data-type");
			this.orderAvt = c.ParseAvtAttribute("order");
			this.caseOrderAvt = c.ParseAvtAttribute("case-order");
			this.lang = this.ParseLang(XslAvt.AttemptPreCalc(ref this.langAvt));
			this.dataType = this.ParseDataType(XslAvt.AttemptPreCalc(ref this.dataTypeAvt));
			this.order = this.ParseOrder(XslAvt.AttemptPreCalc(ref this.orderAvt));
			this.caseOrder = this.ParseCaseOrder(XslAvt.AttemptPreCalc(ref this.caseOrderAvt));
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x0001AFA0 File Offset: 0x000191A0
		public bool IsContextDependent
		{
			get
			{
				return this.orderAvt != null || this.caseOrderAvt != null || this.langAvt != null || this.dataTypeAvt != null;
			}
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0001AFE0 File Offset: 0x000191E0
		private string ParseLang(string value)
		{
			return value;
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0001AFE4 File Offset: 0x000191E4
		private XmlDataType ParseDataType(string value)
		{
			if (value != null)
			{
				if (Sort.<>f__switch$map10 == null)
				{
					Sort.<>f__switch$map10 = new Dictionary<string, int>(2)
					{
						{
							"number",
							0
						},
						{
							"text",
							1
						}
					};
				}
				int num;
				if (Sort.<>f__switch$map10.TryGetValue(value, out num))
				{
					if (num == 0)
					{
						return XmlDataType.Number;
					}
					if (num != 1)
					{
					}
				}
			}
			return XmlDataType.Text;
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0001B050 File Offset: 0x00019250
		private XmlSortOrder ParseOrder(string value)
		{
			if (value != null)
			{
				if (Sort.<>f__switch$map11 == null)
				{
					Sort.<>f__switch$map11 = new Dictionary<string, int>(2)
					{
						{
							"descending",
							0
						},
						{
							"ascending",
							1
						}
					};
				}
				int num;
				if (Sort.<>f__switch$map11.TryGetValue(value, out num))
				{
					if (num == 0)
					{
						return XmlSortOrder.Descending;
					}
					if (num != 1)
					{
					}
				}
			}
			return XmlSortOrder.Ascending;
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0001B0BC File Offset: 0x000192BC
		private XmlCaseOrder ParseCaseOrder(string value)
		{
			if (value != null)
			{
				if (Sort.<>f__switch$map12 == null)
				{
					Sort.<>f__switch$map12 = new Dictionary<string, int>(2)
					{
						{
							"upper-first",
							0
						},
						{
							"lower-first",
							1
						}
					};
				}
				int num;
				if (Sort.<>f__switch$map12.TryGetValue(value, out num))
				{
					if (num == 0)
					{
						return XmlCaseOrder.UpperFirst;
					}
					if (num == 1)
					{
						return XmlCaseOrder.LowerFirst;
					}
				}
			}
			return XmlCaseOrder.None;
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0001B12C File Offset: 0x0001932C
		public void AddToExpr(XPathExpression e, XslTransformProcessor p)
		{
			e.AddSort(this.expr, (this.orderAvt != null) ? this.ParseOrder(this.orderAvt.Evaluate(p)) : this.order, (this.caseOrderAvt != null) ? this.ParseCaseOrder(this.caseOrderAvt.Evaluate(p)) : this.caseOrder, (this.langAvt != null) ? this.ParseLang(this.langAvt.Evaluate(p)) : this.lang, (this.dataTypeAvt != null) ? this.ParseDataType(this.dataTypeAvt.Evaluate(p)) : this.dataType);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0001B1E8 File Offset: 0x000193E8
		public XPathSorter ToXPathSorter(XslTransformProcessor p)
		{
			return new XPathSorter(this.expr, (this.orderAvt != null) ? this.ParseOrder(this.orderAvt.Evaluate(p)) : this.order, (this.caseOrderAvt != null) ? this.ParseCaseOrder(this.caseOrderAvt.Evaluate(p)) : this.caseOrder, (this.langAvt != null) ? this.ParseLang(this.langAvt.Evaluate(p)) : this.lang, (this.dataTypeAvt != null) ? this.ParseDataType(this.dataTypeAvt.Evaluate(p)) : this.dataType);
		}

		// Token: 0x040002CE RID: 718
		private string lang;

		// Token: 0x040002CF RID: 719
		private XmlDataType dataType;

		// Token: 0x040002D0 RID: 720
		private XmlSortOrder order;

		// Token: 0x040002D1 RID: 721
		private XmlCaseOrder caseOrder;

		// Token: 0x040002D2 RID: 722
		private XslAvt langAvt;

		// Token: 0x040002D3 RID: 723
		private XslAvt dataTypeAvt;

		// Token: 0x040002D4 RID: 724
		private XslAvt orderAvt;

		// Token: 0x040002D5 RID: 725
		private XslAvt caseOrderAvt;

		// Token: 0x040002D6 RID: 726
		private XPathExpression expr;
	}
}
