using System;

namespace AeroAdapter.Domain.Constants;

public class MessageConstant
{
      public sealed class Device
      {
            public static string DEVICE_EXCHANGE = "device-exchange"; 
            public static string DEVICE_CREATED_KEY = "device.aero.created";
            public static string DEVICE_UPDATED_KEY = "device.aero.updated";
            public static string DEVICE_UPDATED_IP_KEY = "device.aero.updated.ip";
            public static string DEVICE_UPDATED_PORT_KEY = "device.aero.updated.port";
            public static string DEVICE_MEMORY_ALLOCATED_KEY = "deivce.aero.memory";
      }
      
}
