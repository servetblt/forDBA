using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IntegrationService
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try { 
           string  serverName = Properties.Settings.Default["serverName"].ToString();
           string databaseName = Properties.Settings.Default["databaseName"].ToString();
           string tableName = Properties.Settings.Default["tableName"].ToString();
           string columnName = Properties.Settings.Default["columnName"].ToString();
            string selectedIndex = Properties.Settings.Default["selectedIndex"].ToString();


            if ( !columnCombo.Items.Contains(columnName.ToString()))
            {
                databaseCombo.Items.Add(databaseName.ToString());
                tableCombo.Items.Add(tableName.ToString());
                columnCombo.Items.Add(columnName.ToString());
            }

            serverNameText.Text= Properties.Settings.Default["serverName"].ToString();
            databaseCombo.Text = Properties.Settings.Default["databaseName"].ToString();
            tableCombo.Text = Properties.Settings.Default["tableName"].ToString();
            columnCombo.Text = Properties.Settings.Default["columnName"].ToString();
            }
            catch (Exception ex) { }
        }

        private void databaseCombo_Enter(object sender, EventArgs e)
        {
            try { 
            databaseCombo.Items.Clear();
            tableCombo.Items.Clear();

            string serverName = serverNameText.Text.Trim();

            SqlConnection baglanti = new SqlConnection();
            baglanti.ConnectionString = "Data Source="+serverName+";Initial Catalog=master;Integrated Security=SSPI";
            SqlCommand komut = new SqlCommand();
            komut.CommandText = "select name from sys.databases";
            komut.Connection = baglanti;
            komut.CommandType = CommandType.Text;

            SqlDataReader dr;
            baglanti.Open();
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                databaseCombo.Items.Add(dr["name"]);
            }
            baglanti.Close();
            }
            catch (Exception ex) { }
        }

        private void tableCombo_Enter(object sender, EventArgs e)
        {
            try { 
            tableCombo.Items.Clear();

            string serverName = serverNameText.Text.Trim();
            string databaseName = databaseCombo.Text;

            SqlConnection baglanti = new SqlConnection();
            baglanti.ConnectionString = "Data Source=" + serverName + ";Initial Catalog="+databaseName+";Integrated Security=SSPI";
            SqlCommand komut = new SqlCommand();
            komut.CommandText = "select name from sys.tables";
            komut.Connection = baglanti;
            komut.CommandType = CommandType.Text;

            SqlDataReader dr;
            baglanti.Open();
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                tableCombo.Items.Add(dr["name"]);
                //MessageBox.Show(""+ databaseCombo.Items.Add(dr["name"]));
            }
            baglanti.Close();
            }
            catch (Exception ex) { }
        }

        private void columnCombo_Enter(object sender, EventArgs e)
        {
            try { 
            columnCombo.Items.Clear();

            string serverName = serverNameText.Text.Trim();
            string databaseName = databaseCombo.Text;
            string tableName = tableCombo.Text;

            SqlConnection baglanti = new SqlConnection();
            baglanti.ConnectionString = "Data Source=" + serverName + ";Initial Catalog=" + databaseName + ";Integrated Security=SSPI";
            SqlCommand komut = new SqlCommand();
            komut.CommandText = @"select co.name as name from sys.objects ob 
INNER JOIN sys.columns co on ob.object_id = co.object_id
where ob.name = '"+tableName+"'";
            komut.Connection = baglanti;
            komut.CommandType = CommandType.Text;


            SqlDataReader dr;
            baglanti.Open();
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                columnCombo.Items.Add(dr["name"]);
                //MessageBox.Show(""+ databaseCombo.Items.Add(dr["name"]));
            }
            baglanti.Close();
            }
            catch (Exception ex) { }
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            try {
                if (serverNameText.Text == "" || databaseCombo.Text == "" || tableCombo.Text == "" || columnCombo.Text == "")
                {
                    MessageBox.Show("Boşlukları Doldurunuz..");
                }
                else
                {
                    Properties.Settings.Default["serverName"] = serverNameText.Text.Trim();
                    Properties.Settings.Default["databaseName"] = databaseCombo.Text;
                    Properties.Settings.Default["tableName"] = tableCombo.Text;
                    Properties.Settings.Default["columnName"] = columnCombo.Text;

                    Form2 frm = new Form2();
                    this.Hide();
                    frm.Show();
                }
            }
            catch (Exception ex) {}
            }
    }
}
