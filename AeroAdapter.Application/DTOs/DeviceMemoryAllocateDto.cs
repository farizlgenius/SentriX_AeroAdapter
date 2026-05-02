using System;
using AeroAdapter.Domain.Enums;

namespace AeroAdapter.Application.DTOs;

public sealed record DeviceMemoryAllocateDto(string Mac,ScpSyncStatus Status);
