using Microsoft.AspNetCore.Mvc;
using test4.Models;
using test4.Repository;

namespace test4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpController : ControllerBase
    {
        private readonly IEmpRepository repository;

        public EmpController(IEmpRepository _repository){
            repository = _repository;
        }


        //전체 조회
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {         
            try
            {
                var emp = await repository.GetAll();
                return Ok(emp);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }  
        }


        //단일 항목 조회
        [HttpGet("{empno}")]
        public async Task<IActionResult> Get(int? empno)
        {
            try
            {
                var emp = await repository.Get(empno);

                if (emp == null)
                {
                    return NotFound();
                }
                return Ok(emp);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        //삭제
        [HttpDelete("{empno}")]
        public async Task<IActionResult> Delete(int? empno)
        {
            try
            {
                var emps = await repository.Get(empno);

                if(emps == null)
                {
                    return NotFound();
                }

                await repository.Delete(empno);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        //수정
        [HttpPut("{empno}")]
        public async Task<IActionResult> Update(int empno, Emp emp)
        {    
            try
            {
                var emps = await repository.Get(empno);

                if(emps == null)
                {
                    return NotFound();
                }

                await repository.Update(empno, emp);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        //등록
        [HttpPost]
        public async Task<IActionResult> Add(Emp emp)
        {
           try
           {
                var emps = await repository.Add(emp);
           
                    return Ok(emps);
           }
           catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }    
        }















    }
}