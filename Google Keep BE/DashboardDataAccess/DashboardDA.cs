using Google_Keep_BE.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Google_Keep_BE.DashboardDataAccess
{
    public class DashboardDA : IDashboardDA
    {
        public readonly IConfiguration _configuration;
        public readonly MySqlConnection _mySqlConnection;
        public DashboardDA(IConfiguration configuration)
        {
            _configuration = configuration;
            _mySqlConnection = new MySqlConnection(_configuration["ConnectionStrings:MySqlConnectionString"]);
        }

        public async Task<CreateNoteResponse> CreateNote(CreateNoteRequest request)
        {
            CreateNoteResponse response = new CreateNoteResponse();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {

                if(_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }

                string SqlQuery = @"INSERT INTO googlekeep.NoteDetail(Title, Discription, NoteColor) 
                                                           VALUES(@Title, @Discription, @NoteColor);";
                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@Title", request.Title);
                    sqlCommand.Parameters.AddWithValue("@Discription", request.Discription);
                    sqlCommand.Parameters.AddWithValue("@NoteColor", request.NoteColor);
                    int Status = await sqlCommand.ExecuteNonQueryAsync();
                    if (Status <= 0)
                    {
                        response.IsSuccess = false;
                        response.Message = "Create Note Query Not Executed";
                        return response;
                    }
                }

            }catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            finally
            {
                await _mySqlConnection.CloseAsync();
                await _mySqlConnection.DisposeAsync();
            }

            return response;
        }

        public async Task<GetNoteResponse> GetNote()
        {
            GetNoteResponse response = new GetNoteResponse();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {

                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }

                string SqlQuery = @"SELECT * FROM googlekeep.NoteDetail";
                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    using(DbDataReader dbDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (dbDataReader.HasRows)
                        {
                            response.data = new List<GetNote>();

                            while(await dbDataReader.ReadAsync())
                            {
                                GetNote data = new GetNote();
                                data.NoteID = dbDataReader["NoteID"] != DBNull.Value ? Convert.ToInt32(dbDataReader["NoteID"]) : 0;
                                data.Title = dbDataReader["Title"] != DBNull.Value ? dbDataReader["Title"].ToString() : string.Empty;
                                data.Discription = dbDataReader["Discription"] != DBNull.Value ? dbDataReader["Discription"].ToString() : string.Empty;
                                data.NoteColor = dbDataReader["NoteColor"] != DBNull.Value ? dbDataReader["NoteColor"].ToString() : string.Empty;
                                response.data.Add(data);
                            }
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            finally
            {
                await _mySqlConnection.CloseAsync();
                await _mySqlConnection.DisposeAsync();
            }

            return response;
        }

        public async Task<UpdateNoteResponse> UpdateNote(UpdateNoteRequest request)
        {
            UpdateNoteResponse response = new UpdateNoteResponse();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {

                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }

                string SqlQuery = @"UPDATE googlekeep.NoteDetail
                                    SET Title=@Title, Discription=@Discription, NoteColor=@NoteColor
                                    WHERE NoteID=@NoteID";
                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@NoteID", request.NoteId);
                    sqlCommand.Parameters.AddWithValue("@Title", request.Title);
                    sqlCommand.Parameters.AddWithValue("@Discription", request.Discription);
                    sqlCommand.Parameters.AddWithValue("@NoteColor", request.NoteColor);
                    int Status = await sqlCommand.ExecuteNonQueryAsync();
                    if (Status <= 0)
                    {
                        response.IsSuccess = false;
                        response.Message = "Update Note Query Not Executed";
                        return response;
                    }
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            finally
            {
                await _mySqlConnection.CloseAsync();
                await _mySqlConnection.DisposeAsync();
            }

            return response;
        }

    }
}
