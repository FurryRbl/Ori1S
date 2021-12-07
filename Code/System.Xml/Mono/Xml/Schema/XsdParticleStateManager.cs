using System;
using System.Collections;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x02000025 RID: 37
	internal class XsdParticleStateManager
	{
		// Token: 0x060000DF RID: 223 RVA: 0x00008744 File Offset: 0x00006944
		public XsdParticleStateManager()
		{
			this.table = new Hashtable();
			this.processContents = XmlSchemaContentProcessing.Strict;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00008780 File Offset: 0x00006980
		public XmlSchemaContentProcessing ProcessContents
		{
			get
			{
				return this.processContents;
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00008788 File Offset: 0x00006988
		public void PushContext()
		{
			this.ContextStack.Push(this.Context.Clone());
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000087A0 File Offset: 0x000069A0
		public void PopContext()
		{
			this.Context = (XsdValidationContext)this.ContextStack.Pop();
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000087B8 File Offset: 0x000069B8
		internal void SetProcessContents(XmlSchemaContentProcessing value)
		{
			this.processContents = value;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000087C4 File Offset: 0x000069C4
		public XsdValidationState Get(XmlSchemaParticle xsobj)
		{
			XsdValidationState xsdValidationState = this.table[xsobj] as XsdValidationState;
			if (xsdValidationState == null)
			{
				xsdValidationState = this.Create(xsobj);
			}
			return xsdValidationState;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000087F4 File Offset: 0x000069F4
		public XsdValidationState Create(XmlSchemaObject xsobj)
		{
			string name = xsobj.GetType().Name;
			string text = name;
			switch (text)
			{
			case "XmlSchemaElement":
				return this.AddElement((XmlSchemaElement)xsobj);
			case "XmlSchemaSequence":
				return this.AddSequence((XmlSchemaSequence)xsobj);
			case "XmlSchemaChoice":
				return this.AddChoice((XmlSchemaChoice)xsobj);
			case "XmlSchemaAll":
				return this.AddAll((XmlSchemaAll)xsobj);
			case "XmlSchemaAny":
				return this.AddAny((XmlSchemaAny)xsobj);
			case "EmptyParticle":
				return this.AddEmpty();
			}
			throw new InvalidOperationException("Should not occur.");
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000088FC File Offset: 0x00006AFC
		internal XsdValidationState MakeSequence(XsdValidationState head, XsdValidationState rest)
		{
			if (head is XsdEmptyValidationState)
			{
				return rest;
			}
			return new XsdAppendedValidationState(this, head, rest);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00008914 File Offset: 0x00006B14
		private XsdElementValidationState AddElement(XmlSchemaElement element)
		{
			return new XsdElementValidationState(element, this);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000892C File Offset: 0x00006B2C
		private XsdSequenceValidationState AddSequence(XmlSchemaSequence sequence)
		{
			return new XsdSequenceValidationState(sequence, this);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00008944 File Offset: 0x00006B44
		private XsdChoiceValidationState AddChoice(XmlSchemaChoice choice)
		{
			return new XsdChoiceValidationState(choice, this);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000895C File Offset: 0x00006B5C
		private XsdAllValidationState AddAll(XmlSchemaAll all)
		{
			return new XsdAllValidationState(all, this);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00008974 File Offset: 0x00006B74
		private XsdAnyValidationState AddAny(XmlSchemaAny any)
		{
			return new XsdAnyValidationState(any, this);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x0000898C File Offset: 0x00006B8C
		private XsdEmptyValidationState AddEmpty()
		{
			return new XsdEmptyValidationState(this);
		}

		// Token: 0x04000115 RID: 277
		private Hashtable table;

		// Token: 0x04000116 RID: 278
		private XmlSchemaContentProcessing processContents;

		// Token: 0x04000117 RID: 279
		public XmlSchemaElement CurrentElement;

		// Token: 0x04000118 RID: 280
		public Stack ContextStack = new Stack();

		// Token: 0x04000119 RID: 281
		public XsdValidationContext Context = new XsdValidationContext();
	}
}
