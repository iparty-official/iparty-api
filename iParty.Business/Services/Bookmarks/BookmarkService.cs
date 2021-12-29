using iParty.Business.Infra;
using iParty.Business.Interfaces;
using iParty.Business.Interfaces.Services;
using iParty.Business.Interfaces.Validations;
using iParty.Business.Models.Bookmark;

namespace iParty.Business.Services.Bookmarks
{
    public class BookmarkService : Service<Bookmark, IRepository<Bookmark>>, IBookmarkService
    {
        private IBookmarkValidation _bookmarkValidation;

        public BookmarkService(IRepository<Bookmark> rep, IBookmarkValidation bookmarkValidation) : base(rep)
        {
            _bookmarkValidation = bookmarkValidation;
        }

        public ServiceResult<Bookmark> Create(Bookmark bookmark)
        {
            var result = _bookmarkValidation.Validate(bookmark);

            if (!result.IsValid)
                return GetFailureResult(result);

            Rep.Create(bookmark);

            return GetSuccessResult(bookmark);
        }        
    }
}
