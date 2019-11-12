﻿using Plugin.BluetoothLE;
using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using WindesHeartSDK.Devices.MiBand3Device.Models;
using WindesHeartSDK.Devices.MiBand3Device.Resources;
using WindesHeartSDK.Models;

namespace WindesHeartSDK.Devices.MiBand3Device.Services
{
    public class MiBand3StepsService
    {
        private readonly MiBand3 MiBand3;
        private IDisposable RealtimeDisposable;

        public MiBand3StepsService(MiBand3 device)
        {
            MiBand3 = device;
        }

        /// <summary>
        /// Add a callback to run everytime the Mi Band updates its step count
        /// </summary>
        /// <param name="callback"></param>
        public void EnableRealTimeSteps(Action<StepInfo> callback)
        {
            RealtimeDisposable?.Dispose();
            RealtimeDisposable = MiBand3.GetCharacteristic(MiBand3Resource.GuidCharacteristic7RealtimeSteps).RegisterAndNotify().Subscribe(
                x => callback(new StepInfo(x.Characteristic.Value)));
        }

        /// <summary>
        /// Disables real time step count updates
        /// </summary>
        public void DisableRealTimeSteps()
        {
            RealtimeDisposable?.Dispose();
        }

        public async Task<StepInfo> GetSteps()
        {
            var steps = await MiBand3.GetCharacteristic(MiBand3Resource.GuidCharacteristic7RealtimeSteps).Read();
            return new StepInfo(steps.Characteristic.Value);
        }
    }
}
