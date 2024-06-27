using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Application.Interfaces.Services
{
    public interface IGenericService<SaveViewModel, ViewModel, Entity>
        where SaveViewModel:class
        where ViewModel : class
        where Entity : class
    {
        Task<SaveViewModel> AddAsync(SaveViewModel viewModel);
        Task Update(SaveViewModel viewModel, int id);
        Task Delete(int id);
        Task<IEnumerable<ViewModel>> GetAllViewModel();
        Task<SaveViewModel> GetByIdSaveViewModel(int id);
    }
}
