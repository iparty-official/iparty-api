using iParty.Api.Dtos.Messages;
using iParty.Api.Infra;
using iParty.Business.Models.Messages;

namespace iParty.Api.Interfaces.Mappers
{
    public interface IMessageMapper
    {
        MapperResult<Message> Map(MessageDto dto);
    }
}
