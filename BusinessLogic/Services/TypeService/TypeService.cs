using DataAccess.Repositories.TypeRepo;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using DataAccess.ViewModels.Types;

namespace BusinessLogic.Services.TypeService
{
    public class TypeService : ITypeService
    {
        private ITypeRepo _typeRepo;

        public TypeService(ITypeRepo typeRepo)
        {
            _typeRepo = typeRepo;
        }

        public async Task<IList<ViewType>> GetAllTypesAsync()
        {
            var items = await _typeRepo.GetAllTypes();
            if (items == null) throw new NullReferenceException("Not found any types!");
            return items;
        }
    }
}
