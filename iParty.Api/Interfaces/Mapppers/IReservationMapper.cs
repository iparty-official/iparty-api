using iParty.Api.Dtos.Reservations;
using iParty.Api.Infra;
using iParty.Api.Views.Reservations;
using iParty.Business.Models.Reservation;
using System.Collections.Generic;

namespace iParty.Api.Interfaces.Mappers
{
    public interface IReservationMapper
    {
        MapperResult<Reservation> Map(ReservationDto dto);

        ReservationView Map(Reservation entity);

        List<ReservationView> Map(List<Reservation> entities);
    }
}
