using Microsoft.AspNetCore.Mvc;
using PLL.Data.Dao.Interfaces;
using PLL.Data.Entity;

namespace PLL.Controllers
{
    [Route("[controller]")]
    public class TrainingController : Controller
    {
        private readonly IDaoAccessor _accessor;

        public TrainingController(IDaoAccessor accessor)
        {
            _accessor = accessor;
        }

        [HttpGet("get-trainings")]
        public async Task<ActionResult> GetAllAsync()
        {
            var entityList = await _accessor.TrainingDao.GetAllAsync();

            return Json(entityList);
        }

        [HttpGet("get-training/{id:Guid}")]
        public async Task<ActionResult> GetByIdAsync(Guid id)
        {
            var entity = await _accessor.TrainingDao.GetByIdAsync(id.ToString());

            return Json(entity);
        }

        [HttpPost("create-training")]
        public async Task<ActionResult> CreateTrainingAsync([FromBody] Training training)
        {
            await _accessor.TrainingDao.CreateAsync(training);

            return Ok();
        }

        [HttpPost("update-training")]
        public async Task<ActionResult> UpdateTrainingAsync([FromBody] Training training)
        {
            await _accessor.TrainingDao.UpdateAsync(training);

            return Ok();
        }

        [HttpPost("delete-training/{id:Guid}")]
        public async Task<ActionResult> DeleteTrainingAsync(Guid id)
        {
            await _accessor.TrainingDao.DeleteAsync(id.ToString());

            return Ok();
        }
    }
}
