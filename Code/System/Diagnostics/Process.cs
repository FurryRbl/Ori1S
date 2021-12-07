using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;

namespace System.Diagnostics
{
	/// <summary>Provides access to local and remote processes and enables you to start and stop local system processes.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000241 RID: 577
	[System.ComponentModel.DefaultEvent("Exited")]
	[MonitoringDescription("Represents a system process")]
	[System.ComponentModel.Designer("System.Diagnostics.Design.ProcessDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[System.ComponentModel.DefaultProperty("StartInfo")]
	[PermissionSet((SecurityAction)14, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\"\nUnrestricted=\"true\"/>\n")]
	[PermissionSet((SecurityAction)15, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\"\nUnrestricted=\"true\"/>\n")]
	public class Process : System.ComponentModel.Component
	{
		// Token: 0x060013E6 RID: 5094 RVA: 0x00034C64 File Offset: 0x00032E64
		private Process(IntPtr handle, int id)
		{
			this.process_handle = handle;
			this.pid = id;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Process" /> class.</summary>
		// Token: 0x060013E7 RID: 5095 RVA: 0x00034C7C File Offset: 0x00032E7C
		public Process()
		{
		}

		/// <summary>Occurs when an application writes to its redirected <see cref="P:System.Diagnostics.Process.StandardOutput" /> stream.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000040 RID: 64
		// (add) Token: 0x060013E8 RID: 5096 RVA: 0x00034C84 File Offset: 0x00032E84
		// (remove) Token: 0x060013E9 RID: 5097 RVA: 0x00034CA0 File Offset: 0x00032EA0
		[MonitoringDescription("Raised when it receives output data")]
		[System.ComponentModel.Browsable(true)]
		public event DataReceivedEventHandler OutputDataReceived;

		/// <summary>Occurs when an application writes to its redirected <see cref="P:System.Diagnostics.Process.StandardError" /> stream.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000041 RID: 65
		// (add) Token: 0x060013EA RID: 5098 RVA: 0x00034CBC File Offset: 0x00032EBC
		// (remove) Token: 0x060013EB RID: 5099 RVA: 0x00034CD8 File Offset: 0x00032ED8
		[MonitoringDescription("Raised when it receives error data")]
		[System.ComponentModel.Browsable(true)]
		public event DataReceivedEventHandler ErrorDataReceived;

		/// <summary>Occurs when a process exits.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000042 RID: 66
		// (add) Token: 0x060013EC RID: 5100 RVA: 0x00034CF4 File Offset: 0x00032EF4
		// (remove) Token: 0x060013ED RID: 5101 RVA: 0x00034D5C File Offset: 0x00032F5C
		[MonitoringDescription("Raised when this process exits.")]
		[System.ComponentModel.Category("Behavior")]
		public event EventHandler Exited
		{
			add
			{
				if (this.process_handle != IntPtr.Zero && this.HasExited)
				{
					value.BeginInvoke(null, null, null, null);
				}
				else
				{
					this.exited_event = (EventHandler)Delegate.Combine(this.exited_event, value);
					if (this.exited_event != null)
					{
						this.StartExitCallbackIfNeeded();
					}
				}
			}
			remove
			{
				this.exited_event = (EventHandler)Delegate.Remove(this.exited_event, value);
			}
		}

		/// <summary>Gets the base priority of the associated process.</summary>
		/// <returns>The base priority, which is computed from the <see cref="P:System.Diagnostics.Process.PriorityClass" /> of the associated process.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me); set the <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> property to false to access this property on Windows 98 and Windows Me.</exception>
		/// <exception cref="T:System.InvalidOperationException">The process has exited.-or- The process has not started, so there is no process ID. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x060013EE RID: 5102 RVA: 0x00034D78 File Offset: 0x00032F78
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("Base process priority.")]
		[MonoTODO]
		public int BasePriority
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x00034D7C File Offset: 0x00032F7C
		private void StartExitCallbackIfNeeded()
		{
			bool flag = !this.already_waiting && this.enableRaisingEvents && this.exited_event != null;
			if (flag && this.process_handle != IntPtr.Zero)
			{
				WaitOrTimerCallback callBack = new WaitOrTimerCallback(Process.CBOnExit);
				Process.ProcessWaitHandle waitObject = new Process.ProcessWaitHandle(this.process_handle);
				ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, this, -1, true);
				this.already_waiting = true;
			}
		}

		/// <summary>Gets or sets whether the <see cref="E:System.Diagnostics.Process.Exited" /> event should be raised when the process terminates.</summary>
		/// <returns>true if the <see cref="E:System.Diagnostics.Process.Exited" /> event should be raised when the associated process is terminated (through either an exit or a call to <see cref="M:System.Diagnostics.Process.Kill" />); otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x060013F0 RID: 5104 RVA: 0x00034DF8 File Offset: 0x00032FF8
		// (set) Token: 0x060013F1 RID: 5105 RVA: 0x00034E00 File Offset: 0x00033000
		[MonitoringDescription("Check for exiting of the process to raise the apropriate event.")]
		[System.ComponentModel.Browsable(false)]
		[System.ComponentModel.DefaultValue(false)]
		public bool EnableRaisingEvents
		{
			get
			{
				return this.enableRaisingEvents;
			}
			set
			{
				bool flag = this.enableRaisingEvents;
				this.enableRaisingEvents = value;
				if (this.enableRaisingEvents && !flag)
				{
					this.StartExitCallbackIfNeeded();
				}
			}
		}

		// Token: 0x060013F2 RID: 5106
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int ExitCode_internal(IntPtr handle);

		/// <summary>Gets the value that the associated process specified when it terminated.</summary>
		/// <returns>The code that the associated process specified when it terminated.</returns>
		/// <exception cref="T:System.InvalidOperationException">The process has not exited.-or- The process <see cref="P:System.Diagnostics.Process.Handle" /> is not valid. </exception>
		/// <exception cref="T:System.NotSupportedException">You are trying to access the <see cref="P:System.Diagnostics.Process.ExitCode" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x060013F3 RID: 5107 RVA: 0x00034E34 File Offset: 0x00033034
		[MonitoringDescription("The exit code of the process.")]
		[System.ComponentModel.Browsable(false)]
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		public int ExitCode
		{
			get
			{
				if (this.process_handle == IntPtr.Zero)
				{
					throw new InvalidOperationException("Process has not been started.");
				}
				int num = Process.ExitCode_internal(this.process_handle);
				if (num == 259)
				{
					throw new InvalidOperationException("The process must exit before getting the requested information.");
				}
				return num;
			}
		}

		// Token: 0x060013F4 RID: 5108
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern long ExitTime_internal(IntPtr handle);

		/// <summary>Gets the time that the associated process exited.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> that indicates when the associated process was terminated.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property. </exception>
		/// <exception cref="T:System.NotSupportedException">You are trying to access the <see cref="P:System.Diagnostics.Process.ExitTime" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x060013F5 RID: 5109 RVA: 0x00034E84 File Offset: 0x00033084
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		[System.ComponentModel.Browsable(false)]
		[MonitoringDescription("The exit time of the process.")]
		public DateTime ExitTime
		{
			get
			{
				if (this.process_handle == IntPtr.Zero)
				{
					throw new InvalidOperationException("Process has not been started.");
				}
				if (!this.HasExited)
				{
					throw new InvalidOperationException("The process must exit before getting the requested information.");
				}
				return DateTime.FromFileTime(Process.ExitTime_internal(this.process_handle));
			}
		}

		/// <summary>Gets the native handle of the associated process.</summary>
		/// <returns>The handle that the operating system assigned to the associated process when the process was started. The system uses this handle to keep track of process attributes.</returns>
		/// <exception cref="T:System.InvalidOperationException">The process has not been started. The <see cref="P:System.Diagnostics.Process.Handle" /> property cannot be read because there is no process associated with this <see cref="T:System.Diagnostics.Process" /> instance.-or- The <see cref="T:System.Diagnostics.Process" /> instance has been attached to a running process but you do not have the necessary permissions to get a handle with full access rights. </exception>
		/// <exception cref="T:System.NotSupportedException">You are trying to access the <see cref="P:System.Diagnostics.Process.Handle" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x060013F6 RID: 5110 RVA: 0x00034ED8 File Offset: 0x000330D8
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("Handle for this process.")]
		[System.ComponentModel.Browsable(false)]
		public IntPtr Handle
		{
			get
			{
				return this.process_handle;
			}
		}

		/// <summary>Gets the number of handles opened by the process.</summary>
		/// <returns>The number of operating system handles the process has opened.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me); set the <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> property to false to access this property on Windows 98 and Windows Me.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x060013F7 RID: 5111 RVA: 0x00034EE0 File Offset: 0x000330E0
		[MonitoringDescription("Handles for this process.")]
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		[MonoTODO]
		public int HandleCount
		{
			get
			{
				return 0;
			}
		}

		/// <summary>Gets a value indicating whether the associated process has been terminated.</summary>
		/// <returns>true if the operating system process referenced by the <see cref="T:System.Diagnostics.Process" /> component has terminated; otherwise, false.</returns>
		/// <exception cref="T:System.InvalidOperationException">There is no process associated with the object. </exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The exit code for the process could not be retrieved. </exception>
		/// <exception cref="T:System.NotSupportedException">You are trying to access the <see cref="P:System.Diagnostics.Process.HasExited" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x060013F8 RID: 5112 RVA: 0x00034EE4 File Offset: 0x000330E4
		[MonitoringDescription("Determines if the process is still running.")]
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		[System.ComponentModel.Browsable(false)]
		public bool HasExited
		{
			get
			{
				if (this.process_handle == IntPtr.Zero)
				{
					throw new InvalidOperationException("Process has not been started.");
				}
				int num = Process.ExitCode_internal(this.process_handle);
				return num != 259;
			}
		}

		/// <summary>Gets the unique identifier for the associated process.</summary>
		/// <returns>The system-generated unique identifier of the process that is referenced by this <see cref="T:System.Diagnostics.Process" /> instance.</returns>
		/// <exception cref="T:System.InvalidOperationException">The process's <see cref="P:System.Diagnostics.Process.Id" /> property has not been set.-or- There is no process associated with this <see cref="T:System.Diagnostics.Process" /> object. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me); set the <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> property to false to access this property on Windows 98 and Windows Me.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x060013F9 RID: 5113 RVA: 0x00034F2C File Offset: 0x0003312C
		[MonitoringDescription("Process identifier.")]
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		public int Id
		{
			get
			{
				if (this.pid == 0)
				{
					throw new InvalidOperationException("Process ID has not been set.");
				}
				return this.pid;
			}
		}

		/// <summary>Gets the name of the computer the associated process is running on.</summary>
		/// <returns>The name of the computer that the associated process is running on.</returns>
		/// <exception cref="T:System.InvalidOperationException">There is no process associated with this <see cref="T:System.Diagnostics.Process" /> object. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x060013FA RID: 5114 RVA: 0x00034F4C File Offset: 0x0003314C
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The name of the computer running the process.")]
		[MonoTODO]
		[System.ComponentModel.Browsable(false)]
		public string MachineName
		{
			get
			{
				return "localhost";
			}
		}

		/// <summary>Gets the main module for the associated process.</summary>
		/// <returns>The <see cref="T:System.Diagnostics.ProcessModule" /> that was used to start the process.</returns>
		/// <exception cref="T:System.NotSupportedException">You are trying to access the <see cref="P:System.Diagnostics.Process.MainModule" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me); set <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> to false to access this property on Windows 98 and Windows Me.</exception>
		/// <exception cref="T:System.InvalidOperationException">The process <see cref="P:System.Diagnostics.Process.Id" /> is not available.-or- The process has exited. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x060013FB RID: 5115 RVA: 0x00034F54 File Offset: 0x00033154
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		[System.ComponentModel.Browsable(false)]
		[MonitoringDescription("The main module of the process.")]
		public ProcessModule MainModule
		{
			get
			{
				return this.Modules[0];
			}
		}

		/// <summary>Gets the window handle of the main window of the associated process.</summary>
		/// <returns>The system-generated window handle of the main window of the associated process.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.Process.MainWindowHandle" /> is not defined because the process has exited. </exception>
		/// <exception cref="T:System.NotSupportedException">You are trying to access the <see cref="P:System.Diagnostics.Process.MainWindowHandle" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me); set <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> to false to access this property on Windows 98 and Windows Me.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x060013FC RID: 5116 RVA: 0x00034F64 File Offset: 0x00033164
		[MonitoringDescription("The handle of the main window of the process.")]
		[MonoTODO]
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		public IntPtr MainWindowHandle
		{
			get
			{
				return (IntPtr)0;
			}
		}

		/// <summary>Gets the caption of the main window of the process.</summary>
		/// <returns>The process's main window title.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.Process.MainWindowTitle" /> property is not defined because the process has exited. </exception>
		/// <exception cref="T:System.NotSupportedException">You are trying to access the <see cref="P:System.Diagnostics.Process.MainWindowTitle" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me); set <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> to false to access this property on Windows 98 and Windows Me.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x060013FD RID: 5117 RVA: 0x00034F6C File Offset: 0x0003316C
		[MonitoringDescription("The title of the main window of the process.")]
		[MonoTODO]
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		public string MainWindowTitle
		{
			get
			{
				return "null";
			}
		}

		// Token: 0x060013FE RID: 5118
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetWorkingSet_internal(IntPtr handle, out int min, out int max);

		// Token: 0x060013FF RID: 5119
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetWorkingSet_internal(IntPtr handle, int min, int max, bool use_min);

		/// <summary>Gets or sets the maximum allowable working set size for the associated process.</summary>
		/// <returns>The maximum working set size that is allowed in memory for the process, in bytes.</returns>
		/// <exception cref="T:System.ArgumentException">The maximum working set size is invalid. It must be greater than or equal to the minimum working set size.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">Working set information cannot be retrieved from the associated process resource.-or- The process identifier or process handle is zero because the process has not been started. </exception>
		/// <exception cref="T:System.NotSupportedException">You are trying to access the <see cref="P:System.Diagnostics.Process.MaxWorkingSet" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		/// <exception cref="T:System.InvalidOperationException">The process <see cref="P:System.Diagnostics.Process.Id" /> is not available.-or- The process has exited. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06001400 RID: 5120 RVA: 0x00034F74 File Offset: 0x00033174
		// (set) Token: 0x06001401 RID: 5121 RVA: 0x00034FF0 File Offset: 0x000331F0
		[MonitoringDescription("The maximum working set for this process.")]
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		public IntPtr MaxWorkingSet
		{
			get
			{
				if (this.HasExited)
				{
					throw new InvalidOperationException(string.Concat(new object[]
					{
						"The process ",
						this.ProcessName,
						" (ID ",
						this.Id,
						") has exited"
					}));
				}
				int num;
				int value;
				if (!Process.GetWorkingSet_internal(this.process_handle, out num, out value))
				{
					throw new System.ComponentModel.Win32Exception();
				}
				return (IntPtr)value;
			}
			set
			{
				if (this.HasExited)
				{
					throw new InvalidOperationException(string.Concat(new object[]
					{
						"The process ",
						this.ProcessName,
						" (ID ",
						this.Id,
						") has exited"
					}));
				}
				if (!Process.SetWorkingSet_internal(this.process_handle, 0, value.ToInt32(), false))
				{
					throw new System.ComponentModel.Win32Exception();
				}
			}
		}

		/// <summary>Gets or sets the minimum allowable working set size for the associated process.</summary>
		/// <returns>The minimum working set size that is required in memory for the process, in bytes.</returns>
		/// <exception cref="T:System.ArgumentException">The minimum working set size is invalid. It must be less than or equal to the maximum working set size.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">Working set information cannot be retrieved from the associated process resource.-or- The process identifier or process handle is zero because the process has not been started. </exception>
		/// <exception cref="T:System.NotSupportedException">You are trying to access the <see cref="P:System.Diagnostics.Process.MinWorkingSet" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer. </exception>
		/// <exception cref="T:System.InvalidOperationException">The process <see cref="P:System.Diagnostics.Process.Id" /> is not available.-or- The process has exited.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06001402 RID: 5122 RVA: 0x0003506C File Offset: 0x0003326C
		// (set) Token: 0x06001403 RID: 5123 RVA: 0x000350E8 File Offset: 0x000332E8
		[MonitoringDescription("The minimum working set for this process.")]
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		public IntPtr MinWorkingSet
		{
			get
			{
				if (this.HasExited)
				{
					throw new InvalidOperationException(string.Concat(new object[]
					{
						"The process ",
						this.ProcessName,
						" (ID ",
						this.Id,
						") has exited"
					}));
				}
				int value;
				int num;
				if (!Process.GetWorkingSet_internal(this.process_handle, out value, out num))
				{
					throw new System.ComponentModel.Win32Exception();
				}
				return (IntPtr)value;
			}
			set
			{
				if (this.HasExited)
				{
					throw new InvalidOperationException(string.Concat(new object[]
					{
						"The process ",
						this.ProcessName,
						" (ID ",
						this.Id,
						") has exited"
					}));
				}
				if (!Process.SetWorkingSet_internal(this.process_handle, value.ToInt32(), 0, true))
				{
					throw new System.ComponentModel.Win32Exception();
				}
			}
		}

		// Token: 0x06001404 RID: 5124
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern ProcessModule[] GetModules_internal(IntPtr handle);

		/// <summary>Gets the modules that have been loaded by the associated process.</summary>
		/// <returns>An array of type <see cref="T:System.Diagnostics.ProcessModule" /> that represents the modules that have been loaded by the associated process.</returns>
		/// <exception cref="T:System.NotSupportedException">You are attempting to access the <see cref="P:System.Diagnostics.Process.Modules" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer. </exception>
		/// <exception cref="T:System.InvalidOperationException">The process <see cref="P:System.Diagnostics.Process.Id" /> is not available.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me); set <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> to false to access this property on Windows 98 and Windows Me.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">You are attempting to access the <see cref="P:System.Diagnostics.Process.Modules" /> property for either the system process or the idle process. These processes do not have modules.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06001405 RID: 5125 RVA: 0x00035164 File Offset: 0x00033364
		[MonitoringDescription("The modules that are loaded as part of this process.")]
		[System.ComponentModel.Browsable(false)]
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		public ProcessModuleCollection Modules
		{
			get
			{
				if (this.module_collection == null)
				{
					this.module_collection = new ProcessModuleCollection(this.GetModules_internal(this.process_handle));
				}
				return this.module_collection;
			}
		}

		// Token: 0x06001406 RID: 5126
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern long GetProcessData(int pid, int data_type, out int error);

		/// <summary>Gets the nonpaged system memory size allocated to this process.</summary>
		/// <returns>The amount of memory, in bytes, the system has allocated for the associated process that cannot be written to the virtual memory paging file.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06001407 RID: 5127 RVA: 0x0003519C File Offset: 0x0003339C
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		[Obsolete("Use NonpagedSystemMemorySize64")]
		[MonoTODO]
		[MonitoringDescription("The number of bytes that are not pageable.")]
		public int NonpagedSystemMemorySize
		{
			get
			{
				return 0;
			}
		}

		/// <summary>Gets the paged memory size.</summary>
		/// <returns>The amount of memory, in bytes, allocated by the associated process that can be written to the virtual memory paging file.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06001408 RID: 5128 RVA: 0x000351A0 File Offset: 0x000333A0
		[MonoTODO]
		[MonitoringDescription("The number of bytes that are paged.")]
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		[Obsolete("Use PagedMemorySize64")]
		public int PagedMemorySize
		{
			get
			{
				return 0;
			}
		}

		/// <summary>Gets the paged system memory size.</summary>
		/// <returns>The amount of memory, in bytes, the system has allocated for the associated process that can be written to the virtual memory paging file.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06001409 RID: 5129 RVA: 0x000351A4 File Offset: 0x000333A4
		[Obsolete("Use PagedSystemMemorySize64")]
		[MonitoringDescription("The amount of paged system memory in bytes.")]
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		[MonoTODO]
		public int PagedSystemMemorySize
		{
			get
			{
				return 0;
			}
		}

		/// <summary>Gets the peak paged memory size.</summary>
		/// <returns>The maximum amount of memory, in bytes, allocated by the associated process that could be written to the virtual memory paging file.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x0600140A RID: 5130 RVA: 0x000351A8 File Offset: 0x000333A8
		[MonoTODO]
		[MonitoringDescription("The maximum amount of paged memory used by this process.")]
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		[Obsolete("Use PeakPagedMemorySize64")]
		public int PeakPagedMemorySize
		{
			get
			{
				return 0;
			}
		}

		/// <summary>Gets the peak virtual memory size.</summary>
		/// <returns>The maximum amount of virtual memory, in bytes, that the associated process has requested.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x0600140B RID: 5131 RVA: 0x000351AC File Offset: 0x000333AC
		[Obsolete("Use PeakVirtualMemorySize64")]
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The maximum amount of virtual memory used by this process.")]
		public int PeakVirtualMemorySize
		{
			get
			{
				int num;
				return (int)Process.GetProcessData(this.pid, 8, out num);
			}
		}

		/// <summary>Gets the peak working set size for the associated process.</summary>
		/// <returns>The maximum amount of physical memory that the associated process has required all at once, in bytes.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x0600140C RID: 5132 RVA: 0x000351C8 File Offset: 0x000333C8
		[MonitoringDescription("The maximum amount of system memory used by this process.")]
		[Obsolete("Use PeakWorkingSet64")]
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		public int PeakWorkingSet
		{
			get
			{
				int num;
				return (int)Process.GetProcessData(this.pid, 5, out num);
			}
		}

		/// <summary>Gets the amount of nonpaged system memory allocated for the associated process.</summary>
		/// <returns>The amount of system memory, in bytes, allocated for the associated process that cannot be written to the virtual memory paging file.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x0600140D RID: 5133 RVA: 0x000351E4 File Offset: 0x000333E4
		[ComVisible(false)]
		[MonoTODO]
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The number of bytes that are not pageable.")]
		public long NonpagedSystemMemorySize64
		{
			get
			{
				return 0L;
			}
		}

		/// <summary>Gets the amount of paged memory allocated for the associated process.</summary>
		/// <returns>The amount of memory, in bytes, allocated in the virtual memory paging file for the associated process.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x0600140E RID: 5134 RVA: 0x000351E8 File Offset: 0x000333E8
		[MonitoringDescription("The number of bytes that are paged.")]
		[ComVisible(false)]
		[MonoTODO]
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		public long PagedMemorySize64
		{
			get
			{
				return 0L;
			}
		}

		/// <summary>Gets the amount of pageable system memory allocated for the associated process.</summary>
		/// <returns>The amount of system memory, in bytes, allocated for the associated process that can be written to the virtual memory paging file.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x0600140F RID: 5135 RVA: 0x000351EC File Offset: 0x000333EC
		[MonoTODO]
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The amount of paged system memory in bytes.")]
		[ComVisible(false)]
		public long PagedSystemMemorySize64
		{
			get
			{
				return 0L;
			}
		}

		/// <summary>Gets the maximum amount of memory in the virtual memory paging file used by the associated process.</summary>
		/// <returns>The maximum amount of memory, in bytes, allocated in the virtual memory paging file for the associated process since it was started.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06001410 RID: 5136 RVA: 0x000351F0 File Offset: 0x000333F0
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The maximum amount of paged memory used by this process.")]
		[ComVisible(false)]
		[MonoTODO]
		public long PeakPagedMemorySize64
		{
			get
			{
				return 0L;
			}
		}

		/// <summary>Gets the maximum amount of virtual memory used by the associated process.</summary>
		/// <returns>The maximum amount of virtual memory, in bytes, allocated for the associated process since it was started.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06001411 RID: 5137 RVA: 0x000351F4 File Offset: 0x000333F4
		[ComVisible(false)]
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The maximum amount of virtual memory used by this process.")]
		public long PeakVirtualMemorySize64
		{
			get
			{
				int num;
				return Process.GetProcessData(this.pid, 8, out num);
			}
		}

		/// <summary>Gets the maximum amount of physical memory used by the associated process.</summary>
		/// <returns>The maximum amount of physical memory, in bytes, allocated for the associated process since it was started.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06001412 RID: 5138 RVA: 0x00035210 File Offset: 0x00033410
		[MonitoringDescription("The maximum amount of system memory used by this process.")]
		[ComVisible(false)]
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		public long PeakWorkingSet64
		{
			get
			{
				int num;
				return Process.GetProcessData(this.pid, 5, out num);
			}
		}

		/// <summary>Gets or sets a value indicating whether the associated process priority should temporarily be boosted by the operating system when the main window has the focus.</summary>
		/// <returns>true if dynamic boosting of the process priority should take place for a process when it is taken out of the wait state; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">Priority boost information could not be retrieved from the associated process resource. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.-or- The process identifier or process handle is zero. (The process has not been started.) </exception>
		/// <exception cref="T:System.NotSupportedException">You are attempting to access the <see cref="P:System.Diagnostics.Process.PriorityBoostEnabled" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer. </exception>
		/// <exception cref="T:System.InvalidOperationException">The process <see cref="P:System.Diagnostics.Process.Id" /> is not available.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06001413 RID: 5139 RVA: 0x0003522C File Offset: 0x0003342C
		// (set) Token: 0x06001414 RID: 5140 RVA: 0x00035230 File Offset: 0x00033430
		[MonoTODO]
		[MonitoringDescription("Process will be of higher priority while it is actively used.")]
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		public bool PriorityBoostEnabled
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the overall priority category for the associated process.</summary>
		/// <returns>The priority category for the associated process, from which the <see cref="P:System.Diagnostics.Process.BasePriority" /> of the process is calculated.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">Process priority information could not be set or retrieved from the associated process resource.-or- The process identifier or process handle is zero. (The process has not been started.) </exception>
		/// <exception cref="T:System.NotSupportedException">You are attempting to access the <see cref="P:System.Diagnostics.Process.PriorityClass" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer. </exception>
		/// <exception cref="T:System.InvalidOperationException">The process <see cref="P:System.Diagnostics.Process.Id" /> is not available.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">You have set the <see cref="P:System.Diagnostics.Process.PriorityClass" /> to AboveNormal or BelowNormal when using Windows 98 or Windows Millennium Edition (Windows Me). These platforms do not support those values for the priority class. </exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">Priority class cannot be set because it does not use a valid value, as defined in the <see cref="T:System.Diagnostics.ProcessPriorityClass" /> enumeration.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06001415 RID: 5141 RVA: 0x00035234 File Offset: 0x00033434
		// (set) Token: 0x06001416 RID: 5142 RVA: 0x00035280 File Offset: 0x00033480
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		[MonoLimitation("Under Unix, only root is allowed to raise the priority.")]
		[MonitoringDescription("The relative process priority.")]
		public ProcessPriorityClass PriorityClass
		{
			get
			{
				if (this.process_handle == IntPtr.Zero)
				{
					throw new InvalidOperationException("Process has not been started.");
				}
				int error;
				int priorityClass = Process.GetPriorityClass(this.process_handle, out error);
				if (priorityClass == 0)
				{
					throw new System.ComponentModel.Win32Exception(error);
				}
				return (ProcessPriorityClass)priorityClass;
			}
			set
			{
				if (!Enum.IsDefined(typeof(ProcessPriorityClass), value))
				{
					throw new System.ComponentModel.InvalidEnumArgumentException("value", (int)value, typeof(ProcessPriorityClass));
				}
				if (this.process_handle == IntPtr.Zero)
				{
					throw new InvalidOperationException("Process has not been started.");
				}
				int error;
				if (!Process.SetPriorityClass(this.process_handle, (int)value, out error))
				{
					throw new System.ComponentModel.Win32Exception(error);
				}
			}
		}

		// Token: 0x06001417 RID: 5143
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetPriorityClass(IntPtr handle, out int error);

		// Token: 0x06001418 RID: 5144
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetPriorityClass(IntPtr handle, int priority, out int error);

		/// <summary>Gets the private memory size.</summary>
		/// <returns>The number of bytes allocated by the associated process that cannot be shared with other processes.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001419 RID: 5145 RVA: 0x000352F8 File Offset: 0x000334F8
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The amount of memory exclusively used by this process.")]
		[Obsolete("Use PrivateMemorySize64")]
		public int PrivateMemorySize
		{
			get
			{
				int num;
				return (int)Process.GetProcessData(this.pid, 6, out num);
			}
		}

		/// <summary>Gets the Terminal Services session identifier for the associated process.</summary>
		/// <returns>The Terminal Services session identifier for the associated process.</returns>
		/// <exception cref="T:System.NullReferenceException">There is no session associated with this process.</exception>
		/// <exception cref="T:System.InvalidOperationException">There is no process associated with this session identifier.-or-The associated process is not on this machine. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The <see cref="P:System.Diagnostics.Process.SessionId" /> property is not supported on Windows 98.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x0600141A RID: 5146 RVA: 0x00035314 File Offset: 0x00033514
		[MonitoringDescription("The session ID for this process.")]
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		[MonoNotSupported("")]
		public int SessionId
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600141B RID: 5147
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern long Times(IntPtr handle, int type);

		/// <summary>Gets the privileged processor time for this process.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> that indicates the amount of time that the process has spent running code inside the operating system core.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property. </exception>
		/// <exception cref="T:System.NotSupportedException">You are attempting to access the <see cref="P:System.Diagnostics.Process.PrivilegedProcessorTime" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x0600141C RID: 5148 RVA: 0x0003531C File Offset: 0x0003351C
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The amount of processing time spent in the OS core for this process.")]
		public TimeSpan PrivilegedProcessorTime
		{
			get
			{
				return new TimeSpan(Process.Times(this.process_handle, 1));
			}
		}

		// Token: 0x0600141D RID: 5149
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string ProcessName_internal(IntPtr handle);

		/// <summary>Gets the name of the process.</summary>
		/// <returns>The name that the system uses to identify the process to the user.</returns>
		/// <exception cref="T:System.InvalidOperationException">The process does not have an identifier, or no process is associated with the <see cref="T:System.Diagnostics.Process" />.-or- The associated process has exited. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me); set <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> to false to access this property on Windows 98 and Windows Me.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x0600141E RID: 5150 RVA: 0x00035330 File Offset: 0x00033530
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The name of this process.")]
		public string ProcessName
		{
			get
			{
				if (this.process_name == null)
				{
					if (this.process_handle == IntPtr.Zero)
					{
						throw new InvalidOperationException("No process is associated with this object.");
					}
					this.process_name = Process.ProcessName_internal(this.process_handle);
					if (this.process_name == null)
					{
						throw new InvalidOperationException("Process has exited, so the requested information is not available.");
					}
					if (this.process_name.EndsWith(".exe") || this.process_name.EndsWith(".bat") || this.process_name.EndsWith(".com"))
					{
						this.process_name = this.process_name.Substring(0, this.process_name.Length - 4);
					}
				}
				return this.process_name;
			}
		}

		/// <summary>Gets or sets the processors on which the threads in this process can be scheduled to run.</summary>
		/// <returns>A bitmask representing the processors that the threads in the associated process can run on. The default depends on the number of processors on the computer. The default value is 2 n -1, where n is the number of processors.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">
		///   <see cref="P:System.Diagnostics.Process.ProcessorAffinity" /> information could not be set or retrieved from the associated process resource.-or- The process identifier or process handle is zero. (The process has not been started.) </exception>
		/// <exception cref="T:System.NotSupportedException">You are attempting to access the <see cref="P:System.Diagnostics.Process.ProcessorAffinity" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer. </exception>
		/// <exception cref="T:System.InvalidOperationException">The process <see cref="P:System.Diagnostics.Process.Id" /> was not available.-or- The process has exited. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x0600141F RID: 5151 RVA: 0x000353F4 File Offset: 0x000335F4
		// (set) Token: 0x06001420 RID: 5152 RVA: 0x000353FC File Offset: 0x000335FC
		[MonitoringDescription("Allowed processor that can be used by this process.")]
		[MonoTODO]
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		public IntPtr ProcessorAffinity
		{
			get
			{
				return (IntPtr)0;
			}
			set
			{
			}
		}

		/// <summary>Gets a value indicating whether the user interface of the process is responding.</summary>
		/// <returns>true if the user interface of the associated process is responding to the system; otherwise, false.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me); set <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> to false to access this property on Windows 98 and Windows Me.</exception>
		/// <exception cref="T:System.InvalidOperationException">There is no process associated with this <see cref="T:System.Diagnostics.Process" /> object. </exception>
		/// <exception cref="T:System.NotSupportedException">You are attempting to access the <see cref="P:System.Diagnostics.Process.Responding" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06001421 RID: 5153 RVA: 0x00035400 File Offset: 0x00033600
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		[MonoTODO]
		[MonitoringDescription("Is this process responsive.")]
		public bool Responding
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a stream used to read the error output of the application.</summary>
		/// <returns>A <see cref="T:System.IO.StreamReader" /> that can be used to read the standard error stream of the application.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.Process.StandardError" /> stream has not been defined for redirection; ensure <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardError" /> is set to true and <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> is set to false.- or - The <see cref="P:System.Diagnostics.Process.StandardError" /> stream has been opened for asynchronous read operations with <see cref="M:System.Diagnostics.Process.BeginErrorReadLine" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06001422 RID: 5154 RVA: 0x00035404 File Offset: 0x00033604
		[MonitoringDescription("The standard error stream of this process.")]
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		[System.ComponentModel.Browsable(false)]
		public StreamReader StandardError
		{
			get
			{
				if (this.error_stream == null)
				{
					throw new InvalidOperationException("Standard error has not been redirected");
				}
				if ((this.async_mode & Process.AsyncModes.AsyncError) != Process.AsyncModes.NoneYet)
				{
					throw new InvalidOperationException("Cannot mix asynchronous and synchonous reads.");
				}
				this.async_mode |= Process.AsyncModes.SyncError;
				return this.error_stream;
			}
		}

		/// <summary>Gets a stream used to write the input of the application.</summary>
		/// <returns>A <see cref="T:System.IO.StreamWriter" /> that can be used to write the standard input stream of the application.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.Process.StandardInput" /> stream has not been defined because <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardInput" /> is set to false. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06001423 RID: 5155 RVA: 0x00035454 File Offset: 0x00033654
		[MonitoringDescription("The standard input stream of this process.")]
		[System.ComponentModel.Browsable(false)]
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		public StreamWriter StandardInput
		{
			get
			{
				if (this.input_stream == null)
				{
					throw new InvalidOperationException("Standard input has not been redirected");
				}
				return this.input_stream;
			}
		}

		/// <summary>Gets a stream used to read the output of the application.</summary>
		/// <returns>A <see cref="T:System.IO.StreamReader" /> that can be used to read the standard output stream of the application.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.Process.StandardOutput" /> stream has not been defined for redirection; ensure <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardOutput" /> is set to true and <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> is set to false.- or - The <see cref="P:System.Diagnostics.Process.StandardOutput" /> stream has been opened for asynchronous read operations with <see cref="M:System.Diagnostics.Process.BeginOutputReadLine" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06001424 RID: 5156 RVA: 0x00035474 File Offset: 0x00033674
		[System.ComponentModel.Browsable(false)]
		[MonitoringDescription("The standard output stream of this process.")]
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		public StreamReader StandardOutput
		{
			get
			{
				if (this.output_stream == null)
				{
					throw new InvalidOperationException("Standard output has not been redirected");
				}
				if ((this.async_mode & Process.AsyncModes.AsyncOutput) != Process.AsyncModes.NoneYet)
				{
					throw new InvalidOperationException("Cannot mix asynchronous and synchonous reads.");
				}
				this.async_mode |= Process.AsyncModes.SyncOutput;
				return this.output_stream;
			}
		}

		/// <summary>Gets or sets the properties to pass to the <see cref="M:System.Diagnostics.Process.Start" /> method of the <see cref="T:System.Diagnostics.Process" />.</summary>
		/// <returns>The <see cref="T:System.Diagnostics.ProcessStartInfo" /> that represents the data with which to start the process. These arguments include the name of the executable file or document used to start the process.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value that specifies the <see cref="P:System.Diagnostics.Process.StartInfo" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06001425 RID: 5157 RVA: 0x000354C4 File Offset: 0x000336C4
		// (set) Token: 0x06001426 RID: 5158 RVA: 0x000354E4 File Offset: 0x000336E4
		[MonitoringDescription("Information for the start of this process.")]
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content)]
		[System.ComponentModel.Browsable(false)]
		public ProcessStartInfo StartInfo
		{
			get
			{
				if (this.start_info == null)
				{
					this.start_info = new ProcessStartInfo();
				}
				return this.start_info;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.start_info = value;
			}
		}

		// Token: 0x06001427 RID: 5159
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern long StartTime_internal(IntPtr handle);

		/// <summary>Gets the time that the associated process was started.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> that indicates when the process started. This only has meaning for started processes.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property. </exception>
		/// <exception cref="T:System.NotSupportedException">You are attempting to access the <see cref="P:System.Diagnostics.Process.StartTime" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer. </exception>
		/// <exception cref="T:System.InvalidOperationException">The process has exited.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred in the call to the Windows function.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06001428 RID: 5160 RVA: 0x00035500 File Offset: 0x00033700
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The time this process started.")]
		public DateTime StartTime
		{
			get
			{
				return DateTime.FromFileTime(Process.StartTime_internal(this.process_handle));
			}
		}

		/// <summary>Gets or sets the object used to marshal the event handler calls that are issued as a result of a process exit event.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.ISynchronizeInvoke" /> used to marshal event handler calls that are issued as a result of an <see cref="E:System.Diagnostics.Process.Exited" /> event on the process.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06001429 RID: 5161 RVA: 0x00035514 File Offset: 0x00033714
		// (set) Token: 0x0600142A RID: 5162 RVA: 0x0003551C File Offset: 0x0003371C
		[System.ComponentModel.DefaultValue(null)]
		[System.ComponentModel.Browsable(false)]
		[MonitoringDescription("The object that is used to synchronize event handler calls for this process.")]
		public System.ComponentModel.ISynchronizeInvoke SynchronizingObject
		{
			get
			{
				return this.synchronizingObject;
			}
			set
			{
				this.synchronizingObject = value;
			}
		}

		/// <summary>Gets the set of threads that are running in the associated process.</summary>
		/// <returns>An array of type <see cref="T:System.Diagnostics.ProcessThread" /> representing the operating system threads currently running in the associated process.</returns>
		/// <exception cref="T:System.SystemException">The process does not have an <see cref="P:System.Diagnostics.Process.Id" />, or no process is associated with the <see cref="T:System.Diagnostics.Process" /> instance.-or- The associated process has exited. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me); set <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> to false to access this property on Windows 98 and Windows Me.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x0600142B RID: 5163 RVA: 0x00035528 File Offset: 0x00033728
		[System.ComponentModel.Browsable(false)]
		[MonoTODO]
		[MonitoringDescription("The number of threads of this process.")]
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		public ProcessThreadCollection Threads
		{
			get
			{
				return ProcessThreadCollection.GetEmpty();
			}
		}

		/// <summary>Gets the total processor time for this process.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> that indicates the amount of time that the associated process has spent utilizing the CPU. This value is the sum of the <see cref="P:System.Diagnostics.Process.UserProcessorTime" /> and the <see cref="P:System.Diagnostics.Process.PrivilegedProcessorTime" />.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property. </exception>
		/// <exception cref="T:System.NotSupportedException">You are attempting to access the <see cref="P:System.Diagnostics.Process.TotalProcessorTime" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x0600142C RID: 5164 RVA: 0x00035530 File Offset: 0x00033730
		[MonitoringDescription("The total CPU time spent for this process.")]
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		public TimeSpan TotalProcessorTime
		{
			get
			{
				return new TimeSpan(Process.Times(this.process_handle, 2));
			}
		}

		/// <summary>Gets the user processor time for this process.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> that indicates the amount of time that the associated process has spent running code inside the application portion of the process (not inside the operating system core).</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property. </exception>
		/// <exception cref="T:System.NotSupportedException">You are attempting to access the <see cref="P:System.Diagnostics.Process.UserProcessorTime" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x0600142D RID: 5165 RVA: 0x00035544 File Offset: 0x00033744
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The CPU time spent for this process in user mode.")]
		public TimeSpan UserProcessorTime
		{
			get
			{
				return new TimeSpan(Process.Times(this.process_handle, 0));
			}
		}

		/// <summary>Gets the size of the process's virtual memory.</summary>
		/// <returns>The amount of virtual memory, in bytes, that the associated process has requested.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x0600142E RID: 5166 RVA: 0x00035558 File Offset: 0x00033758
		[MonitoringDescription("The amount of virtual memory currently used for this process.")]
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		[Obsolete("Use VirtualMemorySize64")]
		public int VirtualMemorySize
		{
			get
			{
				int num;
				return (int)Process.GetProcessData(this.pid, 7, out num);
			}
		}

		/// <summary>Gets the associated process's physical memory usage.</summary>
		/// <returns>The total amount of physical memory the associated process is using, in bytes.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x0600142F RID: 5167 RVA: 0x00035574 File Offset: 0x00033774
		[MonitoringDescription("The amount of physical memory currently used for this process.")]
		[Obsolete("Use WorkingSet64")]
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		public int WorkingSet
		{
			get
			{
				int num;
				return (int)Process.GetProcessData(this.pid, 4, out num);
			}
		}

		/// <summary>Gets the amount of private memory allocated for the associated process.</summary>
		/// <returns>The amount of memory, in bytes, allocated for the associated process that cannot be shared with other processes.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06001430 RID: 5168 RVA: 0x00035590 File Offset: 0x00033790
		[ComVisible(false)]
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The amount of memory exclusively used by this process.")]
		public long PrivateMemorySize64
		{
			get
			{
				int num;
				return Process.GetProcessData(this.pid, 6, out num);
			}
		}

		/// <summary>Gets the amount of the virtual memory allocated for the associated process.</summary>
		/// <returns>The amount of virtual memory, in bytes, allocated for the associated process.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06001431 RID: 5169 RVA: 0x000355AC File Offset: 0x000337AC
		[ComVisible(false)]
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The amount of virtual memory currently used for this process.")]
		public long VirtualMemorySize64
		{
			get
			{
				int num;
				return Process.GetProcessData(this.pid, 7, out num);
			}
		}

		/// <summary>Gets the amount of physical memory allocated for the associated process.</summary>
		/// <returns>The amount of physical memory, in bytes, allocated for the associated process.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me), which does not support this property.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06001432 RID: 5170 RVA: 0x000355C8 File Offset: 0x000337C8
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		[ComVisible(false)]
		[MonitoringDescription("The amount of physical memory currently used for this process.")]
		public long WorkingSet64
		{
			get
			{
				int num;
				return Process.GetProcessData(this.pid, 4, out num);
			}
		}

		/// <summary>Frees all the resources that are associated with this component.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001433 RID: 5171 RVA: 0x000355E4 File Offset: 0x000337E4
		public void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x06001434 RID: 5172
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Kill_internal(IntPtr handle, int signo);

		// Token: 0x06001435 RID: 5173 RVA: 0x000355F0 File Offset: 0x000337F0
		private bool Close(int signo)
		{
			if (this.process_handle == IntPtr.Zero)
			{
				throw new SystemException("No process to kill.");
			}
			int num = Process.ExitCode_internal(this.process_handle);
			if (num != 259)
			{
				throw new InvalidOperationException("The process already finished.");
			}
			return Process.Kill_internal(this.process_handle, signo);
		}

		/// <summary>Closes a process that has a user interface by sending a close message to its main window.</summary>
		/// <returns>true if the close message was successfully sent; false if the associated process does not have a main window or if the main window is disabled (for example if a modal dialog is being shown).</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me); set the <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> property to false to access this property on Windows 98 and Windows Me.</exception>
		/// <exception cref="T:System.InvalidOperationException">The process has already exited. -or-No process is associated with this <see cref="T:System.Diagnostics.Process" /> object.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001436 RID: 5174 RVA: 0x0003564C File Offset: 0x0003384C
		public bool CloseMainWindow()
		{
			return this.Close(2);
		}

		/// <summary>Puts a <see cref="T:System.Diagnostics.Process" /> component in state to interact with operating system processes that run in a special mode by enabling the native property SeDebugPrivilege on the current thread.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001437 RID: 5175 RVA: 0x00035658 File Offset: 0x00033858
		[MonoTODO]
		public static void EnterDebugMode()
		{
		}

		// Token: 0x06001438 RID: 5176
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetProcess_internal(int pid);

		// Token: 0x06001439 RID: 5177
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetPid_internal();

		/// <summary>Gets a new <see cref="T:System.Diagnostics.Process" /> component and associates it with the currently active process.</summary>
		/// <returns>A new <see cref="T:System.Diagnostics.Process" /> component associated with the process resource that is running the calling application.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600143A RID: 5178 RVA: 0x0003565C File Offset: 0x0003385C
		public static Process GetCurrentProcess()
		{
			int pid_internal = Process.GetPid_internal();
			IntPtr process_internal = Process.GetProcess_internal(pid_internal);
			if (process_internal == IntPtr.Zero)
			{
				throw new SystemException("Can't find current process");
			}
			return new Process(process_internal, pid_internal);
		}

		/// <summary>Returns a new <see cref="T:System.Diagnostics.Process" /> component, given the identifier of a process on the local computer.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.Process" /> component that is associated with the local process resource identified by the <paramref name="processId" /> parameter.</returns>
		/// <param name="processId">The system-unique identifier of a process resource. </param>
		/// <exception cref="T:System.ArgumentException">The process specified by the <paramref name="processId" /> parameter is not running. The identifier might be expired. </exception>
		/// <exception cref="T:System.InvalidOperationException">The process was not started by this object.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600143B RID: 5179 RVA: 0x00035698 File Offset: 0x00033898
		public static Process GetProcessById(int processId)
		{
			IntPtr process_internal = Process.GetProcess_internal(processId);
			if (process_internal == IntPtr.Zero)
			{
				throw new ArgumentException("Can't find process with ID " + processId.ToString());
			}
			return new Process(process_internal, processId);
		}

		/// <summary>Returns a new <see cref="T:System.Diagnostics.Process" /> component, given a process identifier and the name of a computer on the network.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.Process" /> component that is associated with a remote process resource identified by the <paramref name="processId" /> parameter.</returns>
		/// <param name="processId">The system-unique identifier of a process resource. </param>
		/// <param name="machineName">The name of a computer on the network. </param>
		/// <exception cref="T:System.ArgumentException">The process specified by the <paramref name="processId" /> parameter is not running. The identifier might be expired.-or- The <paramref name="machineName" /> parameter syntax is invalid. The name might have length zero (0). </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="machineName" /> parameter is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The process was not started by this object.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600143C RID: 5180 RVA: 0x000356DC File Offset: 0x000338DC
		[MonoTODO("There is no support for retrieving process information from a remote machine")]
		public static Process GetProcessById(int processId, string machineName)
		{
			if (machineName == null)
			{
				throw new ArgumentNullException("machineName");
			}
			if (!Process.IsLocalMachine(machineName))
			{
				throw new NotImplementedException();
			}
			return Process.GetProcessById(processId);
		}

		// Token: 0x0600143D RID: 5181
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int[] GetProcesses_internal();

		/// <summary>Creates a new <see cref="T:System.Diagnostics.Process" /> component for each process resource on the local computer.</summary>
		/// <returns>An array of type <see cref="T:System.Diagnostics.Process" /> that represents all the process resources running on the local computer.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600143E RID: 5182 RVA: 0x00035714 File Offset: 0x00033914
		public static Process[] GetProcesses()
		{
			int[] processes_internal = Process.GetProcesses_internal();
			ArrayList arrayList = new ArrayList();
			if (processes_internal == null)
			{
				return new Process[0];
			}
			for (int i = 0; i < processes_internal.Length; i++)
			{
				try
				{
					arrayList.Add(Process.GetProcessById(processes_internal[i]));
				}
				catch (SystemException)
				{
				}
			}
			return (Process[])arrayList.ToArray(typeof(Process));
		}

		/// <summary>Creates a new <see cref="T:System.Diagnostics.Process" /> component for each process resource on the specified computer.</summary>
		/// <returns>An array of type <see cref="T:System.Diagnostics.Process" /> that represents all the process resources running on the specified computer.</returns>
		/// <param name="machineName">The computer from which to read the list of processes. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="machineName" /> parameter syntax is invalid. It might have length zero (0). </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="machineName" /> parameter is null. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The operating system platform does not support this operation on remote computers. </exception>
		/// <exception cref="T:System.InvalidOperationException">There are problems accessing the performance counter API's used to get process information. This exception is specific to Windows NT, Windows 2000, and Windows XP. </exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A problem occurred accessing an underlying system API. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600143F RID: 5183 RVA: 0x0003579C File Offset: 0x0003399C
		[MonoTODO("There is no support for retrieving process information from a remote machine")]
		public static Process[] GetProcesses(string machineName)
		{
			if (machineName == null)
			{
				throw new ArgumentNullException("machineName");
			}
			if (!Process.IsLocalMachine(machineName))
			{
				throw new NotImplementedException();
			}
			return Process.GetProcesses();
		}

		/// <summary>Creates an array of new <see cref="T:System.Diagnostics.Process" /> components and associates them with all the process resources on the local computer that share the specified process name.</summary>
		/// <returns>An array of type <see cref="T:System.Diagnostics.Process" /> that represents the process resources running the specified application or file.</returns>
		/// <param name="processName">The friendly name of the process. </param>
		/// <exception cref="T:System.InvalidOperationException">There are problems accessing the performance counter API's used to get process information. This exception is specific to Windows NT, Windows 2000, and Windows XP. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001440 RID: 5184 RVA: 0x000357C8 File Offset: 0x000339C8
		public static Process[] GetProcessesByName(string processName)
		{
			Process[] processes = Process.GetProcesses();
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < processes.Length; i++)
			{
				try
				{
					if (string.Compare(processName, processes[i].ProcessName, true) == 0)
					{
						arrayList.Add(processes[i]);
					}
				}
				catch (Exception)
				{
				}
			}
			return (Process[])arrayList.ToArray(typeof(Process));
		}

		/// <summary>Creates an array of new <see cref="T:System.Diagnostics.Process" /> components and associates them with all the process resources on a remote computer that share the specified process name.</summary>
		/// <returns>An array of type <see cref="T:System.Diagnostics.Process" /> that represents the process resources running the specified application or file.</returns>
		/// <param name="processName">The friendly name of the process. </param>
		/// <param name="machineName">The name of a computer on the network. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="machineName" /> parameter syntax is invalid. It might have length zero (0). </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="machineName" /> parameter is null. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The operating system platform does not support this operation on remote computers. </exception>
		/// <exception cref="T:System.InvalidOperationException">There are problems accessing the performance counter API's used to get process information. This exception is specific to Windows NT, Windows 2000, and Windows XP. </exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A problem occurred accessing an underlying system API. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001441 RID: 5185 RVA: 0x00035850 File Offset: 0x00033A50
		[MonoTODO]
		public static Process[] GetProcessesByName(string processName, string machineName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Immediately stops the associated process.</summary>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The associated process could not be terminated. -or-The process is terminating.-or- The associated process is a Win16 executable.</exception>
		/// <exception cref="T:System.NotSupportedException">You are attempting to call <see cref="M:System.Diagnostics.Process.Kill" /> for a process that is running on a remote computer. The method is available only for processes running on the local computer.</exception>
		/// <exception cref="T:System.InvalidOperationException">The process has already exited. -or-There is no process associated with this <see cref="T:System.Diagnostics.Process" /> object.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001442 RID: 5186 RVA: 0x00035858 File Offset: 0x00033A58
		public void Kill()
		{
			this.Close(1);
		}

		/// <summary>Takes a <see cref="T:System.Diagnostics.Process" /> component out of the state that lets it interact with operating system processes that run in a special mode.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001443 RID: 5187 RVA: 0x00035864 File Offset: 0x00033A64
		[MonoTODO]
		public static void LeaveDebugMode()
		{
		}

		/// <summary>Discards any information about the associated process that has been cached inside the process component.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001444 RID: 5188 RVA: 0x00035868 File Offset: 0x00033A68
		public void Refresh()
		{
		}

		// Token: 0x06001445 RID: 5189
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ShellExecuteEx_internal(ProcessStartInfo startInfo, ref Process.ProcInfo proc_info);

		// Token: 0x06001446 RID: 5190
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CreateProcess_internal(ProcessStartInfo startInfo, IntPtr stdin, IntPtr stdout, IntPtr stderr, ref Process.ProcInfo proc_info);

		// Token: 0x06001447 RID: 5191 RVA: 0x0003586C File Offset: 0x00033A6C
		private static bool Start_shell(ProcessStartInfo startInfo, Process process)
		{
			Process.ProcInfo procInfo = default(Process.ProcInfo);
			if (startInfo.RedirectStandardInput || startInfo.RedirectStandardOutput || startInfo.RedirectStandardError)
			{
				throw new InvalidOperationException("UseShellExecute must be false when redirecting I/O.");
			}
			if (startInfo.HaveEnvVars)
			{
				throw new InvalidOperationException("UseShellExecute must be false in order to use environment variables.");
			}
			Process.FillUserInfo(startInfo, ref procInfo);
			bool flag;
			try
			{
				flag = Process.ShellExecuteEx_internal(startInfo, ref procInfo);
			}
			finally
			{
				if (procInfo.Password != IntPtr.Zero)
				{
					Marshal.FreeBSTR(procInfo.Password);
				}
				procInfo.Password = IntPtr.Zero;
			}
			if (!flag)
			{
				throw new System.ComponentModel.Win32Exception(-procInfo.pid);
			}
			process.process_handle = procInfo.process_handle;
			process.pid = procInfo.pid;
			process.StartExitCallbackIfNeeded();
			return flag;
		}

		// Token: 0x06001448 RID: 5192 RVA: 0x0003595C File Offset: 0x00033B5C
		private static bool Start_noshell(ProcessStartInfo startInfo, Process process)
		{
			Process.ProcInfo procInfo = default(Process.ProcInfo);
			IntPtr intPtr = IntPtr.Zero;
			IntPtr handle = IntPtr.Zero;
			if (startInfo.HaveEnvVars)
			{
				string[] array = new string[startInfo.EnvironmentVariables.Count];
				startInfo.EnvironmentVariables.Keys.CopyTo(array, 0);
				procInfo.envKeys = array;
				array = new string[startInfo.EnvironmentVariables.Count];
				startInfo.EnvironmentVariables.Values.CopyTo(array, 0);
				procInfo.envValues = array;
			}
			bool flag;
			if (startInfo.RedirectStandardInput)
			{
				if (Process.IsWindows)
				{
					int options = 2;
					IntPtr intPtr2;
					flag = MonoIO.CreatePipe(out intPtr, out intPtr2);
					if (flag)
					{
						flag = MonoIO.DuplicateHandle(Process.GetCurrentProcess().Handle, intPtr2, Process.GetCurrentProcess().Handle, out handle, 0, 0, options);
						MonoIOError monoIOError;
						MonoIO.Close(intPtr2, out monoIOError);
					}
				}
				else
				{
					flag = MonoIO.CreatePipe(out intPtr, out handle);
				}
				if (!flag)
				{
					throw new IOException("Error creating standard input pipe");
				}
			}
			else
			{
				intPtr = MonoIO.ConsoleInput;
				handle = (IntPtr)0;
			}
			IntPtr consoleOutput;
			if (startInfo.RedirectStandardOutput)
			{
				IntPtr zero = IntPtr.Zero;
				if (Process.IsWindows)
				{
					int options2 = 2;
					IntPtr intPtr3;
					flag = MonoIO.CreatePipe(out intPtr3, out consoleOutput);
					if (flag)
					{
						MonoIO.DuplicateHandle(Process.GetCurrentProcess().Handle, intPtr3, Process.GetCurrentProcess().Handle, out zero, 0, 0, options2);
						MonoIOError monoIOError;
						MonoIO.Close(intPtr3, out monoIOError);
					}
				}
				else
				{
					flag = MonoIO.CreatePipe(out zero, out consoleOutput);
				}
				process.stdout_rd = zero;
				if (!flag)
				{
					if (startInfo.RedirectStandardInput)
					{
						MonoIOError monoIOError;
						MonoIO.Close(intPtr, out monoIOError);
						MonoIO.Close(handle, out monoIOError);
					}
					throw new IOException("Error creating standard output pipe");
				}
			}
			else
			{
				process.stdout_rd = (IntPtr)0;
				consoleOutput = MonoIO.ConsoleOutput;
			}
			IntPtr consoleError;
			if (startInfo.RedirectStandardError)
			{
				IntPtr zero2 = IntPtr.Zero;
				if (Process.IsWindows)
				{
					int options3 = 2;
					IntPtr intPtr4;
					flag = MonoIO.CreatePipe(out intPtr4, out consoleError);
					if (flag)
					{
						MonoIO.DuplicateHandle(Process.GetCurrentProcess().Handle, intPtr4, Process.GetCurrentProcess().Handle, out zero2, 0, 0, options3);
						MonoIOError monoIOError;
						MonoIO.Close(intPtr4, out monoIOError);
					}
				}
				else
				{
					flag = MonoIO.CreatePipe(out zero2, out consoleError);
				}
				process.stderr_rd = zero2;
				if (!flag)
				{
					if (startInfo.RedirectStandardInput)
					{
						MonoIOError monoIOError;
						MonoIO.Close(intPtr, out monoIOError);
						MonoIO.Close(handle, out monoIOError);
					}
					if (startInfo.RedirectStandardOutput)
					{
						MonoIOError monoIOError;
						MonoIO.Close(process.stdout_rd, out monoIOError);
						MonoIO.Close(consoleOutput, out monoIOError);
					}
					throw new IOException("Error creating standard error pipe");
				}
			}
			else
			{
				process.stderr_rd = (IntPtr)0;
				consoleError = MonoIO.ConsoleError;
			}
			Process.FillUserInfo(startInfo, ref procInfo);
			try
			{
				flag = Process.CreateProcess_internal(startInfo, intPtr, consoleOutput, consoleError, ref procInfo);
			}
			finally
			{
				if (procInfo.Password != IntPtr.Zero)
				{
					Marshal.FreeBSTR(procInfo.Password);
				}
				procInfo.Password = IntPtr.Zero;
			}
			if (!flag)
			{
				if (startInfo.RedirectStandardInput)
				{
					MonoIOError monoIOError;
					MonoIO.Close(intPtr, out monoIOError);
					MonoIO.Close(handle, out monoIOError);
				}
				if (startInfo.RedirectStandardOutput)
				{
					MonoIOError monoIOError;
					MonoIO.Close(process.stdout_rd, out monoIOError);
					MonoIO.Close(consoleOutput, out monoIOError);
				}
				if (startInfo.RedirectStandardError)
				{
					MonoIOError monoIOError;
					MonoIO.Close(process.stderr_rd, out monoIOError);
					MonoIO.Close(consoleError, out monoIOError);
				}
				throw new System.ComponentModel.Win32Exception(-procInfo.pid, string.Concat(new string[]
				{
					"ApplicationName='",
					startInfo.FileName,
					"', CommandLine='",
					startInfo.Arguments,
					"', CurrentDirectory='",
					startInfo.WorkingDirectory,
					"'"
				}));
			}
			process.process_handle = procInfo.process_handle;
			process.pid = procInfo.pid;
			if (startInfo.RedirectStandardInput)
			{
				MonoIOError monoIOError;
				MonoIO.Close(intPtr, out monoIOError);
				process.input_stream = new StreamWriter(new MonoSyncFileStream(handle, FileAccess.Write, true, 8192), Console.Out.Encoding);
				process.input_stream.AutoFlush = true;
			}
			Encoding encoding = startInfo.StandardOutputEncoding ?? Console.Out.Encoding;
			Encoding encoding2 = startInfo.StandardErrorEncoding ?? Console.Out.Encoding;
			if (startInfo.RedirectStandardOutput)
			{
				MonoIOError monoIOError;
				MonoIO.Close(consoleOutput, out monoIOError);
				process.output_stream = new StreamReader(new MonoSyncFileStream(process.stdout_rd, FileAccess.Read, true, 8192), encoding, true, 8192);
			}
			if (startInfo.RedirectStandardError)
			{
				MonoIOError monoIOError;
				MonoIO.Close(consoleError, out monoIOError);
				process.error_stream = new StreamReader(new MonoSyncFileStream(process.stderr_rd, FileAccess.Read, true, 8192), encoding2, true, 8192);
			}
			process.StartExitCallbackIfNeeded();
			return flag;
		}

		// Token: 0x06001449 RID: 5193 RVA: 0x00035E34 File Offset: 0x00034034
		private static void FillUserInfo(ProcessStartInfo startInfo, ref Process.ProcInfo proc_info)
		{
			if (startInfo.UserName != null)
			{
				proc_info.UserName = startInfo.UserName;
				proc_info.Domain = startInfo.Domain;
				if (startInfo.Password != null)
				{
					proc_info.Password = Marshal.SecureStringToBSTR(startInfo.Password);
				}
				else
				{
					proc_info.Password = IntPtr.Zero;
				}
				proc_info.LoadUserProfile = startInfo.LoadUserProfile;
			}
		}

		// Token: 0x0600144A RID: 5194 RVA: 0x00035E9C File Offset: 0x0003409C
		private static bool Start_common(ProcessStartInfo startInfo, Process process)
		{
			if (startInfo.FileName == null || startInfo.FileName.Length == 0)
			{
				throw new InvalidOperationException("File name has not been set");
			}
			if (startInfo.StandardErrorEncoding != null && !startInfo.RedirectStandardError)
			{
				throw new InvalidOperationException("StandardErrorEncoding is only supported when standard error is redirected");
			}
			if (startInfo.StandardOutputEncoding != null && !startInfo.RedirectStandardOutput)
			{
				throw new InvalidOperationException("StandardOutputEncoding is only supported when standard output is redirected");
			}
			if (!startInfo.UseShellExecute)
			{
				return Process.Start_noshell(startInfo, process);
			}
			if (!string.IsNullOrEmpty(startInfo.UserName))
			{
				throw new InvalidOperationException("UserShellExecute must be false if an explicit UserName is specified when starting a process");
			}
			return Process.Start_shell(startInfo, process);
		}

		/// <summary>Starts (or reuses) the process resource that is specified by the <see cref="P:System.Diagnostics.Process.StartInfo" /> property of this <see cref="T:System.Diagnostics.Process" /> component and associates it with the component.</summary>
		/// <returns>true if a process resource is started; false if no new process resource is started (for example, if an existing process is reused).</returns>
		/// <exception cref="T:System.InvalidOperationException">No file name was specified in the <see cref="T:System.Diagnostics.Process" /> component's <see cref="P:System.Diagnostics.Process.StartInfo" />.-or- The <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> member of the <see cref="P:System.Diagnostics.Process.StartInfo" /> property is true while <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardInput" />, <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardOutput" />, or <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardError" /> is true. </exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">There was an error in opening the associated file. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The process object has already been disposed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600144B RID: 5195 RVA: 0x00035F48 File Offset: 0x00034148
		public bool Start()
		{
			if (this.process_handle != IntPtr.Zero)
			{
				this.Process_free_internal(this.process_handle);
				this.process_handle = IntPtr.Zero;
			}
			return Process.Start_common(this.start_info, this);
		}

		/// <summary>Starts the process resource that is specified by the parameter containing process start information (for example, the file name of the process to start) and associates the resource with a new <see cref="T:System.Diagnostics.Process" /> component.</summary>
		/// <returns>A new <see cref="T:System.Diagnostics.Process" /> component that is associated with the process resource, or null if no process resource is started (for example, if an existing process is reused).</returns>
		/// <param name="startInfo">The <see cref="T:System.Diagnostics.ProcessStartInfo" /> that contains the information that is used to start the process, including the file name and any command-line arguments. </param>
		/// <exception cref="T:System.InvalidOperationException">No file name was specified in the <paramref name="startInfo" /> parameter's <see cref="P:System.Diagnostics.ProcessStartInfo.FileName" /> property.-or- The <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> property of the <paramref name="startInfo" /> parameter is true and the <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardInput" />, <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardOutput" />, or <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardError" /> property is also true.-or-The <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> property of the <paramref name="startInfo" /> parameter is true and the <see cref="P:System.Diagnostics.ProcessStartInfo.UserName" /> property is not null or empty or the <see cref="P:System.Diagnostics.ProcessStartInfo.Password" /> property is not null.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="startInfo" /> parameter is null. </exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">There was an error in opening the associated file. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The process object has already been disposed. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified in the <paramref name="startInfo" /> parameter's <see cref="P:System.Diagnostics.ProcessStartInfo.FileName" /> property could not be found.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred when opening the associated file. -or-The sum of the length of the arguments and the length of the full path to the process exceeds 2080. The error message associated with this exception can be one of the following: "The data area passed to a system call is too small." or "Access is denied."</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600144C RID: 5196 RVA: 0x00035F90 File Offset: 0x00034190
		public static Process Start(ProcessStartInfo startInfo)
		{
			if (startInfo == null)
			{
				throw new ArgumentNullException("startInfo");
			}
			Process process = new Process();
			process.StartInfo = startInfo;
			if (Process.Start_common(startInfo, process))
			{
				return process;
			}
			return null;
		}

		/// <summary>Starts a process resource by specifying the name of a document or application file and associates the resource with a new <see cref="T:System.Diagnostics.Process" /> component.</summary>
		/// <returns>A new <see cref="T:System.Diagnostics.Process" /> component that is associated with the process resource, or null, if no process resource is started (for example, if an existing process is reused).</returns>
		/// <param name="fileName">The name of a document or application file to run in the process. </param>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred when opening the associated file. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The process object has already been disposed. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The PATH environment variable has a string containing quotes.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600144D RID: 5197 RVA: 0x00035FCC File Offset: 0x000341CC
		public static Process Start(string fileName)
		{
			return Process.Start(new ProcessStartInfo(fileName));
		}

		/// <summary>Starts a process resource by specifying the name of an application and a set of command-line arguments, and associates the resource with a new <see cref="T:System.Diagnostics.Process" /> component.</summary>
		/// <returns>A new <see cref="T:System.Diagnostics.Process" /> component that is associated with the process, or null, if no process resource is started (for example, if an existing process is reused).</returns>
		/// <param name="fileName">The name of an application file to run in the process. </param>
		/// <param name="arguments">Command-line arguments to pass when starting the process. </param>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="fileName" /> or <paramref name="arguments" /> parameter is null. </exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred when opening the associated file. -or-The sum of the length of the arguments and the length of the full path to the process exceeds 2080. The error message associated with this exception can be one of the following: "The data area passed to a system call is too small." or "Access is denied."</exception>
		/// <exception cref="T:System.ObjectDisposedException">The process object has already been disposed. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The PATH environment variable has a string containing quotes.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600144E RID: 5198 RVA: 0x00035FDC File Offset: 0x000341DC
		public static Process Start(string fileName, string arguments)
		{
			return Process.Start(new ProcessStartInfo(fileName, arguments));
		}

		/// <summary>Starts a process resource by specifying the name of an application, a user name, a password, and a domain and associates the resource with a new <see cref="T:System.Diagnostics.Process" /> component.</summary>
		/// <returns>A new <see cref="T:System.Diagnostics.Process" /> component that is associated with the process resource, or null if no process resource is started (for example, if an existing process is reused).</returns>
		/// <param name="fileName">The name of an application file to run in the process.</param>
		/// <param name="userName">The user name to use when starting the process.</param>
		/// <param name="password">A <see cref="T:System.Security.SecureString" /> that contains the password to use when starting the process.</param>
		/// <param name="domain">The domain to use when starting the process.</param>
		/// <exception cref="T:System.InvalidOperationException">No file name was specified. </exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">
		///   <paramref name="fileName" /> is not an executable (.exe) file.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">There was an error in opening the associated file. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The process object has already been disposed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600144F RID: 5199 RVA: 0x00035FEC File Offset: 0x000341EC
		public static Process Start(string fileName, string username, SecureString password, string domain)
		{
			return Process.Start(fileName, null, username, password, domain);
		}

		/// <summary>Starts a process resource by specifying the name of an application, a set of command-line arguments, a user name, a password, and a domain and associates the resource with a new <see cref="T:System.Diagnostics.Process" /> component.</summary>
		/// <returns>A new <see cref="T:System.Diagnostics.Process" /> component that is associated with the process resource, or null if no process resource is started (for example, if an existing process is reused).</returns>
		/// <param name="fileName">The name of an application file to run in the process. </param>
		/// <param name="arguments">Command-line arguments to pass when starting the process. </param>
		/// <param name="userName">The user name to use when starting the process.</param>
		/// <param name="password">A <see cref="T:System.Security.SecureString" /> that contains the password to use when starting the process.</param>
		/// <param name="domain">The domain to use when starting the process.</param>
		/// <exception cref="T:System.InvalidOperationException">No file name was specified.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred when opening the associated file. -or-The sum of the length of the arguments and the length of the full path to the associated file exceeds 2080. The error message associated with this exception can be one of the following: "The data area passed to a system call is too small." or "Access is denied."</exception>
		/// <exception cref="T:System.ObjectDisposedException">The process object has already been disposed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001450 RID: 5200 RVA: 0x00035FF8 File Offset: 0x000341F8
		public static Process Start(string fileName, string arguments, string username, SecureString password, string domain)
		{
			return Process.Start(new ProcessStartInfo(fileName, arguments)
			{
				UserName = username,
				Password = password,
				Domain = domain,
				UseShellExecute = false
			});
		}

		/// <summary>Formats the process's name as a string, combined with the parent component type, if applicable.</summary>
		/// <returns>The <see cref="P:System.Diagnostics.Process.ProcessName" />, combined with the base component's <see cref="M:System.Object.ToString" /> return value.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">
		///   <see cref="M:System.Diagnostics.Process.ToString" /> is not supported on Windows 98.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001451 RID: 5201 RVA: 0x00036030 File Offset: 0x00034230
		public override string ToString()
		{
			return base.ToString() + " (" + this.ProcessName + ")";
		}

		// Token: 0x06001452 RID: 5202
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool WaitForExit_internal(IntPtr handle, int ms);

		/// <summary>Instructs the <see cref="T:System.Diagnostics.Process" /> component to wait indefinitely for the associated process to exit.</summary>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The wait setting could not be accessed. </exception>
		/// <exception cref="T:System.SystemException">No process <see cref="P:System.Diagnostics.Process.Id" /> has been set, and a <see cref="P:System.Diagnostics.Process.Handle" /> from which the <see cref="P:System.Diagnostics.Process.Id" /> property can be determined does not exist.-or- There is no process associated with this <see cref="T:System.Diagnostics.Process" /> object.-or- You are attempting to call <see cref="M:System.Diagnostics.Process.WaitForExit" /> for a process that is running on a remote computer. This method is available only for processes that are running on the local computer. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001453 RID: 5203 RVA: 0x00036058 File Offset: 0x00034258
		public void WaitForExit()
		{
			this.WaitForExit(-1);
		}

		/// <summary>Instructs the <see cref="T:System.Diagnostics.Process" /> component to wait the specified number of milliseconds for the associated process to exit.</summary>
		/// <returns>true if the associated process has exited; otherwise, false.</returns>
		/// <param name="milliseconds">The amount of time, in milliseconds, to wait for the associated process to exit. The maximum is the largest possible value of a 32-bit integer, which represents infinity to the operating system. </param>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The wait setting could not be accessed. </exception>
		/// <exception cref="T:System.SystemException">No process <see cref="P:System.Diagnostics.Process.Id" /> has been set, and a <see cref="P:System.Diagnostics.Process.Handle" /> from which the <see cref="P:System.Diagnostics.Process.Id" /> property can be determined does not exist.-or- There is no process associated with this <see cref="T:System.Diagnostics.Process" /> object.-or- You are attempting to call <see cref="M:System.Diagnostics.Process.WaitForExit(System.Int32)" /> for a process that is running on a remote computer. This method is available only for processes that are running on the local computer. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001454 RID: 5204 RVA: 0x00036064 File Offset: 0x00034264
		public bool WaitForExit(int milliseconds)
		{
			int num = milliseconds;
			if (num == 2147483647)
			{
				num = -1;
			}
			DateTime d = DateTime.UtcNow;
			if (this.async_output != null && !this.async_output.IsCompleted)
			{
				if (!this.async_output.WaitHandle.WaitOne(num, false))
				{
					return false;
				}
				if (num >= 0)
				{
					DateTime utcNow = DateTime.UtcNow;
					num -= (int)(utcNow - d).TotalMilliseconds;
					if (num <= 0)
					{
						return false;
					}
					d = utcNow;
				}
			}
			if (this.async_error != null && !this.async_error.IsCompleted)
			{
				if (!this.async_error.WaitHandle.WaitOne(num, false))
				{
					return false;
				}
				if (num >= 0)
				{
					num -= (int)(DateTime.UtcNow - d).TotalMilliseconds;
					if (num <= 0)
					{
						return false;
					}
				}
			}
			return this.WaitForExit_internal(this.process_handle, num);
		}

		// Token: 0x06001455 RID: 5205
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool WaitForInputIdle_internal(IntPtr handle, int ms);

		/// <summary>Causes the <see cref="T:System.Diagnostics.Process" /> component to wait indefinitely for the associated process to enter an idle state. This overload applies only to processes with a user interface and, therefore, a message loop.</summary>
		/// <returns>true if the associated process has reached an idle state.</returns>
		/// <exception cref="T:System.InvalidOperationException">The process does not have a graphical interface.-or-An unknown error occurred. The process failed to enter an idle state.-or-The process has already exited. -or-No process is associated with this <see cref="T:System.Diagnostics.Process" /> object.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001456 RID: 5206 RVA: 0x00036150 File Offset: 0x00034350
		[MonoTODO]
		public bool WaitForInputIdle()
		{
			return this.WaitForInputIdle(-1);
		}

		/// <summary>Causes the <see cref="T:System.Diagnostics.Process" /> component to wait the specified number of milliseconds for the associated process to enter an idle state. This overload applies only to processes with a user interface and, therefore, a message loop.</summary>
		/// <returns>true if the associated process has reached an idle state; otherwise, false.</returns>
		/// <param name="milliseconds">A value of 1 to <see cref="F:System.Int32.MaxValue" /> that specifies the amount of time, in milliseconds, to wait for the associated process to become idle. A value of 0 specifies an immediate return, and a value of -1 specifies an infinite wait. </param>
		/// <exception cref="T:System.InvalidOperationException">The process does not have a graphical interface.-or-An unknown error occurred. The process failed to enter an idle state.-or-The process has already exited. -or-No process is associated with this <see cref="T:System.Diagnostics.Process" /> object.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001457 RID: 5207 RVA: 0x0003615C File Offset: 0x0003435C
		[MonoTODO]
		public bool WaitForInputIdle(int milliseconds)
		{
			return this.WaitForInputIdle_internal(this.process_handle, milliseconds);
		}

		// Token: 0x06001458 RID: 5208 RVA: 0x0003616C File Offset: 0x0003436C
		private static bool IsLocalMachine(string machineName)
		{
			return machineName == "." || machineName.Length == 0 || string.Compare(machineName, Environment.MachineName, true) == 0;
		}

		// Token: 0x06001459 RID: 5209 RVA: 0x000361A8 File Offset: 0x000343A8
		private void OnOutputDataReceived(string str)
		{
			if (this.OutputDataReceived != null)
			{
				this.OutputDataReceived(this, new DataReceivedEventArgs(str));
			}
		}

		// Token: 0x0600145A RID: 5210 RVA: 0x000361C8 File Offset: 0x000343C8
		private void OnErrorDataReceived(string str)
		{
			if (this.ErrorDataReceived != null)
			{
				this.ErrorDataReceived(this, new DataReceivedEventArgs(str));
			}
		}

		/// <summary>Begins asynchronous read operations on the redirected <see cref="P:System.Diagnostics.Process.StandardOutput" /> stream of the application.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardOutput" /> property is false.- or - An asynchronous read operation is already in progress on the <see cref="P:System.Diagnostics.Process.StandardOutput" /> stream.- or - The <see cref="P:System.Diagnostics.Process.StandardOutput" /> stream has been used by a synchronous read operation. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600145B RID: 5211 RVA: 0x000361E8 File Offset: 0x000343E8
		[ComVisible(false)]
		public void BeginOutputReadLine()
		{
			if (this.process_handle == IntPtr.Zero || this.output_stream == null || !this.StartInfo.RedirectStandardOutput)
			{
				throw new InvalidOperationException("Standard output has not been redirected or process has not been started.");
			}
			if ((this.async_mode & Process.AsyncModes.SyncOutput) != Process.AsyncModes.NoneYet)
			{
				throw new InvalidOperationException("Cannot mix asynchronous and synchonous reads.");
			}
			this.async_mode |= Process.AsyncModes.AsyncOutput;
			this.output_canceled = false;
			if (this.async_output == null)
			{
				this.async_output = new Process.ProcessAsyncReader(this, this.stdout_rd, true);
				this.async_output.ReadHandler.BeginInvoke(null, this.async_output);
			}
		}

		/// <summary>Cancels the asynchronous read operation on the redirected <see cref="P:System.Diagnostics.Process.StandardOutput" /> stream of an application.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.Process.StandardOutput" /> stream is not enabled for asynchronous read operations. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600145C RID: 5212 RVA: 0x00036294 File Offset: 0x00034494
		[ComVisible(false)]
		public void CancelOutputRead()
		{
			if (this.process_handle == IntPtr.Zero || this.output_stream == null || !this.StartInfo.RedirectStandardOutput)
			{
				throw new InvalidOperationException("Standard output has not been redirected or process has not been started.");
			}
			if ((this.async_mode & Process.AsyncModes.SyncOutput) != Process.AsyncModes.NoneYet)
			{
				throw new InvalidOperationException("OutputStream is not enabled for asynchronous read operations.");
			}
			if (this.async_output == null)
			{
				throw new InvalidOperationException("No async operation in progress.");
			}
			this.output_canceled = true;
		}

		/// <summary>Begins asynchronous read operations on the redirected <see cref="P:System.Diagnostics.Process.StandardError" /> stream of the application.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardError" /> property is false.- or - An asynchronous read operation is already in progress on the <see cref="P:System.Diagnostics.Process.StandardError" /> stream.- or - The <see cref="P:System.Diagnostics.Process.StandardError" /> stream has been used by a synchronous read operation. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600145D RID: 5213 RVA: 0x00036314 File Offset: 0x00034514
		[ComVisible(false)]
		public void BeginErrorReadLine()
		{
			if (this.process_handle == IntPtr.Zero || this.error_stream == null || !this.StartInfo.RedirectStandardError)
			{
				throw new InvalidOperationException("Standard error has not been redirected or process has not been started.");
			}
			if ((this.async_mode & Process.AsyncModes.SyncError) != Process.AsyncModes.NoneYet)
			{
				throw new InvalidOperationException("Cannot mix asynchronous and synchonous reads.");
			}
			this.async_mode |= Process.AsyncModes.AsyncError;
			this.error_canceled = false;
			if (this.async_error == null)
			{
				this.async_error = new Process.ProcessAsyncReader(this, this.stderr_rd, false);
				this.async_error.ReadHandler.BeginInvoke(null, this.async_error);
			}
		}

		/// <summary>Cancels the asynchronous read operation on the redirected <see cref="P:System.Diagnostics.Process.StandardError" /> stream of an application.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.Process.StandardError" /> stream is not enabled for asynchronous read operations. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600145E RID: 5214 RVA: 0x000363C0 File Offset: 0x000345C0
		[ComVisible(false)]
		public void CancelErrorRead()
		{
			if (this.process_handle == IntPtr.Zero || this.output_stream == null || !this.StartInfo.RedirectStandardOutput)
			{
				throw new InvalidOperationException("Standard output has not been redirected or process has not been started.");
			}
			if ((this.async_mode & Process.AsyncModes.SyncOutput) != Process.AsyncModes.NoneYet)
			{
				throw new InvalidOperationException("OutputStream is not enabled for asynchronous read operations.");
			}
			if (this.async_error == null)
			{
				throw new InvalidOperationException("No async operation in progress.");
			}
			this.error_canceled = true;
		}

		// Token: 0x0600145F RID: 5215
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Process_free_internal(IntPtr handle);

		/// <summary>Release all resources used by this process.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06001460 RID: 5216 RVA: 0x00036440 File Offset: 0x00034640
		protected override void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				this.disposed = true;
				if (disposing)
				{
					lock (this)
					{
						if (this.async_output != null)
						{
							this.async_output.Close();
						}
						if (this.async_error != null)
						{
							this.async_error.Close();
						}
					}
				}
				lock (this)
				{
					if (this.process_handle != IntPtr.Zero)
					{
						this.Process_free_internal(this.process_handle);
						this.process_handle = IntPtr.Zero;
					}
					if (this.input_stream != null)
					{
						this.input_stream.Close();
						this.input_stream = null;
					}
					if (this.output_stream != null)
					{
						this.output_stream.Close();
						this.output_stream = null;
					}
					if (this.error_stream != null)
					{
						this.error_stream.Close();
						this.error_stream = null;
					}
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x00036578 File Offset: 0x00034778
		~Process()
		{
			this.Dispose(false);
		}

		// Token: 0x06001462 RID: 5218 RVA: 0x000365B4 File Offset: 0x000347B4
		private static void CBOnExit(object state, bool unused)
		{
			Process process = (Process)state;
			process.OnExited();
		}

		/// <summary>Raises the <see cref="E:System.Diagnostics.Process.Exited" /> event.</summary>
		// Token: 0x06001463 RID: 5219 RVA: 0x000365D0 File Offset: 0x000347D0
		protected void OnExited()
		{
			if (this.exited_event == null)
			{
				return;
			}
			if (this.synchronizingObject == null)
			{
				foreach (EventHandler eventHandler in this.exited_event.GetInvocationList())
				{
					try
					{
						eventHandler(this, EventArgs.Empty);
					}
					catch
					{
					}
				}
				return;
			}
			object[] args = new object[]
			{
				this,
				EventArgs.Empty
			};
			this.synchronizingObject.BeginInvoke(this.exited_event, args);
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06001464 RID: 5220 RVA: 0x00036678 File Offset: 0x00034878
		private static bool IsWindows
		{
			get
			{
				PlatformID platform = Environment.OSVersion.Platform;
				return platform == PlatformID.Win32S || platform == PlatformID.Win32Windows || platform == PlatformID.Win32NT || platform == PlatformID.WinCE;
			}
		}

		// Token: 0x040005D9 RID: 1497
		private IntPtr process_handle;

		// Token: 0x040005DA RID: 1498
		private int pid;

		// Token: 0x040005DB RID: 1499
		private bool enableRaisingEvents;

		// Token: 0x040005DC RID: 1500
		private bool already_waiting;

		// Token: 0x040005DD RID: 1501
		private System.ComponentModel.ISynchronizeInvoke synchronizingObject;

		// Token: 0x040005DE RID: 1502
		private EventHandler exited_event;

		// Token: 0x040005DF RID: 1503
		private IntPtr stdout_rd;

		// Token: 0x040005E0 RID: 1504
		private IntPtr stderr_rd;

		// Token: 0x040005E1 RID: 1505
		private ProcessModuleCollection module_collection;

		// Token: 0x040005E2 RID: 1506
		private string process_name;

		// Token: 0x040005E3 RID: 1507
		private StreamReader error_stream;

		// Token: 0x040005E4 RID: 1508
		private StreamWriter input_stream;

		// Token: 0x040005E5 RID: 1509
		private StreamReader output_stream;

		// Token: 0x040005E6 RID: 1510
		private ProcessStartInfo start_info;

		// Token: 0x040005E7 RID: 1511
		private Process.AsyncModes async_mode;

		// Token: 0x040005E8 RID: 1512
		private bool output_canceled;

		// Token: 0x040005E9 RID: 1513
		private bool error_canceled;

		// Token: 0x040005EA RID: 1514
		private Process.ProcessAsyncReader async_output;

		// Token: 0x040005EB RID: 1515
		private Process.ProcessAsyncReader async_error;

		// Token: 0x040005EC RID: 1516
		private bool disposed;

		// Token: 0x02000242 RID: 578
		private struct ProcInfo
		{
			// Token: 0x040005EF RID: 1519
			public IntPtr process_handle;

			// Token: 0x040005F0 RID: 1520
			public IntPtr thread_handle;

			// Token: 0x040005F1 RID: 1521
			public int pid;

			// Token: 0x040005F2 RID: 1522
			public int tid;

			// Token: 0x040005F3 RID: 1523
			public string[] envKeys;

			// Token: 0x040005F4 RID: 1524
			public string[] envValues;

			// Token: 0x040005F5 RID: 1525
			public string UserName;

			// Token: 0x040005F6 RID: 1526
			public string Domain;

			// Token: 0x040005F7 RID: 1527
			public IntPtr Password;

			// Token: 0x040005F8 RID: 1528
			public bool LoadUserProfile;
		}

		// Token: 0x02000243 RID: 579
		[Flags]
		private enum AsyncModes
		{
			// Token: 0x040005FA RID: 1530
			NoneYet = 0,
			// Token: 0x040005FB RID: 1531
			SyncOutput = 1,
			// Token: 0x040005FC RID: 1532
			SyncError = 2,
			// Token: 0x040005FD RID: 1533
			AsyncOutput = 4,
			// Token: 0x040005FE RID: 1534
			AsyncError = 8
		}

		// Token: 0x02000244 RID: 580
		[StructLayout(LayoutKind.Sequential)]
		private sealed class ProcessAsyncReader
		{
			// Token: 0x06001465 RID: 5221 RVA: 0x000366B0 File Offset: 0x000348B0
			public ProcessAsyncReader(Process process, IntPtr handle, bool err_out)
			{
				this.process = process;
				this.handle = handle;
				this.stream = new FileStream(handle, FileAccess.Read, false);
				this.ReadHandler = new Process.AsyncReadHandler(this.AddInput);
				this.err_out = err_out;
			}

			// Token: 0x06001466 RID: 5222 RVA: 0x0003671C File Offset: 0x0003491C
			public void AddInput()
			{
				lock (this)
				{
					int num = this.stream.Read(this.buffer, 0, this.buffer.Length);
					if (num == 0)
					{
						this.completed = true;
						if (this.wait_handle != null)
						{
							this.wait_handle.Set();
						}
						this.FlushLast();
					}
					else
					{
						try
						{
							this.sb.Append(Encoding.Default.GetString(this.buffer, 0, num));
						}
						catch
						{
							for (int i = 0; i < num; i++)
							{
								this.sb.Append((char)this.buffer[i]);
							}
						}
						this.Flush(false);
						this.ReadHandler.BeginInvoke(null, this);
					}
				}
			}

			// Token: 0x06001467 RID: 5223 RVA: 0x00036820 File Offset: 0x00034A20
			private void FlushLast()
			{
				this.Flush(true);
				if (this.err_out)
				{
					this.process.OnOutputDataReceived(null);
				}
				else
				{
					this.process.OnErrorDataReceived(null);
				}
			}

			// Token: 0x06001468 RID: 5224 RVA: 0x00036854 File Offset: 0x00034A54
			private void Flush(bool last)
			{
				if (this.sb.Length == 0 || (this.err_out && this.process.output_canceled) || (!this.err_out && this.process.error_canceled))
				{
					return;
				}
				string text = this.sb.ToString();
				this.sb.Length = 0;
				string[] array = text.Split(new char[]
				{
					'\n'
				});
				int num = array.Length;
				if (num == 0)
				{
					return;
				}
				for (int i = 0; i < num - 1; i++)
				{
					if (this.err_out)
					{
						this.process.OnOutputDataReceived(array[i]);
					}
					else
					{
						this.process.OnErrorDataReceived(array[i]);
					}
				}
				string text2 = array[num - 1];
				if (last || (num == 1 && text2 == string.Empty))
				{
					if (this.err_out)
					{
						this.process.OnOutputDataReceived(text2);
					}
					else
					{
						this.process.OnErrorDataReceived(text2);
					}
				}
				else
				{
					this.sb.Append(text2);
				}
			}

			// Token: 0x170004C9 RID: 1225
			// (get) Token: 0x06001469 RID: 5225 RVA: 0x00036980 File Offset: 0x00034B80
			public bool IsCompleted
			{
				get
				{
					return this.completed;
				}
			}

			// Token: 0x170004CA RID: 1226
			// (get) Token: 0x0600146A RID: 5226 RVA: 0x00036988 File Offset: 0x00034B88
			public WaitHandle WaitHandle
			{
				get
				{
					WaitHandle result;
					lock (this)
					{
						if (this.wait_handle == null)
						{
							this.wait_handle = new ManualResetEvent(this.completed);
						}
						result = this.wait_handle;
					}
					return result;
				}
			}

			// Token: 0x0600146B RID: 5227 RVA: 0x000369F0 File Offset: 0x00034BF0
			public void Close()
			{
				this.stream.Close();
			}

			// Token: 0x040005FF RID: 1535
			public object Sock;

			// Token: 0x04000600 RID: 1536
			public IntPtr handle;

			// Token: 0x04000601 RID: 1537
			public object state;

			// Token: 0x04000602 RID: 1538
			public AsyncCallback callback;

			// Token: 0x04000603 RID: 1539
			public ManualResetEvent wait_handle;

			// Token: 0x04000604 RID: 1540
			public Exception delayedException;

			// Token: 0x04000605 RID: 1541
			public object EndPoint;

			// Token: 0x04000606 RID: 1542
			private byte[] buffer = new byte[4196];

			// Token: 0x04000607 RID: 1543
			public int Offset;

			// Token: 0x04000608 RID: 1544
			public int Size;

			// Token: 0x04000609 RID: 1545
			public int SockFlags;

			// Token: 0x0400060A RID: 1546
			public object AcceptSocket;

			// Token: 0x0400060B RID: 1547
			public object[] Addresses;

			// Token: 0x0400060C RID: 1548
			public int port;

			// Token: 0x0400060D RID: 1549
			public object Buffers;

			// Token: 0x0400060E RID: 1550
			public bool ReuseSocket;

			// Token: 0x0400060F RID: 1551
			public object acc_socket;

			// Token: 0x04000610 RID: 1552
			public int total;

			// Token: 0x04000611 RID: 1553
			public bool completed_sync;

			// Token: 0x04000612 RID: 1554
			private bool completed;

			// Token: 0x04000613 RID: 1555
			private bool err_out;

			// Token: 0x04000614 RID: 1556
			internal int error;

			// Token: 0x04000615 RID: 1557
			public int operation = 8;

			// Token: 0x04000616 RID: 1558
			public object ares;

			// Token: 0x04000617 RID: 1559
			public int EndCalled;

			// Token: 0x04000618 RID: 1560
			private Process process;

			// Token: 0x04000619 RID: 1561
			private Stream stream;

			// Token: 0x0400061A RID: 1562
			private StringBuilder sb = new StringBuilder();

			// Token: 0x0400061B RID: 1563
			public Process.AsyncReadHandler ReadHandler;
		}

		// Token: 0x02000245 RID: 581
		private class ProcessWaitHandle : WaitHandle
		{
			// Token: 0x0600146C RID: 5228 RVA: 0x00036A00 File Offset: 0x00034C00
			public ProcessWaitHandle(IntPtr handle)
			{
				this.Handle = Process.ProcessWaitHandle.ProcessHandle_duplicate(handle);
			}

			// Token: 0x0600146D RID: 5229
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern IntPtr ProcessHandle_duplicate(IntPtr handle);
		}

		// Token: 0x020004DB RID: 1243
		// (Invoke) Token: 0x06002C18 RID: 11288
		private delegate void AsyncReadHandler();
	}
}
