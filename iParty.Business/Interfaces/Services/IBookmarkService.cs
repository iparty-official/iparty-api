using iParty.Business.Infra;
using iParty.Business.Models.Bookmark;
using System;

namespace iParty.Business.Interfaces.Services
{
    public interface IBookmarkService : IService<Bookmark>
    {
        public ServiceResult<Bookmark> Create(Bookmark bookmark);     
    }
}
