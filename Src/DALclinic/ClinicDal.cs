using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ProjectA.Models;
using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace ProjectA.DALclinic
{
    public class ClinicDal
    {
        public string Connect = "";

        public ClinicDal()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            Connect = builder.GetSection("ConnectionStrings:Connect").Value;
        }
        public int Insertdoc(Doctor doc)
        {
            using (SqlConnection con = new SqlConnection(Connect))
            {
                using (SqlCommand Cmd = new SqlCommand("Insdoctor", con))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    Cmd.Parameters.AddWithValue("@firstname", doc.FirstName);
                    Cmd.Parameters.AddWithValue("@lastname", doc.LastName);
                    Cmd.Parameters.AddWithValue("@sex", doc.Sex);
                    Cmd.Parameters.AddWithValue("@specialization", doc.Specialization);
                    Cmd.Parameters.AddWithValue("@visitfrom", doc.VisitFrom);
                    Cmd.Parameters.AddWithValue("@to", doc.VisitTo);
                    Cmd.CommandType = CommandType.StoredProcedure;
                    int result = Cmd.ExecuteNonQuery();
                    con.Close();
                    return result;
                }
            }
        }
        public List<Appointments> Showappointments()
        {
            List<Appointments> listCustomer = new List<Appointments>();
            using (SqlConnection con = new SqlConnection(Connect))
            {
                using (SqlCommand cmd = new SqlCommand("Showappointments", con))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        listCustomer.Add(new Appointments()
                        {
                            AppointmentId = int.Parse(reader["AppointmentId"].ToString()),
                            PatientId = int.Parse(reader["PatientId"].ToString()),
                            Specialization = reader["Specialization"].ToString(),
                            Doctor = reader["DoctorName"].ToString(),
                            VisitDate = reader["VisitDate"].ToString(),
                            AppointmentTime = reader["AppointmentTime"].ToString()
                        });
                    }
                    con.Close();
                }
            }
            return listCustomer;
        }
        public int Updateappoint(Appointments appoint)
        {
            using (SqlConnection con = new SqlConnection(Connect))
            {
                using (SqlCommand Cmd = new SqlCommand("Editappoint", con))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    Cmd.Parameters.AddWithValue("@appointmentid", appoint.AppointmentId);
                    Cmd.Parameters.AddWithValue("@patientid", appoint.PatientId);
                    Cmd.Parameters.AddWithValue("@specialization", appoint.Specialization);
                    Cmd.Parameters.AddWithValue("@doctorname", appoint.Doctor);
                    Cmd.Parameters.AddWithValue("@visitdate", appoint.VisitDate);
                    Cmd.Parameters.AddWithValue("@tappointmenttime", appoint.AppointmentTime);
                    Cmd.CommandType = CommandType.StoredProcedure;
                    int result = Cmd.ExecuteNonQuery();
                    con.Close();
                    return result;
                }
            }
        }
        public int Cancelappointment(int id)
        {
            using (SqlConnection con = new SqlConnection(Connect))
            {
                using (SqlCommand Cmd = new SqlCommand("Deletedata ", con))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    Cmd.Parameters.AddWithValue("@appointmentid", id);
                    int result = Cmd.ExecuteNonQuery();
                    con.Close();
                    return result;
                }
            }
        }
        public List<Appointments> GetappointmentByID(int id,string date)
        {
            List<Appointments> listappoint = new List<Appointments>();
            using (SqlConnection con = new SqlConnection(Connect))
            {
                using (SqlCommand cmd = new SqlCommand("getbypatientid", con))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    cmd.Parameters.Add("@patientid", SqlDbType.Int);
                    cmd.Parameters["@patientid"].Value = id;
                    cmd.Parameters.Add("@visitdate", SqlDbType.VarChar);
                    cmd.Parameters["@visitdate"].Value = date;
                    cmd.CommandType = CommandType.StoredProcedure;

                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        listappoint.Add(new Appointments()
                        {
                            AppointmentId = int.Parse(reader["AppointmentId"].ToString()),
                            PatientId = int.Parse(reader["PatientId"].ToString()),
                            Specialization = reader["Specialization"].ToString(),
                            Doctor = reader["DoctorName"].ToString(),
                            VisitDate = reader["VisitDate"].ToString(),
                            AppointmentTime = reader["AppointmentTime"].ToString()
                        }); ;
                    }

                }
            }
            return listappoint;
        }

    }
}
