using System;
using System.Linq.Expressions;
using EF.GenericRepository.Common;
using EF.GenericRepository.Entity;

namespace EF.GenericRepository.Specifications
{
    public class LogSearchSpecification : ISpecification<Log>
    {
        public string LevelName { get; set; }
        public string Message { get; set; }
        public Expression<Func<Log, bool>> ToExpression()
        {
            return log => (log.Level.Name == LevelName || LevelName == "") &&
                          (log.Message.Contains(Message) || Message == "");
        }

        public bool IsSatisfiedBy(Log entity)
        {
            return (entity.Level.Name == LevelName || LevelName == "") &&
                   (entity.Message.Contains(Message) || Message == "");
        }
    }
}
