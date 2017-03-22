using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FakeTrello.Models;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Configuration;

namespace FakeTrello.DAL
{
    public class BoardRepository : IBoardManager, IBoardQuery
    {
        SqlConnection _trelloConnection;

        public BoardRepository()
        {
            _trelloConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }
        #region Add Board Method(s)
        public void AddBoard(string name, ApplicationUser owner)
        {
            _trelloConnection.Open();

            try
            {
                var addBoardCommand = _trelloConnection.CreateCommand();
                addBoardCommand.CommandText = "INSERT INTO Boards(Name, Owner_Id) VALUES(@name, @ownerId)";
                var nameParameter = new SqlParameter("name", SqlDbType.VarChar);
                nameParameter.Value = name;
                addBoardCommand.Parameters.Add(nameParameter);
                var ownerParameter = new SqlParameter("owner", SqlDbType.Int);
                ownerParameter.Value = owner.Id;
                addBoardCommand.Parameters.Add(ownerParameter);

                addBoardCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                _trelloConnection.Close();
            }
        }
        #endregion

        //Update Board Method(s)
        #region Update Board Method(s)
        public void EditBoardName(int boardId, string newname)
        {
            _trelloConnection.Open();

            try
            {
                var editBoardCommand = _trelloConnection.CreateCommand();
                editBoardCommand.CommandText = @"
                    UPDATE Boards
                    SET Name = @NewName
                    WHERE boardId = @boardId
                ";
                var nameParameter = new SqlParameter("NewName", SqlDbType.VarChar);
                nameParameter.Value = newname;
                editBoardCommand.Parameters.Add(nameParameter);
                var boardIdParameter = new SqlParameter("boardId", SqlDbType.Int);
                boardIdParameter.Value = boardId;
                editBoardCommand.Parameters.Add(boardIdParameter);

                editBoardCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                _trelloConnection.Close();
            }
        }
        #endregion

        //Get Board Method(s)
        #region Get Board Methods
        public Board GetBoard(int boardId)
        {
            _trelloConnection.Open();

            try
            {
                var getBoardCommand = _trelloConnection.CreateCommand();
                getBoardCommand.CommandText = @"
                    SELECT boardId, Name, Url, Owner_Id 
                    FROM Boards 
                    WHERE BoardId = @boardId
                    ";
                var boardIdParameter = new SqlParameter("boardId", SqlDbType.Int);
                boardIdParameter.Value = boardId;
                getBoardCommand.Parameters.Add(boardIdParameter);

                var reader = getBoardCommand.ExecuteReader();
                reader.Read();

                if (reader.Read())
                {
                    var board = new Board
                    {

                        BoardId = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        URL = reader.GetString(2),
                        Owner = new ApplicationUser { Id = reader.GetString(3) }
                    };
                    return board;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                _trelloConnection.Close();
            }
            return null;
        }

        public List<Board> GetBoardsFromUser(string userId)
        {
            throw new NotImplementedException();
        }
        #endregion

        //Delete Board Method(s)
        #region Delete Board Method(s)
        public bool RemoveBoard(int boardId)
        {
            _trelloConnection.Open();

            try
            {
                var removeBoardCommand = _trelloConnection.CreateCommand();
                removeBoardCommand.CommandText = @"
                    DELETE
                    FROM Boards
                    WHERE BoardId = @boardId
                ";

                var boardIdParameter = new SqlParameter("boardId", SqlDbType.Int);
                boardIdParameter.Value = boardId;
                removeBoardCommand.Parameters.Add(boardIdParameter);

                removeBoardCommand.ExecuteNonQuery();

                return true;
            }
            catch (SqlException ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                _trelloConnection.Close();
            }

            return false;
        }
        #endregion
    }
}