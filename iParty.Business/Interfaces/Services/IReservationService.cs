using iParty.Business.Infra;
using iParty.Business.Models.Reservation;
using System;

namespace iParty.Business.Interfaces.Services
{
    public interface IReservationService : IService<Reservation>
    {
        public ServiceResult<Reservation> Create(Reservation reservation);        
    }
}
