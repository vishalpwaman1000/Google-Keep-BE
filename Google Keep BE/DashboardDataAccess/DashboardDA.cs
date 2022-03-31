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

        public async Task<ArchiveNoteResponse> ArchiveNote(ArchiveNoteRequest request)
        {
            ArchiveNoteResponse response = new ArchiveNoteResponse();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {

                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }

                string SqlQuery = @"UPDATE googlekeep.NoteDetail
                                    SET IsArchieve=1
                                    WHERE NoteID=@NoteID";

                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@NoteID", request.NoteID);
                    int Status = await sqlCommand.ExecuteNonQueryAsync();
                    if (Status <= 0)
                    {
                        response.IsSuccess = false;
                        response.Message = "Archive Note Query Not Executed";
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

        public async Task<ChangeNoteColorResponse> ChangeNoteColor(ChangeNoteColorRequest request)
        {

            ChangeNoteColorResponse response = new ChangeNoteColorResponse();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {

                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }

                string SqlQuery = @"UPDATE googlekeep.NoteDetail
                                    SET NoteColor=@NoteColor
                                    WHERE NoteID=@NoteID";

                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@NoteID", request.NoteID);
                    sqlCommand.Parameters.AddWithValue("@NoteColor", request.NoteColor);
                    int Status = await sqlCommand.ExecuteNonQueryAsync();
                    if (Status <= 0)
                    {
                        response.IsSuccess = false;
                        response.Message = "Change Note Color Query Not Executed";
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

                string SqlQuery = @"INSERT INTO googlekeep.NoteDetail(Title, Discription, NoteColor, ReminderTime) 
                                                           VALUES(@Title, @Discription, @NoteColor, @ReminderTime);";
                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@Title", request.Title);
                    sqlCommand.Parameters.AddWithValue("@Discription", request.Discription);
                    sqlCommand.Parameters.AddWithValue("@NoteColor", request.NoteColor);
                    sqlCommand.Parameters.AddWithValue("@ReminderTime", request.ReminderTime);
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

        public async Task<GetArchiveNoteResponse> GetArchiveNote()
        {
            GetArchiveNoteResponse response = new GetArchiveNoteResponse();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {

                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }

                string SqlQuery = @"SELECT * 
                                    FROM googlekeep.NoteDetail 
                                    WHERE IsArchieve=1";
                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    using (DbDataReader dbDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        response.data = new List<GetArchiveNote>();
                        if (dbDataReader.HasRows)
                        {


                            while (await dbDataReader.ReadAsync())
                            {
                                GetArchiveNote data = new GetArchiveNote();
                                data.NoteID = dbDataReader["NoteID"] != DBNull.Value ? Convert.ToInt32(dbDataReader["NoteID"]) : 0;
                                data.Title = dbDataReader["Title"] != DBNull.Value ? dbDataReader["Title"].ToString() : string.Empty;
                                data.Discription = dbDataReader["Discription"] != DBNull.Value ? dbDataReader["Discription"].ToString() : string.Empty;
                                data.NoteColor = dbDataReader["NoteColor"] != DBNull.Value ? dbDataReader["NoteColor"].ToString() : string.Empty;
                                data.ReminderTime = dbDataReader["ReminderTime"] != DBNull.Value ? Convert.ToDateTime(dbDataReader["ReminderTime"]).ToString("dd'-'MM'-'yyyy' 'HH':'mm tt") : "No Reminder Set";
                                data.IsArchieve = dbDataReader["IsArchieve"] != DBNull.Value ? Convert.ToBoolean(dbDataReader["IsArchieve"]) : false;
                                data.IsDelete = dbDataReader["IsDelete"] != DBNull.Value ? Convert.ToBoolean(dbDataReader["IsDelete"]) : false;
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

                string SqlQuery = @"SELECT * 
                                    FROM googlekeep.NoteDetail 
                                    WHERE IsDelete='0' AND IsArchieve='0'";
                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    using(DbDataReader dbDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        response.data = new List<GetNote>();
                        if (dbDataReader.HasRows)
                        {
                            while(await dbDataReader.ReadAsync())
                            {
                                GetNote data = new GetNote();
                                data.NoteID = dbDataReader["NoteID"] != DBNull.Value ? Convert.ToInt32(dbDataReader["NoteID"]) : 0;
                                data.Title = dbDataReader["Title"] != DBNull.Value ? dbDataReader["Title"].ToString() : string.Empty;
                                data.Discription = dbDataReader["Discription"] != DBNull.Value ? dbDataReader["Discription"].ToString() : string.Empty;
                                data.NoteColor = dbDataReader["NoteColor"] != DBNull.Value ? dbDataReader["NoteColor"].ToString() : string.Empty;
                                data.ReminderTime = dbDataReader["ReminderTime"] != DBNull.Value ? Convert.ToDateTime(dbDataReader["ReminderTime"]).ToString("dd'-'MM'-'yyyy' 'HH':'mm tt") : "No Reminder Set";
                                data.IsArchieve = dbDataReader["IsArchieve"] != DBNull.Value ? Convert.ToBoolean(dbDataReader["IsArchieve"]) : false;
                                data.IsDelete = dbDataReader["IsDelete"] != DBNull.Value ? Convert.ToBoolean(dbDataReader["IsDelete"]) : false;
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

        public async Task<GetReminderNoteResponse> GetReminderNote()
        {
            GetReminderNoteResponse response = new GetReminderNoteResponse();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {

                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }

                string SqlQuery = @"SELECT * FROM googlekeep.NoteDetail WHERE ReminderTime < CURRENT_TIME() AND IsDelete=0";
                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    using (DbDataReader dbDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        response.data = new List<GetReminderNote>();
                        if (dbDataReader.HasRows)
                        {
                            while (await dbDataReader.ReadAsync())
                            {
                                GetReminderNote data = new GetReminderNote();
                                data.NoteID = dbDataReader["NoteID"] != DBNull.Value ? Convert.ToInt32(dbDataReader["NoteID"]) : 0;
                                data.Title = dbDataReader["Title"] != DBNull.Value ? dbDataReader["Title"].ToString() : string.Empty;
                                data.Discription = dbDataReader["Discription"] != DBNull.Value ? dbDataReader["Discription"].ToString() : string.Empty;
                                data.NoteColor = dbDataReader["NoteColor"] != DBNull.Value ? dbDataReader["NoteColor"].ToString() : string.Empty;
                                data.ReminderTime = dbDataReader["ReminderTime"] != DBNull.Value ? Convert.ToDateTime(dbDataReader["ReminderTime"]).ToString("dd'-'MM'-'yyyy' 'HH':'mm tt") : "No Reminder Set";
                                data.IsArchieve = dbDataReader["IsArchieve"] != DBNull.Value ? Convert.ToBoolean(dbDataReader["IsArchieve"]) : false;
                                data.IsDelete = dbDataReader["IsDelete"] != DBNull.Value ? Convert.ToBoolean(dbDataReader["IsDelete"]) : false;
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

        public async Task<GetTrashNoteResponse> GetTrashNote()
        {
            GetTrashNoteResponse response = new GetTrashNoteResponse();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {

                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }

                string SqlQuery = @"SELECT * 
                                    FROM googlekeep.NoteDetail 
                                    WHERE IsDelete='1'";
                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    using (DbDataReader dbDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        response.data = new List<GetTrashNote>();
                        if (dbDataReader.HasRows)
                        {


                            while (await dbDataReader.ReadAsync())
                            {
                                GetTrashNote data = new GetTrashNote();
                                data.NoteID = dbDataReader["NoteID"] != DBNull.Value ? Convert.ToInt32(dbDataReader["NoteID"]) : 0;
                                data.Title = dbDataReader["Title"] != DBNull.Value ? dbDataReader["Title"].ToString() : string.Empty;
                                data.Discription = dbDataReader["Discription"] != DBNull.Value ? dbDataReader["Discription"].ToString() : string.Empty;
                                data.NoteColor = dbDataReader["NoteColor"] != DBNull.Value ? dbDataReader["NoteColor"].ToString() : string.Empty;
                                data.ReminderTime = dbDataReader["ReminderTime"] != DBNull.Value ? Convert.ToDateTime(dbDataReader["ReminderTime"]).ToString("dd'-'MM'-'yyyy' 'HH':'mm tt") : "No Reminder Set";
                                data.IsArchieve = dbDataReader["IsArchieve"] != DBNull.Value ? Convert.ToBoolean(dbDataReader["IsArchieve"]) : false;
                                data.IsDelete = dbDataReader["IsDelete"] != DBNull.Value ? Convert.ToBoolean(dbDataReader["IsDelete"]) : false;
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

        public async Task<TrashNoteResponse> TrashNote(TrashNoteRequest request)
        {
            TrashNoteResponse response = new TrashNoteResponse();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {

                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }

                string SqlQuery = @"UPDATE googlekeep.NoteDetail
                                    SET IsDelete=1
                                    WHERE NoteID=@NoteID";

                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@NoteID", request.NoteID);
                    int Status = await sqlCommand.ExecuteNonQueryAsync();
                    if (Status <= 0)
                    {
                        response.IsSuccess = false;
                        response.Message = "Trash Note Query Not Executed";
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
                                    SET Title=@Title, Discription=@Discription, NoteColor=@NoteColor, ReminderTime=@ReminderTime
                                    WHERE NoteID=@NoteID";
                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@NoteID", request.NoteId);
                    sqlCommand.Parameters.AddWithValue("@Title", request.Title);
                    sqlCommand.Parameters.AddWithValue("@Discription", request.Discription);
                    sqlCommand.Parameters.AddWithValue("@NoteColor", request.NoteColor);
                    sqlCommand.Parameters.AddWithValue("@ReminderTime", request.ReminderTime);
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
