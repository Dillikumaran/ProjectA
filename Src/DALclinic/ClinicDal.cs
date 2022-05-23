﻿using System;
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
        public int Insertpat(Patient pat)
        {
            using (SqlConnection con = new SqlConnection(Connect))
            {
                using (SqlCommand Cmd = new SqlCommand("Inspatient", con))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    Cmd.Parameters.AddWithValue("@firstname", pat.FirstName);
                    Cmd.Parameters.AddWithValue("@lastname", pat.LastName);
                    Cmd.Parameters.AddWithValue("@sex", pat.Sex);
                    Cmd.Parameters.AddWithValue("@age", pat.Age);
                    Cmd.Parameters.AddWithValue("@dateofbirth", pat.DateOfBirth);
                    Cmd.CommandType = CommandType.StoredProcedure;
                    int result = Cmd.ExecuteNonQuery();
                    con.Close();
                    return result;
                }
            }
        }
        public int Insertappoint(Appointments appoint)
        {
            using (SqlConnection con = new SqlConnection(Connect))
            {
                using (SqlCommand Cmd = new SqlCommand("Insappointment", con))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    Cmd.Parameters.AddWithValue("@patientid", appoint.PatientId);
                    Cmd.Parameters.AddWithValue("@visitdate", appoint.VisitDate);
                    Cmd.Parameters.AddWithValue("@appointmenttime", appoint.AppointmentTime);
                    Cmd.Parameters.AddWithValue("@specialization", appoint.Specialization);
                    Cmd.Parameters.AddWithValue("@doctor", appoint.Doctor);
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
        public List<Doctor> Showdoctor()
        {
            List<Doctor> listappoint = new List<Doctor>();
            using (SqlConnection con = new SqlConnection(Connect))
            {
                using (SqlCommand Cmd = new SqlCommand("showdoc", con))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    Cmd.CommandType = CommandType.StoredProcedure;
                    IDataReader reader = Cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        listappoint.Add(new Doctor()
                        {
                            VisitFrom = reader["visitfrom"].ToString(),
                            VisitTo = reader["visitto"].ToString(),
                            FirstName = reader["DoctorName"].ToString(),
                            Specialization = reader["Specialization"].ToString(),
                        }); 
                    }

                }
            }
            return listappoint;
        }
        public int Updateappointment(Appointments appoint)
        {
            using (SqlConnection con = new SqlConnection(Connect))
            {
                using (SqlCommand Cmd = new SqlCommand("updatespec", con))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    Cmd.Parameters.AddWithValue("@specialization",appoint.Specialization);
                    Cmd.Parameters.AddWithValue("@doctorname",appoint.Doctor);
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
                using (SqlCommand Cmd = new SqlCommand("Cancelappointment ", con))
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
        public List<Appointments> GetappointmentByID(Appointments capp)
        {
            List<Appointments> listappoint = new List<Appointments>();
            using (SqlConnection con = new SqlConnection(Connect))
            {
                using (SqlCommand cmd = new SqlCommand("getbypatientid", con))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    cmd.Parameters.Add("@patientid", SqlDbType.Int);
                    cmd.Parameters["@patientid"].Value = capp.PatientId;
                    cmd.Parameters.Add("@visitdate", SqlDbType.VarChar);
                    cmd.Parameters["@visitdate"].Value = capp.VisitDate;
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
        public string CheckUse(UserLogin use)
        {
            SqlConnection con = new SqlConnection(Connect);
            SqlCommand cmd = new SqlCommand("Validate", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@username", use.UserName);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
                return dr[0].ToString();
            con.Close();
            return null;
        }
        //public List<Doctor> Showtime(int id)
        //{
        //    List<Doctor> listtime = new List<Doctor>();
        //    using (SqlConnection con = new SqlConnection(Connect))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("avail", con))
        //        {
        //            if (con.State == ConnectionState.Closed)
        //                con.Open();
        //            cmd.Parameters.Add("@docid", SqlDbType.Int);
        //            cmd.Parameters["@docid"].Value = id;
        //            IDataReader reader = cmd.ExecuteReader();
        //            while (reader.Read())
        //            {
        //                listtime.Add(new Doctor()
        //                {
        //                    VisitFrom = reader["VisitFrom"].ToString(),
        //                    VisitTo = reader["VisitTo"].ToString(),
        //                });
        //            }
        //            con.Close();
        //        }
        //    }
        //    return listtime;
        //}
    }
}
