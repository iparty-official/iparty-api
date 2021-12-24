using FluentValidation;
using iParty.Business.Interfaces.Validations;
using iParty.Business.Models.Items;
using System.Linq;

namespace iParty.Business.Validations
{
    public class ItemScheduleValidation : AbstractValidator<Item>, IItemScheduleValidation
    {        
        public ItemScheduleValidation()
        {
            RuleFor(p => itemScheduleDuplicated(p)).Equal(false).WithMessage("O item possui mais de uma agenda aberta para o mesmo dia da semana.");

            RuleFor(p => hourRangeDuplicated(p)).Equal(false).WithMessage("O item possui um ou mais dias da semana com horários conflitantes.");
        }

        private bool itemScheduleDuplicated(Item item)
        {
            var result = item.Schedules
                .GroupBy(x => x.DayOfWeek)
                .Where(g => g.Count() > 1)
                .Select(x => x.Key);

            return result.Count() > 0;
        }

        private bool hourRangeDuplicated(Item item)
        {           
            foreach (var schedule in item.Schedules)
            {
                foreach (var currentHour in schedule.Hours)
                {                                        
                    var equalHours = schedule
                        .Hours
                        .Where (otherHour => otherHour.Id != currentHour.Id && otherHour.InitialHour == currentHour.InitialHour && otherHour.FinalHour == currentHour.FinalHour);

                    var hoursWhereInitialHourItIsInsideTheRange = schedule
                        .Hours
                        .Where(otherHour => otherHour.Id != currentHour.Id && otherHour.InitialHour < currentHour.InitialHour && currentHour.InitialHour < otherHour.FinalHour);

                    var hoursWhereFinalHourItIsInsideTheRange = schedule
                        .Hours
                        .Where(otherHour => otherHour.Id != currentHour.Id && otherHour.InitialHour < currentHour.FinalHour && currentHour.FinalHour < otherHour.FinalHour);

                    if (equalHours.Count() > 0 || hoursWhereInitialHourItIsInsideTheRange.Count() > 0 || hoursWhereFinalHourItIsInsideTheRange.Count() > 0)
                        return true;                    
                }
            }

            return false;
        }
    }
}
