using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.GenericRepository.Entity
{
    public partial class Log
    {
        public string LevelName
        {
            get
            {
                if (this.Level != null)
                    return this.Level.Name;

                return string.Empty;
            }
        }
    }
}
