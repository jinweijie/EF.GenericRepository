using System;
using System.Windows.Forms;
using EF.GenericRepository.Common;
using EF.GenericRepository.Entity;
using EF.GenericRepository.Logging;
using EF.GenericRepository.Repository;
using EF.GenericRepository.Specifications;

namespace EF.GenericRepository
{
    public partial class Main : Form
    {
        private static readonly ILog Logger = LogProvider.GetCurrentClassLogger();

        public Main()
        {
            Logger.InfoFormat("Starting InitializeComponent...");

            InitializeComponent();
            
            Logger.InfoFormat("InitializeComponent complete.");

        }

        private LogSearchSpecification GetSpecification()
        {
            return new LogSearchSpecification
            {
                LevelName = this.cbLevel.Text,
                Message = this.txtMessage.Text
            };
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var uow = new EFUnitOfWork();
            var repo = uow.GetLogRepository();

            var result = repo.Find(GetSpecification(),
                int.Parse(this.txtPageIndex.Text),
                int.Parse(this.txtPageSize.Text),
                new[] { this.txtSortAsc.Text },
                new string[] { this.txtSortDesc.Text }, l=>l.Level);

            this.dgvData.DataSource = result.Item1;
            this.lTotalCountValue.Text = result.Item2.ToString();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            int current = int.Parse(this.txtPageIndex.Text);
            var pre = current-1;
            if (pre < 0)
                pre = 0;

            this.txtPageIndex.Text = pre.ToString();

            btnSearch.PerformClick();

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            int current = int.Parse(this.txtPageIndex.Text);
            var next = current + 1;
            if (next < 0)
                next = 0;

            this.txtPageIndex.Text = next.ToString();

            btnSearch.PerformClick();
        }
        
        private void btnGenerateInfoLog_Click(object sender, EventArgs e)
        {

            var r = new Random();
            for (int i = 0; i < 100; i++)
            {
                if( i % 3 == 0)
                    Logger.InfoFormat("Randomly generated Info log:{0}", r.Next(1000, 1000000));
                else if (i % 3 == 1)
                    Logger.WarnFormat("Randomly generated Warning log:{0}", r.Next(1000, 1000000));
                else if (i % 3 == 2)
                    Logger.ErrorFormat("Randomly generated Error log:{0}", r.Next(1000, 1000000));

            }

            MessageBox.Show("100 info log entries has been generated.");
        }

        private void btnCreateTest_Click(object sender, EventArgs e)
        {
            var uow = new EFUnitOfWork();
            var repo = uow.GetLogRepository();

            repo.Create(new Log
            {
                LevelId = 1,
                Thread = "",
                Location = "Manual Creation",
                Message = "This is manually created log.",
                CreateTime = DateTimeOffset.Now,
                Date = DateTime.Now
            });

            uow.Commit();

            MessageBox.Show("New log created using unit of work pattern.");
        }
    }
}
