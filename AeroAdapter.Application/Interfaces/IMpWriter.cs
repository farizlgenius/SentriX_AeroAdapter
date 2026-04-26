using System;
using AeroAdapter.Domain.Entities;

namespace AeroAdapter.Application.Interfaces;

public interface IMpWriter
{
      Task<bool> InputPointSpecification(short ScpId,InputPointSpecification spec);
}
