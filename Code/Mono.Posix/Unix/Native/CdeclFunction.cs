using System;
using System.Collections;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;

namespace Mono.Unix.Native
{
	// Token: 0x02000025 RID: 37
	public sealed class CdeclFunction
	{
		// Token: 0x060001F9 RID: 505 RVA: 0x000084E8 File Offset: 0x000066E8
		public CdeclFunction(string library, string method) : this(library, method, typeof(void))
		{
		}

		// Token: 0x060001FA RID: 506 RVA: 0x000084FC File Offset: 0x000066FC
		public CdeclFunction(string library, string method, Type returnType)
		{
			this.library = library;
			this.method = method;
			this.returnType = returnType;
			this.overloads = new Hashtable();
			this.assemblyName = new AssemblyName();
			this.assemblyName.Name = "Mono.Posix.Imports." + library;
			this.assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(this.assemblyName, AssemblyBuilderAccess.Run);
			this.moduleBuilder = this.assemblyBuilder.DefineDynamicModule(this.assemblyName.Name);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00008584 File Offset: 0x00006784
		public object Invoke(object[] parameters)
		{
			Type[] parameterTypes = CdeclFunction.GetParameterTypes(parameters);
			MethodInfo methodInfo = this.CreateMethod(parameterTypes);
			return methodInfo.Invoke(null, parameters);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x000085A8 File Offset: 0x000067A8
		private MethodInfo CreateMethod(Type[] parameterTypes)
		{
			string typeName = this.GetTypeName(parameterTypes);
			Hashtable obj = this.overloads;
			MethodInfo result;
			lock (obj)
			{
				MethodInfo methodInfo = (MethodInfo)this.overloads[typeName];
				if (methodInfo != null)
				{
					result = methodInfo;
				}
				else
				{
					TypeBuilder typeBuilder = this.CreateType(typeName);
					typeBuilder.DefinePInvokeMethod(this.method, this.library, MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Static | MethodAttributes.PinvokeImpl, CallingConventions.Standard, this.returnType, parameterTypes, CallingConvention.Cdecl, CharSet.Ansi);
					methodInfo = typeBuilder.CreateType().GetMethod(this.method);
					this.overloads.Add(typeName, methodInfo);
					result = methodInfo;
				}
			}
			return result;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00008668 File Offset: 0x00006868
		private TypeBuilder CreateType(string typeName)
		{
			return this.moduleBuilder.DefineType(typeName, TypeAttributes.Public);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00008678 File Offset: 0x00006878
		private static Type GetMarshalType(Type t)
		{
			switch (Type.GetTypeCode(t))
			{
			case TypeCode.Boolean:
			case TypeCode.Char:
			case TypeCode.SByte:
			case TypeCode.Int16:
			case TypeCode.Int32:
				return typeof(int);
			case TypeCode.Byte:
			case TypeCode.UInt16:
			case TypeCode.UInt32:
				return typeof(uint);
			case TypeCode.Int64:
				return typeof(long);
			case TypeCode.UInt64:
				return typeof(ulong);
			case TypeCode.Single:
			case TypeCode.Double:
				return typeof(double);
			default:
				return t;
			}
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00008704 File Offset: 0x00006904
		private string GetTypeName(Type[] parameterTypes)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[").Append(this.library).Append("] ").Append(this.method);
			stringBuilder.Append("(");
			if (parameterTypes.Length > 0)
			{
				stringBuilder.Append(parameterTypes[0]);
			}
			for (int i = 1; i < parameterTypes.Length; i++)
			{
				stringBuilder.Append(",").Append(parameterTypes[i]);
			}
			stringBuilder.Append(") : ").Append(this.returnType.FullName);
			return stringBuilder.ToString();
		}

		// Token: 0x06000200 RID: 512 RVA: 0x000087AC File Offset: 0x000069AC
		private static Type[] GetParameterTypes(object[] parameters)
		{
			Type[] array = new Type[parameters.Length];
			for (int i = 0; i < parameters.Length; i++)
			{
				array[i] = CdeclFunction.GetMarshalType(parameters[i].GetType());
			}
			return array;
		}

		// Token: 0x0400008B RID: 139
		private readonly string library;

		// Token: 0x0400008C RID: 140
		private readonly string method;

		// Token: 0x0400008D RID: 141
		private readonly Type returnType;

		// Token: 0x0400008E RID: 142
		private readonly AssemblyName assemblyName;

		// Token: 0x0400008F RID: 143
		private readonly AssemblyBuilder assemblyBuilder;

		// Token: 0x04000090 RID: 144
		private readonly ModuleBuilder moduleBuilder;

		// Token: 0x04000091 RID: 145
		private Hashtable overloads;
	}
}
