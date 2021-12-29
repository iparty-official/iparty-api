using FluentValidation;
using iParty.Business.Models.Bookmark;

namespace iParty.Business.Interfaces.Validations
{
    public interface IBookmarkValidation : IValidator<Bookmark>
    {
    }
}
