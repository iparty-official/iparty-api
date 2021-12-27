using iParty.Api.Dtos.Reservations;
using iParty.Api.Interfaces.Mappers;
using iParty.Api.Views.Reservations;
using iParty.Api.Views.Items;
using iParty.Api.Views.People;
using iParty.Business.Infra.Extensions;
using iParty.Business.Models.Reservation;
using iParty.Business.Models.Items;
using iParty.Business.Models.People;
using iParty.Data.Repositories;
using System.Collections.Generic;
using iParty.Api.Views.Orders;

namespace iParty.Api.Infra.Reservations
{
    public class ReservationMapper : BaseMapper<Reservation>, IReservationMapper
    {
        private IRepository<Person> _personRepository;

        private IRepository<Item> _itemRepository;

        private IItemMapper _itemMapper;

        public ReservationMapper(IRepository<Person> personRepository, IRepository<Item> itemRepository, IItemMapper itemMapper)
        {
            _personRepository = personRepository;
            _itemRepository = itemRepository;
            _itemMapper = itemMapper;
        }

        public MapperResult<Reservation> Map(ReservationDto dto)
        {            
            var item = _itemRepository.RecoverById(dto.ItemId).IfNull(() => { AddError("O item informado não existe."); });

            if (!SuccessResult()) return GetResult();

            SetEntity(new Reservation()
            {
                Date = dto.Date,
                InitialHour = dto.InitialHour,
                FinalHour = dto.FinalHour,
                Item = item,
                ReservationReason = dto.ReservationReason,                
                Order = null,
                
            });

            return GetResult();
        }

        public ReservationView Map(Reservation entity)
        {
            return MapReservationToReservationView(entity);
        }

        public List<ReservationView> Map(List<Reservation> entities)
        {
            var reservations = new List<ReservationView>();

            foreach (var reservation in entities)
            {
                reservations.Add(MapReservationToReservationView(reservation));
            }

            return reservations;
        }

        public ReservationView MapReservationToReservationView(Reservation entity)
        {
            if (entity == null) return null;

            var reservationView = new ReservationView()
            {
                Id = entity.Id,
                Date = entity.Date,
                InitialHour = entity.InitialHour,
                FinalHour = entity.FinalHour,
                Item = new ItemSummarizedView(){ Id = entity.Item.Id, Name = entity.Item.Name},
                ReservationReason = entity.ReservationReason,
                Order = new OrderView()
            };

            return reservationView;
        }
    }
}
