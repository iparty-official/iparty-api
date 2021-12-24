using iParty.Api.Dtos.Bookmarks;
using iParty.Api.Infra;
using iParty.Api.Views.Bookmarks;
using iParty.Business.Models.Bookmark;
using System.Collections.Generic;

namespace iParty.Api.Interfaces.Mappers
{
    public interface IBookmarkMapper
    {
        MapperResult<Bookmark> Map(BookmarkDto dto);

        BookmarkView Map(Bookmark entity);

        List<BookmarkView> Map(List<Bookmark> entities);
    }
}
