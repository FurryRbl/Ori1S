using System;
using System.Reflection;

namespace UWPCompat
{
	// Token: 0x02000864 RID: 2148
	public static class ReflectionHelpers
	{
		// Token: 0x06003097 RID: 12439 RVA: 0x000CEA21 File Offset: 0x000CCC21
		public static void BindValueConvert(ref int outFlags, BindingFlags flags, BindingFlags from, BindingFlags to)
		{
			outFlags |= (((flags & from) != from) ? 0 : Convert.ToInt32(to));
		}

		// Token: 0x06003098 RID: 12440 RVA: 0x000CEA44 File Offset: 0x000CCC44
		public static BindingFlags ConvertBindingFlags(BindingFlags flags)
		{
			int result = 0;
			ReflectionHelpers.BindValueConvert(ref result, flags, BindingFlags.IgnoreCase, BindingFlags.IgnoreCase);
			ReflectionHelpers.BindValueConvert(ref result, flags, BindingFlags.DeclaredOnly, BindingFlags.DeclaredOnly);
			ReflectionHelpers.BindValueConvert(ref result, flags, BindingFlags.Static, BindingFlags.Static);
			ReflectionHelpers.BindValueConvert(ref result, flags, BindingFlags.Instance, BindingFlags.Instance);
			ReflectionHelpers.BindValueConvert(ref result, flags, BindingFlags.Public, BindingFlags.Public);
			ReflectionHelpers.BindValueConvert(ref result, flags, BindingFlags.NonPublic, BindingFlags.NonPublic);
			ReflectionHelpers.BindValueConvert(ref result, flags, BindingFlags.FlattenHierarchy, BindingFlags.FlattenHierarchy);
			ReflectionHelpers.BindValueConvert(ref result, flags, BindingFlags.InvokeMethod, BindingFlags.InvokeMethod);
			ReflectionHelpers.BindValueConvert(ref result, flags, BindingFlags.CreateInstance, BindingFlags.CreateInstance);
			ReflectionHelpers.BindValueConvert(ref result, flags, BindingFlags.GetField, BindingFlags.GetField);
			ReflectionHelpers.BindValueConvert(ref result, flags, BindingFlags.SetField, BindingFlags.SetField);
			ReflectionHelpers.BindValueConvert(ref result, flags, BindingFlags.GetProperty, BindingFlags.GetProperty);
			ReflectionHelpers.BindValueConvert(ref result, flags, BindingFlags.SetProperty, BindingFlags.SetProperty);
			ReflectionHelpers.BindValueConvert(ref result, flags, BindingFlags.PutDispProperty, BindingFlags.PutDispProperty);
			ReflectionHelpers.BindValueConvert(ref result, flags, BindingFlags.PutRefDispProperty, BindingFlags.PutRefDispProperty);
			ReflectionHelpers.BindValueConvert(ref result, flags, BindingFlags.ExactBinding, BindingFlags.ExactBinding);
			ReflectionHelpers.BindValueConvert(ref result, flags, BindingFlags.SuppressChangeType, BindingFlags.SuppressChangeType);
			ReflectionHelpers.BindValueConvert(ref result, flags, BindingFlags.OptionalParamBinding, BindingFlags.OptionalParamBinding);
			ReflectionHelpers.BindValueConvert(ref result, flags, BindingFlags.IgnoreReturn, BindingFlags.IgnoreReturn);
			return (BindingFlags)result;
		}
	}
}
