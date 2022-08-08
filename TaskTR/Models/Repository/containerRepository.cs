using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TaskTR.Models.Repository
{
    public class ContainersRepository :IStore<Containers>
    {


        static string constr = ConfigurationManager.ConnectionStrings["BioLabTaskConnectionString"].ConnectionString.ToString();
        SqlConnection con = new SqlConnection(constr);

        public void Add(Containers entity)
        {

            SqlCommand cmd = new SqlCommand("AddContainer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@primaryName", entity.primaryName);
            cmd.Parameters.AddWithValue("@secondaryName", entity.secondaryName);
            cmd.Parameters.AddWithValue("@FK_TubeTypeId", entity.FK_TubeTypeId);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public bool DuplicationNames(Containers entity)
        {

            DataSet ds = null;
            List<Containers> tubelist = null;
            SqlCommand cmd = new SqlCommand("getContainer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            ds = new DataSet();
            da.Fill(ds);
            tubelist = new List<Containers>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Containers tobj = new Containers();
                tobj.Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Id"].ToString());
                tobj.primaryName = ds.Tables[0].Rows[i]["primaryName"].ToString();
                tobj.secondaryName = ds.Tables[0].Rows[i]["secondaryName"].ToString();
                tobj.FK_TubeTypeId = Convert.ToInt32(ds.Tables[0].Rows[i]["FK_TubeTypeId"].ToString());
                if (tobj.primaryName == entity.primaryName )
                {
                    con.Close();
                    return false;
                }
                tubelist.Add(tobj);
            }
            con.Close();
            return true;
        }

        public Containers find(int id)
        {
            DataSet ds = null;
            Containers tubelist = null;
            SqlCommand com = new SqlCommand("getCirtinContainer", con);
            com.Parameters.AddWithValue("@Id", id);
            com.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = com;
            ds = new DataSet();
            da.Fill(ds);
            tubelist = new Containers();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Containers tobj = new Containers();
                tobj.Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Id"].ToString());
                tobj.primaryName = ds.Tables[0].Rows[i]["primaryName"].ToString();
                tobj.secondaryName = ds.Tables[0].Rows[i]["secondaryName"].ToString();
                tobj.FK_TubeTypeId = Convert.ToInt32(ds.Tables[0].Rows[i]["FK_TubeTypeId"].ToString());

                tubelist = tobj;
            }
            con.Close();
            return tubelist;
        }

        public List<Containers> GetList()
        {
            DataSet ds = null;
            List<Containers> tubelist = null;
            List<Tube> data = null; 
            SqlCommand cmd = new SqlCommand("getContainer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            ds = new DataSet();
            da.Fill(ds);
            data = new List<Tube>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Tube tobj = new Tube();
               tobj.Id = Convert.ToInt32(ds.Tables[0].Rows[i]["FK_TubeTypeId"].ToString());
                tobj.primaryName = ds.Tables[0].Rows[i][5].ToString();
                tobj.secondaryName = ds.Tables[0].Rows[i][6].ToString();
                tobj.colorHex = ds.Tables[0].Rows[i][7].ToString();
              
                //tobj.TubeList.primaryName = ds.Tables[0].Rows[i]["primaryName"].ToString();
                data.Add(tobj);
            }
            tubelist = new List<Containers>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Containers tobj = new Containers();
                tobj.Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Id"].ToString());
                tobj.primaryName = ds.Tables[0].Rows[i]["primaryName"].ToString();
                tobj.secondaryName = ds.Tables[0].Rows[i]["secondaryName"].ToString();
                tobj.FK_TubeTypeId = Convert.ToInt32(ds.Tables[0].Rows[i]["FK_TubeTypeId"].ToString());
                var chek = data.Where(x => x.Id == tobj.FK_TubeTypeId).FirstOrDefault();
                tobj.TubeList = chek;
                //tobj.TubeList.primaryName = ds.Tables[0].Rows[i]["primaryName"].ToString();
                tubelist.Add(tobj);
            }
            
            return tubelist;
        }

        public void Update(Containers entity)
        {


            SqlCommand com = new SqlCommand("updateContainer", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Id", entity.Id);
            com.Parameters.AddWithValue("@primaryName", entity.primaryName);
            com.Parameters.AddWithValue("@secondaryName", entity.secondaryName);
            com.Parameters.AddWithValue("@FK_TubeTypeId", entity.FK_TubeTypeId);
            con.Open();
            com.ExecuteNonQuery();
            con.Close();
        }
    }
}