using System;
using System.Collections;
using System.Security;

namespace Mono.Xml
{
	// Token: 0x020000C7 RID: 199
	[CLSCompliant(false)]
	public class SecurityParser : MiniParser, MiniParser.IReader, MiniParser.IHandler
	{
		// Token: 0x060006FB RID: 1787 RVA: 0x00026A1C File Offset: 0x00024C1C
		public SecurityParser()
		{
			this.stack = new Stack();
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00026A30 File Offset: 0x00024C30
		public void LoadXml(string xml)
		{
			this.root = null;
			this.xmldoc = xml;
			this.pos = 0;
			this.stack.Clear();
			base.Parse(this, this);
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x00026A68 File Offset: 0x00024C68
		public SecurityElement ToXml()
		{
			return this.root;
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x00026A70 File Offset: 0x00024C70
		public int Read()
		{
			if (this.pos >= this.xmldoc.Length)
			{
				return -1;
			}
			return (int)this.xmldoc[this.pos++];
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x00026AB4 File Offset: 0x00024CB4
		public void OnStartParsing(MiniParser parser)
		{
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x00026AB8 File Offset: 0x00024CB8
		public void OnStartElement(string name, MiniParser.IAttrList attrs)
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

		// Token: 0x06000701 RID: 1793 RVA: 0x00026B4C File Offset: 0x00024D4C
		public void OnEndElement(string name)
		{
			this.current = (SecurityElement)this.stack.Pop();
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00026B64 File Offset: 0x00024D64
		public void OnChars(string ch)
		{
			this.current.Text = SecurityElement.Escape(ch);
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x00026B78 File Offset: 0x00024D78
		public void OnEndParsing(MiniParser parser)
		{
		}

		// Token: 0x0400035F RID: 863
		private SecurityElement root;

		// Token: 0x04000360 RID: 864
		private string xmldoc;

		// Token: 0x04000361 RID: 865
		private int pos;

		// Token: 0x04000362 RID: 866
		private SecurityElement current;

		// Token: 0x04000363 RID: 867
		private Stack stack;
	}
}
