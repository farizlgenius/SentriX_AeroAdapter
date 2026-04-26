using System;
using AeroAdapter.Domain.Entities;

namespace AeroAdapter.Application.Interfaces;

public interface IMpRepository
{
      Task<InputPointSpecification> GetInputPointSpecificationByIdAndMacAndSioNumber(short ScpId,string Mac,short SioNumber);
}
