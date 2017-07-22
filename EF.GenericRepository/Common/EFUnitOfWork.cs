using System;
using System.Data.Entity.Validation;
using System.Text;
using System.Threading.Tasks;
using EF.GenericRepository.Repository;

namespace EF.GenericRepository.Common
{
    public class EFUnitOfWork
    {
        /// <summary>
        /// The context
        /// </summary>
        readonly EFContext _context;
        /// <summary>
        /// Initializes a new instance of the <see cref="EFUnitOfWork"/> class.
        /// </summary>
        public EFUnitOfWork()
        {
            _context = new EFContext();
        }

        #region IUnitOfWork Members

        /// <summary>
        /// Commits this instance.
        /// </summary>
        public int Commit()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                throw new Exception(GetValidationErrorMessage(ex), ex);
            }
        }
        
        public async Task<int> CommitAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (DbEntityValidationException ex)
            {
                throw new Exception(GetValidationErrorMessage(ex), ex);
            }
        }

        public LogRepository GetLogRepository()
        {
            return new LogRepository(this._context);
        }
        #endregion

        #region private methods

        /// <summary>
        /// Gets the validation error message.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns></returns>
        private string GetValidationErrorMessage(DbEntityValidationException ex)
        {
            var validationErrorMessage = new StringBuilder();
            foreach (var error in ex.EntityValidationErrors)
            {
                if (error.Entry != null && error.Entry.Entity != null)
                {
                    validationErrorMessage.Append(string.Format("Validation error in entity [{0}]:\r\n", error.Entry.Entity.GetType()));
                }

                foreach (var validationError in error.ValidationErrors)
                {
                    validationErrorMessage.Append(string.Format("[{0}]:{1}\r\n", validationError.PropertyName, validationError.ErrorMessage));
                }
            }

            return validationErrorMessage.ToString();
        }
        #endregion

    }
}
