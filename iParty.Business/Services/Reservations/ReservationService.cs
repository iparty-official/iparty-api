using iParty.Business.Infra;
using iParty.Business.Interfaces.Services;
using iParty.Business.Interfaces.Validations;
using iParty.Business.Models.Reservation;
using iParty.Business.Interfaces;
using System.Collections.Generic;
using System;

namespace iParty.Business.Services.Reservations
{
    public class ReservationService : IReservationService
    {
        private BasicService<Reservation> _basicService;        

        public ReservationService(IRepository<Reservation> repository, IReservationValidation reservationValidation)
        {            
            _basicService = new BasicService<Reservation>(repository, reservationValidation);
        }

        public ServiceResult<Reservation> Create(Reservation reservation)
        {
            return _basicService.Create(reservation);
        }

        public ServiceResult<Reservation> Delete(Guid id)
        {
            return _basicService.Delete(id);
        }

        public Reservation Get(Guid id)
        {
            return _basicService.Get(id);
        }

        public List<Reservation> Get()
        {
            return _basicService.Get();
        }
    }
}
