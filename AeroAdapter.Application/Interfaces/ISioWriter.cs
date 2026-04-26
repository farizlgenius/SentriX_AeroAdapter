using System;
using AeroAdapter.Domain.Entities;

namespace AeroAdapter.Application.Interfaces;

public interface ISioWriter
{
      Task<bool> SioPanelConfiguration(short ScpId,SioPanelConfiguration config);
}
