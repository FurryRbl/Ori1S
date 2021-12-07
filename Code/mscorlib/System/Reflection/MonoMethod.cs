using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Text;
using System.Threading;

namespace System.Reflection
{
	// Token: 0x020002A8 RID: 680
	[Serializable]
	internal class MonoMethod : MethodInfo, ISerializable
	{
		// Token: 0x06002298 RID: 8856 RVA: 0x0007CAE8 File Offset: 0x0007ACE8
		internal MonoMethod()
		{
		}

		// Token: 0x06002299 RID: 8857 RVA: 0x0007CAF0 File Offset: 0x0007ACF0
		internal MonoMethod(RuntimeMethodHandle mhandle)
		{
			this.mhandle = mhandle.Value;
		}

		// Token: 0x0600229A RID: 8858
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string get_name(MethodBase method);

		// Token: 0x0600229B RID: 8859
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern MonoMethod get_base_definition(MonoMethod method);

		// Token: 0x0600229C RID: 8860 RVA: 0x0007CB08 File Offset: 0x0007AD08
		public override MethodInfo GetBaseDefinition()
		{
			return MonoMethod.get_base_definition(this);
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x0600229D RID: 8861 RVA: 0x0007CB10 File Offset: 0x0007AD10
		public override ParameterInfo ReturnParameter
		{
			get
			{
				return MonoMethodInfo.GetReturnParameterInfo(this);
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x0600229E RID: 8862 RVA: 0x0007CB18 File Offset: 0x0007AD18
		public override Type ReturnType
		{
			get
			{
				return MonoMethodInfo.GetReturnType(this.mhandle);
			}
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x0600229F RID: 8863 RVA: 0x0007CB28 File Offset: 0x0007AD28
		public override ICustomAttributeProvider ReturnTypeCustomAttributes
		{
			get
			{
				return MonoMethodInfo.GetReturnParameterInfo(this);
			}
		}

		// Token: 0x060022A0 RID: 8864 RVA: 0x0007CB30 File Offset: 0x0007AD30
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return MonoMethodInfo.GetMethodImplementationFlags(this.mhandle);
		}

		// Token: 0x060022A1 RID: 8865 RVA: 0x0007CB40 File Offset: 0x0007AD40
		public override ParameterInfo[] GetParameters()
		{
			ParameterInfo[] parametersInfo = MonoMethodInfo.GetParametersInfo(this.mhandle, this);
			ParameterInfo[] array = new ParameterInfo[parametersInfo.Length];
			parametersInfo.CopyTo(array, 0);
			return array;
		}

		// Token: 0x060022A2 RID: 8866
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern object InternalInvoke(object obj, object[] parameters, out Exception exc);

		// Token: 0x060022A3 RID: 8867 RVA: 0x0007CB6C File Offset: 0x0007AD6C
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			if (binder == null)
			{
				binder = Binder.DefaultBinder;
			}
			ParameterInfo[] parametersInfo = MonoMethodInfo.GetParametersInfo(this.mhandle, this);
			if ((parameters == null && parametersInfo.Length != 0) || (parameters != null && parameters.Length != parametersInfo.Length))
			{
				throw new TargetParameterCountException("parameters do not match signature");
			}
			if ((invokeAttr & BindingFlags.ExactBinding) == BindingFlags.Default)
			{
				if (!Binder.ConvertArgs(binder, parameters, parametersInfo, culture))
				{
					throw new ArgumentException("failed to convert parameters");
				}
			}
			else
			{
				for (int i = 0; i < parametersInfo.Length; i++)
				{
					if (parameters[i].GetType() != parametersInfo[i].ParameterType)
					{
						throw new ArgumentException("parameters do not match signature");
					}
				}
			}
			if (SecurityManager.SecurityEnabled)
			{
				SecurityManager.ReflectedLinkDemandInvoke(this);
			}
			if (this.ContainsGenericParameters)
			{
				throw new InvalidOperationException("Late bound operations cannot be performed on types or methods for which ContainsGenericParameters is true.");
			}
			object result = null;
			Exception ex;
			try
			{
				result = this.InternalInvoke(obj, parameters, out ex);
			}
			catch (ThreadAbortException)
			{
				throw;
			}
			catch (Exception inner)
			{
				throw new TargetInvocationException(inner);
			}
			if (ex != null)
			{
				throw ex;
			}
			return result;
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x060022A4 RID: 8868 RVA: 0x0007CCB0 File Offset: 0x0007AEB0
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				return new RuntimeMethodHandle(this.mhandle);
			}
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x060022A5 RID: 8869 RVA: 0x0007CCC0 File Offset: 0x0007AEC0
		public override MethodAttributes Attributes
		{
			get
			{
				return MonoMethodInfo.GetAttributes(this.mhandle);
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x060022A6 RID: 8870 RVA: 0x0007CCD0 File Offset: 0x0007AED0
		public override CallingConventions CallingConvention
		{
			get
			{
				return MonoMethodInfo.GetCallingConvention(this.mhandle);
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x060022A7 RID: 8871 RVA: 0x0007CCE0 File Offset: 0x0007AEE0
		public override Type ReflectedType
		{
			get
			{
				return this.reftype;
			}
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x060022A8 RID: 8872 RVA: 0x0007CCE8 File Offset: 0x0007AEE8
		public override Type DeclaringType
		{
			get
			{
				return MonoMethodInfo.GetDeclaringType(this.mhandle);
			}
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x060022A9 RID: 8873 RVA: 0x0007CCF8 File Offset: 0x0007AEF8
		public override string Name
		{
			get
			{
				if (this.name != null)
				{
					return this.name;
				}
				return MonoMethod.get_name(this);
			}
		}

		// Token: 0x060022AA RID: 8874 RVA: 0x0007CD14 File Offset: 0x0007AF14
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		// Token: 0x060022AB RID: 8875 RVA: 0x0007CD20 File Offset: 0x0007AF20
		public override object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, inherit);
		}

		// Token: 0x060022AC RID: 8876 RVA: 0x0007CD2C File Offset: 0x0007AF2C
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
		}

		// Token: 0x060022AD RID: 8877
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern DllImportAttribute GetDllImportAttribute(IntPtr mhandle);

		// Token: 0x060022AE RID: 8878 RVA: 0x0007CD38 File Offset: 0x0007AF38
		internal object[] GetPseudoCustomAttributes()
		{
			int num = 0;
			MonoMethodInfo methodInfo = MonoMethodInfo.GetMethodInfo(this.mhandle);
			if ((methodInfo.iattrs & MethodImplAttributes.PreserveSig) != MethodImplAttributes.IL)
			{
				num++;
			}
			if ((methodInfo.attrs & MethodAttributes.PinvokeImpl) != MethodAttributes.PrivateScope)
			{
				num++;
			}
			if (num == 0)
			{
				return null;
			}
			object[] array = new object[num];
			num = 0;
			if ((methodInfo.iattrs & MethodImplAttributes.PreserveSig) != MethodImplAttributes.IL)
			{
				array[num++] = new PreserveSigAttribute();
			}
			if ((methodInfo.attrs & MethodAttributes.PinvokeImpl) != MethodAttributes.PrivateScope)
			{
				DllImportAttribute dllImportAttribute = MonoMethod.GetDllImportAttribute(this.mhandle);
				if ((methodInfo.iattrs & MethodImplAttributes.PreserveSig) != MethodImplAttributes.IL)
				{
					dllImportAttribute.PreserveSig = true;
				}
				array[num++] = dllImportAttribute;
			}
			return array;
		}

		// Token: 0x060022AF RID: 8879 RVA: 0x0007CDF0 File Offset: 0x0007AFF0
		private static bool ShouldPrintFullName(Type type)
		{
			return type.IsClass && (!type.IsPointer || (!type.GetElementType().IsPrimitive && !type.GetElementType().IsNested));
		}

		// Token: 0x060022B0 RID: 8880 RVA: 0x0007CE3C File Offset: 0x0007B03C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			Type returnType = this.ReturnType;
			if (MonoMethod.ShouldPrintFullName(returnType))
			{
				stringBuilder.Append(returnType.ToString());
			}
			else
			{
				stringBuilder.Append(returnType.Name);
			}
			stringBuilder.Append(" ");
			stringBuilder.Append(this.Name);
			if (this.IsGenericMethod)
			{
				Type[] genericArguments = this.GetGenericArguments();
				stringBuilder.Append("[");
				for (int i = 0; i < genericArguments.Length; i++)
				{
					if (i > 0)
					{
						stringBuilder.Append(",");
					}
					stringBuilder.Append(genericArguments[i].Name);
				}
				stringBuilder.Append("]");
			}
			stringBuilder.Append("(");
			ParameterInfo[] parameters = this.GetParameters();
			for (int j = 0; j < parameters.Length; j++)
			{
				if (j > 0)
				{
					stringBuilder.Append(", ");
				}
				Type type = parameters[j].ParameterType;
				bool isByRef = type.IsByRef;
				if (isByRef)
				{
					type = type.GetElementType();
				}
				if (MonoMethod.ShouldPrintFullName(type))
				{
					stringBuilder.Append(type.ToString());
				}
				else
				{
					stringBuilder.Append(type.Name);
				}
				if (isByRef)
				{
					stringBuilder.Append(" ByRef");
				}
			}
			if ((this.CallingConvention & CallingConventions.VarArgs) != (CallingConventions)0)
			{
				if (parameters.Length > 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append("...");
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x060022B1 RID: 8881 RVA: 0x0007CFE0 File Offset: 0x0007B1E0
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			Type[] genericArguments = (!this.IsGenericMethod || this.IsGenericMethodDefinition) ? null : this.GetGenericArguments();
			MemberInfoSerializationHolder.Serialize(info, this.Name, this.ReflectedType, this.ToString(), MemberTypes.Method, genericArguments);
		}

		// Token: 0x060022B2 RID: 8882 RVA: 0x0007D02C File Offset: 0x0007B22C
		public override MethodInfo MakeGenericMethod(Type[] methodInstantiation)
		{
			if (methodInstantiation == null)
			{
				throw new ArgumentNullException("methodInstantiation");
			}
			for (int i = 0; i < methodInstantiation.Length; i++)
			{
				if (methodInstantiation[i] == null)
				{
					throw new ArgumentNullException();
				}
			}
			MethodInfo methodInfo = this.MakeGenericMethod_impl(methodInstantiation);
			if (methodInfo == null)
			{
				throw new ArgumentException(string.Format("The method has {0} generic parameter(s) but {1} generic argument(s) were provided.", this.GetGenericArguments().Length, methodInstantiation.Length));
			}
			return methodInfo;
		}

		// Token: 0x060022B3 RID: 8883
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern MethodInfo MakeGenericMethod_impl(Type[] types);

		// Token: 0x060022B4 RID: 8884
		[MethodImpl(MethodImplOptions.InternalCall)]
		public override extern Type[] GetGenericArguments();

		// Token: 0x060022B5 RID: 8885
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern MethodInfo GetGenericMethodDefinition_impl();

		// Token: 0x060022B6 RID: 8886 RVA: 0x0007D0A4 File Offset: 0x0007B2A4
		public override MethodInfo GetGenericMethodDefinition()
		{
			MethodInfo genericMethodDefinition_impl = this.GetGenericMethodDefinition_impl();
			if (genericMethodDefinition_impl == null)
			{
				throw new InvalidOperationException();
			}
			return genericMethodDefinition_impl;
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x060022B7 RID: 8887
		public override extern bool IsGenericMethodDefinition { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x060022B8 RID: 8888
		public override extern bool IsGenericMethod { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x060022B9 RID: 8889 RVA: 0x0007D0C8 File Offset: 0x0007B2C8
		public override bool ContainsGenericParameters
		{
			get
			{
				if (this.IsGenericMethod)
				{
					foreach (Type type in this.GetGenericArguments())
					{
						if (type.ContainsGenericParameters)
						{
							return true;
						}
					}
				}
				return this.DeclaringType.ContainsGenericParameters;
			}
		}

		// Token: 0x060022BA RID: 8890 RVA: 0x0007D118 File Offset: 0x0007B318
		public override MethodBody GetMethodBody()
		{
			return MethodBase.GetMethodBody(this.mhandle);
		}

		// Token: 0x04000CED RID: 3309
		internal IntPtr mhandle;

		// Token: 0x04000CEE RID: 3310
		private string name;

		// Token: 0x04000CEF RID: 3311
		private Type reftype;
	}
}
