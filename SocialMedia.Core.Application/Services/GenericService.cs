using AutoMapper;
using SocialMedia.Core.Application.Interfaces.Repositories;
using SocialMedia.Core.Application.Interfaces.Services;

namespace SocialMedia.Core.Application.Services
{
    public class GenericService<SaveViewModel, ViewModel, Entity> : IGenericService<SaveViewModel, ViewModel, Entity>
        where SaveViewModel : class
        where ViewModel : class
        where Entity : class
    {
        private readonly IGenericRepository<Entity> _repository;
        private readonly IMapper _mapper;
        public GenericService(IGenericRepository<Entity> _repository, IMapper _mapper)
        {
            this._repository = _repository;
            this._mapper = _mapper;
        }
        public virtual async Task<SaveViewModel> AddAsync(SaveViewModel viewModel)
        {
            Entity entity=_mapper.Map<Entity>(viewModel);
            entity = await _repository.AddAsync(entity);
            SaveViewModel saveViewModel= _mapper.Map<SaveViewModel>(entity);
            return saveViewModel;
        }

        public virtual async Task Delete(int id)
        {
            Entity entity= await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(entity);
        }

        public virtual async Task<IEnumerable<ViewModel>> GetAllViewModel()
        {
            var entityList=await _repository.GetAllAsync();
            return  _mapper.Map<List<ViewModel>>(entityList);
        }

        public virtual async Task<SaveViewModel> GetByIdSaveViewModel(int id)
        {
            Entity entity=await _repository.GetByIdAsync(id);
            SaveViewModel saveViewModel =_mapper.Map<SaveViewModel>(entity);
            return saveViewModel;
        }

        public virtual async Task Update(SaveViewModel viewModel, int id)
        {
            Entity entity = _mapper.Map<Entity>(viewModel);
            await _repository.UpdateAsync(entity,id);
        }
    }
}
