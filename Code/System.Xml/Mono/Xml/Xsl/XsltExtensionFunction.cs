using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Mono.Xml.Xsl
{
	// Token: 0x020000A4 RID: 164
	internal class XsltExtensionFunction : XPFuncImpl
	{
		// Token: 0x060005AA RID: 1450 RVA: 0x00022F28 File Offset: 0x00021128
		public XsltExtensionFunction(object extension, MethodInfo method, XPathNavigator currentNode)
		{
			this.extension = extension;
			this.method = method;
			ParameterInfo[] parameters = method.GetParameters();
			int num = parameters.Length;
			int maxArgs = parameters.Length;
			this.typeCodes = new TypeCode[parameters.Length];
			XPathResultType[] array = new XPathResultType[parameters.Length];
			bool flag = true;
			int num2 = parameters.Length - 1;
			while (0 <= num2)
			{
				this.typeCodes[num2] = Type.GetTypeCode(parameters[num2].ParameterType);
				array[num2] = XPFuncImpl.GetXPathType(parameters[num2].ParameterType, currentNode);
				if (flag)
				{
					if (parameters[num2].IsOptional)
					{
						num--;
					}
					else
					{
						flag = false;
					}
				}
				num2--;
			}
			base.Init(num, maxArgs, XPFuncImpl.GetXPathType(method.ReturnType, currentNode), array);
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00022FEC File Offset: 0x000211EC
		public override object Invoke(XsltCompiledContext xsltContext, object[] args, XPathNavigator docContext)
		{
			object result;
			try
			{
				ParameterInfo[] parameters = this.method.GetParameters();
				object[] array = new object[parameters.Length];
				int i = 0;
				string fullName;
				while (i < args.Length)
				{
					Type parameterType = parameters[i].ParameterType;
					fullName = parameterType.FullName;
					if (fullName == null)
					{
						goto IL_E5;
					}
					if (XsltExtensionFunction.<>f__switch$map26 == null)
					{
						XsltExtensionFunction.<>f__switch$map26 = new Dictionary<string, int>(8)
						{
							{
								"System.Int16",
								0
							},
							{
								"System.UInt16",
								0
							},
							{
								"System.Int32",
								0
							},
							{
								"System.UInt32",
								0
							},
							{
								"System.Int64",
								0
							},
							{
								"System.UInt64",
								0
							},
							{
								"System.Single",
								0
							},
							{
								"System.Decimal",
								0
							}
						};
					}
					int num;
					if (!XsltExtensionFunction.<>f__switch$map26.TryGetValue(fullName, out num))
					{
						goto IL_E5;
					}
					if (num != 0)
					{
						goto IL_E5;
					}
					array[i] = Convert.ChangeType(args[i], parameterType);
					IL_F0:
					i++;
					continue;
					IL_E5:
					array[i] = args[i];
					goto IL_F0;
				}
				fullName = this.method.ReturnType.FullName;
				object obj;
				if (fullName != null)
				{
					if (XsltExtensionFunction.<>f__switch$map27 == null)
					{
						XsltExtensionFunction.<>f__switch$map27 = new Dictionary<string, int>(8)
						{
							{
								"System.Int16",
								0
							},
							{
								"System.UInt16",
								0
							},
							{
								"System.Int32",
								0
							},
							{
								"System.UInt32",
								0
							},
							{
								"System.Int64",
								0
							},
							{
								"System.UInt64",
								0
							},
							{
								"System.Single",
								0
							},
							{
								"System.Decimal",
								0
							}
						};
					}
					int num;
					if (XsltExtensionFunction.<>f__switch$map27.TryGetValue(fullName, out num))
					{
						if (num == 0)
						{
							obj = Convert.ChangeType(this.method.Invoke(this.extension, array), typeof(double));
							goto IL_1FA;
						}
					}
				}
				obj = this.method.Invoke(this.extension, array);
				IL_1FA:
				IXPathNavigable ixpathNavigable = obj as IXPathNavigable;
				if (ixpathNavigable != null)
				{
					result = ixpathNavigable.CreateNavigator();
				}
				else
				{
					result = obj;
				}
			}
			catch (Exception innerException)
			{
				throw new XsltException("Custom function reported an error.", innerException);
			}
			return result;
		}

		// Token: 0x0400039C RID: 924
		private object extension;

		// Token: 0x0400039D RID: 925
		private MethodInfo method;

		// Token: 0x0400039E RID: 926
		private TypeCode[] typeCodes;
	}
}
