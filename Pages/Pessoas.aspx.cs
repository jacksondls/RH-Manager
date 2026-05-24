using System;
using System.Configuration;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Web.UI.WebControls;

namespace RHManager
{
    public partial class Pessoas : System.Web.UI.Page
    {
        string strConexao = ConfigurationManager.ConnectionStrings["OracleDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarPessoas();
                CarregarCargos();
            }
        }

        private void CarregarPessoas()
        {
            using (OracleConnection con = new OracleConnection(strConexao))
            {
                string query = @"SELECT p.pessoa_id, p.pessoa_nome, p.cargo_id, c.cargo_nome
                                 FROM pessoa p
                                 LEFT JOIN cargo c ON p.cargo_id = c.cargo_id";

                OracleDataAdapter da = new OracleDataAdapter(query, con);

                using (DataTable dt = new DataTable())
                {
                    da.Fill(dt);
                    GridViewPessoa.DataSource = dt;
                    GridViewPessoa.DataBind();
                }
            }
        }

        private void CarregarCargos()
        {
            using (OracleConnection con = new OracleConnection(strConexao))
            {
                string query = "SELECT cargo_id, cargo_nome FROM cargo";
                OracleDataAdapter da = new OracleDataAdapter(query, con);

                using (DataTable dt = new DataTable())
                {
                    da.Fill(dt);
                    DDLcargo.DataSource = dt;
                    DDLcargo.DataTextField = "cargo_nome";
                    DDLcargo.DataValueField = "cargo_id";
                    DDLcargo.DataBind();
                }
            }
        }

        protected void BtnIncluir_Click(object sender, EventArgs e)
        {
            try
            {
                using (OracleConnection con = new OracleConnection(strConexao))
                {
                    con.Open();

                    int novoId;
                    using (OracleCommand cmdId = new OracleCommand("SELECT seq_pessoa_id.NEXTVAL FROM dual", con))
                    {
                        novoId = Convert.ToInt32(cmdId.ExecuteScalar());
                    }

                    string query = "INSERT INTO pessoa (pessoa_id, pessoa_nome, cargo_id) VALUES (:id, :nome, :cargo_id)";
                    using (OracleCommand cmd = new OracleCommand(query, con))
                    {
                        cmd.Parameters.Add(":id", OracleDbType.Int32).Value = novoId;
                        cmd.Parameters.Add(":nome", OracleDbType.Varchar2).Value = TxtNome.Text.Trim();
                        cmd.Parameters.Add(":cargo_id", OracleDbType.Int32).Value = Convert.ToInt32(DDLcargo.SelectedValue);
                        cmd.ExecuteNonQuery();
                    }
                }

                TxtNome.Text = "";
                DDLcargo.SelectedIndex = 0;
                CarregarPessoas();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void GridViewPessoa_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewPessoa.EditIndex = e.NewEditIndex;
            CarregarPessoas();

            DropDownList ddlCargoEdit = (DropDownList)GridViewPessoa.Rows[e.NewEditIndex].FindControl("DDLcargoEdit");
            if (ddlCargoEdit != null)
            {
                using (OracleConnection con = new OracleConnection(strConexao))
                {
                    string query = "SELECT cargo_id, cargo_nome FROM cargo";
                    OracleDataAdapter da = new OracleDataAdapter(query, con);

                    using (DataTable dt = new DataTable())
                    {
                        da.Fill(dt);
                        ddlCargoEdit.DataSource = dt;
                        ddlCargoEdit.DataTextField = "cargo_nome";
                        ddlCargoEdit.DataValueField = "cargo_id";
                        ddlCargoEdit.DataBind();
                    }
                }

                int cargoIdAtual = Convert.ToInt32(GridViewPessoa.DataKeys[e.NewEditIndex]["cargo_id"]);
                ddlCargoEdit.SelectedValue = cargoIdAtual.ToString();
            }
        }

        protected void GridViewPessoa_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewPessoa.EditIndex = -1;
            CarregarPessoas();
        }

        protected void GridViewPessoa_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int pessoa_id = Convert.ToInt32(GridViewPessoa.DataKeys[e.RowIndex].Value);

                TextBox txtNomeEdit = (TextBox)GridViewPessoa.Rows[e.RowIndex].FindControl("txtNomeEdit");
                string nome = txtNomeEdit.Text;

                DropDownList ddlCargoEdit = (DropDownList)GridViewPessoa.Rows[e.RowIndex].FindControl("DDLcargoEdit");
                int cargoId = Convert.ToInt32(ddlCargoEdit.SelectedValue);

                using (OracleConnection con = new OracleConnection(strConexao))
                {
                    con.Open();
                    string query = "UPDATE pessoa SET pessoa_nome = :nome, cargo_id = :cargo_id WHERE pessoa_id = :pessoa_id";
                    using (OracleCommand cmd = new OracleCommand(query, con))
                    {
                        cmd.Parameters.Add(":nome", OracleDbType.Varchar2).Value = nome;
                        cmd.Parameters.Add(":cargo_id", OracleDbType.Int32).Value = cargoId;
                        cmd.Parameters.Add(":pessoa_id", OracleDbType.Int32).Value = pessoa_id;
                        cmd.ExecuteNonQuery();
                    }
                }

                GridViewPessoa.EditIndex = -1;
                CarregarPessoas();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void GridViewPessoa_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int pessoa_id = Convert.ToInt32(GridViewPessoa.DataKeys[e.RowIndex].Value);

                using (OracleConnection con = new OracleConnection(strConexao))
                {
                    con.Open();
                    string query = "DELETE FROM pessoa WHERE pessoa_id = :pessoa_id";
                    using (OracleCommand cmd = new OracleCommand(query, con))
                    {
                        cmd.Parameters.Add(":pessoa_id", OracleDbType.Int32).Value = pessoa_id;
                        cmd.ExecuteNonQuery();
                    }
                }

                CarregarPessoas();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void GridViewPessoa_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewPessoa.PageIndex = e.NewPageIndex;
            CarregarPessoas();
        }
    }
}