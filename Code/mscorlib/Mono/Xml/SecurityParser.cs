using System;
using System.Collections;
using System.IO;
using System.Security;

namespace Mono.Xml
{
	// Token: 0x020000F2 RID: 242
	internal class SecurityParser : SmallXmlParser, SmallXmlParser.IContentHandler
	{
		// Token: 0x06000C50 RID: 3152 RVA: 0x00038388 File Offset: 0x00036588
		public SecurityParser()
		{
			this.stack = new Stack();
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x0003839C File Offset: 0x0003659C
		public void LoadXml(string xml)
		{
			this.root = null;
			this.stack.Clear();
			base.Parse(new StringReader(xml), this);
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x000383C0 File Offset: 0x000365C0
		public SecurityElement ToXml()
		{
			return this.root;
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x000383C8 File Offset: 0x000365C8
		public void OnStartParsing(SmallXmlParser parser)
		{
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x000383CC File Offset: 0x000365CC
		public void OnProcessingInstruction(string name, string text)
		{
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x000383D0 File Offset: 0x000365D0
		public void OnIgnorableWhitespace(string s)
		{
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x000383D4 File Offset: 0x000365D4
		public void OnStartElement(string name, SmallXmlParser.IAttrList attrs)
		{
			SecurityElement securityElement = new SecurityElement(name);
			if (this.root == null)
			{
				this.root = securityElement;
				this.current = securityElement;
			}
			else
			{
				SecurityElement securityElement2 = (SecurityElement)this.stack.Peek();
				securityElement2.AddChild(securityElement);
			}
			this.stack.Push(securityElement);
			this.current = securityElement;
			int length = attrs.Length;
			for (int i = 0; i < length; i++)
			{
				this.current.AddAttribute(attrs.GetName(i), SecurityElement.Escape(attrs.GetValue(i)));
			}
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x00038468 File Offset: 0x00036668
		public void OnEndElement(string name)
		{
			this.current = (SecurityElement)this.stack.Pop();
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x00038480 File Offset: 0x00036680
		public void OnChars(string ch)
		{
			this.current.Text = SecurityElement.Escape(ch);
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x00038494 File Offset: 0x00036694
		public void OnEndParsing(SmallXmlParser parser)
		{
		}

		// Token: 0x04000359 RID: 857
		private SecurityElement root;

		// Token: 0x0400035A RID: 858
		private SecurityElement current;

		// Token: 0x0400035B RID: 859
		private Stack stack;
	}
}
