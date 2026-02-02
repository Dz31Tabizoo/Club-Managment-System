using Krypton.Toolkit;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms_App
{
    public partial class frm_Players_View : KryptonForm
    {
        private readonly PlayerService _playerService = new PlayerService();
        public frm_Players_View()
        {
            
            InitializeComponent();
            this.Size = new Size(1200, 800);
            this.MinimumSize = new Size(1000, 600);
            
        }


        private async Task LoadPlayersData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                // إظهار علامة تحميل (اختياري)
                var players = await _playerService.GetAllPlayersAsync();

                // ربط البيانات
                dgvPlayers.DataSource = null;
                dgvPlayers.DataSource = players;

                // تنسيق الأعمدة
                if (dgvPlayers.Columns["FirstName"] != null)
                {
                    dgvPlayers.Columns["FirstName"].HeaderText = "Prénom";
                    dgvPlayers.Columns["LastName"].HeaderText = "Nom";
                    dgvPlayers.Columns["CategoryName"].HeaderText = "Catégorie";
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to load players into grid");
                KryptonMessageBox.Show("Erreur lors du chargement des données.", null, null);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        

        private async void frm_Players_View_Load(object sender, EventArgs e)
        {
            await LoadPlayersData();
        }
    }
}
