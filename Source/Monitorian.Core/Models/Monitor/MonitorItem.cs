﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using Monitorian.Core.Helper;

namespace Monitorian.Core.Models.Monitor
{
	internal abstract class MonitorItem : IMonitor, IDisposable
	{
		public string DeviceInstanceId { get; }
		public string Description { get; }
		public byte DisplayIndex { get; }
		public byte MonitorIndex { get; }
		public Rect MonitorRect { get; }
		public bool IsReachable { get; }

		public int Brightness { get; protected set; } = -1;
		public int BrightnessSystemAdjusted { get; protected set; } = -1;

		public MonitorItem(
			string deviceInstanceId,
			string description,
			byte displayIndex,
			byte monitorIndex,
			Rect monitorRect,
			bool isReachable)
		{
			if (string.IsNullOrWhiteSpace(deviceInstanceId))
				throw new ArgumentNullException(nameof(deviceInstanceId));
			if (string.IsNullOrWhiteSpace(description))
				throw new ArgumentNullException(nameof(description));

			this.DeviceInstanceId = deviceInstanceId;
			this.Description = description;
			this.DisplayIndex = displayIndex;
			this.MonitorIndex = monitorIndex;
			this.MonitorRect = monitorRect;
			this.IsReachable = isReachable;
		}

		public abstract AccessResult UpdateBrightness(int brightness = -1);
		public abstract AccessResult SetBrightness(int brightness);

		public override string ToString()
		{
			return SimpleSerialization.Serialize(
				(nameof(Type), this.GetType().Name),
				(nameof(DeviceInstanceId), DeviceInstanceId),
				(nameof(Description), Description),
				(nameof(DisplayIndex), DisplayIndex),
				(nameof(MonitorIndex), MonitorIndex),
				(nameof(MonitorRect), MonitorRect),
				(nameof(IsReachable), IsReachable),
				(nameof(Brightness), Brightness),
				(nameof(BrightnessSystemAdjusted), BrightnessSystemAdjusted));
		}

		#region IDisposable

		private bool _isDisposed = false;

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_isDisposed)
				return;

			if (disposing)
			{
				// Free any other managed objects here.
			}

			// Free any unmanaged objects here.
			_isDisposed = true;
		}

		#endregion
	}
}