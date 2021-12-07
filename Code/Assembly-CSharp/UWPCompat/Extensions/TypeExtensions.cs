using System;
using System.Linq;
using System.Reflection;

namespace UWPCompat.Extensions
{
	// Token: 0x02000198 RID: 408
	public static class TypeExtensions
	{
		// Token: 0x06000FC4 RID: 4036 RVA: 0x00048965 File Offset: 0x00046B65
		public static Type BaseType(this Type type)
		{
			return type.BaseType;
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x0004896D File Offset: 0x00046B6D
		public static FieldInfo GetField(this Type type, string name)
		{
			return type.GetField(name);
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x00048976 File Offset: 0x00046B76
		public static FieldInfo[] GetFields(this Type type)
		{
			return type.GetFields();
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x0004897E File Offset: 0x00046B7E
		public static FieldInfo[] GetFields(this Type type, BindingFlags flags)
		{
			return type.GetFields(ReflectionHelpers.ConvertBindingFlags(flags));
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x0004898C File Offset: 0x00046B8C
		public static Type[] GetInterfaces(this Type type)
		{
			return type.GetInterfaces();
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x00048994 File Offset: 0x00046B94
		public static MethodInfo GetMethod(this Type type, string name)
		{
			return type.GetMethod(name);
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x0004899D File Offset: 0x00046B9D
		public static MethodInfo GetMethod(this Type type, string name, Type[] types)
		{
			return type.GetMethod(name, types);
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x000489A8 File Offset: 0x00046BA8
		public static MethodInfo GetMethod(this Type type, string name, object[] parameters)
		{
			Type[] types = (from p in parameters
			select p.GetType()).ToArray<Type>();
			return type.GetMethod(name, types);
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x000489E6 File Offset: 0x00046BE6
		public static MethodInfo GetMethod(this Type type, string name, BindingFlags flags)
		{
			return type.GetMethod(name, ReflectionHelpers.ConvertBindingFlags(flags));
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x000489F5 File Offset: 0x00046BF5
		public static MethodInfo GetMethod(this Type type, string name, BindingFlags flags, Type[] types)
		{
			return type.GetMethod(name, ReflectionHelpers.ConvertBindingFlags(flags), null, types, null);
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x00048A07 File Offset: 0x00046C07
		public static PropertyInfo GetProperty(this Type type, string name)
		{
			return type.GetProperty(name);
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x00048A10 File Offset: 0x00046C10
		public static PropertyInfo[] GetProperties(this Type type)
		{
			return type.GetProperties();
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x00048A18 File Offset: 0x00046C18
		public static object InvokeMember(this Type type, string name, BindingFlags flags, object target, object[] args)
		{
			return type.InvokeMember(name, ReflectionHelpers.ConvertBindingFlags(flags), null, target, args);
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x00048A2B File Offset: 0x00046C2B
		public static bool IsAssignableFrom(this Type type, Type other)
		{
			return type.IsAssignableFrom(other);
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x00048A34 File Offset: 0x00046C34
		public static bool IsDefined(this Type type, Type attributeType)
		{
			return type.IsDefined(attributeType);
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x00048A3D File Offset: 0x00046C3D
		public static bool IsDefined(this Type type, Type attributeType, bool inherit)
		{
			return type.IsDefined(attributeType, inherit);
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x00048A47 File Offset: 0x00046C47
		public static bool IsValueType(this Type type)
		{
			return type.IsValueType;
		}
	}
}
