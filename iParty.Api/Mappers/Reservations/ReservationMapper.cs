using AutoMapper;
using iParty.Api.Dtos.Reservations;
using iParty.Api.Interfaces.Mappers;
using iParty.Api.Views.Items;
using iParty.Api.Views.Reservations;
using iParty.Business.Infra.Extensions;
using iParty.Business.Interfaces;
using iParty.Business.Models.Items;
using iParty.Business.Models.Reservation;
using System.Collections.Generic;

namespace iParty.Api.Infra.Reservations
{
    public class ReservationMapper : BaseMapper<Reservation>, IReservationMapper
    {
        private readonly IRepository<Item> _itemRepository;

        private readonly IMapper _autoMapper;

        public ReservationMapper(IRepository<Item> itemRepository, IMapper autoMapper)
        {
            _itemRepository = itemRepository;
            _autoMapper = autoMapper;
        }

        public MapperResult<Reservation> Map(ReservationDto dto)
        {
            var item = _itemRepository.RecoverById(dto.ItemId).IfNull(() => { AddError("O item informado não existe."); });

            if (!SuccessResult())
            {
                return GetResult();
            }

            SetEntity(new Reservation(dto.Date, dto.InitialHour, dto.FinalHour, item, null, dto.ReservationReason));

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
            if (entity == null)
            {
                return null;
            }

            var reservationView = new ReservationView()
            {
                Id = entity.Id,
                Version = entity.Version,
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
