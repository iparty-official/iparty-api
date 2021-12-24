using FluentValidation;
using iParty.Business.Models.Bookmark;
using iParty.Business.Models.Messages;

namespace iParty.Business.Interfaces.Validations
{
    public interface IBookmarkValidation : IValidator<Bookmark>
    {
    }
}
