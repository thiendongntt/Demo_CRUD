﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _HoangDinhThienDong_5951071017_
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-QNLSN9K3\SQLEXPRESS;Initial Catalog=DemoCRUD;Integrated Security=True");
        public Form1()
        {
            InitializeComponent();
            GetStudentRecord();
        }
        private void GetStudentRecord()
        {
          
            SqlCommand cmd = new SqlCommand("SELECT * FROM StudentsTb", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            StudentRecordData.DataSource = dt;
        }
        private bool IsValidData()
        {
            if(txtFatherName.Text == string.Empty || txtName.Text == string.Empty || txtAddress.Text == string.Empty || string.IsNullOrEmpty(txtNumber.Text) || string.IsNullOrEmpty(txtRoll.Text))
            {
                MessageBox.Show("Có chỗ chưa nhập dữ liệu !!!", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }    
            return true;
        }
      
        private void btnThem_Click(object sender, EventArgs e)
        {
            if(IsValidData())
            {
                
                SqlCommand cmd = new SqlCommand("INSERT INTO StudentsTb VALUES" +
                    "(@Name, @FatherName, @RollNumber, @Address, @Mobile)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@FatherName", txtFatherName.Text);
                cmd.Parameters.AddWithValue("@RollNumber", txtRoll.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtNumber.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                GetStudentRecord();
            }
        }
        public int StudenID;
        private void StudentRecordData_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            //txtName.Text = StudentRecordData.SelectedRows[0].Cells[1].Value.ToString();
            //txtFatherName.Text = StudentRecordData.SelectedRows[0].Cells[2].Value.ToString();
            //txtRoll.Text = StudentRecordData.SelectedRows[0].Cells[3].Value.ToString();
            //txtAddress.Text = StudentRecordData.SelectedRows[0].Cells[4].Value.ToString();
            //txtNumber.Text = StudentRecordData.SelectedRows[0].Cells[5].Value.ToString();
            DataGridViewRow row = StudentRecordData.Rows[e.RowIndex];
            StudenID = Convert.ToInt32(row.Cells[0].Value);
            txtName.Text = row.Cells[1].Value.ToString();
            txtFatherName.Text = row.Cells[2].Value.ToString();
            txtRoll.Text = row.Cells[3].Value.ToString();
            txtAddress.Text = row.Cells[4].Value.ToString();
            txtNumber.Text = row.Cells[5].Value.ToString();
        }

        private void btnCapnhat_Click(object sender, EventArgs e)
        {
            if(StudenID > 0)
            {
                SqlCommand cmd = new SqlCommand("UPDATE StudentsTb SET Name = @Name, FatherName = @FatherName, RollNumber = @RollNumber, Address = @Address, Mobile = @Mobile WHERE StudentID = @ID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@FatherName", txtFatherName.Text);
                cmd.Parameters.AddWithValue("@RollNumber", txtRoll.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtNumber.Text);
                cmd.Parameters.AddWithValue("@ID", this.StudenID);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                GetStudentRecord();
                ResetData();
                MessageBox.Show("Thêm thành công!", "Done", MessageBoxButtons.OK);

            } else
            {
                MessageBox.Show("Cập nhật bị lỗi!!!", "Lỗi!",MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if(StudenID > 0)
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM StudentsTb WHERE StudentID = @ID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", this.StudenID);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                GetStudentRecord();
                ResetData();
            }
        }

        private void ResetData()
        {
            
            txtFatherName.Text = "";
            txtName.Text = "";
            txtRoll.Text = "";
            txtAddress.Text = "";
            txtNumber.Text = "";
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult exit = MessageBox.Show("Bạn có muốn chắc chắn thoát", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if(exit == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
