using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace System.Diagnostics
{
	/// <summary>Provides information about a <see cref="T:System.Diagnostics.StackFrame" />, which represents a function call on the call stack for the current thread.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001E5 RID: 485
	[ComVisible(true)]
	[MonoTODO("Serialized objects are not compatible with MS.NET")]
	[Serializable]
	public class StackFrame
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackFrame" /> class.</summary>
		// Token: 0x06001884 RID: 6276 RVA: 0x0005D85C File Offset: 0x0005BA5C
		public StackFrame()
		{
			bool flag = StackFrame.get_frame_info(2, false, out this.methodBase, out this.ilOffset, out this.nativeOffset, out this.fileName, out this.lineNumber, out this.columnNumber);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackFrame" /> class, optionally capturing source information.</summary>
		/// <param name="fNeedFileInfo">true to capture the file name, line number, and column number of the stack frame; otherwise, false. </param>
		// Token: 0x06001885 RID: 6277 RVA: 0x0005D8AC File Offset: 0x0005BAAC
		public StackFrame(bool fNeedFileInfo)
		{
			bool flag = StackFrame.get_frame_info(2, fNeedFileInfo, out this.methodBase, out this.ilOffset, out this.nativeOffset, out this.fileName, out this.lineNumber, out this.columnNumber);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackFrame" /> class that corresponds to a frame above the current stack frame.</summary>
		/// <param name="skipFrames">The number of frames up the stack to skip. </param>
		// Token: 0x06001886 RID: 6278 RVA: 0x0005D8FC File Offset: 0x0005BAFC
		public StackFrame(int skipFrames)
		{
			bool flag = StackFrame.get_frame_info(skipFrames + 2, false, out this.methodBase, out this.ilOffset, out this.nativeOffset, out this.fileName, out this.lineNumber, out this.columnNumber);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackFrame" /> class that corresponds to a frame above the current stack frame, optionally capturing source information.</summary>
		/// <param name="skipFrames">The number of frames up the stack to skip. </param>
		/// <param name="fNeedFileInfo">true to capture the file name, line number, and column number of the stack frame; otherwise, false. </param>
		// Token: 0x06001887 RID: 6279 RVA: 0x0005D94C File Offset: 0x0005BB4C
		public StackFrame(int skipFrames, bool fNeedFileInfo)
		{
			bool flag = StackFrame.get_frame_info(skipFrames + 2, fNeedFileInfo, out this.methodBase, out this.ilOffset, out this.nativeOffset, out this.fileName, out this.lineNumber, out this.columnNumber);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackFrame" /> class that contains only the given file name and line number.</summary>
		/// <param name="fileName">The file name. </param>
		/// <param name="lineNumber">The line number in the specified file. </param>
		// Token: 0x06001888 RID: 6280 RVA: 0x0005D99C File Offset: 0x0005BB9C
		public StackFrame(string fileName, int lineNumber)
		{
			bool flag = StackFrame.get_frame_info(2, false, out this.methodBase, out this.ilOffset, out this.nativeOffset, out fileName, out lineNumber, out this.columnNumber);
			this.fileName = fileName;
			this.lineNumber = lineNumber;
			this.columnNumber = 0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackFrame" /> class that contains only the given file name, line number, and column number.</summary>
		/// <param name="fileName">The file name. </param>
		/// <param name="lineNumber">The line number in the specified file. </param>
		/// <param name="colNumber">The column number in the specified file. </param>
		// Token: 0x06001889 RID: 6281 RVA: 0x0005D9F8 File Offset: 0x0005BBF8
		public StackFrame(string fileName, int lineNumber, int colNumber)
		{
			bool flag = StackFrame.get_frame_info(2, false, out this.methodBase, out this.ilOffset, out this.nativeOffset, out fileName, out lineNumber, out this.columnNumber);
			this.fileName = fileName;
			this.lineNumber = lineNumber;
			this.columnNumber = colNumber;
		}

		// Token: 0x0600188A RID: 6282
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_frame_info(int skip, bool needFileInfo, out MethodBase method, out int iloffset, out int native_offset, out string file, out int line, out int column);

		/// <summary>Gets the line number in the file that contains the code that is executing. This information is typically extracted from the debugging symbols for the executable.</summary>
		/// <returns>The file line number.-or- Zero if the file line number cannot be determined.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600188B RID: 6283 RVA: 0x0005DA54 File Offset: 0x0005BC54
		public virtual int GetFileLineNumber()
		{
			return this.lineNumber;
		}

		/// <summary>Gets the column number in the file that contains the code that is executing. This information is typically extracted from the debugging symbols for the executable.</summary>
		/// <returns>The file column number.-or- Zero if the file column number cannot be determined.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600188C RID: 6284 RVA: 0x0005DA5C File Offset: 0x0005BC5C
		public virtual int GetFileColumnNumber()
		{
			return this.columnNumber;
		}

		/// <summary>Gets the file name that contains the code that is executing. This information is typically extracted from the debugging symbols for the executable.</summary>
		/// <returns>The file name.-or- null if the file name cannot be determined.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600188D RID: 6285 RVA: 0x0005DA64 File Offset: 0x0005BC64
		public virtual string GetFileName()
		{
			if (SecurityManager.SecurityEnabled && this.fileName != null && this.fileName.Length > 0)
			{
				string fullPath = Path.GetFullPath(this.fileName);
				new FileIOPermission(FileIOPermissionAccess.PathDiscovery, fullPath).Demand();
			}
			return this.fileName;
		}

		// Token: 0x0600188E RID: 6286 RVA: 0x0005DAB8 File Offset: 0x0005BCB8
		internal string GetSecureFileName()
		{
			string result = "<filename unknown>";
			if (this.fileName == null)
			{
				return result;
			}
			try
			{
				result = this.GetFileName();
			}
			catch (SecurityException)
			{
			}
			return result;
		}

		/// <summary>Gets the offset from the start of the Microsoft intermediate language (MSIL) code for the method that is executing. This offset might be an approximation depending on whether or not the just-in-time (JIT) compiler is generating debugging code. The generation of this debugging information is controlled by the <see cref="T:System.Diagnostics.DebuggableAttribute" />.</summary>
		/// <returns>The offset from the start of the MSIL code for the method that is executing.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600188F RID: 6287 RVA: 0x0005DB08 File Offset: 0x0005BD08
		public virtual int GetILOffset()
		{
			return this.ilOffset;
		}

		/// <summary>Gets the method in which the frame is executing.</summary>
		/// <returns>The method in which the frame is executing.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001890 RID: 6288 RVA: 0x0005DB10 File Offset: 0x0005BD10
		public virtual MethodBase GetMethod()
		{
			return this.methodBase;
		}

		/// <summary>Gets the offset from the start of the native just-in-time (JIT)-compiled code for the method that is being executed. The generation of this debugging information is controlled by the <see cref="T:System.Diagnostics.DebuggableAttribute" /> class.</summary>
		/// <returns>The offset from the start of the JIT-compiled code for the method that is being executed.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001891 RID: 6289 RVA: 0x0005DB18 File Offset: 0x0005BD18
		public virtual int GetNativeOffset()
		{
			return this.nativeOffset;
		}

		// Token: 0x06001892 RID: 6290 RVA: 0x0005DB20 File Offset: 0x0005BD20
		internal string GetInternalMethodName()
		{
			return this.internalMethodName;
		}

		/// <summary>Builds a readable representation of the stack trace.</summary>
		/// <returns>A readable representation of the stack trace.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001893 RID: 6291 RVA: 0x0005DB28 File Offset: 0x0005BD28
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.methodBase == null)
			{
				stringBuilder.Append(Locale.GetText("<unknown method>"));
			}
			else
			{
				stringBuilder.Append(this.methodBase.Name);
			}
			stringBuilder.Append(Locale.GetText(" at "));
			if (this.ilOffset == -1)
			{
				stringBuilder.Append(Locale.GetText("<unknown offset>"));
			}
			else
			{
				stringBuilder.Append(Locale.GetText("offset "));
				stringBuilder.Append(this.ilOffset);
			}
			stringBuilder.Append(Locale.GetText(" in file:line:column "));
			stringBuilder.Append(this.GetSecureFileName());
			stringBuilder.AppendFormat(":{0}:{1}", this.lineNumber, this.columnNumber);
			return stringBuilder.ToString();
		}

		/// <summary>Defines the value that is returned from the <see cref="M:System.Diagnostics.StackFrame.GetNativeOffset" /> or <see cref="M:System.Diagnostics.StackFrame.GetILOffset" /> method when the native or Microsoft intermediate language (MSIL) offset is unknown. This field is constant.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040008F1 RID: 2289
		public const int OFFSET_UNKNOWN = -1;

		// Token: 0x040008F2 RID: 2290
		private int ilOffset = -1;

		// Token: 0x040008F3 RID: 2291
		private int nativeOffset = -1;

		// Token: 0x040008F4 RID: 2292
		private MethodBase methodBase;

		// Token: 0x040008F5 RID: 2293
		private string fileName;

		// Token: 0x040008F6 RID: 2294
		private int lineNumber;

		// Token: 0x040008F7 RID: 2295
		private int columnNumber;

		// Token: 0x040008F8 RID: 2296
		private string internalMethodName;
	}
}
