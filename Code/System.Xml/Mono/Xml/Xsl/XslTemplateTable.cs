using System;
using System.Collections;
using System.Xml;
using System.Xml.XPath;

namespace Mono.Xml.Xsl
{
	// Token: 0x0200009A RID: 154
	internal class XslTemplateTable
	{
		// Token: 0x0600052A RID: 1322 RVA: 0x000210A8 File Offset: 0x0001F2A8
		public XslTemplateTable(XslStylesheet parent)
		{
			this.parent = parent;
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x000210D0 File Offset: 0x0001F2D0
		public Hashtable TemplateTables
		{
			get
			{
				return this.templateTables;
			}
		}

		// Token: 0x1700011F RID: 287
		public XslModedTemplateTable this[XmlQualifiedName mode]
		{
			get
			{
				return this.templateTables[mode] as XslModedTemplateTable;
			}
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x000210EC File Offset: 0x0001F2EC
		public void Add(XslTemplate template)
		{
			if (template.Name != XmlQualifiedName.Empty)
			{
				if (this.namedTemplates[template.Name] != null)
				{
					throw new InvalidOperationException("Named template " + template.Name + " is already registered.");
				}
				this.namedTemplates[template.Name] = template;
			}
			if (template.Match == null)
			{
				return;
			}
			XslModedTemplateTable xslModedTemplateTable = this[template.Mode];
			if (xslModedTemplateTable == null)
			{
				xslModedTemplateTable = new XslModedTemplateTable(template.Mode);
				this.Add(xslModedTemplateTable);
			}
			xslModedTemplateTable.Add(template);
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0002118C File Offset: 0x0001F38C
		public void Add(XslModedTemplateTable table)
		{
			if (this[table.Mode] != null)
			{
				throw new InvalidOperationException("Mode " + table.Mode + " is already registered.");
			}
			this.templateTables.Add(table.Mode, table);
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x000211D8 File Offset: 0x0001F3D8
		public XslTemplate FindMatch(XPathNavigator node, XmlQualifiedName mode, XslTransformProcessor p)
		{
			if (this[mode] != null)
			{
				XslTemplate xslTemplate = this[mode].FindMatch(node, p);
				if (xslTemplate != null)
				{
					return xslTemplate;
				}
			}
			for (int i = this.parent.Imports.Count - 1; i >= 0; i--)
			{
				XslStylesheet xslStylesheet = (XslStylesheet)this.parent.Imports[i];
				XslTemplate xslTemplate = xslStylesheet.Templates.FindMatch(node, mode, p);
				if (xslTemplate != null)
				{
					return xslTemplate;
				}
			}
			return null;
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0002125C File Offset: 0x0001F45C
		public XslTemplate FindTemplate(XmlQualifiedName name)
		{
			XslTemplate xslTemplate = (XslTemplate)this.namedTemplates[name];
			if (xslTemplate != null)
			{
				return xslTemplate;
			}
			for (int i = this.parent.Imports.Count - 1; i >= 0; i--)
			{
				XslStylesheet xslStylesheet = (XslStylesheet)this.parent.Imports[i];
				xslTemplate = xslStylesheet.Templates.FindTemplate(name);
				if (xslTemplate != null)
				{
					return xslTemplate;
				}
			}
			return null;
		}

		// Token: 0x04000366 RID: 870
		private Hashtable templateTables = new Hashtable();

		// Token: 0x04000367 RID: 871
		private Hashtable namedTemplates = new Hashtable();

		// Token: 0x04000368 RID: 872
		private XslStylesheet parent;
	}
}
