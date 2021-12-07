using System;
using System.Runtime.CompilerServices;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Represents a performance object, which defines a category of performance counters.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000235 RID: 565
	[PermissionSet((SecurityAction)14, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\"\nUnrestricted=\"true\"/>\n")]
	public sealed class PerformanceCounterCategory
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.PerformanceCounterCategory" /> class, leaves the <see cref="P:System.Diagnostics.PerformanceCounterCategory.CategoryName" /> property empty, and sets the <see cref="P:System.Diagnostics.PerformanceCounterCategory.MachineName" /> property to the local computer.</summary>
		// Token: 0x0600136F RID: 4975 RVA: 0x00033ED4 File Offset: 0x000320D4
		public PerformanceCounterCategory() : this(string.Empty, ".")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.PerformanceCounterCategory" /> class, sets the <see cref="P:System.Diagnostics.PerformanceCounterCategory.CategoryName" /> property to the specified value, and sets the <see cref="P:System.Diagnostics.PerformanceCounterCategory.MachineName" /> property to the local computer.</summary>
		/// <param name="categoryName">The name of the performance counter category, or performance object, with which to associate this <see cref="T:System.Diagnostics.PerformanceCounterCategory" /> instance. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="categoryName" /> is an empty string (""). </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="categoryName" /> is null. </exception>
		// Token: 0x06001370 RID: 4976 RVA: 0x00033EE8 File Offset: 0x000320E8
		public PerformanceCounterCategory(string categoryName) : this(categoryName, ".")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.PerformanceCounterCategory" /> class and sets the <see cref="P:System.Diagnostics.PerformanceCounterCategory.CategoryName" /> and <see cref="P:System.Diagnostics.PerformanceCounterCategory.MachineName" /> properties to the specified values.</summary>
		/// <param name="categoryName">The name of the performance counter category, or performance object, with which to associate this <see cref="T:System.Diagnostics.PerformanceCounterCategory" /> instance. </param>
		/// <param name="machineName">The computer on which the performance counter category and its associated counters exist. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="categoryName" /> is an empty string ("").-or- The <paramref name="machineName" /> syntax is invalid. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="categoryName" /> is null. </exception>
		// Token: 0x06001371 RID: 4977 RVA: 0x00033EF8 File Offset: 0x000320F8
		public PerformanceCounterCategory(string categoryName, string machineName)
		{
			PerformanceCounterCategory.CheckCategory(categoryName);
			if (machineName == null)
			{
				throw new ArgumentNullException("machineName");
			}
			this.categoryName = categoryName;
			this.machineName = machineName;
		}

		// Token: 0x06001372 RID: 4978
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CategoryDelete(string name);

		// Token: 0x06001373 RID: 4979
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string CategoryHelpInternal(string category, string machine);

		// Token: 0x06001374 RID: 4980
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CounterCategoryExists(string counter, string category, string machine);

		// Token: 0x06001375 RID: 4981
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Create(string categoryName, string categoryHelp, PerformanceCounterCategoryType categoryType, CounterCreationData[] items);

		// Token: 0x06001376 RID: 4982
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int InstanceExistsInternal(string instance, string category, string machine);

		// Token: 0x06001377 RID: 4983
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] GetCategoryNames(string machine);

		// Token: 0x06001378 RID: 4984
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] GetCounterNames(string category, string machine);

		// Token: 0x06001379 RID: 4985
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] GetInstanceNames(string category, string machine);

		// Token: 0x0600137A RID: 4986 RVA: 0x00033F38 File Offset: 0x00032138
		private static void CheckCategory(string categoryName)
		{
			if (categoryName == null)
			{
				throw new ArgumentNullException("categoryName");
			}
			if (categoryName == string.Empty)
			{
				throw new ArgumentException("categoryName");
			}
		}

		/// <summary>Gets the category's help text.</summary>
		/// <returns>A description of the performance object that this category measures.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.PerformanceCounterCategory.CategoryName" /> property is null. The category name must be set before getting the category help. </exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x0600137B RID: 4987 RVA: 0x00033F74 File Offset: 0x00032174
		public string CategoryHelp
		{
			get
			{
				string text = PerformanceCounterCategory.CategoryHelpInternal(this.categoryName, this.machineName);
				if (text != null)
				{
					return text;
				}
				throw new InvalidOperationException();
			}
		}

		/// <summary>Gets or sets the name of the performance object that defines this category.</summary>
		/// <returns>The name of the performance counter category, or performance object, with which to associate this <see cref="T:System.Diagnostics.PerformanceCounterCategory" /> instance.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Diagnostics.PerformanceCounterCategory.CategoryName" /> is an empty string (""). </exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Diagnostics.PerformanceCounterCategory.CategoryName" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x0600137C RID: 4988 RVA: 0x00033FA0 File Offset: 0x000321A0
		// (set) Token: 0x0600137D RID: 4989 RVA: 0x00033FA8 File Offset: 0x000321A8
		public string CategoryName
		{
			get
			{
				return this.categoryName;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value == string.Empty)
				{
					throw new ArgumentException("value");
				}
				this.categoryName = value;
			}
		}

		/// <summary>Gets or sets the name of the computer on which this category exists.</summary>
		/// <returns>The name of the computer on which the performance counter category and its associated counters exist.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Diagnostics.PerformanceCounterCategory.MachineName" /> syntax is invalid. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x0600137E RID: 4990 RVA: 0x00033FE0 File Offset: 0x000321E0
		// (set) Token: 0x0600137F RID: 4991 RVA: 0x00033FE8 File Offset: 0x000321E8
		public string MachineName
		{
			get
			{
				return this.machineName;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value == string.Empty)
				{
					throw new ArgumentException("value");
				}
				this.machineName = value;
			}
		}

		/// <summary>Gets the performance counter category type.</summary>
		/// <returns>One of the <see cref="T:System.Diagnostics.PerformanceCounterCategoryType" /> values. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06001380 RID: 4992 RVA: 0x00034020 File Offset: 0x00032220
		public PerformanceCounterCategoryType CategoryType
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Determines whether the specified counter is registered to this category, which is indicated by the <see cref="P:System.Diagnostics.PerformanceCounterCategory.CategoryName" /> and <see cref="P:System.Diagnostics.PerformanceCounterCategory.MachineName" /> properties.</summary>
		/// <returns>true if the counter is registered to the category that is specified by the <see cref="P:System.Diagnostics.PerformanceCounterCategory.CategoryName" /> and <see cref="P:System.Diagnostics.PerformanceCounterCategory.MachineName" /> properties; otherwise, false.</returns>
		/// <param name="counterName">The name of the performance counter to look for. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="counterName" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.PerformanceCounterCategory.CategoryName" /> property has not been set. </exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001381 RID: 4993 RVA: 0x00034028 File Offset: 0x00032228
		public bool CounterExists(string counterName)
		{
			return PerformanceCounterCategory.CounterExists(counterName, this.categoryName, this.machineName);
		}

		/// <summary>Determines whether the specified counter is registered to the specified category on the local computer.</summary>
		/// <returns>true, if the counter is registered to the specified category on the local computer; otherwise, false.</returns>
		/// <param name="counterName">The name of the performance counter to look for. </param>
		/// <param name="categoryName">The name of the performance counter category, or performance object, with which the specified performance counter is associated. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="categoryName" /> is null.-or- The <paramref name="counterName" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="categoryName" /> is an empty string (""). </exception>
		/// <exception cref="T:System.InvalidOperationException">The category name does not exist. </exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001382 RID: 4994 RVA: 0x0003403C File Offset: 0x0003223C
		public static bool CounterExists(string counterName, string categoryName)
		{
			return PerformanceCounterCategory.CounterExists(counterName, categoryName, ".");
		}

		/// <summary>Determines whether the specified counter is registered to the specified category on a remote computer.</summary>
		/// <returns>true, if the counter is registered to the specified category on the specified computer; otherwise, false.</returns>
		/// <param name="counterName">The name of the performance counter to look for. </param>
		/// <param name="categoryName">The name of the performance counter category, or performance object, with which the specified performance counter is associated. </param>
		/// <param name="machineName">The name of the computer on which the performance counter category and its associated counters exist. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="categoryName" /> is null.-or- The <paramref name="counterName" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The category name does not exist. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="categoryName" /> is an empty string ("").-or- The <paramref name="machineName" /> is invalid. </exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001383 RID: 4995 RVA: 0x0003404C File Offset: 0x0003224C
		public static bool CounterExists(string counterName, string categoryName, string machineName)
		{
			if (counterName == null)
			{
				throw new ArgumentNullException("counterName");
			}
			PerformanceCounterCategory.CheckCategory(categoryName);
			if (machineName == null)
			{
				throw new ArgumentNullException("machineName");
			}
			return PerformanceCounterCategory.CounterCategoryExists(counterName, categoryName, machineName);
		}

		/// <summary>Registers the custom performance counter category containing the specified counters on the local computer.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.PerformanceCounterCategory" /> that is associated with the new custom category, or performance object.</returns>
		/// <param name="categoryName">The name of the custom performance counter category to create and register with the system. </param>
		/// <param name="categoryHelp">A description of the custom category. </param>
		/// <param name="counterData">A <see cref="T:System.Diagnostics.CounterCreationDataCollection" /> that specifies the counters to create as part of the new category. </param>
		/// <exception cref="T:System.ArgumentException">A counter name that is specified within the <paramref name="counterData" /> collection is null or an empty string ("").-or- A counter that is specified within the <paramref name="counterData" /> collection already exists.-or- The <paramref name="counterName" /> parameter has invalid syntax. It might contain backslash characters ("\") or have length greater than 80 characters. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="categoryName" /> parameter is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The category already exists on the local computer.-or- The layout of the <paramref name="counterData" /> collection is incorrect for base counters. A counter of type AverageCount64, AverageTimer32, CounterMultiTimer, CounterMultiTimerInverse, CounterMultiTimer100Ns, CounterMultiTimer100NsInverse, RawFraction, SampleFraction or SampleCounter has to be immediately followed by one of the base counter types (AverageBase, MultiBase, RawBase, or SampleBase). </exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001384 RID: 4996 RVA: 0x0003408C File Offset: 0x0003228C
		[Obsolete("Use another overload that uses PerformanceCounterCategoryType instead")]
		public static PerformanceCounterCategory Create(string categoryName, string categoryHelp, CounterCreationDataCollection counterData)
		{
			return PerformanceCounterCategory.Create(categoryName, categoryHelp, PerformanceCounterCategoryType.Unknown, counterData);
		}

		/// <summary>Registers a custom performance counter category containing a single counter of type NumberOfItems32 on the local computer.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.PerformanceCounterCategory" /> that is associated with the new system category, or performance object.</returns>
		/// <param name="categoryName">The name of the custom performance counter category to create and register with the system. </param>
		/// <param name="categoryHelp">A description of the custom category. </param>
		/// <param name="counterName">The name of a new counter, of type NumberOfItems32, to create as part of the new category. </param>
		/// <param name="counterHelp">A description of the counter that is associated with the new custom category. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="counterName" /> is null or is an empty string ("").-or- The counter that is specified by <paramref name="counterName" /> already exists.-or- <paramref name="counterName" /> has invalid syntax. It might contain backslash characters ("\") or have length greater than 80 characters. </exception>
		/// <exception cref="T:System.InvalidOperationException">The category already exists on the local computer. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="categoryName" /> is null. -or-<paramref name="counterHelp" /> is null.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001385 RID: 4997 RVA: 0x00034098 File Offset: 0x00032298
		[Obsolete("Use another overload that uses PerformanceCounterCategoryType instead")]
		public static PerformanceCounterCategory Create(string categoryName, string categoryHelp, string counterName, string counterHelp)
		{
			return PerformanceCounterCategory.Create(categoryName, categoryHelp, PerformanceCounterCategoryType.Unknown, counterName, counterHelp);
		}

		/// <summary>Registers the custom performance counter category containing the specified counters on the local computer.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.PerformanceCounterCategory" /> that is associated with the new custom category, or performance object.</returns>
		/// <param name="categoryName">The name of the custom performance counter category to create and register with the system.</param>
		/// <param name="categoryHelp">A description of the custom category.</param>
		/// <param name="categoryType">One of the <see cref="T:System.Diagnostics.PerformanceCounterCategoryType" />  values. </param>
		/// <param name="counterData">A <see cref="T:System.Diagnostics.CounterCreationDataCollection" /> that specifies the counters to create as part of the new category.</param>
		/// <exception cref="T:System.ArgumentException">A counter name that is specified within the <paramref name="counterData" /> collection is null or an empty string ("").-or- A counter that is specified within the <paramref name="counterData" /> collection already exists.-or- <paramref name="counterName" /> has invalid syntax. It might contain backslash characters ("\") or have length greater than 80 characters. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="categoryName" /> is null. -or-<paramref name="counterData" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="categoryType" /> value is outside of the range of the following values: MultiInstance, SingleInstance, or Unknown.</exception>
		/// <exception cref="T:System.InvalidOperationException">The category already exists on the local computer.-or- The layout of the <paramref name="counterData" /> collection is incorrect for base counters. A counter of type AverageCount64, AverageTimer32, CounterMultiTimer, CounterMultiTimerInverse, CounterMultiTimer100Ns, CounterMultiTimer100NsInverse, RawFraction, SampleFraction, or SampleCounter must be immediately followed by one of the base counter types (AverageBase, MultiBase, RawBase, or SampleBase). </exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001386 RID: 4998 RVA: 0x000340A4 File Offset: 0x000322A4
		public static PerformanceCounterCategory Create(string categoryName, string categoryHelp, PerformanceCounterCategoryType categoryType, CounterCreationDataCollection counterData)
		{
			PerformanceCounterCategory.CheckCategory(categoryName);
			if (counterData == null)
			{
				throw new ArgumentNullException("counterData");
			}
			if (counterData.Count == 0)
			{
				throw new ArgumentException("counterData");
			}
			CounterCreationData[] array = new CounterCreationData[counterData.Count];
			counterData.CopyTo(array, 0);
			if (!PerformanceCounterCategory.Create(categoryName, categoryHelp, categoryType, array))
			{
				throw new InvalidOperationException();
			}
			return new PerformanceCounterCategory(categoryName, categoryHelp);
		}

		/// <summary>Registers the custom performance counter category containing a single counter of type <see cref="F:System.Diagnostics.PerformanceCounterType.NumberOfItems32" /> on the local computer.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.PerformanceCounterCategory" /> that is associated with the new system category, or performance object.</returns>
		/// <param name="categoryName">The name of the custom performance counter category to create and register with the system.</param>
		/// <param name="categoryHelp">A description of the custom category.</param>
		/// <param name="categoryType">One of the <see cref="T:System.Diagnostics.PerformanceCounterCategoryType" />  values specifying whether the category is <see cref="F:System.Diagnostics.PerformanceCounterCategoryType.MultiInstance" />, <see cref="F:System.Diagnostics.PerformanceCounterCategoryType.SingleInstance" />, or <see cref="F:System.Diagnostics.PerformanceCounterCategoryType.Unknown" />.</param>
		/// <param name="counterName">The name of a new counter to create as part of the new category.</param>
		/// <param name="counterHelp">A description of the counter that is associated with the new custom category.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="counterName" /> is null or is an empty string ("").-or- The counter that is specified by <paramref name="counterName" /> already exists.-or- <paramref name="counterName" /> has invalid syntax. It might contain backslash characters ("\") or have length greater than 80 characters. </exception>
		/// <exception cref="T:System.InvalidOperationException">The category already exists on the local computer. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="categoryName" /> is null. -or-<paramref name="counterHelp" /> is null.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001387 RID: 4999 RVA: 0x00034110 File Offset: 0x00032310
		public static PerformanceCounterCategory Create(string categoryName, string categoryHelp, PerformanceCounterCategoryType categoryType, string counterName, string counterHelp)
		{
			PerformanceCounterCategory.CheckCategory(categoryName);
			if (!PerformanceCounterCategory.Create(categoryName, categoryHelp, categoryType, new CounterCreationData[]
			{
				new CounterCreationData(counterName, counterHelp, PerformanceCounterType.NumberOfItems32)
			}))
			{
				throw new InvalidOperationException();
			}
			return new PerformanceCounterCategory(categoryName, categoryHelp);
		}

		/// <summary>Removes the category and its associated counters from the local computer.</summary>
		/// <param name="categoryName">The name of the custom performance counter category to delete. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="categoryName" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="categoryName" /> parameter has invalid syntax. It might contain backslash characters ("\") or have length greater than 80 characters. </exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed. </exception>
		/// <exception cref="T:System.InvalidOperationException">The category cannot be deleted because it is not a custom category. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001388 RID: 5000 RVA: 0x00034158 File Offset: 0x00032358
		public static void Delete(string categoryName)
		{
			PerformanceCounterCategory.CheckCategory(categoryName);
			if (!PerformanceCounterCategory.CategoryDelete(categoryName))
			{
				throw new InvalidOperationException();
			}
		}

		/// <summary>Determines whether the category is registered on the local computer.</summary>
		/// <returns>true if the category is registered; otherwise, false.</returns>
		/// <param name="categoryName">The name of the performance counter category to look for. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="categoryName" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="categoryName" /> parameter is an empty string (""). </exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001389 RID: 5001 RVA: 0x00034174 File Offset: 0x00032374
		public static bool Exists(string categoryName)
		{
			return PerformanceCounterCategory.Exists(categoryName, ".");
		}

		/// <summary>Determines whether the category is registered on the specified computer.</summary>
		/// <returns>true if the category is registered; otherwise, false.</returns>
		/// <param name="categoryName">The name of the performance counter category to look for. </param>
		/// <param name="machineName">The name of the computer to examine for the category. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="categoryName" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="categoryName" /> parameter is an empty string ("").-or- The <paramref name="machineName" /> parameter is invalid. </exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed. </exception>
		/// <exception cref="T:System.IO.IOException">The network path cannot be found.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.-or-Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600138A RID: 5002 RVA: 0x00034184 File Offset: 0x00032384
		public static bool Exists(string categoryName, string machineName)
		{
			PerformanceCounterCategory.CheckCategory(categoryName);
			return PerformanceCounterCategory.CounterCategoryExists(null, categoryName, machineName);
		}

		/// <summary>Retrieves a list of the performance counter categories that are registered on the local computer.</summary>
		/// <returns>An array of <see cref="T:System.Diagnostics.PerformanceCounterCategory" /> objects indicating the categories that are registered on the local computer.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600138B RID: 5003 RVA: 0x00034194 File Offset: 0x00032394
		public static PerformanceCounterCategory[] GetCategories()
		{
			return PerformanceCounterCategory.GetCategories(".");
		}

		/// <summary>Retrieves a list of the performance counter categories that are registered on the specified computer.</summary>
		/// <returns>An array of <see cref="T:System.Diagnostics.PerformanceCounterCategory" /> objects indicating the categories that are registered on the specified computer.</returns>
		/// <param name="machineName">The computer to look on. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="machineName" /> parameter is invalid. </exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600138C RID: 5004 RVA: 0x000341A0 File Offset: 0x000323A0
		public static PerformanceCounterCategory[] GetCategories(string machineName)
		{
			if (machineName == null)
			{
				throw new ArgumentNullException("machineName");
			}
			string[] categoryNames = PerformanceCounterCategory.GetCategoryNames(machineName);
			PerformanceCounterCategory[] array = new PerformanceCounterCategory[categoryNames.Length];
			for (int i = 0; i < categoryNames.Length; i++)
			{
				array[i] = new PerformanceCounterCategory(categoryNames[i], machineName);
			}
			return array;
		}

		/// <summary>Retrieves a list of the counters in a performance counter category that contains exactly one instance.</summary>
		/// <returns>An array of <see cref="T:System.Diagnostics.PerformanceCounter" /> objects indicating the counters that are associated with this single-instance performance counter category.</returns>
		/// <exception cref="T:System.ArgumentException">The category is not a single instance. </exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed. </exception>
		/// <exception cref="T:System.InvalidOperationException">The category does not have an associated instance.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600138D RID: 5005 RVA: 0x000341F0 File Offset: 0x000323F0
		public PerformanceCounter[] GetCounters()
		{
			return this.GetCounters(string.Empty);
		}

		/// <summary>Retrieves a list of the counters in a performance counter category that contains one or more instances.</summary>
		/// <returns>An array of <see cref="T:System.Diagnostics.PerformanceCounter" /> objects indicating the counters that are associated with the specified object instance of this performance counter category.</returns>
		/// <param name="instanceName">The performance object instance for which to retrieve the list of associated counters. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="instanceName" /> parameter is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.PerformanceCounterCategory.CategoryName" /> property for this <see cref="T:System.Diagnostics.PerformanceCounterCategory" /> instance has not been set.-or- The category does not contain the instance that is specified by the <paramref name="instanceName" /> parameter. </exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600138E RID: 5006 RVA: 0x00034200 File Offset: 0x00032400
		public PerformanceCounter[] GetCounters(string instanceName)
		{
			string[] counterNames = PerformanceCounterCategory.GetCounterNames(this.categoryName, this.machineName);
			PerformanceCounter[] array = new PerformanceCounter[counterNames.Length];
			for (int i = 0; i < counterNames.Length; i++)
			{
				array[i] = new PerformanceCounter(this.categoryName, counterNames[i], instanceName, this.machineName);
			}
			return array;
		}

		/// <summary>Retrieves the list of performance object instances that are associated with this category.</summary>
		/// <returns>An array of strings representing the performance object instance names that are associated with this category or, if the category contains only one performance object instance, a single-entry array that contains an empty string ("").</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.PerformanceCounterCategory.CategoryName" /> property is null. The property might not have been set. -or-The category does not have an associated instance.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600138F RID: 5007 RVA: 0x00034258 File Offset: 0x00032458
		public string[] GetInstanceNames()
		{
			return PerformanceCounterCategory.GetInstanceNames(this.categoryName, this.machineName);
		}

		/// <summary>Determines whether the specified performance object instance exists in the category that is identified by this <see cref="T:System.Diagnostics.PerformanceCounterCategory" /> object's <see cref="P:System.Diagnostics.PerformanceCounterCategory.CategoryName" /> property.</summary>
		/// <returns>true if the category contains the specified performance object instance; otherwise, false.</returns>
		/// <param name="instanceName">The performance object instance in this performance counter category to search for. </param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.PerformanceCounterCategory.CategoryName" /> property is null. The property might not have been set. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="instanceName" /> parameter is null. </exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001390 RID: 5008 RVA: 0x0003426C File Offset: 0x0003246C
		public bool InstanceExists(string instanceName)
		{
			return PerformanceCounterCategory.InstanceExists(instanceName, this.categoryName, this.machineName);
		}

		/// <summary>Determines whether a specified category on the local computer contains the specified performance object instance.</summary>
		/// <returns>true if the category contains the specified performance object instance; otherwise, false.</returns>
		/// <param name="instanceName">The performance object instance to search for. </param>
		/// <param name="categoryName">The performance counter category to search. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="instanceName" /> parameter is null.-or- The <paramref name="categoryName" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="categoryName" /> parameter is an empty string (""). </exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001391 RID: 5009 RVA: 0x00034280 File Offset: 0x00032480
		public static bool InstanceExists(string instanceName, string categoryName)
		{
			return PerformanceCounterCategory.InstanceExists(instanceName, categoryName, ".");
		}

		/// <summary>Determines whether a specified category on a specified computer contains the specified performance object instance.</summary>
		/// <returns>true if the category contains the specified performance object instance; otherwise, false.</returns>
		/// <param name="instanceName">The performance object instance to search for. </param>
		/// <param name="categoryName">The performance counter category to search. </param>
		/// <param name="machineName">The name of the computer on which to look for the category instance pair. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="instanceName" /> parameter is null.-or- The <paramref name="categoryName" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="categoryName" /> parameter is an empty string ("").-or- The <paramref name="machineName" /> parameter is invalid. </exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001392 RID: 5010 RVA: 0x00034290 File Offset: 0x00032490
		public static bool InstanceExists(string instanceName, string categoryName, string machineName)
		{
			if (instanceName == null)
			{
				throw new ArgumentNullException("instanceName");
			}
			PerformanceCounterCategory.CheckCategory(categoryName);
			if (machineName == null)
			{
				throw new ArgumentNullException("machineName");
			}
			int num = PerformanceCounterCategory.InstanceExistsInternal(instanceName, categoryName, machineName);
			if (num == 0)
			{
				return false;
			}
			if (num == 1)
			{
				return true;
			}
			throw new InvalidOperationException();
		}

		/// <summary>Reads all the counter and performance object instance data that is associated with this performance counter category.</summary>
		/// <returns>An <see cref="T:System.Diagnostics.InstanceDataCollectionCollection" /> that contains the counter and performance object instance data for the category.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.PerformanceCounterCategory.CategoryName" /> property is null. The property might not have been set. </exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001393 RID: 5011 RVA: 0x000342E4 File Offset: 0x000324E4
		[MonoTODO]
		public InstanceDataCollectionCollection ReadCategory()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000595 RID: 1429
		private string categoryName;

		// Token: 0x04000596 RID: 1430
		private string machineName;

		// Token: 0x04000597 RID: 1431
		private PerformanceCounterCategoryType type = PerformanceCounterCategoryType.Unknown;
	}
}
