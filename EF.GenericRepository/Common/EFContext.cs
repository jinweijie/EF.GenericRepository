using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using EF.GenericRepository.Logging;

namespace EF.GenericRepository.Common
{
    public partial class EFContext : DbContext
    {
        private static readonly ILog Logger = LogProvider.GetCurrentClassLogger();

        public bool EnableTraceSql
        {
            get
            {
                var config = ConfigurationManager.AppSettings["EnableTraceSql"];
                if (string.IsNullOrWhiteSpace(config))
                    return false;

                return config.ToLowerInvariant() == "true";
            }
        }
        #region Constants

        public const string CONNECTION_STRING = "name=DBEntities";
        public const string CONTAINER_NAME = "DBEntities";

        #endregion

        #region Properties

        public ObjectContext ObjectContext
        {
            get { return ((IObjectContextAdapter) this).ObjectContext; }
        }
        
        #endregion

        #region Constructors

        static EFContext()
        {
            // This is a hack to ensure that Entity Framework SQL Provider is copied across to the output folder.
            // As it is installed in the GAC, Copy Local does not work. It is required for probing.
            // Fixed "Provider not loaded" error
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;

            //other solutions:
            //var _ = typeof(System.Data.Entity.SqlServer.SqlProviderServices);
            //var __ = typeof(System.Data.Entity.SqlServerCompact.SqlCeProviderServices);
        }

        public EFContext()
            : base(CONNECTION_STRING)
        {
            Initialize();
        }


        public EFContext(string connectionString)
            : base(connectionString)
        {
            Initialize();
        }


        private void Initialize()
        {
            if (EnableTraceSql)
                base.Database.Log = Logger.Info;
        }

        #endregion

        #region ObjectSet Properties

        #region Samples Set

        //private ObjectSet<Sample> _samples;
        //public ObjectSet<Sample> Samples
        //{
        //    get { return _samples ?? (_samples = this.ObjectContext.CreateObjectSet<Sample>("Samples")); }
        //}

        #endregion

        #endregion

    }
}
