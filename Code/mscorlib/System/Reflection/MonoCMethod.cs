using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;
using System.Text;

namespace System.Reflection
{
	// Token: 0x020002A9 RID: 681
	internal class MonoCMethod : ConstructorInfo, ISerializable
	{
		// Token: 0x060022BC RID: 8892 RVA: 0x0007D130 File Offset: 0x0007B330
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return MonoMethodInfo.GetMethodImplementationFlags(this.mhandle);
		}

		// Token: 0x060022BD RID: 8893 RVA: 0x0007D140 File Offset: 0x0007B340
		public override ParameterInfo[] GetParameters()
		{
			return MonoMethodInfo.GetParametersInfo(this.mhandle, this);
		}

		// Token: 0x060022BE RID: 8894
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern object InternalInvoke(object obj, object[] parameters, out Exception exc);

		// Token: 0x060022BF RID: 8895 RVA: 0x0007D150 File Offset: 0x0007B350
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			if (binder == null)
			{
				binder = Binder.DefaultBinder;
			}
			ParameterInfo[] parameters2 = this.GetParameters();
			if ((parameters == null && parameters2.Length != 0) || (parameters != null && parameters.Length != parameters2.Length))
			{
				throw new TargetParameterCountException("parameters do not match signature");
			}
			if ((invokeAttr & BindingFlags.ExactBinding) == BindingFlags.Default)
			{
				if (!Binder.ConvertArgs(binder, parameters, parameters2, culture))
				{
					throw new ArgumentException("failed to convert parameters");
				}
			}
			else
			{
				for (int i = 0; i < parameters2.Length; i++)
				{
					if (parameters[i].GetType() != parameters2[i].ParameterType)
					{
						throw new ArgumentException("parameters do not match signature");
					}
				}
			}
			if (SecurityManager.SecurityEnabled)
			{
				SecurityManager.ReflectedLinkDemandInvoke(this);
			}
			if (obj == null && this.DeclaringType.ContainsGenericParameters)
			{
				throw new MemberAccessException("Cannot create an instance of " + this.DeclaringType + " because Type.ContainsGenericParameters is true.");
			}
			if ((invokeAttr & BindingFlags.CreateInstance) != BindingFlags.Default && this.DeclaringType.IsAbstract)
			{
				throw new MemberAccessException(string.Format("Cannot create an instance of {0} because it is an abstract class", this.DeclaringType));
			}
			Exception ex = null;
			object obj2 = null;
			try
			{
				obj2 = this.InternalInvoke(obj, parameters, out ex);
			}
			catch (Exception inner)
			{
				throw new TargetInvocationException(inner);
			}
			if (ex != null)
			{
				throw ex;
			}
			return (obj != null) ? null : obj2;
		}

		// Token: 0x060022C0 RID: 8896 RVA: 0x0007D2C8 File Offset: 0x0007B4C8
		public override object Invoke(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			return this.Invoke(null, invokeAttr, binder, parameters, culture);
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x060022C1 RID: 8897 RVA: 0x0007D2D8 File Offset: 0x0007B4D8
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				return new RuntimeMethodHandle(this.mhandle);
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x060022C2 RID: 8898 RVA: 0x0007D2E8 File Offset: 0x0007B4E8
		public override MethodAttributes Attributes
		{
			get
			{
				return MonoMethodInfo.GetAttributes(this.mhandle);
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x060022C3 RID: 8899 RVA: 0x0007D2F8 File Offset: 0x0007B4F8
		public override CallingConventions CallingConvention
		{
			get
			{
				return MonoMethodInfo.GetCallingConvention(this.mhandle);
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x060022C4 RID: 8900 RVA: 0x0007D308 File Offset: 0x0007B508
		public override Type ReflectedType
		{
			get
			{
				return this.reftype;
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x060022C5 RID: 8901 RVA: 0x0007D310 File Offset: 0x0007B510
		public override Type DeclaringType
		{
			get
			{
				return MonoMethodInfo.GetDeclaringType(this.mhandle);
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x060022C6 RID: 8902 RVA: 0x0007D320 File Offset: 0x0007B520
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

		// Token: 0x060022C7 RID: 8903 RVA: 0x0007D33C File Offset: 0x0007B53C
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		// Token: 0x060022C8 RID: 8904 RVA: 0x0007D348 File Offset: 0x0007B548
		public override object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, inherit);
		}

		// Token: 0x060022C9 RID: 8905 RVA: 0x0007D354 File Offset: 0x0007B554
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
		}

		// Token: 0x060022CA RID: 8906 RVA: 0x0007D360 File Offset: 0x0007B560
		public override MethodBody GetMethodBody()
		{
			return MethodBase.GetMethodBody(this.mhandle);
		}

		// Token: 0x060022CB RID: 8907 RVA: 0x0007D370 File Offset: 0x0007B570
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Void ");
			stringBuilder.Append(this.Name);
			stringBuilder.Append("(");
			ParameterInfo[] parameters = this.GetParameters();
			for (int i = 0; i < parameters.Length; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(parameters[i].ParameterType.Name);
			}
			if (this.CallingConvention == CallingConventions.Any)
			{
				stringBuilder.Append(", ...");
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x060022CC RID: 8908 RVA: 0x0007D414 File Offset: 0x0007B614
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			MemberInfoSerializationHolder.Serialize(info, this.Name, this.ReflectedType, this.ToString(), MemberTypes.Constructor);
		}

		// Token: 0x04000CF0 RID: 3312
		internal IntPtr mhandle;

		// Token: 0x04000CF1 RID: 3313
		private string name;

		// Token: 0x04000CF2 RID: 3314
		private Type reftype;
	}
}
