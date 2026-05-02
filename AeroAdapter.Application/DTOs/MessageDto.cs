using System;

namespace AeroAdapter.Application.DTOs;

public sealed record MessageDto(string Exchange,string Key,object? Data=null);
