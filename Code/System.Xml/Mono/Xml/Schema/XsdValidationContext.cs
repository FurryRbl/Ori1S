using System;
using System.Collections;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x02000030 RID: 48
	internal class XsdValidationContext
	{
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000183 RID: 387 RVA: 0x0000CC10 File Offset: 0x0000AE10
		// (set) Token: 0x06000184 RID: 388 RVA: 0x0000CC18 File Offset: 0x0000AE18
		public object XsiType
		{
			get
			{
				return this.xsi_type;
			}
			set
			{
				this.xsi_type = value;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000185 RID: 389 RVA: 0x0000CC24 File Offset: 0x0000AE24
		public XmlSchemaElement Element
		{
			get
			{
				return (this.element_stack.Count <= 0) ? null : (this.element_stack.Peek() as XmlSchemaElement);
			}
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000CC50 File Offset: 0x0000AE50
		public void PushCurrentElement(XmlSchemaElement element)
		{
			this.element_stack.Push(element);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000CC60 File Offset: 0x0000AE60
		public void PopCurrentElement()
		{
			this.element_stack.Pop();
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000188 RID: 392 RVA: 0x0000CC70 File Offset: 0x0000AE70
		public object ActualType
		{
			get
			{
				if (this.element_stack.Count == 0)
				{
					return null;
				}
				if (this.XsiType != null)
				{
					return this.XsiType;
				}
				return (this.Element == null) ? null : this.Element.ElementType;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000189 RID: 393 RVA: 0x0000CCC0 File Offset: 0x0000AEC0
		public XmlSchemaType ActualSchemaType
		{
			get
			{
				object actualType = this.ActualType;
				if (actualType == null)
				{
					return null;
				}
				XmlSchemaType xmlSchemaType = actualType as XmlSchemaType;
				if (xmlSchemaType == null)
				{
					xmlSchemaType = XmlSchemaType.GetBuiltInSimpleType(((XmlSchemaDatatype)actualType).TypeCode);
				}
				return xmlSchemaType;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600018A RID: 394 RVA: 0x0000CCFC File Offset: 0x0000AEFC
		public bool IsInvalid
		{
			get
			{
				return this.State == XsdValidationState.Invalid;
			}
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000CD0C File Offset: 0x0000AF0C
		public object Clone()
		{
			return base.MemberwiseClone();
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000CD14 File Offset: 0x0000AF14
		public void EvaluateStartElement(string localName, string ns)
		{
			this.State = this.State.EvaluateStartElement(localName, ns);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000CD2C File Offset: 0x0000AF2C
		public bool EvaluateEndElement()
		{
			return this.State.EvaluateEndElement();
		}

		// Token: 0x04000149 RID: 329
		private object xsi_type;

		// Token: 0x0400014A RID: 330
		internal XsdValidationState State;

		// Token: 0x0400014B RID: 331
		private Stack element_stack = new Stack();
	}
}
