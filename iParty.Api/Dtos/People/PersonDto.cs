﻿using iParty.Api.Dtos.Addresses;
using System.Collections.Generic;

namespace iParty.Api.Dtos.People
{
    public class PersonDto
    {
        public object User { get; set; }

        public string Name { get; set; }

        public string Document { get; set; }

        public object Photo { get; set; }        

        public List<AddressDto> Addresses { get; set; }

        public List<PhoneDto> Phones { get; set; }
    }
}