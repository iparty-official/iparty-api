using iParty.Api.Dtos.Reservations;
using iParty.Api.Interfaces.Mappers;
using iParty.Api.Views.Reservations;
using iParty.Api.Views.Items;
using iParty.Business.Infra.Extensions;
using iParty.Business.Models.Reservation;
using iParty.Business.Models.Items;
using iParty.Business.Interfaces;
using System.Collections.Generic;
using AutoMapper;

namespace iParty.Api.Infra.Reservations
{
    public class ReservationMapper : BaseMapper<Reservation>, IReservationMapper
    {        
        private IRepository<Item> _itemRepository;

        private IMapper _autoMapper;

        public ReservationMapper(IRepository<Item> itemRepository, IMapper autoMapper)
        {            
            _itemRepository = itemRepository;
            _autoMapper = autoMapper;
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
                OrderItem = null
                
            });

            return GetResult();
        }

        public ReservationView Map(Reservation entity)
        {
            return MapToView(entity);
        }

        public List<ReservationView> Map(List<Reservation> entities)
        {
            var reservations = new List<ReservationView>();

            foreach (var reservation in entities)
            {
                reservations.Add(MapToView(reservation));
            }

            return reservations;
        }

        public ReservationView MapToView(Reservation entity)
        {
            if (entity == null) return null;

            var reservationView = new ReservationView()
            {
                Id = entity.Id,
                Date = entity.Date,
                InitialHour = entity.InitialHour,
                FinalHour = entity.FinalHour,
                Item = _autoMapper.Map<ItemSummarizedView>(entity.Item),
                ReservationReason = entity.ReservationReason,                
                OrderItem = null
            };

            return reservationView;
        }
    }
}
