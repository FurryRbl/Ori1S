using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using Microsoft.Win32;

namespace System.IO.Ports
{
	/// <summary>Represents a serial port resource.</summary>
	// Token: 0x0200029D RID: 669
	[System.Diagnostics.MonitoringDescription("")]
	public class SerialPort : System.ComponentModel.Component
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Ports.SerialPort" /> class.</summary>
		// Token: 0x06001703 RID: 5891 RVA: 0x0003F5D8 File Offset: 0x0003D7D8
		public SerialPort() : this(SerialPort.GetDefaultPortName(), 9600, Parity.None, 8, StopBits.One)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Ports.SerialPort" /> class using the specified <see cref="T:System.ComponentModel.IContainer" /> object.</summary>
		/// <param name="container">An interface to a container. </param>
		/// <exception cref="T:System.IO.IOException">The specified port could not be found or opened.</exception>
		// Token: 0x06001704 RID: 5892 RVA: 0x0003F5F0 File Offset: 0x0003D7F0
		public SerialPort(System.ComponentModel.IContainer container) : this()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Ports.SerialPort" /> class using the specified port name.</summary>
		/// <param name="portName">The port to use (for example, COM1). </param>
		/// <exception cref="T:System.IO.IOException">The specified port could not be found or opened.</exception>
		// Token: 0x06001705 RID: 5893 RVA: 0x0003F5F8 File Offset: 0x0003D7F8
		public SerialPort(string portName) : this(portName, 9600, Parity.None, 8, StopBits.One)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Ports.SerialPort" /> class using the specified port name and baud rate.</summary>
		/// <param name="portName">The port to use (for example, COM1). </param>
		/// <param name="baudRate">The baud rate. </param>
		/// <exception cref="T:System.IO.IOException">The specified port could not be found or opened.</exception>
		// Token: 0x06001706 RID: 5894 RVA: 0x0003F60C File Offset: 0x0003D80C
		public SerialPort(string portName, int baudRate) : this(portName, baudRate, Parity.None, 8, StopBits.One)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Ports.SerialPort" /> class using the specified port name, baud rate, and parity bit.</summary>
		/// <param name="portName">The port to use (for example, COM1). </param>
		/// <param name="baudRate">The baud rate. </param>
		/// <param name="parity">One of the <see cref="P:System.IO.Ports.SerialPort.Parity" /> values. </param>
		/// <exception cref="T:System.IO.IOException">The specified port could not be found or opened.</exception>
		// Token: 0x06001707 RID: 5895 RVA: 0x0003F61C File Offset: 0x0003D81C
		public SerialPort(string portName, int baudRate, Parity parity) : this(portName, baudRate, parity, 8, StopBits.One)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Ports.SerialPort" /> class using the specified port name, baud rate, parity bit, and data bits.</summary>
		/// <param name="portName">The port to use (for example, COM1). </param>
		/// <param name="baudRate">The baud rate. </param>
		/// <param name="parity">One of the <see cref="P:System.IO.Ports.SerialPort.Parity" /> values. </param>
		/// <param name="dataBits">The data bits value. </param>
		/// <exception cref="T:System.IO.IOException">The specified port could not be found or opened.</exception>
		// Token: 0x06001708 RID: 5896 RVA: 0x0003F62C File Offset: 0x0003D82C
		public SerialPort(string portName, int baudRate, Parity parity, int dataBits) : this(portName, baudRate, parity, dataBits, StopBits.One)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Ports.SerialPort" /> class using the specified port name, baud rate, parity bit, data bits, and stop bit.</summary>
		/// <param name="portName">The port to use (for example, COM1). </param>
		/// <param name="baudRate">The baud rate. </param>
		/// <param name="parity">One of the <see cref="P:System.IO.Ports.SerialPort.Parity" /> values. </param>
		/// <param name="dataBits">The data bits value. </param>
		/// <param name="stopBits">One of the <see cref="P:System.IO.Ports.SerialPort.StopBits" /> values. </param>
		/// <exception cref="T:System.IO.IOException">The specified port could not be found or opened.</exception>
		// Token: 0x06001709 RID: 5897 RVA: 0x0003F63C File Offset: 0x0003D83C
		public SerialPort(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
		{
			this.port_name = portName;
			this.baud_rate = baudRate;
			this.data_bits = dataBits;
			this.stop_bits = stopBits;
			this.parity = parity;
		}

		/// <summary>Represents the method that handles the error event of a <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
		// Token: 0x14000048 RID: 72
		// (add) Token: 0x0600170A RID: 5898 RVA: 0x0003F6D0 File Offset: 0x0003D8D0
		// (remove) Token: 0x0600170B RID: 5899 RVA: 0x0003F6E4 File Offset: 0x0003D8E4
		[System.Diagnostics.MonitoringDescription("")]
		public event SerialErrorReceivedEventHandler ErrorReceived
		{
			add
			{
				base.Events.AddHandler(this.error_received, value);
			}
			remove
			{
				base.Events.RemoveHandler(this.error_received, value);
			}
		}

		/// <summary>Represents the method that will handle the serial pin changed event of a <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
		// Token: 0x14000049 RID: 73
		// (add) Token: 0x0600170C RID: 5900 RVA: 0x0003F6F8 File Offset: 0x0003D8F8
		// (remove) Token: 0x0600170D RID: 5901 RVA: 0x0003F70C File Offset: 0x0003D90C
		[System.Diagnostics.MonitoringDescription("")]
		public event SerialPinChangedEventHandler PinChanged
		{
			add
			{
				base.Events.AddHandler(this.pin_changed, value);
			}
			remove
			{
				base.Events.RemoveHandler(this.pin_changed, value);
			}
		}

		/// <summary>Represents the method that will handle the data received event of a <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
		// Token: 0x1400004A RID: 74
		// (add) Token: 0x0600170E RID: 5902 RVA: 0x0003F720 File Offset: 0x0003D920
		// (remove) Token: 0x0600170F RID: 5903 RVA: 0x0003F734 File Offset: 0x0003D934
		[System.Diagnostics.MonitoringDescription("")]
		public event SerialDataReceivedEventHandler DataReceived
		{
			add
			{
				base.Events.AddHandler(this.data_received, value);
			}
			remove
			{
				base.Events.RemoveHandler(this.data_received, value);
			}
		}

		// Token: 0x06001710 RID: 5904 RVA: 0x0003F748 File Offset: 0x0003D948
		private static string GetDefaultPortName()
		{
			string[] portNames = SerialPort.GetPortNames();
			if (portNames.Length > 0)
			{
				return portNames[0];
			}
			int platform = (int)Environment.OSVersion.Platform;
			if (platform == 4 || platform == 128 || platform == 6)
			{
				return "ttyS0";
			}
			return "COM1";
		}

		/// <summary>Gets the underlying <see cref="T:System.IO.Stream" /> object for a <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The stream is closed. This can occur because the <see cref="M:System.IO.Ports.SerialPort.Open" /> method has not been called or the <see cref="M:System.IO.Ports.SerialPort.Close" /> method has been called. </exception>
		/// <exception cref="T:System.NotSupportedException">The stream is in a .NET Compact Framework application and one of the following methods was called:<see cref="M:System.IO.Stream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /><see cref="M:System.IO.Stream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /><see cref="M:System.IO.Stream.EndRead(System.IAsyncResult)" /><see cref="M:System.IO.Stream.EndWrite(System.IAsyncResult)" />The .NET Compact Framework does not support the asynchronous model with base streams.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06001711 RID: 5905 RVA: 0x0003F798 File Offset: 0x0003D998
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		[System.ComponentModel.Browsable(false)]
		public Stream BaseStream
		{
			get
			{
				this.CheckOpen();
				return (Stream)this.stream;
			}
		}

		/// <summary>Gets or sets the serial baud rate.</summary>
		/// <returns>The baud rate.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The baud rate specified is less than or equal to zero, or is greater than the maximum allowable baud rate for the device. </exception>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state. - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06001712 RID: 5906 RVA: 0x0003F7AC File Offset: 0x0003D9AC
		// (set) Token: 0x06001713 RID: 5907 RVA: 0x0003F7B4 File Offset: 0x0003D9B4
		[System.ComponentModel.Browsable(true)]
		[System.Diagnostics.MonitoringDescription("")]
		[System.ComponentModel.DefaultValue(9600)]
		public int BaudRate
		{
			get
			{
				return this.baud_rate;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (this.is_open)
				{
					this.stream.SetAttributes(value, this.parity, this.data_bits, this.stop_bits, this.handshake);
				}
				this.baud_rate = value;
			}
		}

		/// <summary>Gets or sets the break signal state.</summary>
		/// <returns>true if the port is in a break state; otherwise, false.</returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or -An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is closed. This can occur because the <see cref="M:System.IO.Ports.SerialPort.Open" /> method has not been called or the <see cref="M:System.IO.Ports.SerialPort.Close" /> method has been called.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06001714 RID: 5908 RVA: 0x0003F80C File Offset: 0x0003DA0C
		// (set) Token: 0x06001715 RID: 5909 RVA: 0x0003F814 File Offset: 0x0003DA14
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		[System.ComponentModel.Browsable(false)]
		public bool BreakState
		{
			get
			{
				return this.break_state;
			}
			set
			{
				this.CheckOpen();
				if (value == this.break_state)
				{
					return;
				}
				this.stream.SetBreakState(value);
				this.break_state = value;
			}
		}

		/// <summary>Gets the number of bytes of data in the receive buffer.</summary>
		/// <returns>The number of bytes of data in the receive buffer.</returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is closed. This can occur because the <see cref="M:System.IO.Ports.SerialPort.Open" /> method has not been called or the <see cref="M:System.IO.Ports.SerialPort.Close" /> method has been called.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06001716 RID: 5910 RVA: 0x0003F848 File Offset: 0x0003DA48
		[System.ComponentModel.Browsable(false)]
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		public int BytesToRead
		{
			get
			{
				this.CheckOpen();
				return this.stream.BytesToRead;
			}
		}

		/// <summary>Gets the number of bytes of data in the send buffer.</summary>
		/// <returns>The number of bytes of data in the send buffer.</returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is closed. This can occur because the <see cref="M:System.IO.Ports.SerialPort.Open" /> method has not been called or the <see cref="M:System.IO.Ports.SerialPort.Close" /> method has been called.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06001717 RID: 5911 RVA: 0x0003F85C File Offset: 0x0003DA5C
		[System.ComponentModel.Browsable(false)]
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		public int BytesToWrite
		{
			get
			{
				this.CheckOpen();
				return this.stream.BytesToWrite;
			}
		}

		/// <summary>Gets the state of the Carrier Detect line for the port.</summary>
		/// <returns>true if the carrier is detected; otherwise, false.</returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is closed. This can occur because the <see cref="M:System.IO.Ports.SerialPort.Open" /> method has not been called or the <see cref="M:System.IO.Ports.SerialPort.Close" /> method has been called.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06001718 RID: 5912 RVA: 0x0003F870 File Offset: 0x0003DA70
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		[System.ComponentModel.Browsable(false)]
		public bool CDHolding
		{
			get
			{
				this.CheckOpen();
				return (this.stream.GetSignals() & SerialSignal.Cd) != SerialSignal.None;
			}
		}

		/// <summary>Gets the state of the Clear-to-Send line.</summary>
		/// <returns>true if the Clear-to-Send line is detected; otherwise, false.</returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is closed. This can occur because the <see cref="M:System.IO.Ports.SerialPort.Open" /> method has not been called or the <see cref="M:System.IO.Ports.SerialPort.Close" /> method has been called.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06001719 RID: 5913 RVA: 0x0003F88C File Offset: 0x0003DA8C
		[System.ComponentModel.Browsable(false)]
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		public bool CtsHolding
		{
			get
			{
				this.CheckOpen();
				return (this.stream.GetSignals() & SerialSignal.Cts) != SerialSignal.None;
			}
		}

		/// <summary>Gets or sets the standard length of data bits per byte.</summary>
		/// <returns>The data bits length.</returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or -An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The data bits value is less than 5 or more than 8. </exception>
		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x0600171A RID: 5914 RVA: 0x0003F8A8 File Offset: 0x0003DAA8
		// (set) Token: 0x0600171B RID: 5915 RVA: 0x0003F8B0 File Offset: 0x0003DAB0
		[System.ComponentModel.DefaultValue(8)]
		[System.Diagnostics.MonitoringDescription("")]
		[System.ComponentModel.Browsable(true)]
		public int DataBits
		{
			get
			{
				return this.data_bits;
			}
			set
			{
				if (value < 5 || value > 8)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (this.is_open)
				{
					this.stream.SetAttributes(this.baud_rate, this.parity, value, this.stop_bits, this.handshake);
				}
				this.data_bits = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether null bytes are ignored when transmitted between the port and the receive buffer.</summary>
		/// <returns>true if null bytes are ignored; otherwise false. The default is false.</returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is closed. This can occur because the <see cref="M:System.IO.Ports.SerialPort.Open" /> method has not been called or the <see cref="M:System.IO.Ports.SerialPort.Close" /> method has been called.</exception>
		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x0600171C RID: 5916 RVA: 0x0003F90C File Offset: 0x0003DB0C
		// (set) Token: 0x0600171D RID: 5917 RVA: 0x0003F91C File Offset: 0x0003DB1C
		[System.Diagnostics.MonitoringDescription("")]
		[System.ComponentModel.Browsable(true)]
		[MonoTODO("Not implemented")]
		[System.ComponentModel.DefaultValue(false)]
		public bool DiscardNull
		{
			get
			{
				this.CheckOpen();
				throw new NotImplementedException();
			}
			set
			{
				this.CheckOpen();
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the state of the Data Set Ready (DSR) signal.</summary>
		/// <returns>true if a Data Set Ready signal has been sent to the port; otherwise, false.</returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is closed. This can occur because the <see cref="M:System.IO.Ports.SerialPort.Open" /> method has not been called or the <see cref="M:System.IO.Ports.SerialPort.Close" /> method has been called.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x0600171E RID: 5918 RVA: 0x0003F92C File Offset: 0x0003DB2C
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		[System.ComponentModel.Browsable(false)]
		public bool DsrHolding
		{
			get
			{
				this.CheckOpen();
				return (this.stream.GetSignals() & SerialSignal.Dsr) != SerialSignal.None;
			}
		}

		/// <summary>Gets or sets a value that enables the Data Terminal Ready (DTR) signal during serial communication.</summary>
		/// <returns>true to enable Data Terminal Ready (DTR); otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x0600171F RID: 5919 RVA: 0x0003F948 File Offset: 0x0003DB48
		// (set) Token: 0x06001720 RID: 5920 RVA: 0x0003F950 File Offset: 0x0003DB50
		[System.ComponentModel.Browsable(true)]
		[System.ComponentModel.DefaultValue(false)]
		[System.Diagnostics.MonitoringDescription("")]
		public bool DtrEnable
		{
			get
			{
				return this.dtr_enable;
			}
			set
			{
				if (value == this.dtr_enable)
				{
					return;
				}
				if (this.is_open)
				{
					this.stream.SetSignal(SerialSignal.Dtr, value);
				}
				this.dtr_enable = value;
			}
		}

		/// <summary>Gets or sets the byte encoding for pre- and post-transmission conversion of text.</summary>
		/// <returns>An <see cref="T:System.Text.Encoding" /> object. The default is <see cref="T:System.Text.ASCIIEncoding" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.IO.Ports.SerialPort.Encoding" /> property was set to null.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.IO.Ports.SerialPort.Encoding" /> property was set to an encoding that is not <see cref="T:System.Text.ASCIIEncoding" />, <see cref="T:System.Text.UTF8Encoding" />, <see cref="T:System.Text.UTF32Encoding" />, <see cref="T:System.Text.UnicodeEncoding" />, one of the Windows single byte encodings, or one of the Windows double byte encodings.</exception>
		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06001721 RID: 5921 RVA: 0x0003F98C File Offset: 0x0003DB8C
		// (set) Token: 0x06001722 RID: 5922 RVA: 0x0003F994 File Offset: 0x0003DB94
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		[System.ComponentModel.Browsable(false)]
		[System.Diagnostics.MonitoringDescription("")]
		public Encoding Encoding
		{
			get
			{
				return this.encoding;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.encoding = value;
			}
		}

		/// <summary>Gets or sets the handshaking protocol for serial port transmission of data.</summary>
		/// <returns>One of the <see cref="T:System.IO.Ports.Handshake" /> values. The default is None.</returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value passed is not a valid value in the <see cref="T:System.IO.Ports.Handshake" /> enumeration.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06001723 RID: 5923 RVA: 0x0003F9B0 File Offset: 0x0003DBB0
		// (set) Token: 0x06001724 RID: 5924 RVA: 0x0003F9B8 File Offset: 0x0003DBB8
		[System.ComponentModel.DefaultValue(Handshake.None)]
		[System.Diagnostics.MonitoringDescription("")]
		[System.ComponentModel.Browsable(true)]
		public Handshake Handshake
		{
			get
			{
				return this.handshake;
			}
			set
			{
				if (value < Handshake.None || value > Handshake.RequestToSendXOnXOff)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (this.is_open)
				{
					this.stream.SetAttributes(this.baud_rate, this.parity, this.data_bits, this.stop_bits, value);
				}
				this.handshake = value;
			}
		}

		/// <summary>Gets a value indicating the open or closed status of the <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
		/// <returns>true if the serial port is open; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.IO.Ports.SerialPort.IsOpen" /> value passed is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.IO.Ports.SerialPort.IsOpen" /> value passed is an empty string ("").</exception>
		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06001725 RID: 5925 RVA: 0x0003FA14 File Offset: 0x0003DC14
		[System.ComponentModel.Browsable(false)]
		public bool IsOpen
		{
			get
			{
				return this.is_open;
			}
		}

		/// <summary>Gets or sets the value used to interpret the end of a call to the <see cref="M:System.IO.Ports.SerialPort.ReadLine" /> and <see cref="M:System.IO.Ports.SerialPort.WriteLine(System.String)" /> methods.</summary>
		/// <returns>A value that represents the end of a line. The default is a line feed, (<see cref="P:System.Environment.NewLine" />).</returns>
		/// <exception cref="T:System.ArgumentException">The property value is empty.</exception>
		/// <exception cref="T:System.ArgumentNullException">The property value is null.</exception>
		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06001726 RID: 5926 RVA: 0x0003FA1C File Offset: 0x0003DC1C
		// (set) Token: 0x06001727 RID: 5927 RVA: 0x0003FA24 File Offset: 0x0003DC24
		[System.Diagnostics.MonitoringDescription("")]
		[System.ComponentModel.Browsable(false)]
		[System.ComponentModel.DefaultValue("\n")]
		public string NewLine
		{
			get
			{
				return this.new_line;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length == 0)
				{
					throw new ArgumentException("NewLine cannot be null or empty.", "value");
				}
				this.new_line = value;
			}
		}

		/// <summary>Gets or sets the parity-checking protocol.</summary>
		/// <returns>One of the <see cref="T:System.IO.Ports.Parity" /> values that represents the parity-checking protocol. The default is None.</returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.IO.Ports.SerialPort.Parity" /> value passed is not a valid value in the <see cref="T:System.IO.Ports.Parity" /> enumeration.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06001728 RID: 5928 RVA: 0x0003FA5C File Offset: 0x0003DC5C
		// (set) Token: 0x06001729 RID: 5929 RVA: 0x0003FA64 File Offset: 0x0003DC64
		[System.ComponentModel.DefaultValue(Parity.None)]
		[System.Diagnostics.MonitoringDescription("")]
		[System.ComponentModel.Browsable(true)]
		public Parity Parity
		{
			get
			{
				return this.parity;
			}
			set
			{
				if (value < Parity.None || value > Parity.Space)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (this.is_open)
				{
					this.stream.SetAttributes(this.baud_rate, value, this.data_bits, this.stop_bits, this.handshake);
				}
				this.parity = value;
			}
		}

		/// <summary>Gets or sets the byte that replaces invalid bytes in a data stream when a parity error occurs.</summary>
		/// <returns>A byte that replaces invalid bytes.</returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x0600172A RID: 5930 RVA: 0x0003FAC0 File Offset: 0x0003DCC0
		// (set) Token: 0x0600172B RID: 5931 RVA: 0x0003FAC8 File Offset: 0x0003DCC8
		[MonoTODO("Not implemented")]
		[System.ComponentModel.DefaultValue(63)]
		[System.Diagnostics.MonitoringDescription("")]
		[System.ComponentModel.Browsable(true)]
		public byte ParityReplace
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the port for communications, including but not limited to all available COM ports.</summary>
		/// <returns>The communications port. The default is COM1.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.IO.Ports.SerialPort.PortName" /> property was set to a value with a length of zero.-or-The <see cref="P:System.IO.Ports.SerialPort.PortName" /> property was set to a value that starts with "\\".-or-The port name was not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.IO.Ports.SerialPort.PortName" /> property was set to null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The specified port is open. </exception>
		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x0600172C RID: 5932 RVA: 0x0003FAD0 File Offset: 0x0003DCD0
		// (set) Token: 0x0600172D RID: 5933 RVA: 0x0003FAD8 File Offset: 0x0003DCD8
		[System.ComponentModel.DefaultValue("COM1")]
		[System.ComponentModel.Browsable(true)]
		[System.Diagnostics.MonitoringDescription("")]
		public string PortName
		{
			get
			{
				return this.port_name;
			}
			set
			{
				if (this.is_open)
				{
					throw new InvalidOperationException("Port name cannot be set while port is open.");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length == 0 || value.StartsWith("\\\\"))
				{
					throw new ArgumentException("value");
				}
				this.port_name = value;
			}
		}

		/// <summary>Gets or sets the size of the <see cref="T:System.IO.Ports.SerialPort" /> input buffer.</summary>
		/// <returns>The buffer size. The default value is 4096.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.IO.Ports.SerialPort.ReadBufferSize" /> value set is less than or equal to zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.IO.Ports.SerialPort.ReadBufferSize" /> property was set while the stream was open.</exception>
		/// <exception cref="T:System.IO.IOException">The <see cref="P:System.IO.Ports.SerialPort.ReadBufferSize" /> property was set to an odd integer value. </exception>
		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x0600172E RID: 5934 RVA: 0x0003FB3C File Offset: 0x0003DD3C
		// (set) Token: 0x0600172F RID: 5935 RVA: 0x0003FB44 File Offset: 0x0003DD44
		[System.Diagnostics.MonitoringDescription("")]
		[System.ComponentModel.DefaultValue(4096)]
		[System.ComponentModel.Browsable(true)]
		public int ReadBufferSize
		{
			get
			{
				return this.readBufferSize;
			}
			set
			{
				if (this.is_open)
				{
					throw new InvalidOperationException();
				}
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (value <= 4096)
				{
					return;
				}
				this.readBufferSize = value;
			}
		}

		/// <summary>Gets or sets the number of milliseconds before a time-out occurs when a read operation does not finish.</summary>
		/// <returns>The number of milliseconds before a time-out occurs when a read operation does not finish.</returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The read time-out value is less than zero and not equal to <see cref="F:System.IO.Ports.SerialPort.InfiniteTimeout" />. </exception>
		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06001730 RID: 5936 RVA: 0x0003FB88 File Offset: 0x0003DD88
		// (set) Token: 0x06001731 RID: 5937 RVA: 0x0003FB90 File Offset: 0x0003DD90
		[System.ComponentModel.Browsable(true)]
		[System.ComponentModel.DefaultValue(-1)]
		[System.Diagnostics.MonitoringDescription("")]
		public int ReadTimeout
		{
			get
			{
				return this.read_timeout;
			}
			set
			{
				if (value <= 0 && value != -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (this.is_open)
				{
					this.stream.ReadTimeout = value;
				}
				this.read_timeout = value;
			}
		}

		/// <summary>Gets or sets the number of bytes in the internal input buffer before a <see cref="E:System.IO.Ports.SerialPort.DataReceived" /> event occurs.</summary>
		/// <returns>The number of bytes in the internal input buffer before a <see cref="E:System.IO.Ports.SerialPort.DataReceived" /> event is fired. The default is 1.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.IO.Ports.SerialPort.ReceivedBytesThreshold" /> value is less than or equal to zero. </exception>
		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06001732 RID: 5938 RVA: 0x0003FBCC File Offset: 0x0003DDCC
		// (set) Token: 0x06001733 RID: 5939 RVA: 0x0003FBD4 File Offset: 0x0003DDD4
		[MonoTODO("Not implemented")]
		[System.Diagnostics.MonitoringDescription("")]
		[System.ComponentModel.DefaultValue(1)]
		[System.ComponentModel.Browsable(true)]
		public int ReceivedBytesThreshold
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a value indicating whether the Request to Send (RTS) signal is enabled during serial communication.</summary>
		/// <returns>true to enable Request to Transmit (RTS); otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.IO.Ports.SerialPort.RtsEnable" /> property was set or retrieved while the <see cref="P:System.IO.Ports.SerialPort.Handshake" /> property is set to the <see cref="F:System.IO.Ports.Handshake.RequestToSend" /> value or the <see cref="F:System.IO.Ports.Handshake.RequestToSendXOnXOff" /> value.</exception>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06001734 RID: 5940 RVA: 0x0003FBF0 File Offset: 0x0003DDF0
		// (set) Token: 0x06001735 RID: 5941 RVA: 0x0003FBF8 File Offset: 0x0003DDF8
		[System.ComponentModel.Browsable(true)]
		[System.ComponentModel.DefaultValue(false)]
		[System.Diagnostics.MonitoringDescription("")]
		public bool RtsEnable
		{
			get
			{
				return this.rts_enable;
			}
			set
			{
				if (value == this.rts_enable)
				{
					return;
				}
				if (this.is_open)
				{
					this.stream.SetSignal(SerialSignal.Rts, value);
				}
				this.rts_enable = value;
			}
		}

		/// <summary>Gets or sets the standard number of stopbits per byte.</summary>
		/// <returns>One of the <see cref="T:System.IO.Ports.StopBits" /> values.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.IO.Ports.SerialPort.StopBits" /> value is not one of the values from the <see cref="T:System.IO.Ports.StopBits" /> enumeration. </exception>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06001736 RID: 5942 RVA: 0x0003FC28 File Offset: 0x0003DE28
		// (set) Token: 0x06001737 RID: 5943 RVA: 0x0003FC30 File Offset: 0x0003DE30
		[System.ComponentModel.DefaultValue(StopBits.One)]
		[System.ComponentModel.Browsable(true)]
		[System.Diagnostics.MonitoringDescription("")]
		public StopBits StopBits
		{
			get
			{
				return this.stop_bits;
			}
			set
			{
				if (value < StopBits.One || value > StopBits.OnePointFive)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (this.is_open)
				{
					this.stream.SetAttributes(this.baud_rate, this.parity, this.data_bits, value, this.handshake);
				}
				this.stop_bits = value;
			}
		}

		/// <summary>Gets or sets the size of the serial port output buffer. </summary>
		/// <returns>The size of the output buffer. The default is 2048.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.IO.Ports.SerialPort.WriteBufferSize" /> value is less than or equal to zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.IO.Ports.SerialPort.WriteBufferSize" /> property was set while the stream was open.</exception>
		/// <exception cref="T:System.IO.IOException">The <see cref="P:System.IO.Ports.SerialPort.WriteBufferSize" /> property was set to an odd integer value. </exception>
		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06001738 RID: 5944 RVA: 0x0003FC8C File Offset: 0x0003DE8C
		// (set) Token: 0x06001739 RID: 5945 RVA: 0x0003FC94 File Offset: 0x0003DE94
		[System.Diagnostics.MonitoringDescription("")]
		[System.ComponentModel.DefaultValue(2048)]
		[System.ComponentModel.Browsable(true)]
		public int WriteBufferSize
		{
			get
			{
				return this.writeBufferSize;
			}
			set
			{
				if (this.is_open)
				{
					throw new InvalidOperationException();
				}
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (value <= 2048)
				{
					return;
				}
				this.writeBufferSize = value;
			}
		}

		/// <summary>Gets or sets the number of milliseconds before a time-out occurs when a write operation does not finish.</summary>
		/// <returns>The number of milliseconds before a time-out occurs. The default is <see cref="F:System.IO.Ports.SerialPort.InfiniteTimeout" />.</returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.IO.Ports.SerialPort.WriteTimeout" /> value is less than zero and not equal to <see cref="F:System.IO.Ports.SerialPort.InfiniteTimeout" />. </exception>
		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x0600173A RID: 5946 RVA: 0x0003FCD8 File Offset: 0x0003DED8
		// (set) Token: 0x0600173B RID: 5947 RVA: 0x0003FCE0 File Offset: 0x0003DEE0
		[System.ComponentModel.Browsable(true)]
		[System.ComponentModel.DefaultValue(-1)]
		[System.Diagnostics.MonitoringDescription("")]
		public int WriteTimeout
		{
			get
			{
				return this.write_timeout;
			}
			set
			{
				if (value <= 0 && value != -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (this.is_open)
				{
					this.stream.WriteTimeout = value;
				}
				this.write_timeout = value;
			}
		}

		/// <summary>Closes the port connection, sets the <see cref="P:System.IO.Ports.SerialPort.IsOpen" /> property to false, and disposes of the internal <see cref="T:System.IO.Stream" /> object.</summary>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.- or -An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600173C RID: 5948 RVA: 0x0003FD1C File Offset: 0x0003DF1C
		public void Close()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.Ports.SerialPort" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or -An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		// Token: 0x0600173D RID: 5949 RVA: 0x0003FD28 File Offset: 0x0003DF28
		protected override void Dispose(bool disposing)
		{
			if (!this.is_open)
			{
				return;
			}
			this.is_open = false;
			if (disposing)
			{
				this.stream.Close();
			}
			this.stream = null;
		}

		/// <summary>Discards data from the serial driver's receive buffer.</summary>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or -An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is closed. This can occur because the <see cref="M:System.IO.Ports.SerialPort.Open" /> method has not been called or the <see cref="M:System.IO.Ports.SerialPort.Close" /> method has been called.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600173E RID: 5950 RVA: 0x0003FD58 File Offset: 0x0003DF58
		public void DiscardInBuffer()
		{
			this.CheckOpen();
			this.stream.DiscardInBuffer();
		}

		/// <summary>Discards data from the serial driver's transmit buffer.</summary>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is closed. This can occur because the <see cref="M:System.IO.Ports.SerialPort.Open" /> method has not been called or the <see cref="M:System.IO.Ports.SerialPort.Close" /> method has been called.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600173F RID: 5951 RVA: 0x0003FD6C File Offset: 0x0003DF6C
		public void DiscardOutBuffer()
		{
			this.CheckOpen();
			this.stream.DiscardOutBuffer();
		}

		/// <summary>Gets an array of serial port names for the current computer.</summary>
		/// <returns>An array of serial port names for the current computer.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The serial port names could not be queried.</exception>
		// Token: 0x06001740 RID: 5952 RVA: 0x0003FD80 File Offset: 0x0003DF80
		public static string[] GetPortNames()
		{
			int platform = (int)Environment.OSVersion.Platform;
			List<string> list = new List<string>();
			if (platform == 4 || platform == 128 || platform == 6)
			{
				string[] files = Directory.GetFiles("/dev/", "tty*");
				foreach (string text in files)
				{
					if (text.StartsWith("/dev/ttyS") || text.StartsWith("/dev/ttyUSB"))
					{
						list.Add(text);
					}
				}
			}
			else
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("HARDWARE\\DEVICEMAP\\SERIALCOMM"))
				{
					if (registryKey != null)
					{
						string[] valueNames = registryKey.GetValueNames();
						foreach (string name in valueNames)
						{
							string text2 = registryKey.GetValue(name, string.Empty).ToString();
							if (text2 != string.Empty)
							{
								list.Add(text2);
							}
						}
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06001741 RID: 5953 RVA: 0x0003FEC0 File Offset: 0x0003E0C0
		private static bool IsWindows
		{
			get
			{
				PlatformID platform = Environment.OSVersion.Platform;
				return platform == PlatformID.Win32Windows || platform == PlatformID.Win32NT;
			}
		}

		/// <summary>Opens a new serial port connection.</summary>
		/// <exception cref="T:System.InvalidOperationException">The specified port is open. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">One or more of the properties for this instance are invalid. For example, the <see cref="P:System.IO.Ports.SerialPort.Parity" />, <see cref="P:System.IO.Ports.SerialPort.DataBits" />, or <see cref="P:System.IO.Ports.SerialPort.Handshake" /> properties are not valid values; the <see cref="P:System.IO.Ports.SerialPort.BaudRate" /> is less than or equal to zero; the <see cref="P:System.IO.Ports.SerialPort.ReadTimeout" /> or <see cref="P:System.IO.Ports.SerialPort.WriteTimeout" /> property is less than zero and is not <see cref="F:System.IO.Ports.SerialPort.InfiniteTimeout" />. </exception>
		/// <exception cref="T:System.ArgumentException">The port name does not begin with "COM". - or -The file type of the port is not supported.</exception>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Access is denied to the port. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001742 RID: 5954 RVA: 0x0003FEE8 File Offset: 0x0003E0E8
		public void Open()
		{
			if (this.is_open)
			{
				throw new InvalidOperationException("Port is already open");
			}
			if (SerialPort.IsWindows)
			{
				this.stream = new WinSerialStream(this.port_name, this.baud_rate, this.data_bits, this.parity, this.stop_bits, this.dtr_enable, this.rts_enable, this.handshake, this.read_timeout, this.write_timeout, this.readBufferSize, this.writeBufferSize);
			}
			else
			{
				this.stream = new SerialPortStream(this.port_name, this.baud_rate, this.data_bits, this.parity, this.stop_bits, this.dtr_enable, this.rts_enable, this.handshake, this.read_timeout, this.write_timeout, this.readBufferSize, this.writeBufferSize);
			}
			this.is_open = true;
		}

		/// <summary>Reads a number of bytes from the <see cref="T:System.IO.Ports.SerialPort" /> input buffer and writes those bytes into a byte array at the specified offset.</summary>
		/// <returns>The number of bytes read.</returns>
		/// <param name="buffer">The byte array to write the input to. </param>
		/// <param name="offset">The offset in the buffer array to begin writing. </param>
		/// <param name="count">The number of bytes to read. </param>
		/// <exception cref="T:System.ArgumentNullException">The buffer passed is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The specified port is not open. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> or <paramref name="count" /> parameters are outside a valid region of the <paramref name="buffer" /> being passed. Either <paramref name="offset" /> or <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> plus <paramref name="count" /> is greater than the length of the <paramref name="buffer" />. </exception>
		/// <exception cref="T:System.TimeoutException">No bytes were available to read.</exception>
		// Token: 0x06001743 RID: 5955 RVA: 0x0003FFC8 File Offset: 0x0003E1C8
		public int Read(byte[] buffer, int offset, int count)
		{
			this.CheckOpen();
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException("offset or count less than zero.");
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("offset+count", "The size of the buffer is less than offset + count.");
			}
			return this.stream.Read(buffer, offset, count);
		}

		/// <summary>Reads a number of characters from the <see cref="T:System.IO.Ports.SerialPort" /> input buffer and writes them into an array of characters at a given offset.</summary>
		/// <returns>The number of characters read.</returns>
		/// <param name="buffer">The character array to write the input to. </param>
		/// <param name="offset">The offset in the buffer array to begin writing. </param>
		/// <param name="count">The number of characters to read. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> plus <paramref name="count" /> is greater than the length of the buffer.- or -<paramref name="count" /> is 1 and there is a surrogate character in the buffer.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> passed is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> or <paramref name="count" /> parameters are outside a valid region of the <paramref name="buffer" /> being passed. Either <paramref name="offset" /> or <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.InvalidOperationException">The specified port is not open. </exception>
		/// <exception cref="T:System.TimeoutException">No characters were available to read.</exception>
		// Token: 0x06001744 RID: 5956 RVA: 0x00040030 File Offset: 0x0003E230
		[MonoTODO("Read of char buffers is currently broken")]
		public int Read(char[] buffer, int offset, int count)
		{
			this.CheckOpen();
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException("offset or count less than zero.");
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("offset+count", "The size of the buffer is less than offset + count.");
			}
			byte[] bytes = this.encoding.GetBytes(buffer, offset, count);
			return this.stream.Read(bytes, 0, bytes.Length);
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x000400A8 File Offset: 0x0003E2A8
		internal int read_byte()
		{
			byte[] array = new byte[1];
			if (this.stream.Read(array, 0, 1) > 0)
			{
				return (int)array[0];
			}
			return -1;
		}

		/// <summary>Synchronously reads one byte from the <see cref="T:System.IO.Ports.SerialPort" /> input buffer.</summary>
		/// <returns>The byte, cast to an <see cref="T:System.Int32" />, or -1 if the end of the stream has been read.</returns>
		/// <exception cref="T:System.InvalidOperationException">The specified port is not open. </exception>
		/// <exception cref="T:System.ServiceProcess.TimeoutException">The operation did not complete before the time-out period ended.- or -No byte was read.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001746 RID: 5958 RVA: 0x000400D8 File Offset: 0x0003E2D8
		public int ReadByte()
		{
			this.CheckOpen();
			return this.read_byte();
		}

		/// <summary>Synchronously reads one character from the <see cref="T:System.IO.Ports.SerialPort" /> input buffer.</summary>
		/// <returns>The character that was read.</returns>
		/// <exception cref="T:System.InvalidOperationException">The specified port is not open. </exception>
		/// <exception cref="T:System.ServiceProcess.TimeoutException">The operation did not complete before the time-out period ended.- or -No character was available in the allotted time-out period.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001747 RID: 5959 RVA: 0x000400E8 File Offset: 0x0003E2E8
		public int ReadChar()
		{
			this.CheckOpen();
			byte[] array = new byte[16];
			int num = 0;
			char[] chars;
			for (;;)
			{
				int num2 = this.read_byte();
				if (num2 == -1)
				{
					break;
				}
				array[num++] = (byte)num2;
				chars = this.encoding.GetChars(array, 0, 1);
				if (chars.Length > 0)
				{
					goto Block_2;
				}
				if (num >= array.Length)
				{
					return -1;
				}
			}
			return -1;
			Block_2:
			return (int)chars[0];
		}

		/// <summary>Reads all immediately available bytes, based on the encoding, in both the stream and the input buffer of the <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
		/// <returns>The contents of the stream and the input buffer of the <see cref="T:System.IO.Ports.SerialPort" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The specified port is not open. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001748 RID: 5960 RVA: 0x00040144 File Offset: 0x0003E344
		public string ReadExisting()
		{
			this.CheckOpen();
			int bytesToRead = this.BytesToRead;
			byte[] array = new byte[bytesToRead];
			int count = this.stream.Read(array, 0, bytesToRead);
			return new string(this.encoding.GetChars(array, 0, count));
		}

		/// <summary>Reads up to the <see cref="P:System.IO.Ports.SerialPort.NewLine" /> value in the input buffer.</summary>
		/// <returns>The contents of the input buffer up to the first occurrence of a <see cref="P:System.IO.Ports.SerialPort.NewLine" /> value.</returns>
		/// <exception cref="T:System.InvalidOperationException">The specified port is not open. </exception>
		/// <exception cref="T:System.TimeoutException">The operation did not complete before the time-out period ended.- or -No bytes were read.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001749 RID: 5961 RVA: 0x00040188 File Offset: 0x0003E388
		public string ReadLine()
		{
			return this.ReadTo(this.new_line);
		}

		/// <summary>Reads a string up to the specified <paramref name="value" /> in the input buffer.</summary>
		/// <returns>The contents of the input buffer up to the specified <paramref name="value" />.</returns>
		/// <param name="value">A value that indicates where the read operation stops. </param>
		/// <exception cref="T:System.ArgumentException">The length of the <paramref name="value" /> parameter is 0.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The specified port is not open. </exception>
		/// <exception cref="T:System.TimeoutException">The operation did not complete before the time-out period ended. </exception>
		// Token: 0x0600174A RID: 5962 RVA: 0x00040198 File Offset: 0x0003E398
		public string ReadTo(string value)
		{
			this.CheckOpen();
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.Length == 0)
			{
				throw new ArgumentException("value");
			}
			byte[] bytes = this.encoding.GetBytes(value);
			int num = 0;
			List<byte> list = new List<byte>();
			for (;;)
			{
				int num2 = this.read_byte();
				if (num2 == -1)
				{
					break;
				}
				list.Add((byte)num2);
				if (num2 == (int)bytes[num])
				{
					num++;
					if (num == bytes.Length)
					{
						goto Block_5;
					}
				}
				else
				{
					num = (((int)bytes[0] != num2) ? 0 : 1);
				}
			}
			return this.encoding.GetString(list.ToArray());
			Block_5:
			return this.encoding.GetString(list.ToArray(), 0, list.Count - bytes.Length);
		}

		/// <summary>Writes the specified string to the serial port.</summary>
		/// <param name="text">The string for output. </param>
		/// <exception cref="T:System.InvalidOperationException">The specified port is not open. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="str" /> is null.</exception>
		/// <exception cref="T:System.ServiceProcess.TimeoutException">The operation did not complete before the time-out period ended. </exception>
		// Token: 0x0600174B RID: 5963 RVA: 0x00040264 File Offset: 0x0003E464
		public void Write(string str)
		{
			this.CheckOpen();
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			byte[] bytes = this.encoding.GetBytes(str);
			this.Write(bytes, 0, bytes.Length);
		}

		/// <summary>Writes a specified number of bytes to the serial port using data from a buffer.</summary>
		/// <param name="buffer">The byte array that contains the data to write to the port. </param>
		/// <param name="offset">The zero-based byte offset in the <paramref name="buffer" /> parameter at which to begin copying bytes to the port. </param>
		/// <param name="count">The number of bytes to write. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> passed is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The specified port is not open. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> or <paramref name="count" /> parameters are outside a valid region of the <paramref name="buffer" /> being passed. Either <paramref name="offset" /> or <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> plus <paramref name="count" /> is greater than the length of the <paramref name="buffer" />. </exception>
		/// <exception cref="T:System.ServiceProcess.TimeoutException">The operation did not complete before the time-out period ended. </exception>
		// Token: 0x0600174C RID: 5964 RVA: 0x000402A0 File Offset: 0x0003E4A0
		public void Write(byte[] buffer, int offset, int count)
		{
			this.CheckOpen();
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("offset+count", "The size of the buffer is less than offset + count.");
			}
			this.stream.Write(buffer, offset, count);
		}

		/// <summary>Writes a specified number of characters to the serial port using data from a buffer.</summary>
		/// <param name="buffer">The character array that contains the data to write to the port. </param>
		/// <param name="offset">The zero-based byte offset in the <paramref name="buffer" /> parameter at which to begin copying bytes to the port. </param>
		/// <param name="count">The number of characters to write. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> passed is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The specified port is not open. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> or <paramref name="count" /> parameters are outside a valid region of the <paramref name="buffer" /> being passed. Either <paramref name="offset" /> or <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> plus <paramref name="count" /> is greater than the length of the <paramref name="buffer" />. </exception>
		/// <exception cref="T:System.ServiceProcess.TimeoutException">The operation did not complete before the time-out period ended. </exception>
		// Token: 0x0600174D RID: 5965 RVA: 0x00040304 File Offset: 0x0003E504
		public void Write(char[] buffer, int offset, int count)
		{
			this.CheckOpen();
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("offset+count", "The size of the buffer is less than offset + count.");
			}
			byte[] bytes = this.encoding.GetBytes(buffer, offset, count);
			this.stream.Write(bytes, 0, bytes.Length);
		}

		/// <summary>Writes the specified string and the <see cref="P:System.IO.Ports.SerialPort.NewLine" /> value to the output buffer.</summary>
		/// <param name="text">The string to write to the output buffer. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="str" /> parameter is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The specified port is not open. </exception>
		/// <exception cref="T:System.TimeoutException">The <see cref="M:System.IO.Ports.SerialPort.WriteLine(System.String)" /> method could not write to the stream.  </exception>
		// Token: 0x0600174E RID: 5966 RVA: 0x00040378 File Offset: 0x0003E578
		public void WriteLine(string str)
		{
			this.Write(str + this.new_line);
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x0004038C File Offset: 0x0003E58C
		private void CheckOpen()
		{
			if (!this.is_open)
			{
				throw new InvalidOperationException("Specified port is not open.");
			}
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x000403A4 File Offset: 0x0003E5A4
		internal void OnErrorReceived(SerialErrorReceivedEventArgs args)
		{
			SerialErrorReceivedEventHandler serialErrorReceivedEventHandler = (SerialErrorReceivedEventHandler)base.Events[this.error_received];
			if (serialErrorReceivedEventHandler != null)
			{
				serialErrorReceivedEventHandler(this, args);
			}
		}

		// Token: 0x06001751 RID: 5969 RVA: 0x000403D8 File Offset: 0x0003E5D8
		internal void OnDataReceived(SerialDataReceivedEventArgs args)
		{
			SerialDataReceivedEventHandler serialDataReceivedEventHandler = (SerialDataReceivedEventHandler)base.Events[this.data_received];
			if (serialDataReceivedEventHandler != null)
			{
				serialDataReceivedEventHandler(this, args);
			}
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x0004040C File Offset: 0x0003E60C
		internal void OnDataReceived(SerialPinChangedEventArgs args)
		{
			SerialPinChangedEventHandler serialPinChangedEventHandler = (SerialPinChangedEventHandler)base.Events[this.pin_changed];
			if (serialPinChangedEventHandler != null)
			{
				serialPinChangedEventHandler(this, args);
			}
		}

		/// <summary>Indicates that no time-out should occur.</summary>
		// Token: 0x04000EA4 RID: 3748
		public const int InfiniteTimeout = -1;

		// Token: 0x04000EA5 RID: 3749
		private const int DefaultReadBufferSize = 4096;

		// Token: 0x04000EA6 RID: 3750
		private const int DefaultWriteBufferSize = 2048;

		// Token: 0x04000EA7 RID: 3751
		private const int DefaultBaudRate = 9600;

		// Token: 0x04000EA8 RID: 3752
		private const int DefaultDataBits = 8;

		// Token: 0x04000EA9 RID: 3753
		private const Parity DefaultParity = Parity.None;

		// Token: 0x04000EAA RID: 3754
		private const StopBits DefaultStopBits = StopBits.One;

		// Token: 0x04000EAB RID: 3755
		private bool is_open;

		// Token: 0x04000EAC RID: 3756
		private int baud_rate;

		// Token: 0x04000EAD RID: 3757
		private Parity parity;

		// Token: 0x04000EAE RID: 3758
		private StopBits stop_bits;

		// Token: 0x04000EAF RID: 3759
		private Handshake handshake;

		// Token: 0x04000EB0 RID: 3760
		private int data_bits;

		// Token: 0x04000EB1 RID: 3761
		private bool break_state;

		// Token: 0x04000EB2 RID: 3762
		private bool dtr_enable;

		// Token: 0x04000EB3 RID: 3763
		private bool rts_enable;

		// Token: 0x04000EB4 RID: 3764
		private ISerialStream stream;

		// Token: 0x04000EB5 RID: 3765
		private Encoding encoding = Encoding.ASCII;

		// Token: 0x04000EB6 RID: 3766
		private string new_line = Environment.NewLine;

		// Token: 0x04000EB7 RID: 3767
		private string port_name;

		// Token: 0x04000EB8 RID: 3768
		private int read_timeout = -1;

		// Token: 0x04000EB9 RID: 3769
		private int write_timeout = -1;

		// Token: 0x04000EBA RID: 3770
		private int readBufferSize = 4096;

		// Token: 0x04000EBB RID: 3771
		private int writeBufferSize = 2048;

		// Token: 0x04000EBC RID: 3772
		private object error_received = new object();

		// Token: 0x04000EBD RID: 3773
		private object data_received = new object();

		// Token: 0x04000EBE RID: 3774
		private object pin_changed = new object();
	}
}
