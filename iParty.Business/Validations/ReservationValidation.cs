﻿using FluentValidation;
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

            var hoursList = transfomrScheduleItemsIntoHoursList(schedule.Items);

            var reservations = getReservedHours(reservationRepository, reservationFilterBuilder, reservation);

            var availableHours = buildAvailableHoursList(hoursList, reservations);

            return true;
        }

        private List<ScheduleItem> buildAvailableHoursList(List<ScheduleItem> hoursList, List<Reservation> reservations)
        {
            var result = new List<ScheduleItem>();

            result.AddRange(hoursList);

            foreach (var reservation in reservations)
            {
                for (int hour = reservation.InitialHour; hour <= reservation.FinalHour; hour++)
                {
                    result.RemoveAll(x => x.InitialHour == hour && x.FinalHour == hour + 1);
                }
            }

            return result;
        }

        private List<ScheduleItem> transfomrScheduleItemsIntoHoursList(List<ScheduleItem> scheduleItems)
        {
            var result = new List<ScheduleItem>();

            foreach (var scheduleItem in scheduleItems)
            {
                for (int hour = scheduleItem.InitialHour; hour <= scheduleItem.FinalHour; hour++)
                {
                    result.Add(new ScheduleItem() { InitialHour = hour, FinalHour = hour + 1 });
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