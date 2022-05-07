using AutoMapper;
using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Data.Entities;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class EmployeeController : ControllerBase
    {
        private readonly IDepartmentRepository repository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;

        public EmployeeController(IDepartmentRepository repository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.repository = repository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        // GET: api/<EmployeeController>
        [HttpGet()]
        public async Task<ActionResult<EmployeeModel[]>> GetAll()
        {
            try
            {
                //Return Employees
               var emps =  await repository.GetAllEmployees();
                return Ok(mapper.Map<EmployeeModel[]>(emps));
            }catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Internal Failure");
            }
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{employeeid}")]
        public async Task<ActionResult<EmployeeModel>> Get(int employeeid)
        {
            try
            {
                //Return specific employee by employeeID
                var emp = await repository.getemployee(employeeid);
                return Ok(mapper.Map<EmployeeModel>(emp));
            }catch(Exception )
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Internal Failure");
            }

        }

        //POST employee only if department already exist.
        // POST api/<EmployeeController>
        [HttpPost]
        public async Task<ActionResult<EmployeeModel>> Post([FromBody] EmpPostModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }
            try
            {
                var existing = await repository.getemployee(model.Id);

                //if model with  model.Id exist
                if (existing != null)
                {
                    return BadRequest($"{model.Id} already exists");
                }

                //api/employee/2
                var location = linkGenerator.GetPathByAction(
                  "Get", "Employee"
                  , new { employeeid = model.Id });

                if (string.IsNullOrWhiteSpace(location))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                                    "Caching not implemented properly");
                }

                
                if (await repository.exists(model.DepartmentId))
                {
                    var dpmnt = await repository.Getdepartment(model.DepartmentId);
                    
                    //Create a new Employee
                    var employe = mapper.Map<Employee>(model);
                    employe.Department = dpmnt;
                    repository.Add(employe);
                    
                    //Save Changes
                    if (await repository.SaveChangesAsync())
                    {
                        return Created(location!, mapper.Map<EmpPostModel>(employe));
                    }
                }
                else
                {
                    return BadRequest($"{model.DepartmentId} does not exist so can't use");
                }
            }catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                                            "Internal Failure");
            }
            return BadRequest("Bad Request");
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{empId}")]
        public async Task<ActionResult<EmployeeModel>> Put(int empId, [FromBody] EmployeeModel model)
        {
            try
           {
                var employeetoedit = repository.getemployee(empId);
                if(employeetoedit == null)
                {
                 return NotFound($"Employee with {model.Name} {model.Surname} does not exist");
                }
                
                 mapper.Map(model,await employeetoedit);
                if(await repository.SaveChangesAsync())
                {
                    return mapper.Map<EmployeeModel>(await employeetoedit);
                }
                
            }catch (Exception )
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                                            "Internal Failure");
            }
            return BadRequest();
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{empID}")]
        public async Task<IActionResult> Delete(int empID)
        {
            try
            {
                //Get Employee
                var emp = await repository.getemployee(empID);
                
                if (emp == null)
                {
                    return NotFound();
                }
                else
                {
                    //Deletion of employee
                    repository.Delete(emp!);
                    if (await repository.SaveChangesAsync())
                    {
                        return Ok();
                    }
                }
                return BadRequest();
            }
            catch (Exception )
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                                            "Internal Failure");
            }
        }

    }
}
