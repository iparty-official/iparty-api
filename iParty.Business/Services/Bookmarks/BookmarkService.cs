using iParty.Business.Infra;
using iParty.Business.Interfaces.Services;
using iParty.Business.Interfaces.Validations;
using iParty.Business.Models.Bookmark;
using iParty.Data.Repositories;
using System;

namespace iParty.Business.Services.Cities
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

        public ServiceResult<Bookmark> Update(Guid id, Bookmark bookmark)
        {
            var currentBookmark = Get(id);

            if (currentBookmark == null)
                return GetFailureResult("Não foi possível localizar o bookmark informado.");

            var result = _bookmarkValidation.Validate(bookmark);

            if (!result.IsValid)
                return GetFailureResult(result);

            Rep.Update(id, bookmark);

            return GetSuccessResult(bookmark);
        }
    }
}
