using System;
using System.Collections;
using System.Data.Common;
using System.Reflection;
using System.Text;
using Application.Contracts.GeneratedDtos;
using HID.Aero.ScpdNet.Wrapper;

namespace AeroAdapter.Infrastructure.Helpers;

public class ScpReplyMessageBuilder
{
    // public static string ToJsonString(object obj)
    // {   
    //     if(obj == null)
    //         return "{}";

    //     var type = obj.GetType();
    //     var props = type.GetProperties();
    //     var pairs = props.Select(p =>
    //     {
    //         var value = p.GetValue(obj);
    //         return $"\"{p.Name}\" : \"{value}\"";
    //     });

    //     return  $"[{type.Name}] " + "{" + string.Join(", ",pairs) + " }";
    // }

    // public static string ToString(object obj)
    // {
    //     if(obj == null)
    //         return string.Empty;

    //     var sb = new StringBuilder();
    //     var type = obj.GetType();
    //     PropertyInfo[] props = type.GetProperties();

    //     foreach(PropertyInfo prop in props)
    //     {
    //         var value = prop.GetValue(obj,null);
    //         sb.AppendJoin(", ",$"{prop.Name}: {value}");
    //     }

    //     return $"[{type.Name}] " + sb.ToString();


    // }

     // ================= JSON STYLE =================
    public static string ToJsonString(object? obj)
    {
        return ToJsonInternal(obj, 0, new HashSet<object>());
    }

    private static string ToJsonInternal(object? obj, int indent, HashSet<object> visited)
    {
        if (obj == null)
            return "null";

        var type = obj.GetType();

        if (IsSimple(type))
            return $"\"{obj}\"";

        // prevent infinite recursion
        if (!type.IsValueType)
        {
            if (visited.Contains(obj))
                return "\"<circular reference>\"";

            visited.Add(obj);
        }

        // arrays / lists
        if (obj is IEnumerable enumerable && type != typeof(string))
        {
            var items = new List<string>();
            foreach (var item in enumerable)
                items.Add(ToJsonInternal(item, indent + 1, visited));

            return "[ " + string.Join(", ", items) + " ]";
        }

        // complex object
        var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var sb = new StringBuilder();
        sb.Append("{ ");

        var pairs = new List<string>();
        foreach (var prop in props)
        {
            var value = prop.GetValue(obj);
            var jsonValue = ToJsonInternal(value, indent + 1, visited);
            pairs.Add($"\"{prop.Name}\" : {jsonValue}");
        }

        sb.Append(string.Join(", ", pairs));
        sb.Append(" }");

        return $"[{type.Name}] " + sb.ToString();
    }

    // ================= DEBUG STRING STYLE =================
    public static string ToString(object? obj)
    {
        return ToStringInternal(obj, 0, new HashSet<object>());
    }

    private static string ToStringInternal(object? obj, int indent, HashSet<object> visited)
    {
        if (obj == null)
            return "null";

        var type = obj.GetType();

        if (IsSimple(type))
            return obj.ToString() ?? "";

        if (!type.IsValueType)
        {
            if (visited.Contains(obj))
                return "<circular reference>";

            visited.Add(obj);
        }

        // arrays
        if (obj is IEnumerable enumerable && type != typeof(string))
        {
            var items = new List<string>();
            foreach (var item in enumerable)
                items.Add(ToStringInternal(item, indent + 1, visited));

            return "[ " + string.Join(", ", items) + " ]";
        }

        // complex object
        var sb = new StringBuilder();
        var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        sb.Append($"[{type.Name}] ");

        var pairs = new List<string>();
        foreach (var prop in props)
        {
            var value = prop.GetValue(obj);
            var text = ToStringInternal(value, indent + 1, visited);
            pairs.Add($"{prop.Name}: {text}");
        }

        sb.Append("{ " + string.Join(", ", pairs) + " }");

        return sb.ToString();
    }

