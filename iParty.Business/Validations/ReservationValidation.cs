using FluentValidation;
using iParty.Business.Infra.Extensions;
using iParty.Business.Interfaces.Filters;
using iParty.Business.Interfaces.Validations;
using iParty.Business.Models.Items;
using iParty.Business.Models.Reservation;
using iParty.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iParty.Business.Validations
{
    public class ReservationValidation : AbstractValidator<Reservation>, IReservationValidation
    {
        public ReservationValidation(IRepository<Reservation> reservationRepository, IFilterBuilder<Reservation> reservationFilterBuilder)
        {
            //TODO: Validar se a reserva pode ser feita
            RuleFor(x => x.Date).NotNull().WithMessage("A data da reserva não foi informada.");

            RuleFor(x => x.InitialHour).NotNull().WithMessage("A hora inicial da reserva não foi informada.");

            RuleFor(x => x.FinalHour).NotNull().WithMessage("A hora final da reserva não foi informada.");            

            RuleFor(x => x.InitialHour).LessThan(x => x.FinalHour).WithMessage("A hora inicial precisa ser menor que a hora final");

            RuleFor(x => x.ReservationReason).NotNull().WithMessage("O motivo da reserva não foi informado.");

            RuleFor(x => x.ReservationReason).IsInEnum().WithMessage("O motivo da reserva informado é inválido.");

            RuleFor(x => ordemWasInformed(x)).Equal(true).WithMessage("O pedido não foi informado.");

            RuleFor(x => itemHasAnOpenSchedule(x)).Equal(true).WithMessage("O item não possui agenda aberta para a data solicitada.");

            RuleFor(x => itemHasAvailableHours(reservationRepository, reservationFilterBuilder, x)).Equal(true).WithMessage("O item não possui disponibilidade nos horários solicitados.");
        }

        private bool ordemWasInformed(Reservation reservation)
        {
            if (reservation.ReservationReason == ReservationReason.Order && reservation.Order == null)
                return false;
            else
                return true;
        }
        private bool itemHasAnOpenSchedule(Reservation reservation)
        {
            return reservation.Item.Schedules.Where(x => x.DayOfWeek == reservation.Date.DayOfWeek).Count() > 0;
        }

        private bool itemHasAvailableHours(IRepository<Reservation> reservationRepository, IFilterBuilder<Reservation> reservationFilterBuilder, Reservation reservation)
        {
            var schedule = reservation.Item.Schedules.Where(x => x.DayOfWeek == reservation.Date.DayOfWeek).First();

            var scheduleHoursList = transfomrScheduleItemsIntoHoursList(schedule.Items);

            var reservations = getReservedHours(reservationRepository, reservationFilterBuilder, reservation);

            var availableHours = buildAvailableHoursList(scheduleHoursList, reservations);

            var wishedHoursList = buildWishedHoursList(reservation);

            foreach (var wishedHour in wishedHoursList)
            {
                if (!availableHours.Exists(x => x == wishedHour))
                {
                    return false;
                }
            }

            return true;
        }

        private List<int> buildWishedHoursList(Reservation reservation)
        {
            var result = new List<int>();

            for (int hour = reservation.InitialHour; hour < reservation.FinalHour; hour++)
            {
                result.Add(hour);
            }

            return result;
        }

        private List<int> buildAvailableHoursList(List<int> scheduleHoursList, List<Reservation> reservations)
        {            
            foreach (var reservation in reservations)
            {
                for (int hour = reservation.InitialHour; hour < reservation.FinalHour; hour++)
                {
                    scheduleHoursList.RemoveAll(x => x == hour);
                }
            }

            return scheduleHoursList;
        }

        private List<int> transfomrScheduleItemsIntoHoursList(List<ScheduleItem> scheduleItems)
        {
            var result = new List<int>();

            foreach (var scheduleItem in scheduleItems)
            {
                for (int hour = scheduleItem.InitialHour; hour < scheduleItem.FinalHour; hour++)
                {
                    result.Add(hour);
                }
            }

            return result;
        }

        private List<Reservation> getReservedHours(IRepository<Reservation> reservationRepository, IFilterBuilder<Reservation> reservationFilterBuilder, Reservation reservation)
        {
            reservationFilterBuilder
                .Equal(x => x.Item, reservation.Item)
                .GreaterThan(x => x.Date, reservation.Date.Date)
                .LessThan(x => x.Date, reservation.Date.EndOfDay());            

            return reservationRepository.Recover(reservationFilterBuilder);
        }
    }
}
