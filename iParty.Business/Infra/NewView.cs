using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Business.Infra
{
    public class NewView
    {
        public NewView(Guid id)
        {
            Id = id;

            IsValid = true;

            Errors = new List<string>();
        }

        public NewView(ValidationResult validationResult)
        {
            IsValid = validationResult.IsValid;

            Errors = validationResult.Errors.Select(p => p.ErrorMessage).ToList();
        }

        public Guid Id { get; private set; }

        public bool IsValid { get; set; }

        public List<string> Errors { get; set; }
    }
}
