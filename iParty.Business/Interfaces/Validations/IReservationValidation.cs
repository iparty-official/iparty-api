using FluentValidation;
using iParty.Business.Models.Reservation;

namespace iParty.Business.Interfaces.Validations
{
    public interface IReservationValidation : IValidator<Reservation>
    {
    }
}
