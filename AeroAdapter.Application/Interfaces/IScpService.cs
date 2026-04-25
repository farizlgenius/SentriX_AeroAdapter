using System;
using Application.Contracts.GeneratedDtos;

namespace AeroAdapter.Application.Interfaces;

public interface IScpService
{
      void HandleIdReport(SCPReplyMessageDto.SCPReplyIDReportDto id);
      void VerifyAllocateScpMemory(SCPReplyMessageDto.SCPReplyStrStatusDto str);
}
