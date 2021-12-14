using iParty.Business.Infra;
using iParty.Business.Models;
using iParty.Business.Models.Addresses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Business.Interfaces
{
    public interface IServiceCity : IService<City>
    {
        public ServiceResult<City> Create(City city);

        public ServiceResult<City> Update(Guid id, City city);
    }
}
