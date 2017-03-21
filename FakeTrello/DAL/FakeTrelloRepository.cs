using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FakeTrello.Models;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Diagnostics;

namespace FakeTrello.DAL
{
    public class FakeTrelloRepository : IRepository
    {

        //public FakeTrelloContext Context { get; set; }
        SqlConnection _trelloConnection;

        public FakeTrelloRepository()
        {
            _trelloConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }

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
            catch(SqlException ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                _trelloConnection.Close();
            }      
        }

        public void AddCard(string name, int listId, string ownerId)
        {
            throw new NotImplementedException();
        }

        public void AddCard(string name, List list, ApplicationUser owner)
        {
            throw new NotImplementedException();
        }

        public void AddList(string name, int boardId)
        {
            throw new NotImplementedException();
        }

        public void AddList(string name, Board board)
        {
            throw new NotImplementedException();
        }

        public bool AttachUser(string userId, int cardId)
        {
            throw new NotImplementedException();
        }

        public bool CopyCard(int cardId, int newListId, string newOwnerId)
        {
            throw new NotImplementedException();
        }

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
            //return Context.Boards.Where(b => b.Owner.Id == userId).ToList();
            _trelloConnection.Open();

            try
            {
                var getBoardCommand = _trelloConnection.CreateCommand();
                getBoardCommand.CommandText = @"
                    SELECT boardId, Name, Url, Owner_Id 
                    FROM Boards 
                    WHERE Owner_Id = @userId
                    ";
                var boardIdParameter = new SqlParameter("userId", SqlDbType.Int);
                boardIdParameter.Value = userId;
                getBoardCommand.Parameters.Add(boardIdParameter);

                var reader = getBoardCommand.ExecuteReader();
                reader.Read();

                var boards = new List<Board>();
                while (reader.Read())
                {
                    var board = new Board
                    {

                        BoardId = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        URL = reader.GetString(2),
                        Owner = new ApplicationUser { Id = reader.GetString(3) }
                    };

                    boards.Add(board);
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

        public Card GetCard(int cardId)
        {
            throw new NotImplementedException();
        }

        public List<ApplicationUser> GetCardAttendees(int cardId)
        {
            throw new NotImplementedException();
        }

        public List<Card> GetCardsFromBoard(int boardId)
        {
            throw new NotImplementedException();
        }

        public List<Card> GetCardsFromList(int listId)
        {
            throw new NotImplementedException();
        }

        public List GetList(int listId)
        {
            throw new NotImplementedException();
        }

        public List<List> GetListsFromBoard(int boardId)
        {
            throw new NotImplementedException();
        }

        public bool MoveCard(int cardId, int oldListId, int newListId)
        {
            throw new NotImplementedException();
        }

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

        public bool RemoveCard(int cardId)
        {
            throw new NotImplementedException();
        }

        public bool RemoveList(int listId)
        {
            throw new NotImplementedException();
        }

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
    }
}