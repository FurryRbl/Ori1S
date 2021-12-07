using System;
using System.Reflection;
using UWPCompat;
using UWPCompat.Extensions;

// Token: 0x02000197 RID: 407
public static class ReflectionHelper
{
	// Token: 0x06000FC2 RID: 4034 RVA: 0x000488E0 File Offset: 0x00046AE0
	public static object DoInvoke(Type type, string methodName, object[] parameters)
	{
		Type[] array = new Type[parameters.Length];
		for (int i = 0; i < parameters.Length; i++)
		{
			array[i] = parameters[i].GetType();
		}
		MethodInfo method = type.GetMethod(methodName, UWPCompat.BindingFlags.Static | UWPCompat.BindingFlags.Public | UWPCompat.BindingFlags.NonPublic, array);
		return ReflectionHelper.DoInvoke2(type, method, parameters);
	}

	// Token: 0x06000FC3 RID: 4035 RVA: 0x00048928 File Offset: 0x00046B28
	public static object DoInvoke2(Type type, MethodInfo method, object[] parameters)
	{
		if (method.IsStatic)
		{
			return method.Invoke(null, parameters);
		}
		object obj = type.InvokeMember(null, UWPCompat.BindingFlags.DeclaredOnly | UWPCompat.BindingFlags.Instance | UWPCompat.BindingFlags.Public | UWPCompat.BindingFlags.NonPublic | UWPCompat.BindingFlags.CreateInstance, null, new object[0]);
		return method.Invoke(obj, parameters);
	}
}
