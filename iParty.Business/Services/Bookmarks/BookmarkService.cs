using iParty.Business.Infra;
using iParty.Business.Interfaces;
using iParty.Business.Interfaces.Services;
using iParty.Business.Interfaces.Validations;
using iParty.Business.Models.Bookmark;
using System;
using System.Collections.Generic;

namespace iParty.Business.Services.Bookmarks
{
    public class BookmarkService : IBookmarkService
    {
        private BasicService<Bookmark> _basicService;        

        public BookmarkService(IRepository<Bookmark> repository, IBookmarkValidation bookmarkValidation)
        {
            _basicService = new BasicService<Bookmark>(repository, bookmarkValidation);
        }

        public ServiceResult<Bookmark> Create(Bookmark bookmark)
        {
            return _basicService.Create(bookmark);
        }
        public ServiceResult<Bookmark> Delete(Guid id)
        {
            return _basicService.Delete(id);
        }

        public Bookmark Get(Guid id)
        {
            return _basicService.Get(id);
        }

        public List<Bookmark> Get()
        {
            return _basicService.Get();
        }
    }
}
