using EF.GenericRepository.Common;
using EF.GenericRepository.Entity;

namespace EF.GenericRepository.Repository
{
    public class LogRepository : AbstractRepository<Log, int>
    {
        public LogRepository(EFContext context)
            : base(context)
        {
        }
    }


}
