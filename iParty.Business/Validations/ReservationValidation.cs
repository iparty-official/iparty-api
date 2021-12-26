using FluentValidation;
using iParty.Business.Interfaces.Validations;
using iParty.Business.Models.Reservation;

namespace iParty.Business.Validations
{
    public class ReservationValidation : AbstractValidator<Reservation>, IReservationValidation
    {
        public ReservationValidation()
        {
            //TODO: Validar se a reserva pode ser feita
            RuleFor(x => x.Date).NotNull().WithMessage("A data da reserva não foi informada.");

            RuleFor(x => x.InitialHour).NotNull().WithMessage("A hora inicial da reserva não foi informada.");

            RuleFor(x => x.FinalHour).NotNull().WithMessage("A hora final da reserva não foi informada.");            

            RuleFor(x => x.InitialHour).LessThan(x => x.FinalHour).WithMessage("A hora inicial precisa ser menor que a hora final");

            RuleFor(x => x.ReservationReason).NotNull().WithMessage("O motivo da reserva não foi informado.");

            RuleFor(x => x.ReservationReason).IsInEnum().WithMessage("O motivo da reserva informado é inválido.");

            RuleFor(x => ordemWasInformed(x)).Equal(true).WithMessage("O pedido não foi informado.");
        }

        private bool ordemWasInformed(Reservation reservation)
        {
            if (reservation.ReservationReason == ReservationReason.Order && reservation.Order == null)
                return false;
            else
                return true;
        }
    }
}
