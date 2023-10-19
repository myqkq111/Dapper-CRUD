using System.Data;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using test4.Context;
using test4.Models;

namespace test4.Repository
{
    public class EmpRepository : IEmpRepository
    {
        private readonly DapperContext context;
        public EmpRepository(DapperContext _context)
        {
            context = _context;
        }


        //전체 조회
        public async Task<IEnumerable<Emp>> GetAll()
        {
            var query = "SELECT * FROM EMP";

            using (var connection = context.CreateConnection())
            {
                var emps = await connection.QueryAsync<Emp>(query);
                return emps.ToList();
            }
        }


        //단일 항목 조회
        public async Task<Emp> Get(int? empno)
        {
            var query = "SELECT * FROM EMP WHERE EMPNO = :empno";

            using (var connection = context.CreateConnection())
            {
                var emps = await connection.QuerySingleOrDefaultAsync<Emp>(query, new { empno });
                return emps;
            }
        }


        //삭제
        public async Task Delete(int? empno)
        {
            var query = "DELETE FROM EMP WHERE EMPNO = :empno";

            using (var connection = context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { empno });
            }
        }


        //수정
        public Task Update(int? empno, Emp emp)
        {
            var query = "UPDATE EMP SET ENAME = :ename, JOB = :job, MGR = :mgr, HIREDATE = :hiredate, SAL = :sal, COMM = :comm, DEPTNO = :deptno WHERE EMPNO = :empno";
    
            using (var connection = context.CreateConnection())
            {
                connection.Open();

                using (var command = new OracleCommand(query, (OracleConnection)connection)
                {
                    CommandType = CommandType.Text,
                    BindByName = true,
                })
                {
                command.Parameters.Add(new OracleParameter("empno", OracleDbType.Int64){ Value = empno });
                command.Parameters.Add(new OracleParameter(":Ename", OracleDbType.Varchar2) { Value = emp.Ename });
                command.Parameters.Add(new OracleParameter(":Job", OracleDbType.Varchar2) { Value = emp.Job });
                command.Parameters.Add(new OracleParameter(":Mgr", OracleDbType.Int64) { Value = emp.Mgr });
                command.Parameters.Add(new OracleParameter(":Hiredate", OracleDbType.Date) { Value = emp.Hiredate });
                command.Parameters.Add(new OracleParameter(":Sal", OracleDbType.Int64) { Value = emp.Sal });
                command.Parameters.Add(new OracleParameter(":Comm", OracleDbType.Int64) { Value = emp.Comm });
                command.Parameters.Add(new OracleParameter(":Deptno", OracleDbType.Int64) { Value = emp.Deptno });

                command.ExecuteNonQuery();
                }
            }

            return Task.CompletedTask;
        }


        //등록
        public Task<Emp> Add(Emp emp)
        {
            var query = "INSERT INTO EMP (Empno, Ename, Job, Mgr, Hiredate, Sal, Comm, Deptno) VALUES (EMP_SEQ.NEXTVAL, :Ename, :Job, :Mgr, :Hiredate, :Sal, :Comm, :Deptno) RETURNING EMPNO INTO :empno";

            using (var connection = context.CreateConnection())
            {
                connection.Open();

                using (var command = new OracleCommand(query, (OracleConnection)connection)
                {
                    CommandType = CommandType.Text,
                    BindByName = true,
                })
                {
                command.Parameters.Add(new OracleParameter("empno", OracleDbType.Decimal, sizeof(long)){ Direction = ParameterDirection.ReturnValue });
                command.Parameters.Add(new OracleParameter(":Ename", OracleDbType.Varchar2) { Value = emp.Ename });
                command.Parameters.Add(new OracleParameter(":Job", OracleDbType.Varchar2) { Value = emp.Job });
                command.Parameters.Add(new OracleParameter(":Mgr", OracleDbType.Int64) { Value = emp.Mgr });
                command.Parameters.Add(new OracleParameter(":Hiredate", OracleDbType.Date) { Value = emp.Hiredate });
                command.Parameters.Add(new OracleParameter(":Sal", OracleDbType.Int64) { Value = emp.Sal });
                command.Parameters.Add(new OracleParameter(":Comm", OracleDbType.Int64) { Value = emp.Comm });
                command.Parameters.Add(new OracleParameter(":Deptno", OracleDbType.Int64) { Value = emp.Deptno });

                command.ExecuteNonQuery();

                var returnedValue = command.Parameters["empno"].Value;

                OracleDecimal decimalValue = (OracleDecimal)returnedValue;
                emp.Empno = (int?)decimalValue;
                
                return Task.FromResult(emp);
                }
            }
        }















    }
}