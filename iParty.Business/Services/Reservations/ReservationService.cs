using iParty.Business.Infra;
using iParty.Business.Interfaces.Services;
using iParty.Business.Interfaces.Validations;
using iParty.Business.Models.Reservation;
using iParty.Business.Interfaces;
using System;

namespace iParty.Business.Services.Reservations
{
    public class ReservationService : Service<Reservation, IRepository<Reservation>>, IReservationService
    {
        private IReservationValidation _reservationValidation;

        public ReservationService(IRepository<Reservation> rep, IReservationValidation reservationValidation) : base(rep)
        {
            _reservationValidation = reservationValidation;
        }

        public ServiceResult<Reservation> Create(Reservation reservation)
        {
            var result = _reservationValidation.Validate(reservation);

            if (!result.IsValid)
                return GetFailureResult(result);

            Rep.Create(reservation);

            return GetSuccessResult(reservation);
        }        
    }
}
