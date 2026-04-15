using System;
using AeroAdapter.Application.Interfaces;

namespace AeroAdapter.Application.Services;

public sealed class DriverService(ICommandWriter writer) : IDriverService
{
    

    public async Task<bool> SystemLevelSpecification()
    {
        return await writer.SystemLevelSpecification();
    }


}
