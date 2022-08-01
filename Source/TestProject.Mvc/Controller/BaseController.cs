using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestProject.Interfaces;

namespace TestProject.Mvc.Controller {
    [ApiExplorerSettings(IgnoreApi = true)]
    public abstract class BaseController<T, TRepo, TVm> : ControllerBase, IDisposable
        where T : class, new()
        where TRepo : IRepo<T>
        where TVm : IViewModel<T> {
        protected readonly TRepo _repo;
        protected readonly IMapper _mapper;
        protected readonly ILogger _logger;
        #region logs

        //_logger = loggerFactory.CreateLogger(GetType());
        #endregion
        public BaseController(TRepo repo,
            IMapper mapper, ILoggerFactory loggerFactory) {
            _repo = repo;
            _mapper = mapper;
            _logger = loggerFactory.CreateLogger(GetType());
        }

        [HttpGet]
        [HttpGet("{skip}/{take}")]
        public virtual async Task<IActionResult> Get([FromRoute] int skip = 0, [FromRoute] int take = 20, CancellationToken cancellationToken = default) {
            try {
                IQueryable set = _repo.Set().Skip(skip).Take(take);
                return Ok(new {
                    Result = await set.ProjectTo<TVm>(_mapper.ConfigurationProvider).ToListAsync(),
                    Count = set.Cast<T>().Count()
                });
            } catch(Exception ex) {
                _logger.LogError(ex, ex.Message);
                return Ok(ex.Message);
            }

        }

        [HttpGet("{id}")]
        public virtual async Task<TVm> Get([FromRoute] TVm vm, CancellationToken cancellationToken = default) {
            try {
                return await _repo.Set()
                    .Where(vm.EqualsExpression)
                    .ProjectTo<TVm>(_mapper.ConfigurationProvider).SingleOrDefaultAsync(cancellationToken);
            } catch(Exception ex) {
                _logger.LogError(ex, ex.Message);
                return default(TVm);
            }
        }

        [HttpPost]
        public virtual async Task<ActionResult<TVm>> Post([FromBody] TVm vm, CancellationToken cancellationToken = default) {
            try {
                if(vm == null) return BadRequest(ModelState);
                if(_repo.Set().Any(vm.EqualsExpression)) return BadRequest();

                T model;
                using(var transaction = await _repo.BeginTransactionAsync(cancellationToken)) {
                    model = VmToModel(vm);

                    if(model is ITrackable trackable) {
                        trackable.CreatedAtTime = trackable.UpdatedAtTime = DateTime.Now;
                        trackable.CreatedByUser = trackable.UpdatedByUser = User.Identity.Name;
                    }

                    try {
                        await _repo.InsertAsync(model, cancellationToken);
                        transaction.Commit();
                    } catch(Exception ex) {
                        transaction.Rollback();
                        throw ex;
                    }
                }

                return ModelToVm(model);
            } catch(Exception ex) {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
            finally {
            }
        }

        [HttpPut]
        public virtual async Task<ActionResult<TVm>> Put([FromBody] TVm vm, CancellationToken cancellationToken = default) {
            try {
                if(vm == null) return BadRequest(ModelState);
                if(!_repo.Set().Any(vm.EqualsExpression)) return NotFound();

                var model = VmToModel(vm);

                if(model is ITrackable trackable) {
                    trackable.UpdatedAtTime = DateTime.Now;
                    trackable.UpdatedByUser = User.Identity.Name;
                }

                using(var transaction = await _repo.BeginTransactionAsync(cancellationToken)) {
                    try {
                        await _repo.UpdateAsync(model, cancellationToken);
                        transaction.Commit();
                    } catch(Exception ex) {
                        transaction.Rollback();
                        return BadRequest(ex.Message);
                    }
                }

                return ModelToVm(model);

            } catch(Exception ex) {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ModelState);
            }
            finally {
            }
        }

        [HttpDelete("{id}")]
        public virtual async Task<ActionResult> Delete([FromRoute] TVm vm, CancellationToken cancellationToken = default) {
            try {
                using(var transaction = await _repo.BeginTransactionAsync(cancellationToken)) {
                    try {
                        var model = await _repo.Set().SingleAsync(vm.EqualsExpression, cancellationToken);

                        if(model is IRecoverable recoverable) {
                            recoverable.DeletedAtTime = DateTime.Now;
                            recoverable.DeletedByUser = User.Identity.Name;
                            recoverable.IsDeleted = true;
                            await _repo.UpdateAsync(model, cancellationToken);
                        } else {
                            await _repo.DeleteAsync(model, cancellationToken);
                        }

                        transaction.Commit();
                    } catch(Exception ex) {
                        _logger.LogError(ex, ex.Message);
                        transaction.Rollback();
                        throw ex;
                    }
                }
                return Ok();
            } catch(Exception ex) {
                return BadRequest(ex.Message);
            }
            finally {
            }
        }
        protected virtual TVm ModelToVm(T model) {
            return _mapper.Map<TVm>(model);
        }
        protected virtual T VmToModel(TVm vm) {
            T model;
            if(vm.IsNew()) {
                model = new T();
            } else {
                model = _repo.Set().Single(vm.EqualsExpression);
            }

            return _mapper.Map(vm, model);
        }

        protected virtual void Dispose(bool disposing) {
        }

        public void Dispose() {
        }
    }
}
