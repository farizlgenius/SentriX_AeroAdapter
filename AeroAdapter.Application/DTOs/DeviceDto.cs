using System;

namespace AeroAdapter.Application.DTOs;

public sealed record DeviceDto(
      int ComponentId,
      string Mac,
      string SerialNumber,
      string Firmware,
      string Ip,
      DateTime SyncedAt
      );
