using iParty.Api.Dtos;
using iParty.Business.Models.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iParty.Api.Infra
{
    public class MessageMapper
    {
        public Message Map(MessageDto dto)
        {
            return new Message()
            {
                Date = dto.Date,
                From = null,
                Order = null,
                Text = dto.Text,
                Time = dto.Time,
                To = null,
            };
        }
    }
}