    private static bool IsSimple(Type type)
    {
        return type.IsPrimitive
            || type == typeof(string)
            || type == typeof(decimal)
            || type == typeof(DateTime)
            || type == typeof(Guid);
    }
      public static string BuildNakMessage(SCPReplyMessageDto message)
      {
            switch (message.nak.reason)
            {
                case 0:
                    return $"{DateTime.UtcNow.ToLocalTime()} [Nak] reason: Invalid Packet Header, data: {message.nak.data}, command: {UtilitiesHelper.ByteToHexStr(message.nak.command)}, description_code: {message.nak.description_code}";
                case 1:
                case 2: 
                case 3:
                     return $"{DateTime.UtcNow.ToLocalTime()} [Nak] reason: Invalid command type (firmware revision mismatch), data: {message.nak.data}, command: {UtilitiesHelper.ByteToHexStr(message.nak.command)}, description_code: {message.nak.description_code}";

                case 4:
                    return $"{DateTime.UtcNow.ToLocalTime()} [Nak] reason: command content error, data: {message.nak.data}, command: {UtilitiesHelper.ByteToHexStr(message.nak.command)}, description_code: {message.nak.description_code}";

                case 5:
                    return $"{DateTime.UtcNow.ToLocalTime()} [Nak] reason: Cannot execute - requires password logon, data: {message.nak.data}, command: {UtilitiesHelper.ByteToHexStr(message.nak.command)}, description_code: {message.nak.description_code}";

                case 6:
                    return $"{DateTime.UtcNow.ToLocalTime()} [Nak] reason: This port is in standby mode and cannot execute this command, data: {message.nak.data}, command: {UtilitiesHelper.ByteToHexStr(message.nak.command)}, description_code: {message.nak.description_code}";
                    
                case 7:
                    return $"{DateTime.UtcNow.ToLocalTime()} [Nak] reason: Failed logon - password and/or encryption key, data: {message.nak.data}, command: {UtilitiesHelper.ByteToHexStr(message.nak.command)}, description_code: {message.nak.description_code}";
                    
                case 8:
                    return $"{DateTime.UtcNow.ToLocalTime()} [Nak] reason: command not accepted, controller is running in degraded mode and only a limited number of commands are accepted, data: {message.nak.data}, command: {UtilitiesHelper.ByteToHexStr(message.nak.command)}, description_code: {message.nak.description_code}";
                    
                  default:
                    return $"{DateTime.UtcNow.ToLocalTime()} [Nak] reason: Unknown reason code {message.nak.reason}, data: {message.nak.data}, command: {UtilitiesHelper.ByteToHexStr(message.nak.command)}, description_code: {message.nak.description_code}";
            }
      }

      public static string CommStatusMessage(SCPReplyMessageDto message)
      {
            return $"{DateTime.UtcNow.ToLocalTime()} [CommStatus] Status: {message.comm.status}, Status Desc: {DescriptionHelper.GetReplyStatusDesc(message.comm.status)} Error: {message.comm.error_code} Error Desc: {DescriptionHelper.GetErrorCodeDesc((int)message.comm.error_code)} Channel ID: {message.comm.nChannelId}";
      }

      public static string IdReportMessage(SCPReplyMessageDto message)
      {
            return $"{DateTime.UtcNow.ToLocalTime()} [ID Report] SCP Version: {message.id.device_ver}, Firmware Version: {message.id.sft_rev_major}.{message.id.sft_rev_minor}, Serial Number: {message.id.serial_number}, Mac Address: {UtilitiesHelper.ByteToHexStr(message.id.mac_addr)}, TLS Status : {message.id.tls_status}";
      }


      public static string TranStatusMessage(SCPReplyMessageDto message)
      {
            return $"{DateTime.UtcNow.ToLocalTime()} [Transaction Status] Capacity: {message.tran_sts.capacity}, Oldest: {message.tran_sts.oldest}, Last report: {message.tran_sts.last_loggd}, Status: {message.tran_sts.disabled}, Status Desc: {DescriptionHelper.GetStatusTranReportDesc(message.tran_sts.disabled)}";
      }

      public static string Msp1DrvrMessage(SCPReplyMessageDto message)
      {
            return $"{DateTime.UtcNow.ToLocalTime()} [SrMsp1Drvr] Number: {message.sts_drvr.number}, Port: {message.sts_drvr.port}, Mode: {(message.sts_drvr.mode == 0 ? "Disabled" : "Enabled")}, Baudrate: {message.sts_drvr.baud_rate}";
      }
}
