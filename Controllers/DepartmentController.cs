using AutoMapper;
using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Data.Entities;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiController]

    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository repository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public DepartmentController(IDepartmentRepository repository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.repository = repository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        // GET: api/<DepartmentController>
        [HttpGet]
        public async Task<ActionResult<DepartmentModel[]>> GetAll(bool includeEmployee = false)
        {
            try
            {
                //Get All departments
                var departments = await repository.getAllDepartments(includeEmployee);
                return Ok(mapper.Map<DepartmentModel[]>(departments));    
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Databasee Failure");
            }
        }

        // GET api/<DepartmentController>/1
        [HttpGet("{departmentId}")]
        public async Task<ActionResult<DepartmentModel>> Get(int departmentId)
        {
            try
            {
                //Get specific department
                var dprtmnt = await repository.getDepartmentwithEmployee(departmentId);
                return Ok(mapper.Map<DepartmentModel>(dprtmnt));
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Databasee Failure");
            }
        }

        //search for ID associated with departmnet name.
        // GET api/<DepartmentController>/GetIDFromDeptName/{department}
        [HttpGet("GetIDFromDeptName/{department}")]
        public async Task<ActionResult<int[]>> GetIdFromDeptName(string department)
        {
            try
            {
                var Ids = await repository.getDeptIDArrayAsync(department);
                return Ok(Ids);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Databasee Failure");
            }
        }

        // POST api/<DepartmentController>
        [HttpPost]
        public async Task<ActionResult<Department>> Post([FromBody] DepartmentModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }
            try
            {
                //Get department
                var existing =await repository.getDepartmentwithEmployee(model.Id);
                
                //if exists
                if(existing != null)
                {
                    return BadRequest($"{model.DepartmentName} department already exists");
                }

                //api/controller/2
                var location = linkGenerator.GetPathByAction(
                                   "Get", "Department"
                                   , new { departmentId = model.Id });

                //Create new department
                var department = mapper.Map<Department>(model);
                
                repository.Add(department);
                try
                {
                    //Save Changes.
                    if (await repository.SaveChangesAsync())
                    {
                        return Created(location!, mapper.Map<DepartmentModel>(department));
                    }
                }catch (Exception)
                {
                    return BadRequest("Either few of or all employee already exists OR Unable to save changes due to Bad Request");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                    ex.Message);
            }
            return BadRequest("Bad Request");
        }

        // PUT api/<DepartmentController>/1
        [HttpPut("{departmentId}")]
        public async Task<ActionResult<DepartmentPutModel>> Put(int departmentId, [FromBody] DepartmentPutModel model)
        {
            try
            {
                //Get department
                var existing = await repository.Getdepartment(departmentId);
                if (existing == null)
                {
                    return NotFound($"{departmentId} not found");
                }
                else
                {
                    mapper.Map(model, existing);
                    if (await repository.SaveChangesAsync())
                    {
                        return mapper.Map<DepartmentPutModel>(existing);
                    }
                }
            }
            catch(Exception )
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                   "Internal Failure");
            }
            return BadRequest();
         }


        // DELETE api/<ValuesController>/1
        [HttpDelete("{departmentId}")]
        public async Task<IActionResult> Delete(int departmentId)
        {
            try
            {
                //Get department
                var deprmnt = repository.getDepartmentwithEmployee(departmentId);
                if (deprmnt == null)
                {
                    return NotFound();
                }
                else
                {
                    //Delete department.
                    repository.Delete(await deprmnt);

                    //Save changes.
                    if (await repository.SaveChangesAsync())
                    {
                        return Ok();
                    }
                }
            }

            catch (Exception)
            {
              return StatusCode(StatusCodes.Status500InternalServerError,
                              "Internal Failure");  
            }
                return BadRequest();
        }
    }
}

