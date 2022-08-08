using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TaskTR.Models.Repository
{
    public class TubeRepository : IStore<Tube>
    {
        static string constr = ConfigurationManager.ConnectionStrings["BioLabTaskConnectionString"].ConnectionString.ToString();
        SqlConnection con = new SqlConnection(constr);
      

        public void Add(Tube entity)
        {
            SqlCommand cmd = new SqlCommand("AddTube", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@primaryName", entity.primaryName);
            cmd.Parameters.AddWithValue("@secondaryName", entity.secondaryName);
            cmd.Parameters.AddWithValue("@colorHx", entity.colorHex);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public Tube find(int id )
        {
            DataSet ds = null;
            Tube tubelist = null;
             SqlCommand com = new SqlCommand("getCirtinTube", con );
            com.Parameters.AddWithValue("@Id", id);
            com.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = com;
            ds = new DataSet();
            da.Fill(ds);
            tubelist = new Tube();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Tube tobj = new Tube();
                tobj.Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Id"].ToString());
                tobj.primaryName = ds.Tables[0].Rows[i]["primaryName"].ToString();
                tobj.secondaryName = ds.Tables[0].Rows[i]["secondaryName"].ToString();
                tobj.colorHex = ds.Tables[0].Rows[i]["colorHex"].ToString();

                tubelist=tobj;
            }
            con.Close();
            return tubelist;
        }
        public void Update(Tube tube)
        {

            SqlCommand com = new SqlCommand("updateTube", con);
            com.CommandType = CommandType.StoredProcedure; 
            com.Parameters.AddWithValue("@Id", tube.Id);
            com.Parameters.AddWithValue("@primaryName", tube.primaryName);
            com.Parameters.AddWithValue("@secondaryName", tube.secondaryName);
            com.Parameters.AddWithValue("@colorHex", tube.colorHex);
            con.Open(); 
            com.ExecuteNonQuery();
            con.Close(); 

        }
        public List<Tube> GetList()
        {
            DataSet ds = null;
            List<Tube> tubelist = null;
            SqlCommand cmd = new SqlCommand("getTube", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            ds = new DataSet();
            da.Fill(ds);
            tubelist = new List<Tube>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Tube tobj = new Tube();
                tobj.Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Id"].ToString());
                tobj.primaryName = ds.Tables[0].Rows[i]["primaryName"].ToString();
                tobj.secondaryName = ds.Tables[0].Rows[i]["secondaryName"].ToString();
                tobj.colorHex = ds.Tables[0].Rows[i]["colorHex"].ToString();

                tubelist.Add(tobj);
            }
            return tubelist;
        }
        public bool DuplicationNames(Tube entity)
        {
            DataSet ds = null;
            List<Tube> tubelist = null;
            SqlCommand cmd = new SqlCommand("getTube", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            ds = new DataSet();
            da.Fill(ds);
            tubelist = new List<Tube>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Tube tobj = new Tube();
                tobj.Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Id"].ToString());
                tobj.primaryName = ds.Tables[0].Rows[i]["primaryName"].ToString();
                tobj.secondaryName = ds.Tables[0].Rows[i]["secondaryName"].ToString();
                tobj.colorHex = ds.Tables[0].Rows[i]["colorHex"].ToString();
                if(tobj.primaryName == entity.primaryName || tobj.colorHex ==entity.colorHex)
                {
                    con.Close();
                    return false;
                }
                tubelist.Add(tobj);
            }
            con.Close();
            return true;
        }


    }
}