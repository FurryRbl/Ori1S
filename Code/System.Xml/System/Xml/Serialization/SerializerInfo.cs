using System;
using System.Collections;
using System.ComponentModel;

namespace System.Xml.Serialization
{
	// Token: 0x02000262 RID: 610
	[XmlType("serializer")]
	internal class SerializerInfo
	{
		// Token: 0x060018FE RID: 6398 RVA: 0x00084168 File Offset: 0x00082368
		public ArrayList GetHooks(HookType hookType, XmlMappingAccess dir, HookAction action, Type type, string member)
		{
			if ((dir & XmlMappingAccess.Read) != XmlMappingAccess.None)
			{
				return this.FindHook(this.ReaderHooks, hookType, action, type, member);
			}
			if ((dir & XmlMappingAccess.Write) != XmlMappingAccess.None)
			{
				return this.FindHook(this.WriterHooks, hookType, action, type, member);
			}
			throw new Exception("INTERNAL ERROR");
		}

		// Token: 0x060018FF RID: 6399 RVA: 0x000841B8 File Offset: 0x000823B8
		private ArrayList FindHook(Hook[] hooks, HookType hookType, HookAction action, Type type, string member)
		{
			ArrayList arrayList = new ArrayList();
			if (hooks == null)
			{
				return arrayList;
			}
			foreach (Hook hook in hooks)
			{
				if (action != HookAction.InsertBefore || (hook.InsertBefore != null && !(hook.InsertBefore == string.Empty)))
				{
					if (action != HookAction.InsertAfter || (hook.InsertAfter != null && !(hook.InsertAfter == string.Empty)))
					{
						if (action != HookAction.Replace || (hook.Replace != null && !(hook.Replace == string.Empty)))
						{
							if (hook.HookType == hookType)
							{
								if (hook.Select != null)
								{
									if (hook.Select.TypeName != null && hook.Select.TypeName != string.Empty && hook.Select.TypeName != type.FullName)
									{
										goto IL_1DD;
									}
									if (hook.Select.TypeMember != null && hook.Select.TypeMember != string.Empty && hook.Select.TypeMember != member)
									{
										goto IL_1DD;
									}
									if (hook.Select.TypeAttributes != null && hook.Select.TypeAttributes.Length > 0)
									{
										object[] customAttributes = type.GetCustomAttributes(true);
										bool flag = false;
										foreach (object obj in customAttributes)
										{
											if (Array.IndexOf<string>(hook.Select.TypeAttributes, obj.GetType().FullName) != -1)
											{
												flag = true;
												break;
											}
										}
										if (!flag)
										{
											goto IL_1DD;
										}
									}
								}
								arrayList.Add(hook);
							}
						}
					}
				}
				IL_1DD:;
			}
			return arrayList;
		}

		// Token: 0x04000A47 RID: 2631
		[XmlAttribute("class")]
		public string ClassName;

		// Token: 0x04000A48 RID: 2632
		[XmlAttribute("assembly")]
		public string Assembly;

		// Token: 0x04000A49 RID: 2633
		[XmlElement("reader")]
		public string ReaderClassName;

		// Token: 0x04000A4A RID: 2634
		[XmlElement("writer")]
		public string WriterClassName;

		// Token: 0x04000A4B RID: 2635
		[XmlElement("baseSerializer")]
		public string BaseSerializerClassName;

		// Token: 0x04000A4C RID: 2636
		[XmlElement("implementation")]
		public string ImplementationClassName;

		// Token: 0x04000A4D RID: 2637
		[XmlElement("noreader")]
		public bool NoReader;

		// Token: 0x04000A4E RID: 2638
		[XmlElement("nowriter")]
		public bool NoWriter;

		// Token: 0x04000A4F RID: 2639
		[XmlElement("generateAsInternal")]
		public bool GenerateAsInternal;

		// Token: 0x04000A50 RID: 2640
		[XmlElement("namespace")]
		public string Namespace;

		// Token: 0x04000A51 RID: 2641
		[XmlArrayItem("namespaceImport")]
		[XmlArray("namespaceImports")]
		public string[] NamespaceImports;

		// Token: 0x04000A52 RID: 2642
		[DefaultValue(SerializationFormat.Literal)]
		public SerializationFormat SerializationFormat = SerializationFormat.Literal;

		// Token: 0x04000A53 RID: 2643
		[XmlElement("outFileName")]
		public string OutFileName;

		// Token: 0x04000A54 RID: 2644
		[XmlArray("readerHooks")]
		public Hook[] ReaderHooks;

		// Token: 0x04000A55 RID: 2645
		[XmlArray("writerHooks")]
		public Hook[] WriterHooks;
	}
}
