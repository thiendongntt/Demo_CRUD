using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using DemoCRUD.Models;

namespace DemoCRUD.Models
{
    public class db
    {
        //"Provider = SQLNCLI11;"+
        SqlConnection con = new SqlConnection("Data Source=IVANTIME\\SQLEXPRESS;Initial Catalog=BaiTapAPI;Integrated Security=True");

        public DataSet Empget(Employee emp, out string msg)
        {
            DataSet ds = new DataSet();
            msg = "";
            try
            {
                SqlCommand com = new SqlCommand("sp_Employee", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Sr_no", emp.Sr_no);
                com.Parameters.AddWithValue("@Emp_name", emp.Emp_name);
                com.Parameters.AddWithValue("@City", emp.City);
                com.Parameters.AddWithValue("@State", emp.state);
                com.Parameters.AddWithValue("@Country", emp.Country);
                com.Parameters.AddWithValue("@Department", emp.Department);
                com.Parameters.AddWithValue("@flag", emp.flag);

                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(ds);
                msg = "OK";
                return ds;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return ds;
            }
        }

        public string Empdml(Employee emp, out string msg)
        {
            msg = "";
            try
            {
                SqlCommand com = new SqlCommand("sp_Employee", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Sr_no", emp.Sr_no);
                com.Parameters.AddWithValue("@Emp_name", emp.Emp_name);
                com.Parameters.AddWithValue("@City", emp.City);
                com.Parameters.AddWithValue("@State", emp.state);
                com.Parameters.AddWithValue("@Country", emp.Country);
                com.Parameters.AddWithValue("@Department", emp.Department);
                com.Parameters.AddWithValue("@flag", emp.flag);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
                msg = "OK";
                return msg;
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                msg = ex.Message;
                return msg;
            }
        }

        public void EmpDelete(int id)
        {
            con.Open();
            SqlCommand com = new SqlCommand("sp_Delete", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlParameter paramId = new SqlParameter();
            paramId.ParameterName = "@Sr_no";
            paramId.Value = id;
            com.Parameters.Add(paramId);

            com.ExecuteNonQuery();

        }

    }
} 
