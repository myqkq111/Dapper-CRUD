
using test4.Models;

namespace test4.Repository
{
    public interface IEmpRepository
    {
       //전체 조회
       public Task<IEnumerable<Emp>> GetAll();

       //단일 항목 조회
       public Task<Emp> Get(int? empno);

        //삭제
       public Task Delete(int? empno); 

       //수정
       public Task Update(int? empno, Emp emp);

       //등록
       public Task<Emp> Add(Emp emp);

    }
}