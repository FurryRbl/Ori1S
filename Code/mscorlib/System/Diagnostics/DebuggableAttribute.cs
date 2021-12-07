using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	/// <summary>Modifies code generation for runtime just-in-time (JIT) debugging. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001DA RID: 474
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module)]
	[ComVisible(true)]
	public sealed class DebuggableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggableAttribute" /> class, using the specified tracking and optimization options for the just-in-time (JIT) compiler.</summary>
		/// <param name="isJITTrackingEnabled">true to enable debugging; otherwise, false. </param>
		/// <param name="isJITOptimizerDisabled">true to disable the optimizer for execution; otherwise, false. </param>
		// Token: 0x06001853 RID: 6227 RVA: 0x0005D4D0 File Offset: 0x0005B6D0
		public DebuggableAttribute(bool isJITTrackingEnabled, bool isJITOptimizerDisabled)
		{
			this.JITTrackingEnabledFlag = isJITTrackingEnabled;
			this.JITOptimizerDisabledFlag = isJITOptimizerDisabled;
			if (isJITTrackingEnabled)
			{
				this.debuggingModes |= DebuggableAttribute.DebuggingModes.Default;
			}
			if (isJITOptimizerDisabled)
			{
				this.debuggingModes |= DebuggableAttribute.DebuggingModes.DisableOptimizations;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggableAttribute" /> class, using the specified debugging modes for the just-in-time (JIT) compiler. </summary>
		/// <param name="modes">A bitwise combination of the <see cref="T:System.Diagnostics.DebuggableAttribute.DebuggingModes" />  values specifying the debugging mode for the JIT compiler.</param>
		// Token: 0x06001854 RID: 6228 RVA: 0x0005D520 File Offset: 0x0005B720
		public DebuggableAttribute(DebuggableAttribute.DebuggingModes modes)
		{
			this.debuggingModes = modes;
			this.JITTrackingEnabledFlag = ((this.debuggingModes & DebuggableAttribute.DebuggingModes.Default) != DebuggableAttribute.DebuggingModes.None);
			this.JITOptimizerDisabledFlag = ((this.debuggingModes & DebuggableAttribute.DebuggingModes.DisableOptimizations) != DebuggableAttribute.DebuggingModes.None);
		}

		/// <summary>Gets the debugging modes for the attribute.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Diagnostics.DebuggableAttribute.DebuggingModes" /> values describing the debugging mode for the just-in-time (JIT) compiler. The default is <see cref="F:System.Diagnostics.DebuggableAttribute.DebuggingModes.Default" />. </returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06001855 RID: 6229 RVA: 0x0005D55C File Offset: 0x0005B75C
		public DebuggableAttribute.DebuggingModes DebuggingFlags
		{
			get
			{
				return this.debuggingModes;
			}
		}

		/// <summary>Gets a value that indicates whether the runtime will track information during code generation for the debugger.</summary>
		/// <returns>true if the runtime will track information during code generation for the debugger; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06001856 RID: 6230 RVA: 0x0005D564 File Offset: 0x0005B764
		public bool IsJITTrackingEnabled
		{
			get
			{
				return this.JITTrackingEnabledFlag;
			}
		}

		/// <summary>Gets a value that indicates whether the runtime optimizer is disabled.</summary>
		/// <returns>true if the runtime optimizer is disabled; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06001857 RID: 6231 RVA: 0x0005D56C File Offset: 0x0005B76C
		public bool IsJITOptimizerDisabled
		{
			get
			{
				return this.JITOptimizerDisabledFlag;
			}
		}

		// Token: 0x040008D5 RID: 2261
		private bool JITTrackingEnabledFlag;

		// Token: 0x040008D6 RID: 2262
		private bool JITOptimizerDisabledFlag;

		// Token: 0x040008D7 RID: 2263
		private DebuggableAttribute.DebuggingModes debuggingModes;

		/// <summary>Specifies the debugging mode for the just-in-time (JIT) compiler.</summary>
		// Token: 0x020001DB RID: 475
		[Flags]
		[ComVisible(true)]
		public enum DebuggingModes
		{
			/// <summary>In the .NET Framework version 2.0, JIT tracking information is always generated, and this flag has the same effect as <see cref="F:System.Diagnostics.DebuggableAttribute.DebuggingModes.Default" /> with the exception of the <see cref="P:System.Diagnostics.DebuggableAttribute.IsJITTrackingEnabled" /> property being false, which has no meaning in version 2.0.</summary>
			// Token: 0x040008D9 RID: 2265
			None = 0,
			/// <summary>Instructs the just-in-time (JIT) compiler to use its default behavior, which includes enabling optimizations, disabling Edit and Continue support, and using symbol store sequence points if present. In the .NET Framework version 2.0, JIT tracking information, the Microsoft intermediate language (MSIL) offset to the native-code offset within a method, is always generated.</summary>
			// Token: 0x040008DA RID: 2266
			Default = 1,
			/// <summary>Use the implicit MSIL sequence points, not the program database (PDB) sequence points. The symbolic information normally includes at least one Microsoft intermediate language (MSIL) offset for each source line. When the just-in-time (JIT) compiler is about to compile a method, it asks the profiling services for a list of MSIL offsets that should be preserved. These MSIL offsets are called sequence points.</summary>
			// Token: 0x040008DB RID: 2267
			IgnoreSymbolStoreSequencePoints = 2,
			/// <summary>Enable edit and continue. Edit and continue enables you to make changes to your source code while your program is in break mode. The ability to edit and continue is compiler dependent. </summary>
			// Token: 0x040008DC RID: 2268
			EnableEditAndContinue = 4,
			/// <summary>Disable optimizations performed by the compiler to make your output file smaller, faster, and more efficient. Optimizations result in code rearrangement in the output file, which can make debugging difficult. Typically optimization should be disabled while debugging. </summary>
			// Token: 0x040008DD RID: 2269
			DisableOptimizations = 256
		}
	}
}
