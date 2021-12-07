using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Mono.Xml.Xsl.Operations
{
	// Token: 0x0200004F RID: 79
	internal class XslAvt
	{
		// Token: 0x06000346 RID: 838 RVA: 0x00015FA0 File Offset: 0x000141A0
		public XslAvt(string str, Compiler comp)
		{
			if (str.IndexOf("{") == -1 && str.IndexOf("}") == -1)
			{
				this.simpleString = str;
				return;
			}
			this.avtParts = new ArrayList();
			StringBuilder stringBuilder = new StringBuilder();
			StringReader stringReader = new StringReader(str);
			while (stringReader.Peek() != -1)
			{
				char c = (char)stringReader.Read();
				switch (c)
				{
				case '{':
					if ((ushort)stringReader.Peek() == 123)
					{
						stringBuilder.Append((char)stringReader.Read());
						continue;
					}
					if (stringBuilder.Length != 0)
					{
						this.avtParts.Add(new XslAvt.SimpleAvtPart(stringBuilder.ToString()));
						stringBuilder.Length = 0;
					}
					while ((c = (char)stringReader.Read()) != '}')
					{
						char c2 = c;
						if (c2 != '"' && c2 != '\'')
						{
							stringBuilder.Append(c);
						}
						else
						{
							char c3 = c;
							stringBuilder.Append(c);
							while ((c = (char)stringReader.Read()) != c3)
							{
								stringBuilder.Append(c);
								if (stringReader.Peek() == -1)
								{
									throw new XsltCompileException("Unexpected end of AVT", null, comp.Input);
								}
							}
							stringBuilder.Append(c);
						}
						if (stringReader.Peek() == -1)
						{
							throw new XsltCompileException("Unexpected end of AVT", null, comp.Input);
						}
					}
					this.avtParts.Add(new XslAvt.XPathAvtPart(comp.CompileExpression(stringBuilder.ToString())));
					stringBuilder.Length = 0;
					continue;
				case '}':
					c = (char)stringReader.Read();
					if (c != '}')
					{
						throw new XsltCompileException("Braces must be escaped", null, comp.Input);
					}
					break;
				}
				stringBuilder.Append(c);
			}
			if (stringBuilder.Length != 0)
			{
				this.avtParts.Add(new XslAvt.SimpleAvtPart(stringBuilder.ToString()));
				stringBuilder.Length = 0;
			}
		}

		// Token: 0x06000347 RID: 839 RVA: 0x000161A8 File Offset: 0x000143A8
		public static string AttemptPreCalc(ref XslAvt avt)
		{
			if (avt == null)
			{
				return null;
			}
			if (avt.simpleString != null)
			{
				string result = avt.simpleString;
				avt = null;
				return result;
			}
			return null;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x000161D8 File Offset: 0x000143D8
		public string Evaluate(XslTransformProcessor p)
		{
			if (this.simpleString != null)
			{
				return this.simpleString;
			}
			if (this.avtParts.Count == 1)
			{
				return ((XslAvt.AvtPart)this.avtParts[0]).Evaluate(p);
			}
			StringBuilder avtStringBuilder = p.GetAvtStringBuilder();
			int count = this.avtParts.Count;
			for (int i = 0; i < count; i++)
			{
				avtStringBuilder.Append(((XslAvt.AvtPart)this.avtParts[i]).Evaluate(p));
			}
			return p.ReleaseAvtStringBuilder();
		}

		// Token: 0x0400023E RID: 574
		private string simpleString;

		// Token: 0x0400023F RID: 575
		private ArrayList avtParts;

		// Token: 0x02000050 RID: 80
		private abstract class AvtPart
		{
			// Token: 0x0600034A RID: 842
			public abstract string Evaluate(XslTransformProcessor p);
		}

		// Token: 0x02000051 RID: 81
		private sealed class SimpleAvtPart : XslAvt.AvtPart
		{
			// Token: 0x0600034B RID: 843 RVA: 0x00016274 File Offset: 0x00014474
			public SimpleAvtPart(string val)
			{
				this.val = val;
			}

			// Token: 0x0600034C RID: 844 RVA: 0x00016284 File Offset: 0x00014484
			public override string Evaluate(XslTransformProcessor p)
			{
				return this.val;
			}

			// Token: 0x04000240 RID: 576
			private string val;
		}

		// Token: 0x02000052 RID: 82
		private sealed class XPathAvtPart : XslAvt.AvtPart
		{
			// Token: 0x0600034D RID: 845 RVA: 0x0001628C File Offset: 0x0001448C
			public XPathAvtPart(XPathExpression expr)
			{
				this.expr = expr;
			}

			// Token: 0x0600034E RID: 846 RVA: 0x0001629C File Offset: 0x0001449C
			public override string Evaluate(XslTransformProcessor p)
			{
				return p.EvaluateString(this.expr);
			}

			// Token: 0x04000241 RID: 577
			private XPathExpression expr;
		}
	}
}
