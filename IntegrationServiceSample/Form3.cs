using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IntegrationService
{

    public partial class Form3 : MetroFramework.Forms.MetroForm
    {

        public Form3()
        {
            InitializeComponent();
        }
        string serverName = Properties.Settings.Default["serverName"].ToString();
        string databaseName = Properties.Settings.Default["databaseName"].ToString();
        string tableName = Properties.Settings.Default["tableName"].ToString();
        string columnName = Properties.Settings.Default["columnName"].ToString();
        string query = Properties.Settings.Default["Query"].ToString();
        string selectedIndex = Properties.Settings.Default["selectedIndex"].ToString();


        string serverName2 ="", sqlInfo = "";
        private void Form3_Load(object sender, EventArgs e)
        {
            getServer();
            timer1.Enabled = true;
        }
        private void metroButton1_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            this.Hide();
            frm.Show();
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            this.Hide();
            frm.Show();
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            //this.Hide();
            getData();
        }
        private void getServer()
        {
            try
            {
                SqlConnection con = new SqlConnection("server="+serverName+"; Initial Catalog="+databaseName+";Integrated Security=SSPI");
                SqlDataAdapter da = new SqlDataAdapter(@"select "+columnName+ ",sqlInfo from " + tableName+" ", con);
                DataSet ds = new DataSet();
                con.Open();
                da.Fill(ds, "query");
                dataGridView2.DataSource = ds.Tables["query"];
                con.Close();

            }
            catch(Exception errorText)
            {
                MessageBox.Show("Error : "+ errorText);
            }
        }
        private void getData()
        {
            this.Hide();   
                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                try
                {
                    sqlInfo = dataGridView2.Rows[i].Cells[1].Value.ToString();

                    if (sqlInfo == "MSSQL") {

                        serverName2 = dataGridView2.Rows[i].Cells[0].Value.ToString().Trim();

                        //MessageBox.Show("" + domain);

                        SqlConnection con = new SqlConnection("server=" + serverName2 + "; Initial Catalog=master;Integrated Security=SSPI; CONNECT TIMEOUT=30");
                        SqlDataAdapter da = new SqlDataAdapter(@"" + query + "", con);
                        DataSet ds = new DataSet();
                        con.Open();
                        da.Fill(ds, "query");
                        dataGridView1.DataSource = ds.Tables["query"];
                        setQuery();
                    }
                    else if (sqlInfo=="POSTGRESQL")
                    {
                        serverName2 = dataGridView2.Rows[i].Cells[0].Value.ToString().Trim();


                        NpgsqlConnection con = new NpgsqlConnection("Server=localhost;Port=5432;Database=forTest; User Id=postgres; Password=123");
                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(@"select * from account", con);
                        DataSet ds = new DataSet();
                        con.Open();
                        da.Fill(ds);
                        dataGridView1.DataSource = ds.Tables[0];
                    }

                }
                catch (Exception es) {


                    SqlConnection con = new SqlConnection(@"server=" + serverName + "; Initial Catalog=" + databaseName + ";Integrated Security=SSPI");
                    SqlCommand cmd = new SqlCommand("insert into tblConnectError values (@name,@error,getdate())", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@name", serverName2);
                    cmd.Parameters.AddWithValue("@error", es.ToString());
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                }
            }
        }
        private void setQuery()
        {
            this.Hide();
            if (Convert.ToInt32(selectedIndex) == 0)
            {

                for (int i = 0; i <= dataGridView1.Rows.Count; i++)
                {
                    //serverName2 = dataGridView1.Rows[i].Cells[0].Value0].Value.ToString();
                    try
                    {


                        DateTime date1 = Convert.ToDateTime(dataGridView1.Rows[i].Cells[20].Value.ToString());
                        DateTime date2 = Convert.ToDateTime(dataGridView1.Rows[i].Cells[34].Value.ToString());
                        DateTime date3 = Convert.ToDateTime(dataGridView1.Rows[i].Cells[35].Value.ToString());
                        DateTime date4 = Convert.ToDateTime(dataGridView1.Rows[i].Cells[38].Value.ToString());


                        string sqlFormattedDate1 = date1.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        string sqlFormattedDate2 = date2.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        string sqlFormattedDate3 = date3.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        string sqlFormattedDate4 = date4.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        SqlConnection con = new SqlConnection(@"server=" + serverName + "; Initial Catalog=" + databaseName + ";Integrated Security=SSPI; CONNECT TIMEOUT=30");
                        SqlCommand cmd = new SqlCommand(@"
insert into tbl_backup_check_ForMe values( 	
    @1ServerName ,
	@1InstanceName ,
	@1VersionInf ,
	@1ProductLevel ,
	@1Edition ,
	@1ProductVersion ,
	@1net_transport ,
	@1protocol_type ,
	@1auth_scheme ,
	@1local_net_address ,
	@1local_tcp_port ,
	@1client_net_address ,
	@1databasename ,
	@1databaseStatus ,
	@1PhysicalMemoryMb ,
	@1MaxServerMemory ,
	@1SQLServerMemoryUsageMB ,
	@1AvailableMemoryMB ,
	@1SERVER_COST_THRESHOLD ,
	@1SERVER_MAX_DEGREE ,
	@1Database_create_date,
	@1compatibilitylevel ,
	@1collation_name ,
	@1user_access_desc ,
	@1recovery_model_desc ,
	@1databaseFilePath ,
	@1databaseSize ,
	@1databaseMaxsize ,
	@1databaseGrown ,
	@1nis_percent_growth_Buyume_Yuzdesiame,
	@1last_backup_device_name ,
	@1backupSecond ,
	@1backupSize_GB ,
	@1is_compressed ,
	@1last_db_backup_start_date,
	@1last_db_backup_finish_date,
	@1backupType ,
	@1backupAgeDays ,
	@1LogTime)", con);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@1ServerName", dataGridView1.Rows[i].Cells[0].Value);
                        cmd.Parameters.AddWithValue("@1InstanceName", dataGridView1.Rows[i].Cells[1].Value);
                        cmd.Parameters.AddWithValue("@1VersionInf", dataGridView1.Rows[i].Cells[2].Value);
                        cmd.Parameters.AddWithValue("@1ProductLevel", dataGridView1.Rows[i].Cells[3].Value);
                        cmd.Parameters.AddWithValue("@1Edition", dataGridView1.Rows[i].Cells[4].Value);
                        cmd.Parameters.AddWithValue("@1ProductVersion", dataGridView1.Rows[i].Cells[5].Value);
                        cmd.Parameters.AddWithValue("@1net_transport", dataGridView1.Rows[i].Cells[6].Value);
                        cmd.Parameters.AddWithValue("@1protocol_type", dataGridView1.Rows[i].Cells[7].Value);
                        cmd.Parameters.AddWithValue("@1auth_scheme", dataGridView1.Rows[i].Cells[8].Value);
                        cmd.Parameters.AddWithValue("@1local_net_address", dataGridView1.Rows[i].Cells[9].Value);
                        cmd.Parameters.AddWithValue("@1local_tcp_port", dataGridView1.Rows[i].Cells[10].Value);
                        cmd.Parameters.AddWithValue("@1client_net_address", dataGridView1.Rows[i].Cells[11].Value);
                        cmd.Parameters.AddWithValue("@1databasename", dataGridView1.Rows[i].Cells[12].Value);
                        cmd.Parameters.AddWithValue("@1databaseStatus", dataGridView1.Rows[i].Cells[13].Value);
                        cmd.Parameters.AddWithValue("@1PhysicalMemoryMb", Convert.ToInt64(dataGridView1.Rows[i].Cells[14].Value));
                        cmd.Parameters.AddWithValue("@1MaxServerMemory", Convert.ToInt64(dataGridView1.Rows[i].Cells[15].Value));
                        cmd.Parameters.AddWithValue("@1SQLServerMemoryUsageMB", Convert.ToInt64(dataGridView1.Rows[i].Cells[16].Value));
                        cmd.Parameters.AddWithValue("@1AvailableMemoryMB", Convert.ToInt64(dataGridView1.Rows[i].Cells[17].Value));
                        cmd.Parameters.AddWithValue("@1SERVER_COST_THRESHOLD", dataGridView1.Rows[i].Cells[18].Value);
                        cmd.Parameters.AddWithValue("@1SERVER_MAX_DEGREE", dataGridView1.Rows[i].Cells[19].Value);
                        cmd.Parameters.AddWithValue("@1Database_create_date", sqlFormattedDate1);
                        cmd.Parameters.AddWithValue("@1compatibilitylevel", Convert.ToInt64(dataGridView1.Rows[i].Cells[21].Value));
                        cmd.Parameters.AddWithValue("@1collation_name", dataGridView1.Rows[i].Cells[22].Value);
                        cmd.Parameters.AddWithValue("@1user_access_desc", dataGridView1.Rows[i].Cells[23].Value);
                        cmd.Parameters.AddWithValue("@1recovery_model_desc", dataGridView1.Rows[i].Cells[24].Value);
                        cmd.Parameters.AddWithValue("@1databaseFilePath", dataGridView1.Rows[i].Cells[25].Value);
                        cmd.Parameters.AddWithValue("@1databaseSize", Convert.ToInt64(dataGridView1.Rows[i].Cells[26].Value));
                        cmd.Parameters.AddWithValue("@1databaseMaxsize", Convert.ToInt64(dataGridView1.Rows[i].Cells[27].Value));
                        cmd.Parameters.AddWithValue("@1databaseGrown", Convert.ToInt64(dataGridView1.Rows[i].Cells[28].Value));
                        cmd.Parameters.AddWithValue("@1nis_percent_growth_Buyume_Yuzdesiame", Convert.ToInt64(dataGridView1.Rows[i].Cells[29].Value));
                        cmd.Parameters.AddWithValue("@1last_backup_device_name", dataGridView1.Rows[i].Cells[30].Value);
                        cmd.Parameters.AddWithValue("@1backupSecond", Convert.ToInt64(dataGridView1.Rows[i].Cells[31].Value));
                        cmd.Parameters.AddWithValue("@1backupSize_GB", Convert.ToInt64(dataGridView1.Rows[i].Cells[32].Value));
                        cmd.Parameters.AddWithValue("@1is_compressed", dataGridView1.Rows[i].Cells[33].Value);
                        cmd.Parameters.AddWithValue("@1last_db_backup_start_date", sqlFormattedDate2);
                        cmd.Parameters.AddWithValue("@1last_db_backup_finish_date", sqlFormattedDate3);
                        cmd.Parameters.AddWithValue("@1backupType", dataGridView1.Rows[i].Cells[36].Value);
                        cmd.Parameters.AddWithValue("@1backupAgeDays", Convert.ToInt64(dataGridView1.Rows[i].Cells[37].Value));
                        cmd.Parameters.AddWithValue("@1LogTime", sqlFormattedDate4);


                        //MessageBox.Show(""+cmd.CommandText+"        ---    ");

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    catch (Exception errorText)
                    {
                        // MessageBox.Show(""+errorText);
                        SqlConnection con = new SqlConnection(@"server=" + serverName + "; Initial Catalog=" + databaseName + ";Integrated Security=SSPI");
                        SqlCommand cmd = new SqlCommand("insert into tblQueryError values (@name,@error,getdate())", con);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@name", serverName2);
                        cmd.Parameters.AddWithValue("@error", errorText.ToString());
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            else if (Convert.ToInt32(selectedIndex) == 1)
            {
                for (int i = 0; i <= dataGridView1.Rows.Count; i++)
                {
                    try
                    {

                        DateTime date1 = Convert.ToDateTime(dataGridView1.Rows[i].Cells[15].Value.ToString());
                        DateTime date2 = Convert.ToDateTime(dataGridView1.Rows[i].Cells[16].Value.ToString());
                       



                        string sqlFormattedDate1 = date1.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        string sqlFormattedDate2 = date2.ToString("yyyy-MM-dd HH:mm:ss.fff");

                        SqlConnection con = new SqlConnection(@"server = " + serverName + "; Initial Catalog = " + databaseName + "; Integrated Security = SSPI; CONNECT TIMEOUT=30");
                        SqlCommand cmd = new SqlCommand(@"
 insert into [dbo].[tblUserInformation] values(
	@serverName ,
	@serverIP ,
	@serverPort ,
	@dbname ,
	@dbUser ,
	@serverLogin ,
	@logintype ,
	@sysadmin ,
	@securityadmin ,
	@serveradmin ,
	@diskadmin ,
	@setupadmin ,
	@processadmin ,
	@dbcreator ,
	@bulkadmin ,
	@create_date ,
	@modify_date ,
	@Permissions_user,
      GETDATE() )", con);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@serverName", dataGridView1.Rows[i].Cells[0].Value);
                        cmd.Parameters.AddWithValue("@serverIP", dataGridView1.Rows[i].Cells[1].Value);
                        cmd.Parameters.AddWithValue("@serverPort", dataGridView1.Rows[i].Cells[2].Value);
                        cmd.Parameters.AddWithValue("@dbname", dataGridView1.Rows[i].Cells[3].Value);
                        cmd.Parameters.AddWithValue("@dbUser", Convert.ToString(dataGridView1.Rows[i].Cells[4].Value));
                        cmd.Parameters.AddWithValue("@serverLogin", dataGridView1.Rows[i].Cells[5].Value);
                        cmd.Parameters.AddWithValue("@logintype", dataGridView1.Rows[i].Cells[6].Value);
                        cmd.Parameters.AddWithValue("@sysadmin", dataGridView1.Rows[i].Cells[7].Value);
                        cmd.Parameters.AddWithValue("@securityadmin", dataGridView1.Rows[i].Cells[8].Value);
                        cmd.Parameters.AddWithValue("@serveradmin", dataGridView1.Rows[i].Cells[9].Value);
                        cmd.Parameters.AddWithValue("@diskadmin", dataGridView1.Rows[i].Cells[10].Value);
                        cmd.Parameters.AddWithValue("@setupadmin", dataGridView1.Rows[i].Cells[11].Value);
                        cmd.Parameters.AddWithValue("@processadmin", dataGridView1.Rows[i].Cells[12].Value);
                        cmd.Parameters.AddWithValue("@dbcreator", dataGridView1.Rows[i].Cells[13].Value);
                        cmd.Parameters.AddWithValue("@bulkadmin", dataGridView1.Rows[i].Cells[14].Value);
                        cmd.Parameters.AddWithValue("@create_date", sqlFormattedDate1);
                        cmd.Parameters.AddWithValue("@modify_date", sqlFormattedDate2);
                        cmd.Parameters.AddWithValue("@Permissions_user", Convert.ToString(dataGridView1.Rows[i].Cells[17].Value));

                        //MessageBox.Show(""+cmd.CommandText+"        ---    ");

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    catch (Exception errorText)
                    {
                        //MessageBox.Show(""+errorText);
                        SqlConnection con = new SqlConnection(@"server=" + serverName + "; Initial Catalog=" + databaseName + ";Integrated Security=SSPI");
                        SqlCommand cmd = new SqlCommand("insert into tblBackupInfoError values (@name,@error,getdate())", con);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@name", serverName2);
                        cmd.Parameters.AddWithValue("@error", errorText.ToString());
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }

            }
            else if (Convert.ToInt32(selectedIndex) == 2)
            {
                for (int i = 0; i <= dataGridView1.Rows.Count; i++)
                {
                    try
                    {

                        DateTime date1 = Convert.ToDateTime(dataGridView1.Rows[i].Cells[4].Value.ToString());
                        DateTime date2 = Convert.ToDateTime(dataGridView1.Rows[i].Cells[5].Value.ToString());
                        DateTime date3 = Convert.ToDateTime(dataGridView1.Rows[i].Cells[17].Value.ToString());



                        string sqlFormattedDate1 = date1.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        string sqlFormattedDate2 = date2.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        string sqlFormattedDate3 = date3.ToString("yyyy-MM-dd HH:mm:ss.fff");

                        SqlConnection con = new SqlConnection(@"server = " + serverName + "; Initial Catalog = " + databaseName + "; Integrated Security = SSPI; CONNECT TIMEOUT=30");
                        SqlCommand cmd = new SqlCommand(@"
insert into tblBackupInfoNews values( 	
      @SQLInstanceName,
      @ServerIP,
      @SQLInstancePort,
      @DatabaseName,
      @LastFullBackup,
      @LastDiffBackup,
      @LastBackup_Hrs,
      @dbstatus,
      @dbsize ,
      @dbbackupsize ,
      @fullbackuptime,
      @diffbackuptime,
      @logbackuptime,
      @dbcompretionRate ,
      @dbInPerSec ,
      @dbOutPerSec,
      @BackupStatus,
      @lastTlogBackup,
      @RecoveryModel,
      @NoTLogSince,
      @TlogBkpStatus,
      GETDATE() )", con);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@SQLInstanceName", dataGridView1.Rows[i].Cells[0].Value);
                        cmd.Parameters.AddWithValue("@ServerIP", dataGridView1.Rows[i].Cells[1].Value);
                        cmd.Parameters.AddWithValue("@SQLInstancePort", dataGridView1.Rows[i].Cells[2].Value);
                        cmd.Parameters.AddWithValue("@DatabaseName", dataGridView1.Rows[i].Cells[3].Value);
                        cmd.Parameters.AddWithValue("@LastFullBackup", sqlFormattedDate1);
                        cmd.Parameters.AddWithValue("@LastDiffBackup", sqlFormattedDate2);
                        cmd.Parameters.AddWithValue("@LastBackup_Hrs", Convert.ToInt32(dataGridView1.Rows[i].Cells[6].Value));
                        cmd.Parameters.AddWithValue("@dbstatus", dataGridView1.Rows[i].Cells[7].Value);
                        cmd.Parameters.AddWithValue("@dbsize", dataGridView1.Rows[i].Cells[8].Value);
                        cmd.Parameters.AddWithValue("@dbbackupsize", dataGridView1.Rows[i].Cells[9].Value);
                        cmd.Parameters.AddWithValue("@fullbackuptime", dataGridView1.Rows[i].Cells[10].Value);
                        cmd.Parameters.AddWithValue("@diffbackuptime", dataGridView1.Rows[i].Cells[11].Value);
                        cmd.Parameters.AddWithValue("@logbackuptime", dataGridView1.Rows[i].Cells[12].Value);
                        cmd.Parameters.AddWithValue("@dbcompretionRate", dataGridView1.Rows[i].Cells[13].Value);
                        cmd.Parameters.AddWithValue("@dbInPerSec", dataGridView1.Rows[i].Cells[14].Value);
                        cmd.Parameters.AddWithValue("@dbOutPerSec", dataGridView1.Rows[i].Cells[15].Value);
                        cmd.Parameters.AddWithValue("@BackupStatus", dataGridView1.Rows[i].Cells[16].Value);
                        cmd.Parameters.AddWithValue("@lastTlogBackup", sqlFormattedDate3);
                        cmd.Parameters.AddWithValue("@RecoveryModel", dataGridView1.Rows[i].Cells[18].Value);
                        cmd.Parameters.AddWithValue("@NoTLogSince", Convert.ToInt32(dataGridView1.Rows[i].Cells[19].Value));
                        cmd.Parameters.AddWithValue("@TlogBkpStatus", dataGridView1.Rows[i].Cells[20].Value);


                        /*
                        MessageBox.Show(""+cmd.CommandText+"        ---    "  +
                            " \n SQLInstanceName : " + dataGridView1.Rows[i].Cells[0].Value +
                            " \n DatabaseName : " + dataGridView1.Rows[i].Cells[1].Value +
                            " \n LastFullBackup : " + sqlFormattedDate1 +
                            " \n LastDiffBackup : " + sqlFormattedDate2 +
                            " \n LastBackup_Hrs : " + Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value) +
                            " \n dbsize : " + dataGridView1.Rows[i].Cells[6].Value +
                            " \n lastTlogBackup : " + sqlFormattedDate3
                            );
                        */

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    catch (Exception errorText)
                    {
                        // MessageBox.Show(""+errorText);
                        SqlConnection con = new SqlConnection(@"server=" + serverName + "; Initial Catalog=" + databaseName + ";Integrated Security=SSPI");
                        SqlCommand cmd = new SqlCommand("insert into tblBackupInfoError values (@name,@error,getdate())", con);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@name", serverName2);
                        cmd.Parameters.AddWithValue("@error", errorText.ToString());
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            else if (Convert.ToInt32(selectedIndex) == 3)
            {


                for (int i = 0; i <= dataGridView1.Rows.Count; i++)
                {
                    try
                    {

                        DateTime date1 = Convert.ToDateTime(dataGridView1.Rows[i].Cells[15].Value.ToString());



                        string sqlFormattedDate1 = date1.ToString("yyyy-MM-dd HH:mm:ss.fff");




                        SqlConnection con = new SqlConnection(@"server = " + serverName + "; Initial Catalog = " + databaseName + "; Integrated Security = SSPI");
                        SqlCommand cmd = new SqlCommand(@"Insert into tblServerInformation values(
                                                                                              @instance,
                                                                                              @ServerIP,
                                                                                              @ServerPort,
                                                                                              @sql_version,
                                                                                              @sql_edition,
                                                                                              @service_pack_level,
                                                                                              @build_number,
                                                                                              @port_number,
                                                                                              @min_server_memory,
                                                                                              @max_server_memory ,
                                                                                              @server_memory ,
                                                                                              @server_core,
                                                                                              @sql_core ,
                                                                                              @max_dop ,
                                                                                              @cost_threshould_for_paralelism,
                                                                                              @logtime)", con);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@instance", dataGridView1.Rows[i].Cells[0].Value);
                        cmd.Parameters.AddWithValue("@ServerIP", dataGridView1.Rows[i].Cells[1].Value);
                        cmd.Parameters.AddWithValue("@ServerPort", dataGridView1.Rows[i].Cells[2].Value);
                        cmd.Parameters.AddWithValue("@sql_version", dataGridView1.Rows[i].Cells[3].Value);
                        cmd.Parameters.AddWithValue("@sql_edition", dataGridView1.Rows[i].Cells[4].Value);
                        cmd.Parameters.AddWithValue("@service_pack_level", dataGridView1.Rows[i].Cells[5].Value);
                        cmd.Parameters.AddWithValue("@build_number", dataGridView1.Rows[i].Cells[6].Value);
                        cmd.Parameters.AddWithValue("@port_number", dataGridView1.Rows[i].Cells[7].Value);
                        cmd.Parameters.AddWithValue("@min_server_memory", dataGridView1.Rows[i].Cells[8].Value);
                        cmd.Parameters.AddWithValue("@max_server_memory", dataGridView1.Rows[i].Cells[9].Value);
                        cmd.Parameters.AddWithValue("@server_memory", dataGridView1.Rows[i].Cells[10].Value);
                        cmd.Parameters.AddWithValue("@server_core", dataGridView1.Rows[i].Cells[11].Value);

                        cmd.Parameters.AddWithValue("@sql_core", dataGridView1.Rows[i].Cells[12].Value);
                        cmd.Parameters.AddWithValue("@max_dop", dataGridView1.Rows[i].Cells[13].Value);

                        cmd.Parameters.AddWithValue("@cost_threshould_for_paralelism", dataGridView1.Rows[i].Cells[14].Value);
                        cmd.Parameters.AddWithValue("@logtime", sqlFormattedDate1);




                       // MessageBox.Show("" + cmd.CommandText + "        ---    ");

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    catch (Exception errorText)
                    {
                        // MessageBox.Show(""+errorText);
                        SqlConnection con = new SqlConnection(@"server=" + serverName + "; Initial Catalog=" + databaseName + ";Integrated Security=SSPI");
                        SqlCommand cmd = new SqlCommand("insert into tblServerInformationError values (@name,@error,getdate())", con);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@name", serverName2);
                        cmd.Parameters.AddWithValue("@error", errorText.ToString());
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }

            }
        }

        private void gösterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Maximized;
            this.ShowInTaskbar = true;
        }

        private void gizleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            string saat = DateTime.Now.ToString("HH:mm:ss");
            //label1.Text = saat;

         if (saat == "06:00:00")
            {
                selectedIndex = "2";
                query = @"



SET NOCOUNT ON
declare @check int
set @check = 24-- In hours, based on backup status will be updated, if no full or


declare @CheckTlog int
set @CheckTlog = 4-- In hours, based on tlog backup, status will be updated. 

declare @LastFullBackup datetime
declare @LastDiffBackup datetime
declare @lastTlogBackup datetime
declare @NotBackedupSinceHrs int

declare @NoTLogSince int
declare @status nvarchar(40)
declare @Recovery nvarchar(20)
declare @TlogBkpStatus nvarchar(40)
declare @dbstatus nvarchar(30)
declare @dbsize nvarchar(300)
declare @dbbackupsize nvarchar(300)
declare @dbbackuptimef nvarchar(500)
declare @dbbackuptimed nvarchar(500)
declare @dbbackuptimel nvarchar(500)

declare @dbcompretionRate nvarchar(50)
declare @dbOutPerSec nvarchar(50)
declare @dbInPerSec nvarchar(50)

declare @FinalAge int
declare @hf int
declare @hd int
declare @hl int
declare @ServerName nvarchar(60)
declare @ServerIP nvarchar(60)
declare @ServerPort nvarchar(60)
declare @dbname nvarchar(60)


DECLARE @table1 table(Servername nvarchar(60),ServerIP nvarchar(60),ServerPort nvarchar(60), DBName nvarchar(60), LastFullBackup datetime,
LastDiffBackup datetime, NotBackedUpSince int, dbstatus nvarchar(30), dbsize nvarchar(300), dbbackupsize nvarchar(300),
dbbackuptimef nvarchar(500),dbbackuptimed nvarchar(500),dbbackuptimel nvarchar(500),dbcompretionRate nvarchar(50),dbInPerSec nvarchar(50),dbOutPerSec nvarchar(50), [Status] nvarchar(40), lastTlogBackup datetime,
[Recovery] varchar(20), NoTLogSince int, TlogBkpStatus nvarchar(40))

declare c1 cursor for Select Distinct convert(varchar(60),@@SERVERNAME) as Servername,
convert(varchar(60),CONNECTIONPROPERTY('local_net_address')) ServerIP,
convert(varchar(60),CONNECTIONPROPERTY('local_tcp_port')) ServerPort,
   convert(varchar(60), e.database_name) as DBname,
   (Select  convert(varchar(25), Max(backup_finish_date), 100)
   FROM msdb..backupset a Where a.database_name = e.database_name

and  type = 'D'

Group by a.database_name) Last_FullBackup,
(Select convert(varchar(25), Max(backup_finish_date), 100) From msdb..backupset c
Where c.database_name = e.database_name 
and type = 'I' Group by c.database_name) Last_Diff_Backup, NULL as NotBackedUpSinceHrs, NULL as [DBStatus],
NULL as [Status], (Select convert(varchar(25), Max(backup_finish_date), 100)
From msdb..backupset c Where c.database_name = e.database_name


and type = 'L' Group by c.database_name) Last_Diff_Backup,
convert(varchar(20), convert(sysname, DatabasePropertyEx(e.database_name, 'Recovery'))) as Recovery,
NULL, NULL as TlogBkpStatus From msdb..backupset e WHERE e.database_name not in ('tempdb')
and e.database_name in (Select Distinct name from master..sysdatabases where dbid <> 2)

UNION ALL

SELECT DISTINCT convert(varchar(60),@@SERVERNAME) as Servername,
convert(varchar(60),CONNECTIONPROPERTY('local_net_address')) ServerIP,
convert(varchar(60),CONNECTIONPROPERTY('local_tcp_port')) ServerPort,
convert(varchar(60), name) as DBname,NULL, NULL,NULL as NotBackedUpSinceHrs,NULL AS[DBStatus],
NULL as [Status],NULL,convert(varchar(20), convert(sysname, DatabasePropertyEx(name, 'Recovery'))),
NULL,NULL from master..sysdatabases as record

WHERE name NOT IN(SELECT DISTINCT database_name FROM msdb..backupset) 
and dbid<>2 ORDER BY 1,2
OPEN c1

FETCH NEXT FROM c1 INTO @ServerName,@ServerIP,@ServerPort,@dbname, @LastFullBackup, @LastDiffBackup,
@NotBackedupSinceHrs, @dbstatus,@status, @lastTlogBackup, @Recovery, @NoTLogSince, @TlogBkpStatus


WHILE @@FETCH_STATUS = 0

BEGIN

IF(@LastFullBackup IS NULL)

BEGIN

set @LastFullBackup = '1900-01-01 00:00:00.000'

END

IF(@LastDiffBackup IS NULL)

BEGIN

set @LastDiffBackup = '1900-01-01 00:00:00.000'

END

IF(@lastTlogBackup IS NULL)

BEGIN

set @lastTlogBackup = '1900-01-01 00:00:00.000'

END

select @hf = datediff(hh, @LastFullBackup, GETDATE())

select @hd = datediff(hh, @LastDiffBackup, GETDATE())

select @NoTLogSince = datediff(hh, @lastTlogBackup, GETDATE())

IF(@hf < @hd)

SET @FinalAge = @hf

ELSE

SET @FinalAge = @hd
SET @NotBackedupSinceHrs = @FinalAge

--set @dbstatus = null

set @dbstatus = (select ISNULL(convert(varchar(20), DATABASEPROPERTYEX(@dbname, 'status')),''))
set @dbsize = (select ISNULL(cast(CONVERT( DECIMAL(10,2),SUM(cast(size *8.0/1024   as bigint)))as nvarchar(300))+ ' MB','')  from sys.master_files where db_name(database_id)=@dbname)
set @dbbackupsize = (select ISNULL(cast(CONVERT(DECIMAL(10,2),MAX(compressed_backup_size /1024.0/1024.0))as nvarchar(300))+' MB','') from msdb.dbo.backupset WHERE database_name=@dbname)
set @dbbackuptimef = (SELECT  ISNULL(right ('0'+CONVERT(varchar(6), DATEDIFF(second, MAX(backup_start_date), MAX(backup_finish_date))/3600),2)+ ':'+ RIGHT('0' + CONVERT(varchar(2), (DATEDIFF(second,MAX(backup_start_date), MAX(backup_finish_date)) % 3600) / 60), 2)+ ':'+ RIGHT('0' + CONVERT(varchar(2), DATEDIFF(second, MAX(backup_start_date), MAX(backup_finish_date)) % 60), 2),'') from msdb..backupset WHERE database_name=@dbname and type='D')
set @dbbackuptimed = (SELECT  ISNULL(right ('0'+CONVERT(varchar(6), DATEDIFF(second, MAX(backup_start_date), MAX(backup_finish_date))/3600),2)+ ':'+ RIGHT('0' + CONVERT(varchar(2), (DATEDIFF(second,MAX(backup_start_date), MAX(backup_finish_date)) % 3600) / 60), 2)+ ':'+ RIGHT('0' + CONVERT(varchar(2), DATEDIFF(second, MAX(backup_start_date), MAX(backup_finish_date)) % 60), 2),'') from msdb..backupset WHERE database_name=@dbname and type='I')
set @dbbackuptimel = (SELECT  ISNULL(right ('0'+CONVERT(varchar(6), DATEDIFF(second, MAX(backup_start_date), MAX(backup_finish_date))/3600),2)+ ':'+ RIGHT('0' + CONVERT(varchar(2), (DATEDIFF(second,MAX(backup_start_date), MAX(backup_finish_date)) % 3600) / 60), 2)+ ':'+ RIGHT('0' + CONVERT(varchar(2), DATEDIFF(second, MAX(backup_start_date), MAX(backup_finish_date)) % 60), 2),'') from msdb..backupset WHERE database_name=@dbname and type='L')

set @dbcompretionRate = (select ISNULL(CAST(CAST(CONVERT(DECIMAL(10,2),MAX(backup_size)/1024.0/1024.0)/CONVERT(DECIMAL(10,2),MAX(compressed_backup_size)/1024.0/1024.0) AS decimal(10,2))AS nvarchar(30)),'') from msdb.dbo.backupset WHERE database_name=@dbname)

set @dbInPerSec  =(SELECT   ISNULL(cast(SUM(cast(num_of_bytes_written/1024/1024 as bigint)) as nvarchar(50))+ ' MB','') AS DISK_num_of_bytes_written FROM sys.dm_io_virtual_file_stats(NULL, NULL) AS vfs INNER JOIN sys.master_files AS mf WITH (NOLOCK) ON vfs.database_id = mf.database_id AND vfs.file_id = mf.file_id and db_name(mf.database_id)=@dbname)
set @dbOutPerSec=(select ISNULL(CAST(CAST(CONVERT(DECIMAL(10,2),MAX(backup_size)/1024.0/1024.0)/(case when CONVERT(DECIMAL(10,2),DATEDIFF(second, MAX(backup_start_date), MAX(backup_finish_date)))=0 then 1 else CONVERT(DECIMAL(10,2),DATEDIFF(second, MAX(backup_start_date), MAX(backup_finish_date))) end) AS decimal(10,2))AS nvarchar(30))+' MB','') from msdb.dbo.backupset WHERE database_name=@dbname)


--UPDATE @table1 SET[Status] = 'DB in ' + @dbstatus + ' state' where dbStatus<>'ONLINE'

  --print @dbstatus


--print @dbname


INSERT INTO @table1 values(@ServerName,@ServerIP,@ServerPort, @dbname, @LastFullBackup, @LastDiffBackup,
@NotBackedupSinceHrs, @dbstatus,@dbsize,@dbbackupsize,@dbbackuptimef,@dbbackuptimed,@dbbackuptimel,@dbcompretionRate,@dbInPerSec, @dbOutPerSec ,@status, @lastTlogBackup, @Recovery, @NoTLogSince, @TlogBkpStatus)

--set @dbstatus = null

UPDATE @table1 SET[Status] = CASE
WHEN NotBackedUpSince <= @check   THEN 'Success'
WHEN NotBackedUpSince > = @check THEN '!!! Failed, Action required !!!!'

END
--Print @dbstatus

UPDATE @table1 SET Status = @dbstatus where dbstatus<>'ONLINE'


UPDATE @table1 SET Status = 'Success'where DBName = 'master' and NotBackedUpSince< = @check + 144
UPDATE @table1 SET TlogBkpStatus = CASE
WHEN NoTLogSince<= @CheckTlog THEN 'Success'
WHEN NoTLogSince>= @CheckTlog THEN '!!! Failed, Action required !!!!'
     END


UPDATE @table1 SET TlogBkpStatus = 'NA' where[Recovery] = 'SIMPLE' OR DBName = 'model'

  --print @dbstatus


FETCH NEXT FROM c1 INTO @ServerName,@ServerIP,@ServerPort,@dbname, @LastFullBackup, @LastDiffBackup,
@NotBackedupSinceHrs, @dbstatus,@status,
@lastTlogBackup, @Recovery, @NoTLogSince, @TlogBkpStatus
END
UPDATE @table1 SET Status = 'Not in Online',TlogBkpStatus = 'Not in Online' where dbstatus<>'ONLINE'

SELECT Servername as 'SQLInstanceName',ServerIP,ServerPort,DBName as 'DatabaseName',LastFullBackup,
LastDiffBackup,NotBackedUpSince as 'LastBackup_Hrs',dbstatus,dbsize,dbbackupsize,dbbackuptimef as fullBackupTime,dbbackuptimed as diffBackupTime,dbbackuptimel as logBackupTime,dbcompretionRate,dbInPerSec,dbOutPerSec  ,[Status] as 'Backup Status',lastTlogBackup , [Recovery] ,
NoTLogSince,TlogBkpStatus FROM @table1 order by DBName

CLOSE c1
DEALLOCATE c1



";

            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Maximized;
            this.ShowInTaskbar = true;
        }
    }
}

   

