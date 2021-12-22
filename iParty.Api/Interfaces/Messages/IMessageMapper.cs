using iParty.Api.Dtos.Messages;
using iParty.Business.Models.Messages;

namespace iParty.Api.Interfaces.Messages
{
    public interface IMessageMapper
    {
        Message Map(MessageDto dto);
    }
}
