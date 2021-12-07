using System;
using System.Reflection;
using System.Xml.XPath;

namespace Mono.Xml.Xsl
{
	// Token: 0x020000A2 RID: 162
	internal class XsltDebuggerWrapper
	{
		// Token: 0x0600059D RID: 1437 RVA: 0x00022D3C File Offset: 0x00020F3C
		public XsltDebuggerWrapper(object impl)
		{
			this.impl = impl;
			this.on_compile = impl.GetType().GetMethod("OnCompile", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (this.on_compile == null)
			{
				throw new InvalidOperationException("INTERNAL ERROR: the debugger does not look like what System.Xml.dll expects. OnCompile method was not found");
			}
			this.on_execute = impl.GetType().GetMethod("OnExecute", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (this.on_execute == null)
			{
				throw new InvalidOperationException("INTERNAL ERROR: the debugger does not look like what System.Xml.dll expects. OnExecute method was not found");
			}
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x00022DB4 File Offset: 0x00020FB4
		public void DebugCompile(XPathNavigator style)
		{
			this.on_compile.Invoke(this.impl, new object[]
			{
				style.Clone()
			});
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x00022DD8 File Offset: 0x00020FD8
		public void DebugExecute(XslTransformProcessor p, XPathNavigator style)
		{
			this.on_execute.Invoke(this.impl, new object[]
			{
				p.CurrentNodeset.Clone(),
				style.Clone(),
				p.XPathContext
			});
		}

		// Token: 0x04000395 RID: 917
		private readonly MethodInfo on_compile;

		// Token: 0x04000396 RID: 918
		private readonly MethodInfo on_execute;

		// Token: 0x04000397 RID: 919
		private readonly object impl;
	}
}
