using iParty.Api.Dtos;
using iParty.Business.Models.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iParty.Api.Interfaces
{
    public interface IMessageMapper
    {
        Message Map(MessageDto dto);
    }
}
