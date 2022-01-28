using AutoMapper;
using iParty.Api.Dtos.Bookmarks;
using iParty.Api.Interfaces.Mappers;
using iParty.Api.Views.Bookmarks;
using iParty.Api.Views.Items;
using iParty.Api.Views.People;
using iParty.Business.Infra.Extensions;
using iParty.Business.Models.Bookmark;
using iParty.Business.Models.Items;
using iParty.Business.Models.People;
using iParty.Business.Interfaces;
using System;
using System.Collections.Generic;

namespace iParty.Api.Infra.Bookmarks
{
    public class BookmarkMapper : BaseMapper<Bookmark>, IBookmarkMapper
    {
        private IRepository<Person> _personRepository;

        private IRepository<Item> _itemRepository;

        private IMapper _autoMapper;

        public BookmarkMapper(IRepository<Person> personRepository, IRepository<Item> itemRepository, IMapper autoMapper)
        {
            _personRepository = personRepository;
            _itemRepository = itemRepository;
            _autoMapper = autoMapper;
        }

        public MapperResult<Bookmark> Map(BookmarkDto dto)
        {
            var customer = _personRepository.RecoverById(dto.CustomerId).IfNull(() => { AddError("O cliente informado não existe."); });

            var item = _itemRepository.RecoverById(dto.ItemId).IfNull(() => { AddError("O item informado não existe."); });

            if (!SuccessResult()) return GetResult();

            SetEntity(new Bookmark()
            {
                Customer = customer,
                Item = item,
                DateTime = DateTime.Now
            });

            return GetResult();
        }

        public BookmarkView Map(Bookmark entity)
        {            
            return MapToView(entity);
        }

        public List<BookmarkView> Map(List<Bookmark> entities)
        {
            var bookmarks = new List<BookmarkView>();

            foreach (var bookmark in entities)
            {
                bookmarks.Add(MapToView(bookmark));
            }

            return bookmarks;
        }

        public BookmarkView MapToView(Bookmark entity)
        {
            if (entity == null) return null;

            var bookmarkView = new BookmarkView()
            {
                Id = entity.Id,
                Version = entity.Version,
                Customer = _autoMapper.Map<PersonSummarizedView>(entity.Customer),
                Item = _autoMapper.Map<ItemSummarizedView>(entity.Item),
                DateTime = entity.DateTime
            };

            return bookmarkView;
        }
    }
}
