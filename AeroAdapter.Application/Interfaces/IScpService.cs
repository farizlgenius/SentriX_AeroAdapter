using System;
using AeroAdapter.Application.DTOs;
using Application.Contracts.GeneratedDtos;

namespace AeroAdapter.Application.Interfaces;

public interface IScpService
{
      Task HandleIdReport(SCPReplyMessageDto.SCPReplyIDReportDto id);
      void VerifyAllocateScpMemory(SCPReplyMessageDto.SCPReplyStrStatusDto str);
      Task<bool> SendASCIICommandAsync(ASCIICommandDto Command);
}
