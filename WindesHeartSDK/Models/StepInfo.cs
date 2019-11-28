﻿using System;
using WindesHeartSDK.Helpers;

namespace WindesHeartSDK.Models
{
    public class StepInfo
    {
        public byte[] RawData { get; set; }
        public int StepCount { get; set; }
        public DateTime DateTime { get; set; }

        public StepInfo()
        {

        }

        public StepInfo(DateTime dateTime, byte[] rawData)
        {
            DateTime = dateTime;
            RawData = rawData;
            byte[] stepsValue = new byte[] { RawData[1], RawData[2] };
            StepCount = ConversionHelper.ToUint16(stepsValue);
        }
    }
}
